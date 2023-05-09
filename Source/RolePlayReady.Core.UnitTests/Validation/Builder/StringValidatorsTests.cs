namespace System.Validation.Builder;

public class StringValidatorsTests {
    public record TestObject : IValidatable {
        private readonly IPasswordPolicy _fakePolicy = Substitute.For<IPasswordPolicy>();

        public TestObject() {
            _fakePolicy.TryValidate(Arg.Any<string>(), out Arg.Any<ICollection<ValidationError>>()).Returns(x => {
                var errors = new List<ValidationError>();
                if (x[0] is "Invalid") {
                    errors.Add(new("Some error.", "Password"));
                }

                x[1] = errors;
                return x[0] is not "Invalid";
            });
        }

        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string Empty { get; } = string.Empty;

        public ValidationResult ValidateSelf(bool negate = false) {
            var result = ValidationResult.Success();
            result += Name.IsRequired()
                          .And().IsNotEmptyOrWhiteSpace()
                          .And().LengthIsAtLeast(3)
                          .And().Contains("ext")
                          .And().LengthIsAtMost(10)
                          .And().LengthIs(5)
                          .And().IsIn("Text1", "Text2", "Text3").Result;
            result += Email.IsRequired()
                           .And().IsNotEmpty()
                           .And().IsEmail().Result;
            result += Password.IsOptional()
                           .And().IsNotEmpty()
                           .And().IsPassword(_fakePolicy).Result;
            result += Empty.IsRequired().And().IsEmpty().And().IsEmptyOrWhiteSpace().Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, int> {
        public TestData() {
            Add(new() { Name = "Text1", Email = "some@email.com" }, 0);
            Add(new() { Name = "Text1", Email = "" }, 2);
            Add(new() { Name = "Text1", Email = "NotEmail" }, 1);
            Add(new() { Name = null, Password = "AnyTh1n6!" }, 2);
            Add(new() { Name = "" }, 6);
            Add(new() { Name = "  " }, 6);
            Add(new() { Name = "12" }, 5);
            Add(new() { Name = "12345678901" }, 5);
            Add(new() { Name = "Other", Password = "Invalid" }, 5);
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