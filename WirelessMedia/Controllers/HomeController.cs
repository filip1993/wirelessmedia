using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WirelessMedia.Models;

namespace WirelessMedia.Controllers
{
    public class HomeController : Controller
    {
        private WirelessMediaEntities db = new WirelessMediaEntities();
        public ActionResult Index()
        {
            return View(db.Proizvods.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProizvodId,Kategorija,Proizvodjac,Dobavljac,Naziv,Opis,Cena")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Proizvods.Add(proizvod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proizvod);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvods.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProizvodId,Kategorija,Proizvodjac,Dobavljac,Naziv,Opis,Cena")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proizvod);
        }

    }
}