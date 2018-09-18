using System;

namespace KL.Domain
{
  public class KnowledgeLibraryDetail
  {
    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual string URL { get; set; }
    public virtual Guid DevelopmentTypeId { get; set; }
  }
}
