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

      //source: https://stackoverflow.com/questions/27901175/how-to-get-dropdownlist-selectedvalue-in-controller-in-mvc
      List<DevelopmentType> developmentTypes = new List<DevelopmentType>();
      using (var handler = new HttpClientHandler())
      {
        using (var client = new HttpClient(handler))
        {
          client.BaseAddress = new Uri("http://localhost:52462/api/");
          var result = client.GetAsync("kld/getalldevtypes").Result;
          string resultContent = result.Content.ReadAsStringAsync().Result;

          JavaScriptSerializer json_serializer = new JavaScriptSerializer();
          developmentTypes = json_serializer.Deserialize<List<DevelopmentType>>(resultContent);
          var fromDatabaseEF = new SelectList(developmentTypes, "Id", "Name");
          ViewData["DevelopmentType"] = fromDatabaseEF;
        }
      }

      return View("CreateKld", kldRecordToLoad);
    }

    [HttpPost]
    public ActionResult CreateKld(KnowledgeLibraryDetail kldRecordToSave)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:52462/api/");

        //todo: this is a hack, fix later and load dynamic dropdowns
        kldRecordToSave.DevelopmentType 
          = new DevelopmentType()
            {
              Id = Guid.Parse(Request.Form["DevelopmentType"].ToString())
            };

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

    [HttpGet, ActionName("Update")]
    public ActionResult UpdateKld(Guid id)
    {
      KnowledgeLibraryDetail record = new KnowledgeLibraryDetail();

      //todo: not the best way to reload the dropdownlist
      List<DevelopmentType> developmentTypes = new List<DevelopmentType>();
      using (var handler = new HttpClientHandler())
      {
        using (var client = new HttpClient(handler))
        {
          client.BaseAddress = new Uri("http://localhost:52462/api/");
          var result = client.GetAsync("kld/getalldevtypes").Result;
          string resultContent = result.Content.ReadAsStringAsync().Result;

          JavaScriptSerializer json_serializer = new JavaScriptSerializer();
          developmentTypes = json_serializer.Deserialize<List<DevelopmentType>>(resultContent);
          var fromDatabaseEF = new SelectList(developmentTypes, "Id", "Name");
          ViewData["DevelopmentType"] = fromDatabaseEF;
        }
      }


      using (var client = new HttpClient())
      {        
        client.BaseAddress = new Uri("http://localhost:52462/api/");

        var getTask = client.GetAsync("kld/get/" + id.ToString());
        getTask.Wait();

        var result = getTask.Result;
        if (result.IsSuccessStatusCode)
        {
          string resultContent = result.Content.ReadAsStringAsync().Result;

          JavaScriptSerializer json_serializer = new JavaScriptSerializer();
          record = json_serializer.Deserialize<KnowledgeLibraryDetail>(resultContent);
        }
        else
          ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
      }      

      return View(record);
    }

    [HttpPost, ActionName("Update")]
    [ValidateAntiForgeryToken]
    public ActionResult UpdateKld(KnowledgeLibraryDetail kldRecordToSave)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:52462/api/");

        kldRecordToSave.DevelopmentType
          = new DevelopmentType()
          {
            Id = Guid.Parse(Request.Form["DevelopmentType"].ToString())
          };

        var putTask = client.PutAsJsonAsync<KnowledgeLibraryDetail>("kld/update", kldRecordToSave);
        putTask.Wait();

        var result = putTask.Result;
        if (result.IsSuccessStatusCode)        
          return RedirectToAction("Index");        
      }

      ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
      return View(kldRecordToSave);
    }    
  }
}