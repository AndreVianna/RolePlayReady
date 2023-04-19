namespace System.Validators;

public class ValidatorFactoryTests {
    [Theory]
    [ClassData(typeof(TestData))]
    public void Create_ForType_ReturnsValidator(Type valueType, string validatorName, Type validatorType, params object?[] args) {
        var validator = ValidatorFactory.For("Attribute").Create(valueType, validatorName, args);

        validator.Should().BeOfType(validatorType);
    }

    [Fact]
    public void Create_ForUnsupportedType_Throws() {
        var action = () => ValidatorFactory.For("Attribute").Create(typeof(Dictionary<int, int>), "Anything", Array.Empty<object?>());

        action.Should().Throw<ArgumentException>();
    }

    public class TestData : TheoryData<Type, string, Type, object?[]> {
        public TestData() {
            Add(typeof(decimal), nameof(LessThan<decimal>), typeof(LessThan<decimal>), new object?[] { 20.0m });
            Add(typeof(int), nameof(LessThan<int>), typeof(LessThan<int>), new object?[] { 20 } );
            Add(typeof(int), nameof(GreaterThan<int>), typeof(GreaterThan<int>), new object?[] { 20 });
            Add(typeof(int), nameof(MinimumValueIs<int>), typeof(MinimumValueIs<int>), new object?[] { 20 });
            Add(typeof(int), nameof(MaximumValueIs<int>), typeof(MaximumValueIs<int>), new object?[] { 20 });
            Add(typeof(int), nameof(EqualTo<int>), typeof(EqualTo<int>), new object?[] { 20 });

            Add(typeof(string), nameof(LengthIs), typeof(LengthIs), new object?[] { 20 });
            Add(typeof(string), nameof(MaximumLengthIs), typeof(MaximumLengthIs), new object?[] { 20 });
            Add(typeof(string), nameof(MinimumLengthIs), typeof(MinimumLengthIs), new object?[] { 20 });

            Add(typeof(List<int>), nameof(CountIs<int>), typeof(CountIs<int>), new object?[] { 20 });
            Add(typeof(List<string>), nameof(CountIs<string>), typeof(CountIs<string>), new object?[] { 20 });
            Add(typeof(List<string>), nameof(MaximumCountIs<string>), typeof(MaximumCountIs<string>), new object?[] { 20 });
            Add(typeof(List<string>), nameof(MinimumCountIs<string>), typeof(MinimumCountIs<string>), new object?[] { 20 });

            Add(typeof(Dictionary<string, int>), nameof(CountIs<KeyValuePair<string, int>>), typeof(CountIs<KeyValuePair<string, int>>), new object?[] { 20 });
            Add(typeof(Dictionary<string, decimal>), nameof(CountIs<KeyValuePair<string, decimal>>), typeof(CountIs<KeyValuePair<string, decimal>>), new object?[] { 20 });
            Add(typeof(Dictionary<string, string>), nameof(CountIs<KeyValuePair<string, int>>), typeof(CountIs<KeyValuePair<string, string>>), new object?[] { 20 });
            Add(typeof(Dictionary<string, string>), nameof(MaximumCountIs<KeyValuePair<string, int>>), typeof(MaximumCountIs<KeyValuePair<string, string>>), new object?[] { 20 });
            Add(typeof(Dictionary<string, string>), nameof(MinimumCountIs<KeyValuePair<string, int>>), typeof(MinimumCountIs<KeyValuePair<string, string>>), new object?[] { 20 });
        }
    }
}