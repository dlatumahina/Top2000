using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Top2000GOED.Models;

namespace Top2000GOED.Controllers
{
    public class LijstsController : Controller
    {
        private Top2000Entities db = new Top2000Entities();

        // GET: Lijsts
        public ActionResult Index(int? i=0)
        {
            if (i == 0)
            {
                i = DateTime.Now.Year - 3;
            }


            // Hier wordt alles omgezet naar een variabel x en dat wordt gesorteerd op jaar
            var lijst = db.Lijst.Include(l => l.Song).OrderBy(x => x.top2000jaar);
            return View(lijst.ToList().ToPagedList(i ?? 1, 10));
        }

        [HttpPost]
        public ActionResult Index(int dropdown)
        {
			// Hier wordt aangegeven dat het jaar wat je opvraagt van de dropdown gelijk moet zijn met hetgene wat hij aantoont
            var lijst = db.spGetDetailsArtiest(dropdown);
            var ListCollection = new List<Lijst>();

            foreach (var item in lijst)
            {
				// Alle variabelen die zijn toegevoegt aan lijst
				Song song = new Song();
                Artiest artiest = new Artiest();
                artiest.naam = item.naam;
                song.Artiest = artiest;
                song.titel = item.titel;
                song.jaar = item.jaar;

                // Alle variabelen die zijn toegevoegt aan de dropdown van lijst
                Lijst lijstDropdown = new Lijst();
                lijstDropdown.positie = item.positie;
                lijstDropdown.Song = song;
                lijstDropdown.Song.Artiest = artiest;
                lijstDropdown.Song.titel = item.titel;
                lijstDropdown.top2000jaar = item.top2000jaar;
                lijstDropdown.Song.jaar = item.jaar;

                ListCollection.Add(lijstDropdown);
                ViewBag.SelectedItem = dropdown;
            }

            //return View(lijst.ToList().ToPagedList(1, 10));
			//Je vraagt de paging op
            return View(ListCollection.ToPagedList(1, 10));
        }

        // GET: Lijsts/Details/5
		// Je vraagt hier de details van lijst op
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
		// Hier kun je het Jaar, Song en Positie aanmaken
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
		// Als je niks aanpast en dan op Edit klikt geeft hij een foutmelding
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
		// Hier kun je het Jaar, Song en Positie aanpassen
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
		// Hier geeft hij aan dat je het niet kan verwijderen als je niks aangeeft
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
		// Als je het verwijdert (jaar, en positie) van de lijst wordt je teruggestuurd naar de index van lijst
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
