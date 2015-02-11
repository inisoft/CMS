using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Inisoft.Core
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Zwraca listę wszystkkich Assembly
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        /// Zwraca wszystkie mające dany typ bazowy (dziedziczące po baseType)
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypeCollectionForBaseType(IEnumerable<Assembly> assemblies, Type baseType)
        {
            List<Type> types = new List<Type>();
            bool isInterface = baseType.IsInterface;
            foreach (Assembly loopAssembly in assemblies)
            {
                foreach (Type loopType in loopAssembly.GetTypes())
                {
                    if (isInterface && loopType.ImplementsInterface(baseType))
                    {
                        types.Add(loopType);
                    }
                    else if (loopType.IsSubclassOf(baseType))
                    {
                        types.Add(loopType);
                    }
                }
            }
            return types;
        }

        private static bool ImplementsInterface(this Type type, Type ifaceType)
        {
            Type[] intf = type.GetInterfaces();
            for (int i = 0; i < intf.Length; i++)
            {
                if (intf[i] == ifaceType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}