﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecursosHumanos.Models;

namespace RecursosHumanos.Controllers
{
    public class EmpleadoesController : Controller
    {
        private DatabaseEntities4 db = new DatabaseEntities4();
        private string searchby;

        // GET: Empleadoes
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Cargo1).Include(e => e.Departamento1);
            return View(empleados.ToList());
            //if (searchby == "Gender")
            //{
            //    return View(db.Empleados.Where(x => x.Gender == search || search == null).ToList());
            //}
            //else
            //{
            //    return View(db.Empleados.Where(x => x.Nombre.StartsWith(Search)).ToList());
            //}
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo");
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo_Empleado,Nombre,Apellido,Telefono,Departamento,Cargo,Fecha_Ingreso,Salario,Estatus")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo", empleado.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo", empleado.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento", empleado.Departamento);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo_Empleado,Nombre,Apellido,Telefono,Departamento,Cargo,Fecha_Ingreso,Salario,Estatus")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo", empleado.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
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
