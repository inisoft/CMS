using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;

namespace Inisoft.Core
{
    /// <summary>
    /// Systemowy Service Locator (! - not IoC, because of hosting environment)
    /// </summary>
    public static class RepositoryServiceLocator
    {
        private static Dictionary<Type, Type> providers = new Dictionary<Type, Type>();

        /// <summary>
        /// zwraca konkretny provider dla zarejestrowanego typu interfejsu providera
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <returns></returns>
        public static TProvider Get<TProvider>()
            where TProvider : IBaseRepository
        {
            Type genericProviderType = typeof(TProvider);
            if (providers.ContainsKey(genericProviderType))
            {
                return (TProvider)Activator.CreateInstance(providers[genericProviderType]);
            }
            throw new CoreException("RepositoryServiceLocator", "Get", "Cannot get repository provider for type " + typeof(TProvider));
        }

        /// <summary>
        /// Rejestruje konkretny typ providera dla określonego typu bazowego.
        /// Typem bazowym jest interface
        /// </summary>
        public static void Register<TGenerigProvider, TConcreteProvider>()
            where TGenerigProvider : IBaseRepository
            where TConcreteProvider : IBaseRepository, new()
        {
            Type genericProviderType = typeof(TGenerigProvider);
            if (providers.ContainsKey(genericProviderType))
            {
                providers.Remove(genericProviderType);
            }
            providers.Add(genericProviderType, typeof(TConcreteProvider));
        }
    }
}