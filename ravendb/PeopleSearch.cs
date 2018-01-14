using System.Collections.Generic;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;

namespace ravendb
{
    public class PeopleSearch :
        AbstractMultiMapIndexCreationTask<PeopleSearch.Result>
    {
        public class Result
        {
            public string SourceId { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }

        public PeopleSearch()
        {
            AddMap<Company>(companies =>
                from company in companies
                select new Result
                {
                    SourceId = company.Id,
                    Name = company.Contact.Name,
                    Type = "Company's contact"
                }
            );

            AddMap<Supplier>(suppliers =>
                from supplier in suppliers
                select new Result
                {
                    SourceId = supplier.Id,
                    Name = supplier.Contact.Name,
                    Type = "Supplier's contact"
                }
            );

            AddMap<Employee>(employees =>
                from employee in employees
                select new Result
                {
                    SourceId = employee.Id,
                    Name = $"{employee.FirstName} {employee.LastName}",
                    Type = "Employee"
                }
            );

            Index(entry => entry.Name, FieldIndexing.Analyzed);

            Store(entry => entry.SourceId, FieldStorage.Yes);
            Store(entry => entry.Name, FieldStorage.Yes);
            Store(entry => entry.Type, FieldStorage.Yes);
        }

        public static IEnumerable<Result> Search(
            IDocumentSession session,
            string searchTerms
        )
        {
            var results = session.Query<Result, PeopleSearch>()
                .Search(
                    r => r.Name,
                    searchTerms)
                .ProjectFromIndexFieldsInto<Result>()
                .ToList();

            return results;
        }
    }
}
