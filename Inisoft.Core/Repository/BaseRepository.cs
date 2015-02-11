using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;
using Inisoft.Core.Object;
using Newtonsoft.Json;

namespace Inisoft.Core
{
    public abstract class BaseRepository
    {
        protected IStorageProvider storageProvider = null;

        public BaseRepository()
        {
            this.storageProvider = StorageServiceLocator.Create();
        }

        /// <summary>
        /// Zwraca zarejestrowaną nazwę typu obiektu jakim zarządza dany provider
        /// Na jego podstawie będzie pobierany ObjectType (ObjectDefinition)
        /// </summary>
        public abstract ObjectName ObjectName { get; }

        private ObjectDefinition objectDefinition = null;
        public ObjectDefinition ObjectDefinition
        {
            get
            {
                if (objectDefinition == null)
                {
                    objectDefinition = ReadObjectDefinition();
                }
                return objectDefinition;
            }
        }

        protected virtual ObjectDefinition ReadObjectDefinition()
        {
            IObjectTypeRepository objectTypeProvider = RepositoryServiceLocator.Get<IObjectTypeRepository>();
            ObjectType objectType = objectTypeProvider.Get(ObjectName);
            if (objectType == null)
            {
                throw new CoreException("BaseObjectProvider", "ReadObjectDefinition", string.Format("Cannot read object type for {0}", ObjectName.FullName));
            }
            return JsonConvert.DeserializeObject<ObjectDefinition>(objectType.Definition);
        }
    }
}