using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KL.WebAPI.Models;
using KL.Persistence;

namespace KL.WebAPI.Controllers
{
  public class KnowledgeLibraryDetailsController : ApiController
  {
    //get all
    public IEnumerable<KnowledgeLibraryDetail> Get()
    {
      BaseRepository<KnowledgeLibraryDetail> repo = new BaseRepository<KnowledgeLibraryDetail>();
      IList<KnowledgeLibraryDetail> records = repo.RetrieveAll();

      if (records == null || records.Count < 1)
        return null;

      return records;
    }

    public IHttpActionResult Get(Guid id)
    {
      var records = Get().FirstOrDefault((r) => r.Id == id);
      if (records == null)
        return NotFound();

      return Ok(records);
    }

    public IHttpActionResult Post(KnowledgeLibraryDetail record)
    {
      if (!ModelState.IsValid)
        return BadRequest("Record is not valid. Check your JSON format.");

      BaseRepository<KnowledgeLibraryDetail> repo = new BaseRepository<KnowledgeLibraryDetail>();
      try
      {
        repo.Save(record);
      }
      catch (Exception ex)
      {
        return BadRequest("Exception: " + ex.Message);
      }

      return Ok();
    }

    public IHttpActionResult Put(KnowledgeLibraryDetail record)
    {
      if (!ModelState.IsValid)
        return BadRequest("Record is not valid. Check your JSON format.");

      BaseRepository<KnowledgeLibraryDetail> repo = new BaseRepository<KnowledgeLibraryDetail>();
      try
      {
        repo.Save(record);
      }
      catch (Exception ex)
      {
        return BadRequest("Exception: " + ex.Message);
      }

      return Ok();
    }

    public IHttpActionResult Delete(Guid id)
    {
      Guid parsedGuid = Guid.Empty;
      
      if (id == null || id == Guid.Empty || !Guid.TryParse(id.ToString(), out parsedGuid))
        return BadRequest("Not a valid knowledge library id: " + id + "; " + parsedGuid);

      BaseRepository<KnowledgeLibraryDetail> repo = new BaseRepository<KnowledgeLibraryDetail>();
      try
      {
        repo.Delete(id);
      }
      catch (Exception ex)
      {
        return BadRequest("Exception: " + ex.Message);
      }

      return Ok();
    }       
  }
}
