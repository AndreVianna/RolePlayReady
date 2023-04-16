using System.Results;
using System.Validations.Abstractions;
using System.Validations.Extensions;

namespace System.Validators;

public class ValidationExtensionsTests {
    private class TestObject : IValidatable {
        public string? Text { get; init; }
        public int Integer1 { get; init; }
        public int? Integer2 { get; init; }
        public decimal Decimal1 { get; init; }
        public decimal? Decimal2 { get; init; }
        public DateTime DateTime1 { get; init; }
        public DateTime? DateTime2 { get; init; }
        public Type? Type { get; init; }
        public ICollection<TestObject> TestObjects { get; init; } = new List<TestObject>();
        public ICollection<int> Integers { get; init; } = new List<int>();
        public IDictionary<string, TestObject> Map { get; init; } = new Dictionary<string, TestObject>();
        public IDictionary<int, string> Links { get; init; } = new Dictionary<int, string>();
        public ValidationResult Validate() {
            var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
            var result = new ValidationResult();
            result += Text.Is().NotNull().And.NotEmptyOrWhiteSpace().Result;
            result += Text.Is().NotShorterThan(3).And.NotLongerThan(10).Result;
            result += Integer1.Is().GreaterThan(10).And.LessThan(20).Result;
            result += Integer2.Is().NotNull().And.GreaterOrEqualTo(10).And.LessOrEqualTo(20).Result;
            result += Decimal1.Is().GreaterThan(10).And.LessThan(20).Result;
            result += Decimal2.Is().NotNull().And.GreaterOrEqualTo(10).And.LessOrEqualTo(20).Result;
            result += DateTime1.Is().After(baseDate).And.Before(baseDate.AddDays(1)).Result;
            result += DateTime2.Is().NotNull().And.StartsOn(baseDate).And.EndsOn(baseDate.AddDays(1)).Result;
            result += Type.Is().NotNull().Result;
            result += TestObjects.ForEach(item => item.Is().NotNull().And.Valid()).Result;
            result += Integers.ListIs().NotEmpty().Result;
            result += Map.ForEach(entry => entry.Value.Is().NotNull().And.Valid()).Result;
            result += Links.MappingIs().NotShorterThan(3).And.NotLongerThan(5).Result;
            return result;
        }
    }

