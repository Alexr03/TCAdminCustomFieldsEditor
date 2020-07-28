using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TCAdmin.SDK.Web.MVC.Controllers;
using TCAdminCustomFieldsEditor.Models;

namespace TCAdminCustomFieldsEditor.Controllers
{
    [Authorize]
    public class CustomFieldsEditorController : BaseController
    {
        public ActionResult Index(int id, EObjectBaseType objectBaseType)
        {
            var objectBase = Extensions.GetInstance(id, objectBaseType);

            var model = new CustomFieldsEditorViewModel
            {
                ObjectId = id,
                ObjectBaseType = objectBaseType,
                CustomFieldRows = objectBase.ConvertToCustomFieldRows()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GetCustomFields(int id, EObjectBaseType objectBaseType, [DataSourceRequest] DataSourceRequest request)
        {
            var objectBase = Extensions.GetInstance(id, objectBaseType);
            return Json(objectBase.ConvertToCustomFieldRows().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveCustomField(int id, EObjectBaseType objectBaseType, CustomFieldRow row,
            [DataSourceRequest] DataSourceRequest request)
        {
            if (row != null && ModelState.IsValid)
            {
                var objectBase = Extensions.GetInstance(id, objectBaseType);
                objectBase.AppData.RemoveValue(row.RowId);
                objectBase.Save();
            }

            return Json(new[] {row}.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult SetCustomField(int id, EObjectBaseType objectBaseType, CustomFieldRow row,
            [DataSourceRequest] DataSourceRequest request)
        {
            if (row != null && ModelState.IsValid)
            {
                var objectBase = Extensions.GetInstance(id, objectBaseType);
                if (objectBase.AppData[row.RowId] != null)
                {
                    objectBase.AppData[row.RowId] = row.Value;
                }
                else
                {
                    objectBase.AppData[row.Key] = row.Value;
                }
                
                objectBase.Save();
            }

            return Json(new[] {row}.ToDataSourceResult(request, ModelState));
        }
    }
}