using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface.Storage;

namespace Inisoft.Core.Storage
{
    public class StorageQueryParameter : IStorageQueryParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public bool IsNull { get; set; }
        public PropertyTypeEnum ParameterType { get; set; }
        public int Size { get; set; }
    }
}