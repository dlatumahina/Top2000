﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Top2000GOED.Models
{
    public class ArtiestsController : Controller
    {
        private Top2000DataBase db = new Top2000DataBase();

        // GET: Artiests
        public ActionResult Index()
        {
            return View(db.Artiest.ToList());
        }

        // GET: Artiests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artiest artiest = db.Artiest.Find(id);
            if (artiest == null)
            {
                return HttpNotFound();
            }
            return View(artiest);
        }

        // GET: Artiests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artiests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "artiestid,naam")] Artiest artiest)
        {
            if (ModelState.IsValid)
            {
                db.Artiest.Add(artiest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artiest);
        }

        // GET: Artiests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artiest artiest = db.Artiest.Find(id);
            if (artiest == null)
            {
                return HttpNotFound();
            }
            return View(artiest);
        }

        // POST: Artiests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "artiestid,naam")] Artiest artiest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artiest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artiest);
        }

        // GET: Artiests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artiest artiest = db.Artiest.Find(id);
            if (artiest == null)
            {
                return HttpNotFound();
            }
            return View(artiest);
        }

        // POST: Artiests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artiest artiest = db.Artiest.Find(id);
            db.Artiest.Remove(artiest);
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