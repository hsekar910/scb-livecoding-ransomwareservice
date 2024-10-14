using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ransomware_webapi.Models;
using ransomware_webapi.Service;

namespace ransomware_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RansomwareController : ControllerBase
    {
        private readonly RansomwareService _ransomwareService;
        
        public RansomwareController(RansomwareService ransomwareService)
        {
            _ransomwareService = ransomwareService;
        }

        [HttpGet]
        public async Task<List<Ransomware>> Get() =>
            await _ransomwareService.GetAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Ransomware>> Get(string id)
        {
            var ransomeware = await _ransomwareService.GetAsync(id);

            if (ransomeware is null)
            {
                return NotFound();
            }

            return ransomeware;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Ransomware newRansomware)
        {
            await _ransomwareService.CreateAsync(newRansomware);

            return CreatedAtAction(nameof(Get), new { id = newRansomware.Id }, newRansomware);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Ransomware updatedRansomware)
        {
            var ransomware = await _ransomwareService.GetAsync(id);

            if (ransomware is null)
            {
                return NotFound();
            }

            updatedRansomware.Id = ransomware.Id;

            await _ransomwareService.UpdateAsync(id, updatedRansomware);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ransomware = await _ransomwareService.GetAsync(id);

            if (ransomware is null)
            {
                return NotFound();
            }

            await _ransomwareService.RemoveAsync(id);

            return NoContent();
        }
    }
}
