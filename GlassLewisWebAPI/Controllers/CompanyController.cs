using GlassLewisWebAPI.Database;
using GlassLewisWebAPI.Models;
using GlassLewisWebAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyDbContext _context;
        private ICompanyService _companyService;

        public CompanyController(CompanyDbContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        // 1. Create a Company
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            return Ok(await _companyService.CreateCompany(company));            
        }

        // 2. Retrieve a Company by Id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var result = await _companyService.GetCompanyById(id);

            if(result != null)
                return Ok(result);
            else
                return NotFound();
        }

        // 3. Retrieve a Company by ISIN
        [HttpGet("by-isin/{isin}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompanyByIsin(string isin)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Isin == isin);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        // 4. Retrieve all Companies
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _context.Companies.ToListAsync());
        }

        // 5. Update an existing Company
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCompany([FromBody] Company updatedCompany)
        {            
            return Ok(await _companyService.UpdateCompany(updatedCompany));            
        }
    }
}
