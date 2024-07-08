using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;
using MusicLibrary.Services;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly BandService _bandService;

        public BandsController(BandService bandService)
        {
            _bandService = bandService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Band>>> Get() {
            Console.WriteLine("aici");
            return await _bandService.GetBandsAsync();
        }


        [HttpGet("{id:length(24)}", Name = "GetBand")]
        public async Task<ActionResult<Band>> Get(string id)
        {
            var band = await _bandService.GetBandByIdAsync(ObjectId.Parse(id));

            if (band == null)
            {
                return NotFound();
            }

            return band;
        }

        [HttpPost]
        public async Task<ActionResult<Band>> Create(Band band)
        {
            await _bandService.CreateBandAsync(band);

            return CreatedAtRoute("GetBand", new { id = band.Id }, band);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Band bandIn)
        {
            var band = await _bandService.GetBandByIdAsync(ObjectId.Parse(id));

            if (band == null)
            {
                return NotFound();
            }

            await _bandService.UpdateBandAsync(ObjectId.Parse(id), bandIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var band = await _bandService.GetBandByIdAsync(ObjectId.Parse(id));

            if (band == null)
            {
                return NotFound();
            }

            await _bandService.DeleteBandAsync(ObjectId.Parse(id));

            return NoContent();
        }
    }
}