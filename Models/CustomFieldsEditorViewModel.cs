using System.Collections.Generic;

namespace TCAdminCustomFieldsEditor.Models
{
    public class CustomFieldsEditorViewModel
    {
        public int ObjectId;

        public List<CustomFieldRow> CustomFieldRows;

        public EObjectBaseType ObjectBaseType;
    }
}