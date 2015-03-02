using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    public static class Const
    {
        /// <summary>
        /// Zdefiniowane nazwy namespacow dla typów obiektów
        /// </summary>
        public static class Namespace
        {
            /// <summary>
            /// Systemowy namespace
            /// </summary>
            public const string System = "System";
        }

        /// <summary>
        /// Nazwy obiektów
        /// </summary>
        public static class Name
        {
            public const string User = "User";
            public static readonly Inisoft.Core.ObjectName UserObjectName = new ObjectName() { Name = User, Namespace = Namespace.System};

            public const string ObjectType = "ObjectType";
            public static readonly Inisoft.Core.ObjectName ObjectTypeName = new ObjectName() { Name = ObjectType, Namespace = Namespace.System };

            public const string Right = "Right";
            public static readonly Inisoft.Core.ObjectName RightName = new ObjectName() { Name = Right, Namespace = Namespace.System };

            public const string Group = "Group";
            public static readonly Inisoft.Core.ObjectName GroupName = new ObjectName() { Name = Group, Namespace = Namespace.System };

            public const string Role = "Role";
            public static readonly Inisoft.Core.ObjectName RoleName = new ObjectName() { Name = Role, Namespace = Namespace.System };
        }
    }
}