using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface.Storage;

namespace Inisoft.Core.Storage
{
    public abstract class BaseStorageQuery : IStorageQuery
    {
        protected IList<IStorageQueryParameter> parameters = new List<IStorageQueryParameter>();
        
        public abstract string QueryText { get; }
        public IList<IStorageQueryParameter> Parameters { get { return parameters; } }
        public abstract StorageQueryTypeEnum StorageQueryType { get; }

        public IStorageQueryParameter AddParam(string name, object value)
        {
            StorageQueryParameter storageQueryParameter = new StorageQueryParameter();
            storageQueryParameter.IsNull = (value == null || value == System.DBNull.Value);
            storageQueryParameter.Value = value;
            storageQueryParameter.Name = name;

            parameters.Add(storageQueryParameter);
            return storageQueryParameter;
        }
    }
}