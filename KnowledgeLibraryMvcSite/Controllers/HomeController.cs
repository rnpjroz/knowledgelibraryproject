using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using System.Web.Script.Serialization;

using KL.Domain;

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
      KnowledgeLibraryDetail kldRecordToLoad = new KnowledgeLibraryDetail();
      return View("CreateKld", kldRecordToLoad);
    }

    [HttpPost]
    public ActionResult CreateKld(KnowledgeLibraryDetail kldRecordToSave)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:52462/api/");

        //todo: this is a hack, fix later and load dynamic dropdowns
        kldRecordToSave.DevelopmentTypeId = Guid.Parse("71BE2DB6-C7EE-4EF9-9333-C204E1F61DBA");

        //HTTP POST
        var postTask = client.PostAsJsonAsync<KnowledgeLibraryDetail>("kld/add", kldRecordToSave);
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