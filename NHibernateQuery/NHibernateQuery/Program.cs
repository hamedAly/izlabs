using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NHibernateQuery.Domain;

namespace NHibernateQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Employee).Assembly);

            // create session factory
            var sessionFactory = configuration.BuildSessionFactory();

            SetupDb(configuration, sessionFactory);
            HQLTest(sessionFactory);
            LinqTest(sessionFactory);
            ICriteriaTest(sessionFactory);
            QueryOverTest(sessionFactory);

        }

        static void SetupDb(Configuration configuration, ISessionFactory sessionFactory)
        {
            Console.WriteLine("Running DB setup");
            // recreate db
            new SchemaExport(configuration).Execute(false, true, false);

            // fill db with data
            using (ISession session = sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5000, Age = 25, Name = "Joe" });
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5100, Age = 26, Name = "Michael" });
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5100, Age = 28, Name = "John" });
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5000, Age = 27, Name = "Bob" });
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5300, Age = 30, Name = "Ron" });
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5500, Age = 35, Name = "Kevin" });
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5500, Age = 35, Name = "David" });
                session.Save(new Employee { Id = Guid.NewGuid(), Salary = 5000, Age = 22, Name = "Tom" });
                transaction.Commit();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static void HQLTest(ISessionFactory sessionFactory)
        {
            Console.WriteLine("Running HQL sample");
            using (ISession session = sessionFactory.OpenSession())
            {
                var salary = session.CreateQuery(
                    @"
select 
    avg(employee.Salary)
from
    Employee employee
where
    employee.Age >= 25 and employee.Age <=30
"
                    )
                    .UniqueResult<double>();

                Console.WriteLine(salary);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static void LinqTest(ISessionFactory sessionFactory)
        {
            Console.WriteLine("Running Linq sample");
            using (ISession session = sessionFactory.OpenSession())
            {
                var query = from employee in session.Query<Employee>()
                            where employee.Age >= 25 && employee.Age <= 30
                            select employee.Salary;

                var salary = query.Average();

                Console.WriteLine(salary);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static void ICriteriaTest(ISessionFactory sessionFactory)
        {
            Console.WriteLine("Running ICriteria sample");
            using (ISession session = sessionFactory.OpenSession())
            {
                var salary = session.CreateCriteria(typeof(Employee))
                    .Add(Restrictions.Between("Age", 25, 30))
                    .SetProjection(Projections.Avg("Salary"))
                    .UniqueResult<double>();

                Console.WriteLine(salary);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        static void QueryOverTest(ISessionFactory sessionFactory)
        {
            Console.WriteLine("Running QueryOver sample");
            using (ISession session = sessionFactory.OpenSession())
            {
                var salary = session.QueryOver<Employee>()
                    .WhereRestrictionOn(e => e.Age).IsBetween(25).And(30)
                    .Select(e1 => e1.SelectAvg(e2 => e2.Salary))
                    .List<double>()
                    .Single();

                Console.WriteLine(salary);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}

