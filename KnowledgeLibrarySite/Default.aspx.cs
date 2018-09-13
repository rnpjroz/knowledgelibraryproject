using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using KL.Domain;
using KL.Persistence;

public partial class _Default : System.Web.UI.Page
{
  protected void CleanupForm()
  {
    ddlDevelopmentType.SelectedIndex = -1;
    gvRecords.SelectedIndex = -1;

    hdnIdToDelete.Value = "";
    txtName.Text = "";
    txtURL.Text = "";
  }

  protected void ReloadGridView()
  {
    BaseRepository<KnowledgeLibraryDetail> knowledgeLibraryDetailRepo = new BaseRepository<KnowledgeLibraryDetail>();
    IList<KnowledgeLibraryDetail> knowledgeLibraryDetails = knowledgeLibraryDetailRepo.RetrieveAll();

    gvRecords.DataSource = knowledgeLibraryDetails;
    gvRecords.DataBind();
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      BaseRepository<DevelopmentType> devTypeRepo = new BaseRepository<DevelopmentType>();
      IList<DevelopmentType> developmentTypes = devTypeRepo.RetrieveAll();
      
      ddlDevelopmentType.DataSource = developmentTypes;
      ddlDevelopmentType.DataBind();

      ReloadGridView();     
    }
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    KnowledgeLibraryDetail record;
    BaseRepository<KnowledgeLibraryDetail> repo = new BaseRepository<KnowledgeLibraryDetail>();

    if (string.IsNullOrEmpty(hdnIdToDelete.Value))
      record = new KnowledgeLibraryDetail();
    else
      record = repo.RetrieveBy(Guid.Parse(hdnIdToDelete.Value));

    record.Name = txtName.Text.Trim();
    record.URL = txtURL.Text.Trim();
    record.Description = "Desc: " + txtName.Text.Trim();
    record.DevelopmentTypeId = Guid.Parse(ddlDevelopmentType.SelectedValue);
    
    try
    {
      repo.Save(record);
      CleanupForm();
      ReloadGridView();      
    }
    catch (Exception ex)
    {
      throw ex;
    }

  }

  protected void gvRecords_RowCommand(object sender, GridViewCommandEventArgs e)
  {    
    Guid idToProcess
      = Guid.Parse(gvRecords.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());

    BaseRepository<KnowledgeLibraryDetail> repo = new BaseRepository<KnowledgeLibraryDetail>();

    if (e.CommandName.ToUpper().Equals("SELECT"))
    {
      KnowledgeLibraryDetail record = repo.RetrieveBy(idToProcess);

      hdnIdToDelete.Value = idToProcess.ToString();
      txtName.Text = record.Name;
      txtURL.Text = record.URL;
      ddlDevelopmentType.SelectedValue = record.DevelopmentTypeId.ToString();
    }

    if (e.CommandName.ToUpper().Equals("DELETE"))
    {
      repo.Delete(idToProcess);
      ReloadGridView();
    }
  }

  protected void gvRecords_RowDeleting(object sender, GridViewDeleteEventArgs e)
  { }
}