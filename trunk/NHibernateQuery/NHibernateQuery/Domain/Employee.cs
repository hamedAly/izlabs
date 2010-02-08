using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateQuery.Domain
{
    class Employee
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual double Salary { get; set; }
        public virtual int Age { get; set; }
    }
}
