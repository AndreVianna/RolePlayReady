﻿namespace System.Constants;

public static class Constants {
    public static class ErrorMessages {
        public const string CannotAssignNull = "Cannot assign null to '{0}'.";
        public const string CannotAssign = "Cannot assign '{1}' to '{0}'.";
        public const string IsNotOfType = "'{0}' is not of type '{1}'. Found: '{2}'.";
        public const string MustBeNull = "'{0}' must be null.";
        public const string CannotBeNull = "'{0}' cannot be null.";
        public const string CannotBeEmpty = "'{0}' cannot be empty.";
        public const string CannotBeWhitespace = "'{0}' cannot be whitespace.";
        public const string CannotBeEmptyOrWhitespace = "'{0}' cannot be empty or whitespace.";
        public const string CannotContainNull = "'{0}' cannot contain null.";
        public const string CannotContainNullOrEmpty = "'{0}' cannot contain null or empty.";
        public const string CannotContainEmpty = "'{0}' cannot contain empty.";
        public const string CannotContainNullOrWhitespace = "'{0}' cannot contain null or whitespace.";
        public const string CannotContainWhitespace = "'{0}' cannot contain whitespace.";
        public const string CannotContainEmptyOrWhitespace = "'{0}' cannot contain empty or whitespace.";

        public const string IsNotEqual = "'{0}' is not equal to {1}. Found: {2}.";

        public const string LengthCannotBeGreaterThan = "'{0}' minimum length is {1} character(s). Found: {2}.";
        public const string LengthCannotBeLessThan = "'{0}' maximum length is {1} character(s). Found: {2}.";
        public const string MustBeIn = "'{0}' must be in [{1}]. Found: {2}.";
        public const string LengthMustBe = "'{0}' length must be exactly {1}. Found: {2}.";
        public const string CannotHaveLessThan = "'{0}' cannot have less than {1} item(s). Found: {2}.";
        public const string CannotHaveMoreThan = "'{0}' cannot have more than {1} item(s). Found: {2}.";
        public const string MustHave = "'{0}' must have exactly {1} item(s). Found: {2}.";
        public const string MustContain = "'{0}' must contain '{1}'.";
        public const string MustNotContain = "'{0}' must not contain '{1}'.";
        public const string MustContainKey = "'{0}' must contain '{1}' as key.";
        public const string MustNotContainKey = "'{0}' must not contain '{1}' as key.";
        public const string IsNotAValidEmail = "'{0}' is not a valid email.";
        public const string IsNotAValidPassword = "'{0}' is not a valid email.";

        public const string CannotBeLessThan = "'{0}' cannot be less then {1}. Found: {2}.";
        public const string MustBeLessThan = "'{0}' must be less than {1}. Found: {2}.";
        public const string CannotBeGreaterThan = "'{0}' cannot be greater then {1}. Found: {2}.";
        public const string MustBeGraterThan = "'{0}' must be grather than {1}. Found: {2}.";
        public const string MustBeEqualTo = "'{0}' must be equal to {1}. Found: {2}.";
        public const string CannotBeBefore = "'{0}' cannot be before {1}. Found: {2}.";
        public const string MustBeBefore = "'{0}' must be befor {1}. Found: {2}.";
        public const string CannotBeAfter = "'{0}' cannot be after {1}. Found: {2}.";
        public const string MustBeAfter = "'{0}' must be after {1}. Found: {2}.";

        public const string CountOutOfRange = "'{0}' cannot have less than {1} or more than {2} items. Found: {3}.";

        public const string CannotAssignToResult = "The value cannot be assined to a result of type '{0}'.";
        public const string ResultIsNull = "The result cannot be null.";
        public const string ResultContainErrors = "The result contains {0} errors.";
    }
}
