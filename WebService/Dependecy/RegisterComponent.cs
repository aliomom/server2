using Microsoft.Practices.Unity;

namespace WebService.Dependecy
{
    internal class RegisterComponent : IRegisterComponent
    {
        private readonly IUnityContainer _container;

        public RegisterComponent(IUnityContainer container)
        {
            this._container = container;
            //Register interception behaviour if any
        }

        public void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            if (withInterception)
            {
                //register with interception
            }
            else
            {
                this._container.RegisterType<TFrom, TTo>();
            }
        }

        

        public void RegisterTypeWithControlledLifeTime<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
        public void Resolve<TFrom>() 
        {
            this._container.Resolve<TFrom>();
        }

        public void RegisterTypeWithInjectedConstructor<TFrom, TTo>(string param) where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(new InjectionConstructor(param));
        }


        public void RegisterTypeWithTransientLifetimeManager<TFrom, TTo>() where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(new TransientLifetimeManager());
        }


        public void RegisterTypeWithInjectedFactory<TFrom>(object obj)
        {
            this._container.RegisterType<TFrom>(new InjectionFactory(c => obj));
        }
    }
}