using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParcialDos.Models;

namespace ParcialDos.Controllers
{
    public class RecepcionsController : Controller
    {
        private VeterlabEntities db = new VeterlabEntities();

        // GET: Recepcions
        public ActionResult Index()
        {
            var recepcion = db.Recepcion.Include(r => r.int_clientes).Include(r => r.Laboratorios);
            return View(recepcion.ToList());
        }

        // GET: Recepcions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcion recepcion = db.Recepcion.Find(id);
            if (recepcion == null)
            {
                return HttpNotFound();
            }
            return View(recepcion);
        }

        // GET: Recepcions/Create
        public ActionResult Create()
        {
            ViewBag.CliRut = new SelectList(db.int_clientes, "Rut", "Nombre");
            ViewBag.LabId = new SelectList(db.Laboratorios, "Id", "Nombre");
            return View();
        }

        // POST: Recepcions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CliRut,LabId,Folio,FechaRecepcion,FechaEntrega,Localidad,CantidadMuestras")] Recepcion recepcion)
        {
            if (ModelState.IsValid)
            {
                db.Recepcion.Add(recepcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CliRut = new SelectList(db.int_clientes, "Rut", "Nombre", recepcion.CliRut);
            ViewBag.LabId = new SelectList(db.Laboratorios, "Id", "Nombre", recepcion.LabId);
            return View(recepcion);
        }

        // GET: Recepcions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcion recepcion = db.Recepcion.Find(id);
            if (recepcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CliRut = new SelectList(db.int_clientes, "Rut", "Nombre", recepcion.CliRut);
            ViewBag.LabId = new SelectList(db.Laboratorios, "Id", "Nombre", recepcion.LabId);
            return View(recepcion);
        }

        // POST: Recepcions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CliRut,LabId,Folio,FechaRecepcion,FechaEntrega,Localidad,CantidadMuestras")] Recepcion recepcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CliRut = new SelectList(db.int_clientes, "Rut", "Nombre", recepcion.CliRut);
            ViewBag.LabId = new SelectList(db.Laboratorios, "Id", "Nombre", recepcion.LabId);
            return View(recepcion);
        }

        // GET: Recepcions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcion recepcion = db.Recepcion.Find(id);
            if (recepcion == null)
            {
                return HttpNotFound();
            }
            return View(recepcion);
        }

        // POST: Recepcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recepcion recepcion = db.Recepcion.Find(id);
            db.Recepcion.Remove(recepcion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
