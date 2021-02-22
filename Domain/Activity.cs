using System;
namespace Domain
{
    //Activities is name of table name in our database. Properties means columns in table of database.

    // These properties 7 of them are going to be coulumns in our database table.
    public class Activity    
    {
        public Guid Id { get; set; }  // Primary Key in our table.

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string  Category { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }
    }
}