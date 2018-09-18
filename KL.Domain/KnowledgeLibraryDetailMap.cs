using FluentNHibernate.Mapping;

namespace KL.Domain
{
  public class KnowledgeLibraryDetailMap : ClassMap<KnowledgeLibraryDetail>
  {
    public KnowledgeLibraryDetailMap()
    {
      Table("KnowledgeLibraryDetail");
      LazyLoad();
      Id(x => x.Id);
      Map(x => x.Name).Not.Nullable();
      Map(x => x.Description);
      Map(x => x.URL);
      Map(x => x.DevelopmentTypeId);     
    }
  }
}

