using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core.Interface
{
    public interface IEngineModule
    {
        void RegisterObjectTypes();
        void RegisterObjectRepositories();
        void CreateCustomObjectTypes();
    }
}