    private static TestObject GenerateGoodData() {
        var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
        var testObject1 = new TestObject() {
            Text = "TestData2",
            Integer1 = 11,
            Integer2 = 11,
            Decimal1 = 10.1m,
            Decimal2 = 10.1m,
            DateTime1 = baseDate.AddSeconds(1),
            DateTime2 = baseDate.AddSeconds(1),
            Type = typeof(int),
            Integers = new[] { 1, 2, 3 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C" }
        };
        var testObject2 = new TestObject() {
            Text = "TestData3",
            Integer1 = 19,
            Integer2 = 19,
            Decimal1 = 19.9m,
            Decimal2 = 19.9m,
            DateTime1 = baseDate.AddDays(1).AddSeconds(-1),
            DateTime2 = baseDate.AddDays(1).AddSeconds(-1),
            Type = typeof(DateTime),
            Integers = new[] { 1, 2, 3 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C" }
        };
        var children = new List<TestObject>() {
            testObject1,
            testObject2,
        };
        var map = new Dictionary<string, TestObject>() {
            { "Test1", testObject1 },
            { "Test2", testObject2 },
        };
        return new TestObject {
            Text = "TestData",
            Integer1 = 15,
            Integer2 = 15,
            Decimal1 = 15.0m,
            Decimal2 = 15.0m,
            DateTime1 = baseDate.AddDays(0.5),
            DateTime2 = baseDate.AddDays(0.5),
            Type = typeof(string),
            Integers = new[] { 1, 2, 3 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C" },
            TestObjects = children,
            Map = map,
        };
    }

    private static TestObject GenerateBadData() {
        var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
        var testObject0 = new TestObject();
        var testObject1 = new TestObject {
            Text = "12",
            Integer1 = 5,
            Integer2 = 1,
            Decimal1 = 5,
            Decimal2 = 1.0m,
            DateTime1 = baseDate.AddDays(-1),
            DateTime2 = baseDate.AddDays(-1),
            Type = typeof(string),
            TestObjects = new[] { testObject0 },
            Integers = Array.Empty<int>(),
            Map = new Dictionary<string, TestObject> { ["Test0"] = testObject0 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B" }
        };
        var testObject2 = new TestObject() {
            Text = "12345678901",
            Integer1 = 30,
            Integer2 = 30,
            Decimal1 = 30,
            Decimal2 = 30.0m,
            DateTime1 = baseDate.AddDays(3),
            DateTime2 = baseDate.AddDays(3),
            Type = typeof(int),
            TestObjects = new [] { testObject0 },
            Integers = new[] { 1, 2, 3 },
            Map = new Dictionary<string, TestObject> { ["Test0"] = testObject0 },
            Links = new Dictionary<int, string> { [1] = "A", [2] = "B", [3] = "C", [4] = "D", [5] = "E", [6] = "F" }
        };
        var testObject3 = new TestObject() {
            Text = null,
            Integer2 = null,
            Decimal2 = null,
            DateTime2 = null,
            Type = null,
            TestObjects = null!,
            Integers = null!,
            Map = null!,
            Links = null!,
        };
        var testObject4 = new TestObject() {
            Text = "   ",
            Integer2 = 4,
            Decimal2 = 1000.0m,
            DateTime2 = baseDate,
            Type = typeof(decimal),
            Integers = Array.Empty<int>(),
            Links = new Dictionary<int, string>(),
        };
        var children = new List<TestObject>() {
            testObject1,
            null!,
            testObject2,
        };
        var map = new Dictionary<string, TestObject>() {
            ["Test1"] = testObject3,
            ["Test2"] = null!,
            ["Test3"] = testObject4,
        };
        return new TestObject() {
            Text = "",
            Integer2 = 1,
            Decimal2 = null,
            DateTime2 = baseDate,
            Type = typeof(string),
            TestObjects = children,
            Map = map,
        };
    }

    [Fact]
    public void Validators_WithGoodData_ReturnsValid() {
        // Arrange
        var subject = GenerateGoodData();

        // Act
        var result = subject.Validate();

        // Assert
        //result.IsSuccessful.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Validators_WithBadData_ReturnsFailure() {
        // Arrange
        var baseDate = DateTime.Parse("2023-04-01 12:34:56.789");
        var subject = GenerateBadData();

        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccessful.Should().BeFalse();
        result.Errors.Select(i => i.Message).Should().BeEquivalentTo(new[] {
            "'Text' cannot be empty or whitespace.",
            "'Text' length cannot be less than 3. Found: 0.",
            "'Integer1' value cannot be less or equal to 10. Found: 0.",
            "'Integer2' value cannot be less then 10. Found: 1.",
            "'Decimal1' value cannot be less or equal to 10. Found: 0.",
            "'Decimal2' cannot be null.",
            "'DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 0001-01-01 00:00:00.",
            "'TestObjects[0].Text' length cannot be less than 3. Found: 2.",
            "'TestObjects[0].Integer1' value cannot be less or equal to 10. Found: 5.",
            "'TestObjects[0].Integer2' value cannot be less then 10. Found: 1.",
            "'TestObjects[0].Decimal1' value cannot be less or equal to 10. Found: 5.",
            "'TestObjects[0].Decimal2' value cannot be less then 10. Found: 1.0.",
            "'TestObjects[0].DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 2023-03-31 12:34:56.",
            "'TestObjects[0].DateTime2' value cannot be before then 2023-04-01 12:34:56. Found: 2023-03-31 12:34:56.",
            "'TestObjects[0].TestObjects[0].Text' cannot be null.",
            "'TestObjects[0].TestObjects[0].Text' length cannot be less than 3. Found: 0.",
            "'TestObjects[0].TestObjects[0].Integer1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[0].TestObjects[0].Integer2' cannot be null.",
            "'TestObjects[0].TestObjects[0].Decimal1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[0].TestObjects[0].Decimal2' cannot be null.",
            "'TestObjects[0].TestObjects[0].DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 0001-01-01 00:00:00.",
            "'TestObjects[0].TestObjects[0].DateTime2' cannot be null.",
            "'TestObjects[0].TestObjects[0].Type' cannot be null.",
            "'TestObjects[0].TestObjects[0].Integers' cannot be empty.",
            "'TestObjects[0].TestObjects[0].Links' count cannot be less than 3. Found: 0.",
            "'TestObjects[0].Integers' cannot be empty.",
            "'TestObjects[0].Map[Test0].Text' cannot be null.",
            "'TestObjects[0].Map[Test0].Text' length cannot be less than 3. Found: 0.",
            "'TestObjects[0].Map[Test0].Integer1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[0].Map[Test0].Integer2' cannot be null.",
            "'TestObjects[0].Map[Test0].Decimal1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[0].Map[Test0].Decimal2' cannot be null.",
            "'TestObjects[0].Map[Test0].DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 0001-01-01 00:00:00.",
            "'TestObjects[0].Map[Test0].DateTime2' cannot be null.",
            "'TestObjects[0].Map[Test0].Type' cannot be null.",
            "'TestObjects[0].Map[Test0].Integers' cannot be empty.",
            "'TestObjects[0].Map[Test0].Links' count cannot be less than 3. Found: 0.",
            "'TestObjects[0].Links' count cannot be less than 3. Found: 2.",
            "'TestObjects[1]' cannot be null.",
            "'TestObjects[2].Text' length cannot be greater than 10. Found: 11.",
            "'TestObjects[2].Integer1' value cannot be greater or equal to 20. Found: 30.",
            "'TestObjects[2].Integer2' value cannot be greater then 20. Found: 30.",
            "'TestObjects[2].Decimal1' value cannot be greater or equal to 20. Found: 30.",
            "'TestObjects[2].Decimal2' value cannot be greater then 20. Found: 30.0.",
            "'TestObjects[2].DateTime1' value cannot be 2023-04-02 12:34:56 or after. Found: 2023-04-04 12:34:56.",
            "'TestObjects[2].DateTime2' value cannot be after then 2023-04-02 12:34:56. Found: 2023-04-04 12:34:56.",
            "'TestObjects[2].TestObjects[0].Text' cannot be null.",
            "'TestObjects[2].TestObjects[0].Text' length cannot be less than 3. Found: 0.",
            "'TestObjects[2].TestObjects[0].Integer1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[2].TestObjects[0].Integer2' cannot be null.",
            "'TestObjects[2].TestObjects[0].Decimal1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[2].TestObjects[0].Decimal2' cannot be null.",
            "'TestObjects[2].TestObjects[0].DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 0001-01-01 00:00:00.",
            "'TestObjects[2].TestObjects[0].DateTime2' cannot be null.",
            "'TestObjects[2].TestObjects[0].Type' cannot be null.",
            "'TestObjects[2].TestObjects[0].Integers' cannot be empty.",
            "'TestObjects[2].TestObjects[0].Links' count cannot be less than 3. Found: 0.",
            "'TestObjects[2].Map[Test0].Text' cannot be null.",
            "'TestObjects[2].Map[Test0].Text' length cannot be less than 3. Found: 0.",
            "'TestObjects[2].Map[Test0].Integer1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[2].Map[Test0].Integer2' cannot be null.",
            "'TestObjects[2].Map[Test0].Decimal1' value cannot be less or equal to 10. Found: 0.",
            "'TestObjects[2].Map[Test0].Decimal2' cannot be null.",
            "'TestObjects[2].Map[Test0].DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 0001-01-01 00:00:00.",
            "'TestObjects[2].Map[Test0].DateTime2' cannot be null.",
            "'TestObjects[2].Map[Test0].Type' cannot be null.",
            "'TestObjects[2].Map[Test0].Integers' cannot be empty.",
            "'TestObjects[2].Map[Test0].Links' count cannot be less than 3. Found: 0.",
            "'TestObjects[2].Links' count cannot be greater than 5. Found: 6.",
            "'Integers' cannot be empty.",
            "'Map[Test1].Text' cannot be null.",
            "'Map[Test1].Text' length cannot be less than 3. Found: 0.",
            "'Map[Test1].Integer1' value cannot be less or equal to 10. Found: 0.",
            "'Map[Test1].Integer2' cannot be null.",
            "'Map[Test1].Decimal1' value cannot be less or equal to 10. Found: 0.",
            "'Map[Test1].Decimal2' cannot be null.",
            "'Map[Test1].DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 0001-01-01 00:00:00.",
            "'Map[Test1].DateTime2' cannot be null.",
            "'Map[Test1].Type' cannot be null.",
            "'Map[Test1].Links' count cannot be less than 3. Found: 0.",
            "'Map[Test2]' cannot be null.",
            "'Map[Test3].Text' cannot be empty or whitespace.",
            "'Map[Test3].Integer1' value cannot be less or equal to 10. Found: 0.",
            "'Map[Test3].Integer2' value cannot be less then 10. Found: 4.",
            "'Map[Test3].Decimal1' value cannot be less or equal to 10. Found: 0.",
            "'Map[Test3].Decimal2' value cannot be greater then 20. Found: 1000.0.",
            "'Map[Test3].DateTime1' value cannot be 2023-04-01 12:34:56 or before. Found: 0001-01-01 00:00:00.",
            "'Map[Test3].Integers' cannot be empty.",
            "'Map[Test3].Links' count cannot be less than 3. Found: 0.",
            "'Links' count cannot be less than 3. Found: 0.",
        });
    }
}
