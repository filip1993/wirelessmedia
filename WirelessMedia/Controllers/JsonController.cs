using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirelessMedia.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections;

namespace WirelessMedia.Controllers
{
    public class JsonController : Controller
    {
        public string jsonFile = "~/App_Data/Proizvodi.json";
       
        public ActionResult Index()
        {
            string file = Server.MapPath(jsonFile);
            string json = System.IO.File.ReadAllText(file);
            List<Proizvod> lista = JsonConvert.DeserializeObject < List < Proizvod >> (json);
            return View(lista);
        }
    }
}