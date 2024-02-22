using LBm295.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LBm295.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly Game _game;
        private readonly Publisher _publisher;
        private readonly PublisherDB _publisherDB;


        public PublisherController(PublisherDB context)
        {
            _publisherDB = context;
        }


        //Get function for all the publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublisher()
        {
            if (_publisherDB.Publishers == null)
            {
                return NotFound();
            }
            return await _publisherDB.Publishers.Select(x => PublisherToPublisherDto(x)).ToListAsync();
        }

        


        //Get function for one specific publisher
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
        {
            if (_publisherDB.Publishers == null)
            {
                return NotFound();
            }
            var publisher = await _publisherDB.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return PublisherToPublisherDto(publisher);
        }





        [HttpPost("addPublisher")]
        public ActionResult<Publisher> AddDeveloper(PublisherDto request)
        {
            var newPublisher = new Publisher
            {
                
                PublisherName = request.PublisherName,
                CompanyWorth = request.CompanyWorth,
                FoundingYear = request.FoundingYear
            };

            return Ok(newPublisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            var publisher = await _publisherDB.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _publisherDB.Publishers.Remove(publisher);
            await _publisherDB.SaveChangesAsync();

            return NoContent();
        }


        

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(Guid id, PublisherDto publisherDto)
        {
            if (id != publisherDto.Id)
            {
                return BadRequest();
            }
            var publisher = await _publisherDB.Publishers.FindAsync(publisherDto.Id);
            if (publisher == null)
            {
                return NotFound();
            }
            publisherDto.PublisherName = publisherDto.PublisherName;
            publisherDto.CompanyWorth = publisherDto.CompanyWorth;
            publisherDto.FoundingYear = publisherDto.FoundingYear;

            _publisherDB.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _publisherDB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        




        private bool PublisherExists(Guid id)
        {
            return (_publisherDB.Publishers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool GameExists(Guid id)
        {
            return (_publisherDB.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }




        private static PublisherDto PublisherToPublisherDto(Publisher publisher )
        {
            return new PublisherDto
            {
                Id = publisher.Id,
                PublisherName = publisher.PublisherName,
                CompanyWorth = publisher.CompanyWorth,
                FoundingYear = publisher.FoundingYear

            };
        }
        


    }
}
