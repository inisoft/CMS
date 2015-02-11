using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core.Interface.Storage
{
    public interface IStorageQueryParameter
    {
        string Name { get; set; }
        object Value { get; set; }
        bool IsNull { get; set; }
        int Size { get; set; }
    }
}
