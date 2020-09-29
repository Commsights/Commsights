using Commsights.Data.Enum;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Commsights.Data.Helpers
{
    public static class ObjectHelper
    {
        public static T Initialization<T>(this T model, InitType type, int initUser) where T : BaseModel
        {
            model.DateUpdated = AppGlobal.InitDateTime;
            model.UserUpdated = initUser;
            if (model.ParentID == null)
            {
                model.ParentID = 0;
            }
            switch (type)
            {
                case InitType.Update:
                    break;
                case InitType.Insert:
                    model.DateCreated = AppGlobal.InitDateTime;
                    model.UserCreated = initUser;
                    break;
            }
            return model;
        }      
        public static Dictionary<string, string> ToDictionaryStringString(this object obj)
        {
            if (obj != null)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                PropertyInfo[] props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    result.Add(prop.Name, prop.GetValue(obj)?.ToString());
                }
                return result;
            }
            return null;
        }

        public static T MapTo<T>(this object obj)
        {
            var result = Activator.CreateInstance<T>();
            if (obj != null)
            {
                PropertyInfo[] props = result.GetType().GetProperties();
                foreach (var prop in props)
                {
                    try
                    {
                        prop.SetValue(result, prop.GetValue(obj));
                    }
                    catch
                    {

                    }
                }
            }
            return result;
        }
    }
}
