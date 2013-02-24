using System;
using System.Globalization;

namespace AutoConfig
{
	class AutoConfigException : Exception
	{
		public AutoConfigException(string message) : base (message)
		{			
		}

		public AutoConfigException(string message, params object[] @params) : base(string.Format(message, @params))
		{
		}
	}
}
