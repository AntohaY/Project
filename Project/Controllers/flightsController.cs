using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class flightsController : Controller
    {
        private controltowerEntities db = new controltowerEntities();

        // GET: flights
        public ActionResult Index()
        {
            var flight = db.flight.Include(f => f.plane);
            return View(flight.ToList());
        }
        //Returning a collection of passengers with tickets for flight
        public ActionResult Passengers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            flight flight = db.flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }

            IEnumerable<passengers> passengers = flight.ticket.Select(a => a.passengers);

            return View(passengers);
        }
        //Employees
        public ActionResult Employees(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            flight flight = db.flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            // Creating a list of employees and filling it up with employees objects.
            var emp = new List<employees>();

            employees pilot = flight.plane.crew1.employees;

            employees engineer = flight.plane.crew1.employees3;

            employees stewardess = flight.plane.crew1.employees2;

            employees secondPilot = flight.plane.crew1.employees1;

            emp.Add(pilot);
            emp.Add(engineer);
            emp.Add(stewardess);
            emp.Add(secondPilot);
            //Adding an enumerator to the list, to be able to cycle through with foreach later on.
            IEnumerable<employees> empl = emp;

            return View(empl);
        }

        // GET: flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            flight flight = db.flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        //// GET: flights/Create
        //public ActionResult Create()
        //{
        //    ViewBag.PlaneID = new SelectList(db.plane, "PlaneID", "Type");
        //    return View();
        //}

        //// POST: flights/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FlightID,DeparturePlace,DestinationPlace,DepartureTime,ArrivalTime,FlightStatus,PlaneID,StripID")] flight flight)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.flight.Add(flight);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.PlaneID = new SelectList(db.plane, "PlaneID", "Type", flight.PlaneID);
        //    return View(flight);
        //}

        // GET: flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            flight flight = db.flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaneID = new SelectList(db.plane, "PlaneID", "Type", flight.PlaneID);

            IEnumerable<passengers> passengers = flight.ticket.Select(a => a.passengers);
            ViewBag.PassengersList = passengers;

            return View(flight);
        }

        // POST: flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightID,DeparturePlace,DestinationPlace,DepartureTime,ArrivalTime,FlightStatus,PlaneID,StripID")] flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaneID = new SelectList(db.plane, "PlaneID", "Type", flight.PlaneID);
            return View(flight);
        }

        //// GET: flights/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    flight flight = db.flight.Find(id);
        //    if (flight == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(flight);
        //}

        //// POST: flights/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    flight flight = db.flight.Find(id);
        //    db.flight.Remove(flight);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
