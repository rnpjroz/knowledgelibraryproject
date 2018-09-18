using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KL.WebAPI
{
  public class AdrenalineTest
  {
    public class Person
    {
      public string FirstName { get; set; }
      public string LastName { get; private set; } //once you assign, you cannot change it.

      public Person(string firstName, string lastName)
      {
        FirstName = firstName;
        LastName = lastName;
      }

      public virtual void Enroll()
      {
        
      }
    }
  }
}