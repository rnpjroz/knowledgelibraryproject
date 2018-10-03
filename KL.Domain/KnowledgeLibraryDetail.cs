using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KL.Domain
{
  [DataContract]
  public class KnowledgeLibraryDetail
  {
    [DataMember]
    public virtual Guid Id { get; set; }
    [DataMember]
    public virtual string Name { get; set; }
    [DataMember]
    public virtual string Description { get; set; }
    [DataMember]
    public virtual string URL { get; set; }
    //[DataMember]
    //public virtual Guid DevelopmentTypeId { get; set; }
    [DataMember]
    public virtual DevelopmentType DevelopmentType { get; set; }
  }
}
