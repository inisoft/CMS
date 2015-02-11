using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Interface;
using System.Configuration;
using System.Reflection;

namespace Inisoft.Core
{
    public static class StorageServiceLocator
    {
        private static IStorageProvider provider = null;

        public static IStorageProvider Create()
        {
            if (provider == null)
            {
                string[] storageProviderConfig = ConfigurationManager.AppSettings["StorageProvider"].Split(',');
                Assembly assembly = Assembly.LoadFrom(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", storageProviderConfig[1].Trim() + ".dll"));
                Type type = assembly.GetType(storageProviderConfig[0].Trim());
                provider = Activator.CreateInstance(type) as IStorageProvider;
            }
            return provider;
        }
    }
}