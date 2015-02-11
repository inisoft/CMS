using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;
using Inisoft.Core.Object;
using Inisoft.Core.Extension;
using Newtonsoft.Json;

namespace Inisoft.Core.Provider
{

    public abstract class BaseRepository<TObject> : BaseRepository, IBaseRepository<TObject>
        where TObject : GenericObject, new()
    {

        public MethodResult<TObject> Save(TObject genericObject, User user)
        {
            if (genericObject.State == GenericObjectState.Modified || genericObject.IsNew)
            {
                if (genericObject.IsNew)
                {
                    // defaultowe wartosci
                    genericObject.CreateDate = DateTime.Now;
                    genericObject.CreateUser = user.FullName;
                    genericObject.CreateUserId = user.Id;
                    genericObject.Version = 0;
                }
                else
                {
                    // ustawienie kto edytuje i kiedy 
                    genericObject.UpdateDate = DateTime.Now;
                    genericObject.UpdateUser = user.FullName;
                    genericObject.UpdateUserId = user.Id;
                    genericObject.Version = genericObject.Version + 1;
                }

                MethodResult<TObject> result = DoSave(genericObject, user);
                if (result.Success)
                {
                    genericObject.MarkNotModified();
                }
                return result;
            }
            return new MethodResult<TObject>() { Data = genericObject };
        }

        public MethodResult Delete(TObject genericObject, User user)
        {
            MethodResult result = DoDelete(genericObject, user);
            if (result.Success)
            {
                genericObject.MarkNotModified();
            }
            return result;
        }

        public MethodResult<IList<TObject>> Get()
        {
            return DoGet();
        }

        public MethodResult<TObject> Get(int id)
        {
            return DoGet(id);
        }


        protected virtual MethodResult<TObject> DoSave(TObject genericObject, User user)
        {
            return storageProvider.Save(genericObject, ObjectDefinition);
        }

        protected virtual MethodResult DoDelete(TObject genericObject, User user)
        {
            return MethodResult.TRUE;
        }

        protected virtual MethodResult<IList<TObject>> DoGet()
        {
            return new MethodResult<IList<TObject>>()
            {
                Data = storageProvider.Select<TObject>(ObjectDefinition).ToList()
            };
        }

        protected virtual MethodResult<TObject> DoGet(int id)
        {
            return new MethodResult<TObject>()
            {
                Data = storageProvider.Select<TObject>(ObjectDefinition).Where(x => x.Id == id).FirstOrDefault()
            };
        }

    }
}