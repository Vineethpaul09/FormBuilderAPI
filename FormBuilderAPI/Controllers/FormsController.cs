using FormBuilderAPI.Data.Entities;
using FormBuilderAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FormBuilderAPI.DisplayClasses;
using AutoMapper;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace FormBuilderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [McpServerToolType]
    public class FormsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public FormsController(AppDbContext context, IMapper mapper)
        {
            _context = context; _mapper = mapper;
        }

        // GET: api/forms
        [HttpGet]
        [McpServerTool(Name = "GetForms"), Description("Retrieve all forms with their sections and fields.")]
        public async Task<ActionResult<IEnumerable<Form>>> GetForms()
        {
            // Retrieve all forms with their sections and fields
            return await _context.Forms
                .Include(f => f.Fields)
                    .ThenInclude(fs => fs.Columns)
                .ToListAsync();
        }

        // GET: api/forms/{id}
        [HttpGet("{id}")]
        [McpServerTool(Name = "GetFormById"), Description("Retrieve a specific form by ID, including its sections and fields.")]
        public async Task<ActionResult<Form>> GetForm(int id)
        {
            // Retrieve a specific form by ID, including its sections and fields
            var form = await _context.Forms
                .Include(f => f.Fields)
                    .ThenInclude(s => s.Columns)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (form == null)
            {
                return NotFound($"Form with ID {id} not found.");
            }

            return form;
        }

        [HttpPost]
        [McpServerTool(Name = "CreateForm"), Description("Create a new form for a tenant.")]
        public async Task<ActionResult<Form>> CreateForm([FromBody] FormRequest formRequest)
        {
            // Validate form input
            if (formRequest == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if TenantId is provided
            if (formRequest.TenantId == 0)
            {
                return BadRequest("TenantId must be provided.");
            }

            // Ensure Tenant exists in the database
            var tenant = await _context.Tenants.FindAsync(formRequest.TenantId);
            if (tenant == null)
            {
                return BadRequest($"Tenant with ID {formRequest.TenantId} not found.");
            }

            // Check if a form with the same name already exists
            var existingForm = await _context.Forms
                .Where(a => a.Name.Trim().ToLower() == formRequest.Name.Trim().ToLower())
                .FirstOrDefaultAsync();

            if (existingForm != null)
            {
                // Return 409 Conflict if a form with the same name already exists
                return Conflict($"A form with the name '{formRequest.Name}' already exists.");
            }

            // Map FormRequest to Form entity
            var form = _mapper.Map<Form>(formRequest);

            // Set the Tenant navigation property
            form.Tenant = tenant;

            // Add form to the context
            _context.Forms.Add(form);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return 201 Created with the newly created form
            return CreatedAtAction(nameof(GetForm), new { id = form.Id }, form);
        }
    }
}