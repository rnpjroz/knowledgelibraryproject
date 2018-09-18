﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using System.Web.Script.Serialization;

namespace KnowledgeLibraryMvcSite.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public JsonResult GetAll()
    {
      var records = new object();
      using (var handler = new HttpClientHandler())
      {
        using (var client = new HttpClient(handler))
        {
          client.BaseAddress = new Uri("http://localhost:52462/api/");
          var result = client.GetAsync("kld/getall").Result;
          string resultContent = result.Content.ReadAsStringAsync().Result;

          JavaScriptSerializer json_serializer = new JavaScriptSerializer();
          records = json_serializer.DeserializeObject(resultContent);
        }
      }

      return Json(records, JsonRequestBehavior.AllowGet);
    }

    public ActionResult CreateKld()
    {
      StudentDetailViewModel kldRecordToLoad = new StudentDetailViewModel();
      return View("CreateKld", kldRecordToLoad);
    }

    [HttpPost]
    public ActionResult CreateKld(myObject kldRecordToSave)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:52462/api/");

        //HTTP POST
        var postTask = client.PostAsJsonAsync<StudentViewModel>("kld/add", kldRecordToSave);
        postTask.Wait();

        var result = postTask.Result;
        if (result.IsSuccessStatusCode)
        {
          return RedirectToAction("Index");
        }
      }

      ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

      return View(kldRecordToSave);
    }

    // GET: Home
    //public ActionResult Index()
    //{
    //  var records = new object();

    //  using (var handler = new HttpClientHandler())
    //  {
    //    //kung me credentials na kailangan
    //    //handler.Credentials = new System.Net.NetworkCredential(@"DOMAIN\USERNAME", "PASSWORD");
    //    using (var client = new HttpClient(handler))
    //    {
    //      client.BaseAddress = new Uri("http://localhost:52462/api/");
    //      var result = client.GetAsync("kld").Result;
    //      string resultContent = result.Content.ReadAsStringAsync().Result;

    //      JavaScriptSerializer json_serializer = new JavaScriptSerializer();
    //      records = json_serializer.DeserializeObject(resultContent);
    //    }
    //  }      
    //  return View(records);
    //}
  }
}