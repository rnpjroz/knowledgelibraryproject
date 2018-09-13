using System;
using System.Collections.Generic;

namespace KL.Persistence
{
  public interface IBaseRepository<T>
  {
    void Save(T objectToSave);
    IList<T> RetrieveAll();
    T RetrieveBy(Guid id);
    void Delete(Guid id);   
  }
}
