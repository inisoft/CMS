using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Storage;

namespace Inisoft.Core.Interface.Storage
{
    public interface IStorageQuery
    {
        string QueryText { get; }
        IList<IStorageQueryParameter> Parameters { get; }
        StorageQueryTypeEnum StorageQueryType { get; }
    }
}