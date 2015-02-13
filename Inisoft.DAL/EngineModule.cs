using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;
using Inisoft.Core;
using Inisoft.DAL.DTO.Definition;
using Inisoft.DAL.Interface;
using Inisoft.DAL.Repository;

namespace Inisoft.DAL
{
    public class EngineModule : IEngineModule
    {
        public void RegisterObjectTypes()
        {
            Engine.CheckSystemSchemaForObject(MenuObjectDefinition.Get());
            Engine.CheckSystemSchemaForObject(RoleObjectDefinition.Get());
            Engine.CheckSystemSchemaForObject(GroupObjectDefinition.Get());
        }

        public void RegisterObjectRepositories()
        {
            RepositoryServiceLocator.Register<IMenuRepository, MenuRepository>();
            RepositoryServiceLocator.Register<IRoleRepository, RoleRepository>();
            RepositoryServiceLocator.Register<IGroupRepository, GroupRepository>();
        }

        public void CreateCustomObjectTypes()
        {
            IObjectTypeRepository objectTypeProvider = RepositoryServiceLocator.Get<IObjectTypeRepository>();
            objectTypeProvider.Save(MenuObjectDefinition.Get(), Engine.SystemCreator);
            objectTypeProvider.Save(RoleObjectDefinition.Get(), Engine.SystemCreator);
            objectTypeProvider.Save(GroupObjectDefinition.Get(), Engine.SystemCreator);
        }
    }
}