using System;
using System.Collections.Generic;
using System.Linq;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.SDK.Objects;
using TCAdminCustomFieldsEditor.Models;
using Service = TCAdmin.GameHosting.SDK.Objects.Service;

namespace TCAdminCustomFieldsEditor
{
    public static class Extensions
    {
        public static ObjectBase GetInstance(int id, EObjectBaseType objectBaseType)
        {
            switch (objectBaseType)
            {
                case EObjectBaseType.Service:
                    return new Service(id);
                case EObjectBaseType.Server:
                    return new TCAdmin.GameHosting.SDK.Objects.Server(id);
                case EObjectBaseType.Datacenter:
                    return new Datacenter(id);
                case EObjectBaseType.FileServer:
                    return new FileServer(id);
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectBaseType), objectBaseType, null);
            }
        }

        public static List<CustomFieldRow> ConvertToCustomFieldRows(this ObjectBase objectBase)
        {
            return objectBase.AppData.Keys.Select(appDataKey => new CustomFieldRow
                {RowId = appDataKey, Key = appDataKey, Value = objectBase.AppData[appDataKey].ToString()}).ToList();
                // {RowId = appDataKey, Key = appDataKey, Value = objectBase.AppData.GetValue(appDataKey)}).ToList();
        }
    }
}