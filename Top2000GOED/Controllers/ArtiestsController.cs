using PagedList;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Top2000GOED.Models;

namespace Top2000GOED.Controllers
{
	
	public class ArtiestsController : Controller
	{
		private Top2000Entities db = new Top2000Entities();

		// Hier vraag je de noteringen op van Artiesten
		[HttpGet]
		public ActionResult SongDetails(int? id)
		{
			var noteringen = db.Lijst.Where(l => l.songid == id).OrderByDescending(l => l.top2000jaar).ToList();
			return View(noteringen);
		}

		// GET: Artiests
		// Hier vraag je de index op van artiest
		public ActionResult Index(int? i, string searching)
		{
			return View(db.Artiest.Where(x => x.naam.Contains(searching) || searching == null).ToList().ToPagedList(i ?? 1, 3));
		}

		// GET: Artiests/Details/5
		// Als je details opvraagt van een bepaalde artiest en die artiest heeft geen details dan geeft hij een foutmelding aan
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
		// Je kan een artiest toevoegen met een naam
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
		// Als je een artiest aanmaakt met lege velden dan geeft hij een foutmelding aan
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
		// Je kan een artiest aanpassen met een naam 
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
		// Als je een artiest wil verwijderen die niet bestaat dan geeft hij een foutmelding aan
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
		// Je kan een artiest verwijderen als je dit doet dan wordt je teruggestuurd naar de index van artiest
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Artiest artiest = db.Artiest.Find(id);
			Song song = db.Song.Find(id);
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
