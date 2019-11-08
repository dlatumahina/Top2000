using PagedList;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Top2000GOED.Models
{
    public class SongsController : Controller
    {
        private Top2000Entities db = new Top2000Entities();

        // GET: Songs
        // In de index van Songs kun je zoeken op titel, artiest etc
        public ActionResult Index(string searching, int? i, string sortBy)
        {
            return View(db.Song.Where(x => x.titel.Contains(searching) || searching == null).ToList().ToPagedList(i ?? 1, 10));
        }


        // GET: Songs/Details/5
        // Als je geen id aangeeft bij details dan geeft hij een foutmelding terug
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Song.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        // Als de rol admin is dan kun je iets aanmaken bij artiest
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.artiestid = new SelectList(db.Artiest, "artiestid", "naam");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // Als je admin bent dan kan je een Song aanmaken met De naam van de Song, Artiest en de Titel en Jaar van dat nummer.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "songid,artiestid,titel,jaar")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Song.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.artiestid = new SelectList(db.Artiest, "artiestid", "naam", song.artiestid);
            return View(song);
        }

        // GET: Songs/Edit/5
        // Als je admin bent en iets wil aanmaken wat leeg is geeft hij een foutmelding
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Song.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.artiestid = new SelectList(db.Artiest, "artiestid", "naam", song.artiestid);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // Als je admin bent dan kan je de Song, Artiest, Titel en jaar aanpassen
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "songid,artiestid,titel,jaar")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.artiestid = new SelectList(db.Artiest, "artiestid", "naam", song.artiestid);
            return View(song);
        }

        // GET: Songs/Delete/5
        // Als je admin bent kun je ook dingen verwijderen. als je niks aangeeft wat je wil verwijderen dan geeft hij een foutmelding aan
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Song.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }
        // POST: Songs/Delete/5
		// als admin kun je songs verwijderen.
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Song.Find(id);
            db.Song.Remove(song);
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
