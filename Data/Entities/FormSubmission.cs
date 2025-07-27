using System;

namespace FormBuilderAPI.Data.Entities
{
    public class FormSubmission
    {
        public int Id { get; set; }

        public int FormId { get; set; }

        public string Data { get; set; } // JSON string for submission data

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public int? UserId { get; set; } // Optional: Track which user submitted the form
    }
}