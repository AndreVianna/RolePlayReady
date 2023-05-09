namespace System.Validation.Builder;

public class CollectionValidatorsTests {
    public record TestObject : IValidatable {
        public required ICollection<int> Numbers { get; init; } = Array.Empty<int>();
        public required ICollection<(string Name, int Age)> Names { get; init; } = Array.Empty<(string Name, int Age)>();
        public ICollection<string> Empty { get; } = Array.Empty<string>();

        public ValidationResult ValidateSelf(bool negate = false) {
            var result = ValidationResult.Success();
            result += Numbers.IsRequired()
                .And().IsNotEmpty()
                .And().HasAtLeast(2)
                .And().HasAtMost(4)
                .And().Has(3)
                .And().Contains(5)
                .And().Each(item => item.IsRequired().And().IsGreaterThan(0)).Result;
            result += Names.CheckIfEach(value => value.Name.IsRequired()).Result;
            result += Empty!.IsRequired().And().IsEmpty().Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, int> {
        public TestData() {
            Add(new() { Numbers = new[] { 1, 3, 5 }, Names = new[] { ("Name", 30) } }, 0);
            Add(new() { Numbers = Array.Empty<int>(), Names = new[] { ("Name", 30), default! } }, 5);
            Add(new() { Numbers = new[] { 0, 5, 10, 13, 20 }, Names = new[] { ("Name", 30) } }, 3);
            Add(new() { Numbers = null!, Names = null! }, 2);
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