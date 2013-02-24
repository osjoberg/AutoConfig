using System;

namespace AutoConfig
{
	public class Factory<TType>
	{
		public delegate TTypeDelegate CreateInstanceDelegate<out TTypeDelegate>();

		private readonly CreateInstanceDelegate<TType> _createInstance;

		internal Factory(CreateInstanceDelegate<TType> createInstance)
		{
			if (createInstance == null)
				throw new ArgumentNullException("createInstance");

			_createInstance = createInstance;
		}

		internal Factory(Type instanceType)
		{
			if (instanceType == null)
				throw new ArgumentNullException("instanceType");

			_createInstance = () => (TType) Activator.CreateInstance(instanceType);
		}

		public TType CreateInstance()
		{
			return _createInstance();
		}
	}
}
