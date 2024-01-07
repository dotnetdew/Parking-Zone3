using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data;
using Parking_Zone.Models;
using Parking_Zone.Services;
using Parking_Zone.ViewModels.ParkingSlot;

namespace Parking_Zone.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParkingSlotsController : Controller
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IParkingZoneService _parkingZoneService;
        public ParkingSlotsController(IParkingZoneService parkingZoneService, IParkingSlotService parkingSlotService)
        {
            _parkingZoneService = parkingZoneService;
            _parkingSlotService = parkingSlotService;
        }

        // GET: Admin/ParkingSlots
        public IActionResult Index(Guid parkingZoneId)
        {
            var slots = _parkingSlotService.GetByParkingZoneId(parkingZoneId);

            var slotVMs = slots.Select(x => new ParkingSlotListItemVM(x)).ToList();

            var parkingZone = _parkingZoneService.GetById(parkingZoneId);

            ViewData["parkingZoneName"] = parkingZone.Name;
            ViewData["parkingZoneId"] = parkingZoneId;

            return View(slotVMs);
        }

        // GET: Admin/ParkingSlots/Details/5
        public IActionResult Details(Guid id)
        {
            var slot = _parkingSlotService.GetById(id);

            if (slot is null)
                return NotFound();

            slot.ParkingZone = _parkingZoneService.GetById(slot.ParkingZoneId);
            var slotVM = new ParkingSlotDetailsVM(slot);

            return View(slotVM);
        }

        // GET: Admin/ParkingSlots/Create
        public IActionResult Create(Guid parkingZoneId)
        {
            var parkingSlotCreateVM = new ParkingSlotCreateVM();

            parkingSlotCreateVM.ParkingZoneId = parkingZoneId;
            parkingSlotCreateVM.ParkingZoneName = _parkingZoneService.GetById(parkingZoneId).Name;

            return View(parkingSlotCreateVM);
        }

        // POST: Admin/ParkingSlots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ParkingSlotCreateVM parkingSlotCreateVM)
        {
            if (ModelState.IsValid)
            {
                var slot = parkingSlotCreateVM.MapToModel();
                _parkingSlotService.Insert(slot);
                return RedirectToAction("Index", "ParkingSlots", new { parkingZoneId = parkingSlotCreateVM.ParkingZoneId });
            }
            return View(parkingSlotCreateVM);
        }

        // GET: Admin/ParkingSlots/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parkingSlot = _parkingSlotService.GetById(id);
            parkingSlot.ParkingZone = _parkingZoneService.GetById(parkingSlot.ParkingZoneId);

            if (parkingSlot == null)
            {
                return NotFound();
            }
            var parkingSlotEditVM = new ParkingSlotEditVM(parkingSlot);

            return View(parkingSlotEditVM);
        }

        // POST: Admin/ParkingSlots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ParkingSlotEditVM parkingSlotEditVM)
        {
            if (id != parkingSlotEditVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingSlot = _parkingSlotService.GetById(parkingSlotEditVM.Id);

                if (existingSlot == null)
                {
                    return NotFound();
                }

                var slotVM = parkingSlotEditVM.MapToModel(existingSlot);
                _parkingSlotService.Update(slotVM);

                return RedirectToAction("Index", "ParkingSlots", new { ParkingZoneId = parkingSlotEditVM.ParkingZoneId });
            }
            return View(parkingSlotEditVM);
        }

        // GET: Admin/ParkingSlots/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var slot = _parkingSlotService.GetById(id);
            slot.ParkingZone = _parkingZoneService.GetById(slot.ParkingZoneId);

            if (slot == null)
            {
                return NotFound();
            }
            var slotVM = new ParkingSlotDetailsVM(slot);

            return View(slotVM);
        }

        // POST: Admin/ParkingSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id, Guid parkingZoneId)
        {
            var parkingSlot = _parkingSlotService.GetById(id);

            if (parkingSlot != null)
            {
                _parkingSlotService.Delete(parkingSlot);
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Index", "ParkingSlots", new { ParkingZoneId = parkingZoneId });
        }

        //private bool ParkingSlotExists(Guid id)
        //{
        //    return _context.ParkingSlots.Any(e => e.Id == id);
        //}
    }
}
