using FluentNHibernate.Mapping;

namespace KL.WebAPI.Models
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

