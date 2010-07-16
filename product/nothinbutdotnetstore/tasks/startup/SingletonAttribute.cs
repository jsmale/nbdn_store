using System;

namespace nothinbutdotnetstore.tasks.startup
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TransientAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    {
    }
}