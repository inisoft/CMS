using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Attribute;
using Inisoft.Core.Provider;
using Inisoft.Core.Interface;
using Inisoft.Core.Object;

namespace Inisoft.Core
{
    /// <summary>
    /// Punkt uruchomieniowy całego engine
    /// Ścieżka inicjalizacji oraz przetwarzania zdarzeń
    /// </summary>
    public static class Engine
    {
        /// <summary>
        /// Inicjalizacja calego silnika
        /// </summary>
        public static void Initialize()
        {
#if DEBUG
            DebugUtils.TraceOut("Engine", "Initialize", "Initialize begin...");
#endif
            // wszystkie zaladowane Assemblies
            // TODO = tutaj wcześniej jakiś AssemblyPreloader dla dodatkowych Assembly, ktore nalezy przegladac podczas inicjalizacji
            var loadedAssemblies = ReflectionUtils.GetAllAssemblies();

#if DEBUG
            DebugUtils.TraceOut("Engine", "Initialize", " > Assemblies loaded");
#endif

#if DEBUG
            DebugUtils.TraceOut("Engine", "Initialize", " > Testing connections for registered storage providers");
#endif
            // 2. Testowanie zarejestrowanych storage providerów
            MethodResult result = StorageServiceLocator.Create().TestConnection();
            if (!result.Success)
            {
                throw new CoreException("Engine", "Initialize", string.Format("Cannot test storage provider.\r\n{0}", result.GetMessage()));
            }
#if DEBUG
            DebugUtils.TraceOut("Engine", "Initialize", "Storage Provider - connection test OK.");
#endif

            // 4. Rejestracja systemowych repozytoriow
            CoreEngineModule.RegisterObjectRepositories();

#if DEBUG
            DebugUtils.TraceOut("Engine", "Initialize", " > Check storage schema");
#endif
            // 5. Sprawdzanie systemowego schematu danych dla defaultowego storage providera
            CoreEngineModule.RegisterObjectTypes();

#if DEBUG
            DebugUtils.TraceOut("Engine", "Initialize", " > Create system objects");
#endif

            // 6. Rejestrowanie systemowych typów danych
            CoreEngineModule.CreateCustomObjectTypes();

#if DEBUG
            DebugUtils.TraceOut("Engine", "Initialize", " > Create default system administrator user");
#endif
            // 7. Sprawdzanie czy domyślne konto administratora zostało utworzone
            CreateDefaultSystemAdministrator();

            CreateDefaultRights();

            // 8. Odpalanie innych eventow podczas inicjalizacji systemu
            IEnumerable<Type> engineModules = ReflectionUtils.GetTypeCollectionForBaseType(loadedAssemblies, typeof(IEngineModule));
            foreach (Type loopType in engineModules)
            {
                IEngineModule engineModule = Activator.CreateInstance(loopType) as IEngineModule;
                if (engineModule != null)
                {
                    engineModule.RegisterObjectTypes();
                    engineModule.RegisterObjectRepositories();
                    engineModule.CreateCustomObjectTypes();
                }
            }
        }

        public static void CheckSystemSchemaForObject(ObjectDefinition objectDefinition)
        {
            IStorageProvider defaultStorageProvider = StorageServiceLocator.Create();
            MethodResult checkSchema = defaultStorageProvider.CheckObjectStorageExists(objectDefinition.ObjectName);
            if (!checkSchema.Success)
            {
#if DEBUG
                DebugUtils.TraceOut("Engine", "CheckSystemSchemaForObject", "Storage for object doesn't exist: {0}. {1}", objectDefinition.ObjectName.FullName, checkSchema.GetMessage());
#endif
                checkSchema = defaultStorageProvider.CreateStorage(objectDefinition);
#if DEBUG
                if (checkSchema.Success)
                {
                    DebugUtils.TraceOut("Engine", "CheckSystemSchemaForObject", "Storage for object created OK: {0}. {1}", objectDefinition.ObjectName.FullName, checkSchema.GetMessage());
                }
                else
                {
                    DebugUtils.TraceOut("Engine", "CheckSystemSchemaForObject", "Error creating storage for object : {0}. {1}", objectDefinition.ObjectName.FullName, checkSchema.GetMessage());
                }
#endif
            }
            else
            {
#if DEBUG
                DebugUtils.TraceOut("Engine", "CheckSystemSchemaForObject", "Storage for object exist: {0}. Updating structure.", objectDefinition.ObjectName.FullName);
#endif
                checkSchema = defaultStorageProvider.UpdateStorage(objectDefinition);
#if DEBUG
                if (checkSchema.Success)
                {
                    DebugUtils.TraceOut("Engine", "CheckSystemSchemaForObject", "Storage for object updated OK: {0}. {1}", objectDefinition.ObjectName.FullName, checkSchema.GetMessage());
                }
                else
                {
                    DebugUtils.TraceOut("Engine", "CheckSystemSchemaForObject", "Error updating storage for object : {0}. {1}", objectDefinition.ObjectName.FullName, checkSchema.GetMessage());
                }
#endif
            }
        }

        #region Create System default objects
        public static User SystemCreator = new User() { Id = 0, FirstName = "System", LastName = "Creator", Email = "system.creator@inisoft.pl" };
        public static User DefaultUser = new User() { Id = -1, FirstName = "System", LastName = "Default", Email = "system.default@inisoft.pl" };

        private static void CreateDefaultSystemAdministrator()
        {
            IUserRepository userProvider = RepositoryServiceLocator.Get<IUserRepository>();
            User user = userProvider.Get("admin@inisoft.cms", string.Empty).Data;
            if (user == null)
            {
                user = new User();
                user.Comment = "Default system administrator";
                user.Email = "admin@inisoft.pl";
                user.FirstName = "System";
                user.LastName = "Administrator";
                user.IsApproved = true;
                user.IsLockedOut = false;
                user.LastActivityDate = DateTime.Now;
                user.Nick = "admin";
                user.Version = 1;
                user.ApplicationName = string.Empty;
                user.Password = string.Empty;
                userProvider.Save(user, SystemCreator);
            }
        }

        private static void CreateDefaultRights()
        {
            IRightRepository rightRepository = RepositoryServiceLocator.Get<IRightRepository>();
            IList<Right> rights = rightRepository.Get().Data;
            foreach (Right loopRight in Right.All)
            {
                if (rights.Where(x => x.CodeName == loopRight.CodeName).FirstOrDefault() == null)
                {
                    rightRepository.Save(loopRight, SystemCreator);
                }
            }
        }

        #endregion
    }
}