using System;

namespace nothinbutdotnetstore.infrastructure.containers.basic
{
    public class SingletonFactory : DependencyFactory
    {
        private readonly DependencyFactory _dependencyFactory;

        public SingletonFactory(DependencyFactory dependencyFactory) {
            _dependencyFactory = dependencyFactory;
        }

        private object instance;

        public object create() {
            if (instance != null) instance = _dependencyFactory.create();
            return instance;
        }
    }
}