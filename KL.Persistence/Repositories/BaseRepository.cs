using System;
using System.Collections.Generic;
using KL.Persistence;
using NHibernate;
using NHibernate.Criterion;

namespace KL.Persistence
{
  public class BaseRepository<T> : IBaseRepository<T> where T : class
  {
    public readonly ISessionBuilder _sessionBuilder;

    private ISession _session;
    private ITransaction _transaction;

    public BaseRepository()
    {
      _sessionBuilder = SessionBuilderFactory<T>.CurrentBuilder;
    }

    public BaseRepository(T t)
    {
      _sessionBuilder = SessionBuilderFactory<T>.CurrentBuilder;
    }

    public BaseRepository(T t, ISession session, ITransaction transaction)
    {
      _sessionBuilder = SessionBuilderFactory<T>.CurrentBuilder;
      _session = session;
      _transaction = transaction;
    }

    protected virtual ISession GetSession()
    {
      return _sessionBuilder.CurrentSession;
    }

    public void Save(T objectToSave)
    {
      if (_session == null)
      { _session = GetSession(); }

      try
      {
        //_session.Clear();
        _session.SaveOrUpdate(objectToSave);
        _session.Flush();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Delete(Guid id)
    {
      if (_session == null)
        _session = GetSession();

      var objectToDelete = _session.Get<T>(id);

      _session.Delete(objectToDelete);
      _session.Flush();
    }

    public IList<T> RetrieveAll()
    {
      if (_session == null)
        _session = GetSession();

      var list = _session.CreateCriteria(typeof(T)).List<T>();
      return list;
    }

    public T RetrieveBy(Guid id)
    {
      if (_session == null)
        _session = GetSession();

      return _session.Get<T>(id);
    }
  }
}
