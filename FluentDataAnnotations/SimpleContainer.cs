using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentDataAnnotations
{
    public class SimpleContainer : IContainer
    {
        private readonly Dictionary<Type, Func<object>> _container = new Dictionary<Type, Func<object>>();

        public object GetInstance(Type t)
        {
            return t != null && IsTypeRegistered(t) ? _container[t]() : null;
        }

        public void Register(Type interfaceType, Type implementationType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException("implementationType");
            }

            var ctor = implementationType.GetConstructor(Type.EmptyTypes);

            if (ctor == null)
            {
                throw new Exception(string.Format(Resource.Error_NoDefaultCtorFormat, "ImplementationType"));
            }

            if (!typeof (IFluentAnnotation).IsAssignableFrom(implementationType))
            {
                throw new Exception(string.Format(Resource.Error_HasToImplementAnInterface, "ImplementationType", "IFluentAnnotation"));
            }

            if (!typeof (IFluentAnnotation<>).IsAssignableFrom(interfaceType))
            {
                throw new Exception(string.Format(Resource.Error_HasToImplementAnInterface, "InterfaceType", "IFluentAnnotation<>"));
            }

            Func<object> del = Expression.Lambda<Func<object>>(
                Expression.New(ctor)).Compile();
            _container[interfaceType] = del;
        }

        public void Register(Type interfaceType, Func<object> instanceCreator)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }

            if (!typeof (IFluentAnnotation<>).IsAssignableFrom(interfaceType))
            {
                throw new Exception(string.Format(Resource.Error_HasToImplementAnInterface, "InterfaceType", "IFluentAnnotation<>"));
            }

            if (instanceCreator == null)
            {
                throw new ArgumentNullException("instanceCreator");
            }

            _container[interfaceType] = instanceCreator;
        }

        public void Register<T>(Func<object> instanceCreator) where T : IFluentAnnotation
        {
            if (!typeof (IFluentAnnotation<>).IsAssignableFrom(typeof (T)))
            {
                throw new Exception(string.Format(Resource.Error_HasToImplementAnInterface, "InterfaceType", "IFluentAnnotation<>"));
            }

            if (instanceCreator == null)
            {
                throw new ArgumentNullException("instanceCreator");
            }
            _container[typeof (T)] = instanceCreator;
        }

        public void Register<TInt, TImpl>()
            where TInt : IFluentAnnotation
            where TImpl : IFluentAnnotation, new()
        {
            if (!typeof (IFluentAnnotation<>).IsAssignableFrom(typeof (TInt)))
            {
                throw new Exception(string.Format(Resource.Error_HasToImplementAnInterface, "TInt", "IFluentAnnotation<>"));
            }
            _container[typeof (TInt)] = () => new TImpl();
        }

        public bool IsTypeRegistered(Type type)
        {
            return type != null && _container.ContainsKey(type);
        }
    }
}