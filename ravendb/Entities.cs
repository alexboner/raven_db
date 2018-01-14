namespace ravendb
{
    public class Employee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ContactClass
    {
        public string Name { get; set; }
    }

    public class Company
    {
        public string Id { get; set; }
        public ContactClass Contact { get; set; }
    }

    public class Supplier
    {
        public string Id { get; set; }
        public ContactClass Contact { get; set; }
    }
}
