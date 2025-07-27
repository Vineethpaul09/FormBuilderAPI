using FormBuilderAPI.Data.Entities;
using FormBuilderAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace FormBuilderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [McpServerToolType]
    public class FormSubmissionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FormSubmissionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FormSubmissions
        [HttpGet]
        [McpServerTool(Name = "GetSubmissions"), Description("Retrieve all form submissions.")]
        public async Task<ActionResult<IEnumerable<FormSubmission>>> GetSubmissions()
        {
            try
            {
                var submissions = await _context.FormSubmissions.ToListAsync();
                return Ok(submissions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving submissions.");
            }
        }

        // GET: api/FormSubmissions/5
        [HttpGet("{id}")]
        [McpServerTool(Name = "GetSubmissionById"), Description("Retrieve a specific form submission by ID.")]
        public async Task<ActionResult<FormSubmission>> GetSubmission(int id)
        {
            try
            {
                var submission = await _context.FormSubmissions.FindAsync(id);

                if (submission == null)
                {
                    return NotFound();
                }

                return Ok(submission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the submission.");
            }
        }

        // POST: api/FormSubmissions
        [HttpPost]
        [McpServerTool(Name = "SubmitForm"), Description("Submit a new form submission.")]
        public async Task<ActionResult<FormSubmission>> SubmitForm(FormSubmission submission)
        {
            try
            {
                if (submission == null)
                {
                    return BadRequest("Submission data is null.");
                }

                _context.FormSubmissions.Add(submission);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSubmission), new { id = submission.Id }, submission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while submitting the form.");
            }
        }

        // DELETE: api/FormSubmissions/5
        [HttpDelete("{id}")]
        [McpServerTool(Name = "DeleteSubmission"), Description("Delete a form submission by ID.")]
        public async Task<IActionResult> DeleteSubmission(int id)
        {
            try
            {
                var submission = await _context.FormSubmissions.FindAsync(id);
                if (submission == null)
                {
                    return NotFound();
                }

                _context.FormSubmissions.Remove(submission);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the submission.");
            }
        }

        // GET: api/FormSubmissions/test
        [HttpGet("test")]
        [McpServerTool(Name = "TestFormSubmissions"), Description("Test endpoint for backend status.")]
        public IActionResult Test()
        {
            return Ok("Backend is running!");
        }
    }
}