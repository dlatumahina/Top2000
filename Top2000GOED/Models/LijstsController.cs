using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Top2000GOED.Models
{
    public class LijstsController : Controller
    {
        private Top2000DataBase db = new Top2000DataBase();

        // GET: Lijsts
        public ActionResult Index(int? i)
        {
          // db.Lijst = omgezet naar variabel x en gesorteerd op jaar
            var lijst = db.Lijst.Include(l => l.Song).OrderBy(x => x.top2000jaar);
            return View(lijst.ToList().ToPagedList(i ?? 1, 10));
        }

        [HttpPost]
        public ActionResult Index(int dropdown)
        {
            // db.Lijst = Het top2000 jaar moet gelijk zijn aan wat er is aangeklikt op de dropdownmenu
            var lijst = db.Lijst.Include(l => l.Song).Where(x => x.top2000jaar == dropdown);
            return View(lijst.ToList());
        }

        // GET: Lijsts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lijst lijst = db.Lijst.Find(id);
            if (lijst == null)
            {
                return HttpNotFound();
            }
            return View(lijst);
        }

        // GET: Lijsts/Create
        public ActionResult Create()
        {
            ViewBag.songid = new SelectList(db.Song, "songid", "titel");
            return View();
        }

        // POST: Lijsts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "songid,top2000jaar,positie")] Lijst lijst)
        {
            if (ModelState.IsValid)
            {
                db.Lijst.Add(lijst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.songid = new SelectList(db.Song, "songid", "titel", lijst.songid);
            return View(lijst);
        }

        // GET: Lijsts/Edit/5
        public ActionResult Edit(int? idJaar, int? idPositie)
        {
            if (idJaar == null || idPositie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lijst lijst = db.Lijst.Where(x => x.top2000jaar == idJaar && x.positie == idPositie).First();
            if (lijst == null)
            {
                return HttpNotFound();
            }
            ViewBag.songid = new SelectList(db.Song, "songid", "titel", lijst.songid);
            return View(lijst);
        }

        // POST: Lijsts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "songid,top2000jaar,positie")] Lijst lijst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lijst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.songid = new SelectList(db.Song, "songid", "titel", lijst.songid);
            return View(lijst);
        }

        // GET: Lijsts/Delete/5
        public ActionResult Delete(int? idJaar, int? idPositie)
        { 
            if (idJaar == null || idPositie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lijst lijst = db.Lijst.Where(x => x.top2000jaar == idJaar && x.positie == idPositie).First();
            if (lijst == null)
            {
                return HttpNotFound();
            }
            return View(lijst);
        }

        // POST: Lijsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idJaar, int idPositie)
        {
            Lijst lijst = db.Lijst.Where(x => x.top2000jaar == idJaar && x.positie == idPositie).First();
            db.Lijst.Remove(lijst);
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
