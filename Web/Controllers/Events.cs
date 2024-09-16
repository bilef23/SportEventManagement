using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Interface;
using SportEvents.Domain;

namespace Web.Controllers
{
    public class Events : Controller
    {
        private readonly IEventService _eventService;
        private readonly IOrganizerService _organizerService;
        public Events(IEventService eventService, IOrganizerService organizerService)
        {
            _eventService = eventService;
            _organizerService = organizerService;
        }

        // GET: Event_
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetEvents();
            events = events.FindAll(l=>l.StartDate > DateTime.Now);
            return View(events);
        }
   
        // GET: Event_/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventById(id);

            return View(@event);
        }

        // GET: Event_/Create
        public async Task<IActionResult> Create()
        {
            var organizers = await _organizerService.GetOrganizers();
            ViewData["OrganizerId"] = new SelectList(organizers, "Id", "Name");
            return View();
        }

        // POST: Event_/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Location,StartDate,EndDate,EventType,OrganizerId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.Id = Guid.NewGuid();
                await _eventService.CreateNewEvent(@event);
                return RedirectToAction(nameof(Index));
            }
            var organizers = await _organizerService.GetOrganizers();
            ViewData["OrganizerId"] = new SelectList(organizers, "Id", "Name", @event.OrganizerId);
            return View(@event);
        }

        // GET: Event_/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventById(id);
            
            var organizers = await _organizerService.GetOrganizers();
            ViewData["OrganizerId"] = new SelectList(organizers, "Id", "Name", @event.OrganizerId);
            return View(@event);
        }

        // POST: Event_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Location,StartDate,EndDate,EventType,Status,OrganizerId,Id")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("OK");
                await _eventService.UpdateEvent(@event);
                
                var organizers = await _organizerService.GetOrganizers();
                ViewData["OrganizerId"] = new SelectList(organizers, "Id", "Name", @event.OrganizerId);
                return RedirectToAction(nameof(Index));
            }

            @event.StartDate=@event.StartDate.ToLocalTime();
            @event.EndDate=@event.EndDate.ToLocalTime();
            return View(@event);
            
        }

        // GET: Event_/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _eventService.DeleteEvent(id);
            
            return View(result);
        }

        // POST: Event_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _eventService.GetEventById(id);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
