using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FormBuilderAPI.Data.Entities
{
    public class FormSection
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Alignment { get; set; }

        [Range(1, 12)]
        public int ColumnsPerRow { get; set; }

        // One-to-Many Relationship: FormSection → FormFields
        [JsonPropertyName("columns")] // Rename the property in JSONs
        public List<FormField> Columns { get; set; } = new();

        // Foreign Key for Form
        public int? FormId { get; set; }
        [JsonIgnore]
        public Form Form { get; set; }

    }
}