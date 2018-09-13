using System.Web;

namespace KL.Persistence
{
  //3 - Reusable factory class that creates a static session, depending on type being persisted
  public class SessionBuilderFactory<T>
  {
    private static readonly ISessionBuilder contextualBuilder = new ContextualSessionBuilder<T>();

    public static ISessionBuilder GetBuilder()
    { return contextualBuilder; }

    public static ISessionBuilder CurrentBuilder
    {
      get
      {
        ISessionBuilder CurrentSessionBuilder = default(ISessionBuilder);

        if (HttpContext.Current == null)
        {
          if (CurrentSessionBuilder == null)
          { ISessionBuilder CurrentBuilderItem = GetBuilder(); }

          return GetBuilder();
        }
        else
        {
          CurrentSessionBuilder 
            = HttpContext.Current.Application["NHibernateSessionBuilder"] as ISessionBuilder;

          if (CurrentSessionBuilder == null)
          {
            ISessionBuilder CurrentBuilderItem = GetBuilder();
            HttpContext.Current.Application["NHibernateSessionBuilder"] = CurrentBuilderItem;

            return GetBuilder();
          }
        }

        return CurrentSessionBuilder;
      }
    }
  }
}

