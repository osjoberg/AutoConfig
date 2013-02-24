using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test
{
	class ExpectedExceptionMessageAttribute : ExpectedExceptionBaseAttribute
    {
        private readonly string _message;
		private readonly Type _type;

        public ExpectedExceptionMessageAttribute(Type type, string message, params object[] @params)
        {
        	_type = type;
        	_message = string.Format(message, @params);
        }

        protected override void Verify(Exception exception)
        {
			Assert.AreEqual(_type, exception.GetType());
			Assert.AreEqual(_message, exception.Message);
        }
	}
}
