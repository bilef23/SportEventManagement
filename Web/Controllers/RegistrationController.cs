using System.Security.Claims;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Drawing.Chart.ChartEx;
using Repository;
using Service.Interface;
using SportEvents.Domain;
using SportEvents.Enum;

namespace Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IParticipantService _participantService;
        private readonly IRegistrationService _registrationService;
        private readonly IEventService _eventService;

        public RegistrationController( IParticipantService participantService, IRegistrationService registrationService, IEventService eventService)
        {
            _participantService = participantService;
            _registrationService = registrationService;
            _eventService = eventService;
        }

        // GET: Registration
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var registrations = await _registrationService.GetRegistrations();
            
            var userRegistrations=registrations.Where(z => z.UserId.Equals(userId)).ToList();
            return View(userRegistrations);
        }

        // GET: Registration/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _registrationService.GetRegistrationById(id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registration/Create
        public async Task<IActionResult> Create(Guid eventId)
        {
            var events = await _eventService.GetEvents();
            ViewData["EventId"] = new SelectList(events, "Id", "Description",eventId);
            ParticipantRegistrationViewModel model = new ParticipantRegistrationViewModel();
            model.EventId = eventId;
            ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(Gender)));
            return View(model);
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParticipantRegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var @event = await _eventService.GetEventById(viewModel.EventId);
                if (@event.Registrations.Count + 1 > @event.MaximumRegistrations)
                {
                    @event.OpenForRegistrations = false;
                    await _eventService.UpdateEvent(@event);
                }
                Participant participant = new Participant()
                {
                    Id = Guid.NewGuid(),
                    Email = viewModel.Participant.Email,
                    DateOfBirth = viewModel.Participant.DateOfBirth.ToUniversalTime(),
                    Gender = viewModel.Participant.Gender,
                    FirstName = viewModel.Participant.FirstName,
                    PhoneNumber = viewModel.Participant.PhoneNumber,
                    LastName = viewModel.Participant.LastName
                };
                await _participantService.CreateNewParticipant(participant);
                Registration registration = new Registration()
                {
                    RegistrationDate = DateTime.UtcNow.ToUniversalTime(),
                    EventId = viewModel.EventId,
                    ParticipantId = participant.Id,
                    Status = RegistrationStatus.Pending,
                    UserId = userId
                };
                
                await _registrationService.CreateNewRegistration(registration);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(Gender)));
            ParticipantRegistrationViewModel model = new ParticipantRegistrationViewModel();
            return View(model);
        }

        // GET: Registration/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _registrationService.GetRegistrationById(id);
            if (registration == null)
            {
                return NotFound();
            }

            var events = await  _eventService.GetEvents();
            var participants = await _participantService.GetParticipants();
            ViewData["EventId"] = new SelectList(events, "Id", "Description", registration.EventId);
            ViewData["ParticipantId"] = new SelectList(participants, "Id", "Email", registration.ParticipantId);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RegistrationDate,Status,EventId,ParticipantId,Id")] Registration registration)
        {
            if (id != registration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _registrationService.UpdateRegistration(registration);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var events = await  _eventService.GetEvents();
            var participants = await _participantService.GetParticipants();
            ViewData["EventId"] = new SelectList(events, "Id", "Description", registration.EventId);
            ViewData["ParticipantId"] = new SelectList(participants, "Id", "Email", registration.ParticipantId);
            return View(registration);
        }*/

        // GET: Registration/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration =await  _registrationService.GetRegistrationById(id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id != null)
            {
                await _registrationService.DeleteRegistration(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        /*private bool RegistrationExists(Guid id)
        {
            return _context.Registrations.Any(e => e.Id == id);
        }*/
    }
}
