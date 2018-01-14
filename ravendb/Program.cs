using System;

namespace ravendb
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Multimapowanie";
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                while (true)
                {
                    Console.Write("Szukana fraza: ");
                    var searchTerms = Console.ReadLine();
                    if(searchTerms == "exit")
                        Environment.Exit(0);

                    foreach (var result in PeopleSearch.Search(session, searchTerms))
                    {
                        Console.WriteLine($"{result.SourceId}\t{result.Type}\t{result.Name}");
                    }
                }
            }
        }
    }
}


