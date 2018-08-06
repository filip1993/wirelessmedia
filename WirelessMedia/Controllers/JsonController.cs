using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirelessMedia.Models;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace WirelessMedia.Controllers
{
    public class JsonController : Controller
    {
        public ActionResult Index()
        {
            return View(GetDeserialized());
        }

        private List<Proizvod> GetDeserialized()
        {
            return JsonConvert.DeserializeObject<List<Proizvod>>(System.IO.File.ReadAllText(Server.MapPath("~/App_Data/Proizvodi.json")));
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
                List<Proizvod> lista = GetDeserialized();
                Random r = new Random();
                proizvod.ProizvodId = r.Next(10000000, 99999999);
                lista.Add(proizvod);
                string newJson = JsonConvert.SerializeObject(lista, Formatting.Indented);
                System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Proizvodi.json"), newJson);
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

            List<Proizvod> lista = GetDeserialized();
            Proizvod proizvod = lista.Find(p => p.ProizvodId == id);
            return View(proizvod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProizvodId,Kategorija,Proizvodjac,Dobavljac,Naziv,Opis,Cena")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                List<Proizvod> lista = GetDeserialized();
                Proizvod stariProizvod = lista.Find(p => p.ProizvodId == proizvod.ProizvodId);
                lista.Remove(stariProizvod);
                lista.Add(proizvod);
                string newJson = JsonConvert.SerializeObject(lista, Formatting.Indented);
                System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Proizvodi.json"), newJson);
                return RedirectToAction("Index");
            }
            return View(proizvod);
        }
    }
}