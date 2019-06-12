using GC_Dealership.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace GC_Dealership.Controllers
{
    public class VehiclesController : ApiController
    {
        private DealershipEntities db = new DealershipEntities();

        // GET: api/Vehicles
        public List<Vehicle> GetVehicles(string make = null, string model = null, int? year = null,string color = null )
        {
 
            List<Vehicle> vehicles = db.Vehicles.ToList();
            if (make != null)
            {
                vehicles = db.Vehicles.Where(x => x.make == make).ToList();
            }
            if (model != null)
            {
                vehicles = vehicles.Where(x => x.model == model).ToList();
            }
            if (year != null)
            {
                vehicles = vehicles.Where(x => x.year == year).ToList();
            }
            if (color != null)
            {
                vehicles = vehicles.Where(x => x.color == color).ToList();
            }
            return vehicles;
        }

        // GET: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PutVehicle(int id, string make = null, string model = null, int? year = null, string color = null)
        {
            Vehicle v = db.Vehicles.Find(id);
            if (v != null)
            {

                if (make != null)
                {
                    v.make = make;
                }
                if (model != null)
                {
                    v.model = model;
                }
                if (year != null)
                {
                    v.year = year;
                }
                if (color != null)
                {
                    v.color = color;
                }
                try
                {
                    db.Vehicles.AddOrUpdate(v);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                v = db.Vehicles.Find(id);
                return StatusCode(HttpStatusCode.OK);
            }
            return NotFound();
        }

        // POST: api/Vehicles
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(string make = null, string model = null, int? year = null, string color = null)
        {
            Vehicle v = new Vehicle()
            {
                make = make,
                model = model,
                year = year,
                color = color
            };

            try
            {
            db.Vehicles.Add(v);
            db.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return CreatedAtRoute("DefaultApi", new { id = v.id }, v);
        }

        // DELETE: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            return Ok(vehicle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehicleExists(int id)
        {
            return db.Vehicles.Count(e => e.id == id) > 0;
        }
    }
}