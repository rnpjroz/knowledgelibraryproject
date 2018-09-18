using FluentNHibernate.Mapping;

namespace KL.Domain
{
  public class DevelopmentTypeMap : ClassMap<DevelopmentType>
  {
    public DevelopmentTypeMap()
    {
      Table("DevelopmentTypes");
      LazyLoad();
      Id(x => x.Id);
      Map(x => x.Name);
    }
  }
}

