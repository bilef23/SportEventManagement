using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using SportEvents.Domain;

namespace Web.Controllers.api;


    [Route("api/[controller]")]
    [ApiController]
    public class EventsApiController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IOrganizerService _organizerService;

        public EventsApiController(IEventService eventService, IOrganizerService organizerService)
        {
            _eventService = eventService;
            _organizerService = organizerService;
        }
    
        [HttpGet("[action]")]
        public async Task<List<Event>> GetAllEvents()
        {
            return await _eventService.GetEvents();
        }
        
        [HttpGet("[action]")]
        public async Task<List<Organizer>> GetAllOrganizers()
        {
            return await _organizerService.GetOrganizers();
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> Details([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventById(id);
            
            return Ok(@event);
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Event @event)
        {
            var result=await _eventService.CreateNewEvent(@event);
            if (result is null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportEvents(List<Event> events)
        {
            var result = await _eventService.AddEvents(events);
            if (result <= 0)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Edit([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var @event = await _eventService.GetEventById(id);
            var organizers = await _organizerService.GetOrganizers();
            var payload = new { Event = @event, Organizers = organizers };
            return Ok(payload);
        }

        // POST: Event_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit(Event? @event)
        {
            if (@event is null)
            {
                return BadRequest();
            }

            await _eventService.UpdateEvent(@event);
            
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Delete([FromQuery]Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _eventService.GetEventById(id);
            
            return Ok(result);
        }

        // GET: Event_/Delete/5
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteConfirmed([FromBody]Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _eventService.DeleteEvent(id);
            
            return Ok(result);
        }
    }
