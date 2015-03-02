using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;
using Inisoft.Core.Object.Definition;
using Inisoft.Core.Repository;

namespace Inisoft.Core
{
    public class CoreEngineModule
    {
        public static void RegisterObjectTypes()
        {
            Engine.CheckSystemSchemaForObject(ObjectTypeObjectDefinition.Get());
            Engine.CheckSystemSchemaForObject(UserObjectDefinition.Get());
            Engine.CheckSystemSchemaForObject(RoleObjectDefinition.Get());
            Engine.CheckSystemSchemaForObject(GroupObjectDefinition.Get());
            Engine.CheckSystemSchemaForObject(RightObjectDefinition.Get());
        }

        public static void RegisterObjectRepositories()
        {
            RepositoryServiceLocator.Register<IObjectTypeRepository, ObjectTypeRepository>();
            RepositoryServiceLocator.Register<IUserRepository, UserRepository>();
            RepositoryServiceLocator.Register<IRoleRepository, RoleRepository>();
            RepositoryServiceLocator.Register<IGroupRepository, GroupRepository>();
            RepositoryServiceLocator.Register<IRightRepository, RightRepository>();
        }

        public static void CreateCustomObjectTypes()
        {
            IObjectTypeRepository objectTypeProvider = RepositoryServiceLocator.Get<IObjectTypeRepository>();
            objectTypeProvider.Save(ObjectTypeObjectDefinition.Get(), Engine.SystemCreator);
            objectTypeProvider.Save(UserObjectDefinition.Get(), Engine.SystemCreator);
            objectTypeProvider.Save(RoleObjectDefinition.Get(), Engine.SystemCreator);
            objectTypeProvider.Save(GroupObjectDefinition.Get(), Engine.SystemCreator);
            objectTypeProvider.Save(RightObjectDefinition.Get(), Engine.SystemCreator);
        }
    }
}
