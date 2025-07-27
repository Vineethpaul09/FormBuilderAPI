using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FormBuilderAPI.Data.Entities
{
    public class Form
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public int? TenantId { get; set; } // Foreign Key for Multi-Tenancy

        [JsonIgnore] // Ignore this property during serialization
        public Tenant Tenant { get; set; }

        // One-to-Many Relationship: Form → FormSections
        public List<FormSection> Fields { get; set; } = new();
    }
}