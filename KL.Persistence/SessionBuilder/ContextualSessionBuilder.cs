using System;
using System.Collections.Generic;
using System.Web;
using NHibernate;
using NHibernate.Context;

namespace KL.Persistence
{
  //2nd dependency of SessionBuilderFactory 
  public class ContextualSessionBuilder<T> : SessionBuilderBase<T>
  {
    public override ISession CurrentSession
    {
      get
      {
        /* if current web context has no session bound to it, create a session from the factory, 
         open one and then bind it to the session */
        if (!WebSessionContext.HasBind(GetSessionFactory()))        
          WebSessionContext.Bind(GetSessionFactory().OpenSession());
        
        return GetSessionFactory().GetCurrentSession();
      }
    }
  }
}
