using System;
using System.Linq;
using ContactListApp.Data;
using ContactListApp.Models;

namespace ContactListApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if there are already contacts in the database
            if (context.Contacts.Any())
            {
                return; // Database has been seeded
            }

            string[] firstNames = ["Ole", "Lars", "Hilde", "Ingrid", "Anders", "Mette", "Kari", "Per", "Sofie", "Erik", "Anna", "Torbjørn", "Eva", "Nils", "Gunn", "Arne", "Silje", "Petter", "Sara", "Kristian", "Hanne", "Jon", "Lene", "Jørgen", "Helene", "Knut", "Camilla", "Geir", "Marit"];
            string[] LastNames = ["Olsen", "Larsen", "Haugen", "Bakke", "Eriksen", "Svendsen", "Solberg", "Andersen", "Lund", "Johansen", "Dahl", "Holm", "Moen", "Hansen", "Nielsen", "Pedersen", "Kristiansen", "Jensen", "Karlsen", "Nilsen", "Eide", "Berg", "Gundersen", "Rasmussen", "Hagen", "Haugland", "Strand", "Sandvik", "Myhre"];
            string[] addresses = [
    "Storgata 1, Oslo",
    "Nygata 5, Bergen",
    "Solsiden 10, Trondheim",
    "Karl Johans gate 15, Oslo",
    "Strandgaten 20, Stavanger",
    "Biskop Gunnerus' gate 3, Oslo",
    "Torget 8, Tromsø",
    "Ole Bulls gate 7, Bergen",
    "Akersgata 2, Oslo",
    "Dronningens gate 18, Trondheim",
    "Skippergata 24, Drammen",
    "Kongens gate 14, Stavanger",
    "Storgata 8, Kristiansand",
    "Lille Grensen 7, Oslo",
    "Markens gate 12, Kristiansand",
    "Kirkegata 10, Molde",
    "Grønland 12, Oslo",
    "Frognerveien 30, Oslo",
    "Kongens gate 40, Trondheim",
    "Storgata 15, Tromsø",
    "Bryggen 5, Bergen",
    "Vestre Strandgate 7, Kristiansand",
    "Nedre Slottsgate 4, Oslo",
    "Øvre Slottsgate 2, Oslo",
    "Sjøgata 12, Bodø",
    "Karl Johans gate 30, Oslo",
    "Pilestredet 8, Oslo",
    "Grünerløkka 22, Oslo",
    "Torggata 14, Stavanger",
    "Olav Tryggvasons gate 9, Trondheim",
    "Åsane Senter 18, Bergen",
    "Kirkeveien 15, Oslo",
    "St. Olavs gate 25, Oslo",
    "Birkebeinervegen 25, Lillehammer",
    "Sjølystveien 12, Oslo",
    "Bryggen 10, Stavanger",
    "Biskop Gunnerus' gate 9, Oslo",
    "Drammensveien 20, Oslo",
    "Akersgata 10, Oslo",
    "Munkedamsveien 15, Oslo",
    "Tollbugata 8, Bergen",
    "Karl Johans gate 20, Oslo",
    "Nedre Slottsgate 10, Oslo",
    "Pilestredet 18, Oslo",
    "Grünerløkka 40, Oslo",
    "Strandgaten 15, Trondheim",
    "Storgata 25, Tromsø",
    "Olav Tryggvasons gate 15, Trondheim",
    "Bryggen 20, Bergen"
];

            //50 random contacts
            var random = new Random();
            var contacts = Enumerable.Range(1, 50).Select(i => new Contact
            {
                FirstName = $"{firstNames[random.Next(firstNames.Length)]}",
                LastName = $"{LastNames[random.Next(LastNames.Length)]}",
                Address = $"{addresses[random.Next(addresses.Length)]}",
                PhoneNumber = $"+47 {random.Next(100, 999)} {random.Next(10, 99)} {random.Next(100, 999)}",
                DOB = DateTime.Now.AddYears(-random.Next(20, 50)),
            });

            context.Contacts.AddRange(contacts);
            context.SaveChanges();
        }
    }
}
