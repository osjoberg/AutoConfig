namespace AutoConfig
{
	static class ExceptionMessage
	{
		public const string ArgumentIsNotEmptyMessage = "Argument cannot be an empty string.";

		public const string OnlyOneMatchingEnvironmentMessage = "Multiple matching environments found, only one matching environment is possible.";

		public const string AppSettingsKeyNotFound = "appSettings key '{0}' not found.";
		public const string ConfigSectionNotFound = "configSection '{0}' not found.";
		public const string ConnectionStringNameNotFound = "connectionStrings name '{0}' not found.";
		public const string CouldNotCastObjectOfTypeToType = "Could not cast object of type '{0}' to '{1}'.";
		public const string CouldNotFindTypeInAnyLoadedAssembly = "Could not find type '{0}' in any loaded assembly.";
		public const string CouldNotLoadEnvironmentSpecificConfigurationFile = "Could not load environment specific configuration file '{0}'.";
		public const string ValueCouldNotBeConvertedToType = "Value '{0}' could not be converted to type '{1}'.";
		public const string ValueIsNotDefinedInEnumOfType = "Value '{0}' is not defined in the enum of type '{1}'.";
	}
}
