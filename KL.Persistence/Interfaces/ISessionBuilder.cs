using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;

namespace KL.Persistence
{
  public interface ISessionBuilder
  {
    ISession CurrentSession
    {
      get;
    }
    ISession OpenSession();
    ISessionFactory GetSessionFactory();
    FluentConfiguration GetConfiguration();
  }
}

