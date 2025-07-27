namespace FormBuilderAPI.DisplayClasses
{
    
        public class FormRequest
        {
            public string Name { get; set; }
            public int TenantId { get; set; } // Only include TenantId
            public List<FormFieldRequest> Fields { get; set; }
        }

    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FormFieldRequest
    {
        public string Alignment { get; set; }
        public int ColumnsPerRow { get; set; }
        public List<FormColumnRequest> Columns { get; set; }
    }

    public class FormColumnRequest
    {
        public string Type { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public string Placeholder { get; set; }
        public bool Required { get; set; }
        public List<string> Options { get; set; }
        public FormSectionRequest FormSection { get; set; }
    }

    public class FormSectionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FormRequest Form { get; set; }
    }
}