using HospitalPatientAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TreatmentsController : ControllerBase
{
    private readonly HospitalContext _context;

    public TreatmentsController(HospitalContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Treatment>>> GetTreatments()
    {
        return await _context.Treatments
                             .Include(o => o.Doctor)
                             .Include(o => o.Patient)
                             .ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Treatment>> GetTreatment(int id)
    {
        var treatment = await _context.Treatments
                                   .Include(o => o.Doctor)
                                   .Include(o => o.Patient)
                                   .FirstOrDefaultAsync(o => o.Id == id);

        if (treatment == null)
        {
            return NotFound();
        }

        return treatment;
    }


    [HttpPost]
    public async Task<ActionResult<Treatment>> PostTreatment(Treatment treatment)
    {
        _context.Treatments.Add(treatment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTreatment), new { id = treatment.Id }, treatment);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutTreatment(int id, Treatment treatment)
    {
        if (id != treatment.Id)
        {
            return BadRequest();
        }

        _context.Entry(treatment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Treatments.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTreatment(int id)
    {
        var treatment = await _context.Treatments.FindAsync(id);
        if (treatment == null)
        {
            return NotFound();
        }

        _context.Treatments.Remove(treatment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
