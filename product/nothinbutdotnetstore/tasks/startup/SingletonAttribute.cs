using System;

namespace nothinbutdotnetstore.tasks.startup
{
	[AttributeUsage(AttributeTargets.Class)]
	public class IoCAttribute : Attribute
	{
		public Type ContractType { get; set; }
	}

    [AttributeUsage(AttributeTargets.Class)]
	public class TransientAttribute : IoCAttribute
	 {
    }

    [AttributeUsage(AttributeTargets.Class)]
	 public class SingletonAttribute : IoCAttribute
    {
    }
}