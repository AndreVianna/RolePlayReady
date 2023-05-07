namespace System.Validation.Builder;

public class IntegerValidatorsTests {
    public record TestObject : IValidatable {
        public int NonNull { get; init; }
        public int? Nullable { get; init; }
        public int? Required { get; init; }

        public ValidationResult ValidateSelf(bool negate = false) {
            var result = ValidationResult.Success();
            result += NonNull.IsRequired().Result;
            result += Nullable.IsOptional().And().IsGreaterThan(10).And().IsLessThan(20).And().IsEqualTo(15).Result;
            result += Required.IsRequired().And().MinimumIs(10).And().MaximumIs(20).Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, int> {
        public TestData() {
            Add(new() { Nullable = 15, Required = 15, }, 0);
            Add(new() { Nullable = null, Required = null, }, 1);
            Add(new() { Nullable = 5, Required = 5, }, 3);
            Add(new() { Nullable = 25, Required = 25, }, 3);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestObject subject, int errorCount) {
        // Act
        var result = subject.ValidateSelf();

        // Assert
        result.Errors.Should().HaveCount(errorCount);
    }
}