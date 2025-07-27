using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FormBuilderAPI.Data.Entities
{
    public class FormField
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Label { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Placeholder { get; set; }

        public bool Required { get; set; }

        public List<string> Options { get; set; } = new(); // Drop-down options (if applicable)

        [JsonIgnore] // Ignore this property during serialization
        public int FormSectionId { get; set; }
        [JsonIgnore]
        public FormSection FormSection { get; set; }
    }
}