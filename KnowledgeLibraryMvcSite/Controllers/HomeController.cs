using System;
using System.Collections.Generic;
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
      List<KnowledgeLibraryDetail> records = new List<KnowledgeLibraryDetail>();

      using (var handler = new HttpClientHandler())
      {
        using (var client = new HttpClient(handler))
        {
          client.BaseAddress = new Uri("http://localhost:52462/api/");
          var result = client.GetAsync("kld/getall").Result;         
          string resultContent = result.Content.ReadAsStringAsync().Result;

          JavaScriptSerializer json_serializer = new JavaScriptSerializer();
          records = json_serializer.Deserialize<List<KnowledgeLibraryDetail>>(resultContent);
        }
      }

      return View(records);
    }

    //old working Json
    //[HttpGet]
    //public JsonResult GetAll()
    //{
    //  var records = new object();
    //  using (var handler = new HttpClientHandler())
    //  {
    //    using (var client = new HttpClient(handler))
    //    {
    //      client.BaseAddress = new Uri("http://localhost:52462/api/");
    //      var result = client.GetAsync("kld/getall").Result;
    //      string resultContent = result.Content.ReadAsStringAsync().Result;


    //      JavaScriptSerializer json_serializer = new JavaScriptSerializer();
    //      records = json_serializer.DeserializeObject(resultContent);
    //    }
    //  }

    //  return Json(records, JsonRequestBehavior.AllowGet);
    //}

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

    //[HttpDelete]
    //public ActionResult Delete(Guid id)
    //{
    //  using (var client = new HttpClient())
    //  {
    //    client.BaseAddress = new Uri("http://localhost:52462/api/");

    //    var deleteTask = client.DeleteAsync("kld/delete/" + id.ToString());
    //    deleteTask.Wait();

    //    var result = deleteTask.Result;
    //    if (result.IsSuccessStatusCode)
    //      return RedirectToAction("Index");        
    //  }

    //  return RedirectToAction("Index");
    //}

    /*
     * secure delete: 
       source: http://stephenwalther.com/archive/2009/01/21/asp-net-mvc-tip-46-ndash-donrsquot-use-delete-links-because
     */
    //[AcceptVerbs(HttpVerbs.Delete)]
    /*
     * source: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods
     */
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(Guid id)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:52462/api/");

        var deleteTask = client.DeleteAsync("kld/delete/" + id.ToString());
        deleteTask.Wait();

        var result = deleteTask.Result;
        if (result.IsSuccessStatusCode)
          return RedirectToAction("Index");
      }

      return RedirectToAction("Index");
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