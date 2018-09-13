using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Event;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace KL.Persistence
{
  //TestEdit
  //1st dependency of Session Builder Factory class; Defines the connection string and mapping
  public abstract class SessionBuilderBase<T> : ISessionBuilder
  {
    private static ISessionFactory SessionFactory;

    public ISession OpenSession()
    {
      return GetSessionFactory().OpenSession();
    }

    public FluentConfiguration GetConfiguration()
    {
      return Fluently.Configure()
        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConfigureDatabase.DbConnectionString))
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())        
        .CurrentSessionContext(typeof(WebSessionContext).FullName); //todo: find out what this line does
      //NOTE: exposeconfiguration is to enable audit logging
      /*
      .ExposeConfiguration(c => c.EventListeners.PostInsertEventListeners
                              = new IPostInsertEventListener[] { new AuditEventListener() })
      .ExposeConfiguration(c => c.EventListeners.PostUpdateEventListeners
                              = new IPostUpdateEventListener[] { new AuditEventListener() })
      .ExposeConfiguration(c => c.EventListeners.PostDeleteEventListeners
                              = new IPostDeleteEventListener[] { new AuditEventListener() })
      .ExposeConfiguration(c => { c.SetProperty("hibernate.show_sql", "true"); });
      */
    }

    public ISessionFactory GetSessionFactory()
    {
      if (SessionFactory == null)      
        SessionFactory = GetConfiguration().BuildSessionFactory();      

      return SessionFactory;
    }

    public abstract ISession CurrentSession
    {
      get;
    }
  }
}

