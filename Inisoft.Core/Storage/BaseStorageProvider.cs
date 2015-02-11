using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Interface;
using Inisoft.Core.Provider;
using Inisoft.Core.Object;

namespace Inisoft.Core
{
    /// <summary>
    /// Klasa bazowa dla Storage Providera
    /// </summary>
    public abstract class BaseStorageProvider : IStorageProvider
    {
        public BaseStorageProvider()
        {
        }

        public abstract MethodResult CheckObjectStorageExists(ObjectName objectName);

        public abstract MethodResult CreateStorage(ObjectDefinition objectDefinition);

        public abstract MethodResult UpdateStorage(ObjectDefinition objectDefinition);

        public abstract MethodResult RemoveStorage(ObjectDefinition objectDefinition);

        public abstract MethodResult<ObjectDefinition> GetObjectDefinition(string objectName);

        public abstract MethodResult TestConnection();

        public abstract IStorageQueryable<TObject> Select<TObject>(ObjectDefinition objectDefinition)
            where TObject : GenericObject, new();


        public MethodResult<TObject> Save<TObject>(TObject genericObject, ObjectDefinition objectDefinition)
                        where TObject : GenericObject
        {
            
            // walidacja obiektu
            // a na koncu zapis
            return DoSave<TObject>(genericObject, objectDefinition);
        }

        protected abstract MethodResult<TObject> DoSave<TObject>(TObject genericObject, ObjectDefinition objectDefinition)
                        where TObject : GenericObject;
    }
}
