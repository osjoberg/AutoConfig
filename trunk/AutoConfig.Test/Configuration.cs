using System.Data.Common;

namespace AutoConfig.Test
{
    class Configuration : ConfigurationBase
    {
        public static int TestInt32ThrowsExceptionWhenValueIsEmptyString { get { return GetAppSetting<int>("testInt32ThrowsExceptionWhenValueIsEmptyString"); } }
        public static int TestInt32ReturnsValue { get { return GetAppSetting<int>("testInt32ReturnsValue"); } }
        public static int? TestNullableInt32ReturnsNullWhenValueIsEmptyString { get { return GetAppSettingOrNull<int>("testNullableInt32ReturnsNullWhenValueIsEmptyString"); } }
        public static int? TestNullableInt32ReturnsValue { get { return GetAppSettingOrNull<int>("testNullableInt32ReturnsValue"); } }
        public static int TestMissingInt32ThrowsException { get { return GetAppSetting<int>("testMissingInt32ThrowsException"); } }
        public static int? TestMissingNullableInt32ThrowsException { get { return GetAppSettingOrNull<int>("testMissingNullableInt32ThrowsException"); } }

        public static double TestDoubleThrowsExceptionWhenValueIsEmptyString { get { return GetAppSetting<double>("testDoubleThrowsExceptionWhenValueIsEmptyString"); } }
        public static double TestDoubleReturnsValue { get { return GetAppSetting<double>("testDoubleReturnsValue"); } }
        public static double TestDoubleReturnsValueOtherCulture { get { return GetAppSetting<double>("testDoubleReturnsValueOtherCulture"); } }
        public static double? TestNullableDoubleReturnsNullWhenValueIsEmptyString { get { return GetAppSettingOrNull<double>("testNullableDoubleReturnsNullWhenValueIsEmptyString"); } }
        public static double? TestNullableDoubleReturnsValue { get { return GetAppSettingOrNull<double>("testNullableDoubleReturnsValue"); } }
        public static double TestMissingDoubleThrowsException { get { return GetAppSetting<double>("testMissingDoubleThrowsException"); } }
        public static double? TestMissingNullableDoubleThrowsException { get { return GetAppSettingOrNull<double>("testMissingNullableDoubleThrowsException"); } }

        public static TestEnum TestEnumThrowsExceptionWhenValueIsEmptyString { get { return GetAppSetting<TestEnum>("testEnumThrowsExceptionWhenValueIsEmptyString"); } }
        public static TestEnum TestEnumReturnsValue { get { return GetAppSetting<TestEnum>("testEnumReturnsValue"); } }
        public static TestEnum? TestNullableEnumReturnsNullWhenValueIsEmptyString { get { return GetAppSettingOrNull<TestEnum>("testNullableEnumReturnsNullWhenValueIsEmptyString"); } }
        public static TestEnum? TestNullableEnumReturnsValue { get { return GetAppSettingOrNull<TestEnum>("testNullableEnumReturnsValue"); } }
        public static TestEnum TestMissingEnumThrowsException { get { return GetAppSetting<TestEnum>("testMissingEnumThrowsException"); } }
        public static TestEnum? TestMissingNullableEnumThrowsException { get { return GetAppSettingOrNull<TestEnum>("testMissingNullableEnumThrowsException"); } }
        public static TestEnum TestEnumInvalidValue { get { return GetAppSetting<TestEnum>("testEnumInvalidValue"); } }
        public static TestEnum TestEnumOutOfRange { get { return GetAppSetting<TestEnum>("testEnumOutOfRange"); } }
        public static TestEnum TestEnumReturnsValueWhenInt { get { return GetAppSetting<TestEnum>("testEnumReturnsValueWhenInt"); } }
        public static TestEnum TestEnumUndefinedValueThrowsException { get { return GetAppSetting<TestEnum>("testEnumUndefinedValue"); } }
        public static TestEnum TestEnumReturnsValueWhenCasesMismatch { get { return GetAppSetting<TestEnum>("testEnumRetusrnsValueWhenCasingMismatch"); } }

        public static string TestStringReturnsValueWhenValueIsEmptyString { get { return GetAppSetting<string>("testStringReturnsValueWhenValueIsEmptyString"); } }
        public static string TestStringReturnsValue { get { return GetAppSetting<string>("testStringReturnsValue"); } }
        public static string TestMissingStringThrowsException { get { return GetAppSetting<string>("testMissingStringThrowsException"); } }

        public static DbConnection TestConnectionString { get { return CreateConnection("testConnectionString"); } }


    }
}
