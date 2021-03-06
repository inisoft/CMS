﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inisoft.Core.Interface.Storage;

namespace Inisoft.Core.Storage
{
    public abstract class StorageQueryView : BaseStorageQuery, IStorageQuery
    {
        public override StorageQueryTypeEnum StorageQueryType
        {
            get { return StorageQueryTypeEnum.View; }
        }
    }
}