using System;
using HAXEMJ_HFT_2022231.Models;
using System.Linq;
using System.Collections.Generic;
using HAXEMJ_HFT_2022231.RestClient;
using ConsoleTools;

namespace HAXEMJ_HFT_2022231.Client
{
    class Program
    {
        static RestService rest;

        static void GetAlliPhones()
        {
            try
            {
                List<Phone> phones = rest.Get<Phone>("NonCrud/GetAlliPhones");
                phones.ForEach(p => Console.WriteLine(p.Name));
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }         
        }

        static void GetPreferredColorPhones()
        {
            try
            {
                Console.Write("Enter color: ");
                string color = Console.ReadLine();
                List<Manufacturer> manus = rest.Get<Manufacturer>($"NonCrud/GetPreferredColorPhones/{color}");

                manus.ForEach(m => Console.WriteLine($"{m.Name}: {m.ColorPhoneCount}"));
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e); }       
        }

        static void GetPhoneCountByUser()
        {
            try
            {
                List<User> users = rest.Get<User>("NonCrud/GetPhoneCountByUser");
                users.ForEach(u => Console.WriteLine($"{u.Name}: {u.PhoneCount}"));
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }      
        }

        static void GetScreenTimeBd()
        {
            try
            {
                List<User> users = rest.Get<User>("NonCrud/GetScreenTimeBd");
                users.ForEach(u => Console.WriteLine($"{u.Name}: {u.TotalAvgScrTime}"));
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }       
        }

        static void PhonesBySpecificLocation()
        {
            try
            {
                Console.Write("Enter location: ");
                string location = Console.ReadLine();
                List<Phone> phones = rest.Get<Phone>($"NonCrud/PhonesBySpecificLocation/{location}");

                phones.ForEach(p => Console.WriteLine($"{p.PhoneId}: {p.Name}"));
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }      
        }

        static void Create(string entity)
        {
            try
            {
                Console.Write($"Enter {entity.ToLower()}'s name: ");
                string name = Console.ReadLine();

                switch (entity)
                {
                    case "Phone":
                        rest.Post(new Phone() { Name = name }, entity.ToLower());
                        break;
                    case "User":
                        rest.Post(new User() { Name = name }, entity.ToLower());
                        break;
                    case "Manufacturer":
                        rest.Post(new Manufacturer() { Name = name }, entity.ToLower());
                        break;
                }

                Console.WriteLine($"'{name}' created!");
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }
        }
        static void List(string entity)
        {
            try
            {
                switch (entity)
                {
                    case "Phone":
                        List<Phone> phones = rest.Get<Phone>(entity.ToLower());
                        foreach (var item in phones)
                        {
                            Console.WriteLine(item.PhoneId + ": " + item.Name);
                        }
                        break;
                    case "User":
                        List<User> users = rest.Get<User>(entity.ToLower());
                        foreach (var item in users)
                        {
                            Console.WriteLine(item.UserID + ": " + item.Name);
                        }
                        break;
                    case "Manufacturer":
                        List<Manufacturer> manufacturers = rest.Get<Manufacturer>(entity.ToLower());
                        foreach (var item in manufacturers)
                        {
                            Console.WriteLine(item.ManufacturerId + ": " + item.Name);
                        }
                        break;
                }
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }      
        }

        static void Read(string entity)
        {
            try
            {
                Console.Write($"Enter {entity.ToLower()}'s ID to get: ");
                string id = Console.ReadLine();

                switch (entity)
                {
                    case "Phone":
                        Console.WriteLine((rest.GetSingle<Phone>($"phone/{id}")).Name);                     
                        break;
                    case "User":
                        Console.WriteLine((rest.GetSingle<User>($"user/{id}")).Name);
                        break;
                    case "Manufacturer":
                        Console.WriteLine((rest.GetSingle<Manufacturer>($"manufacturer/{id}")).Name);
                        break;
                }

                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }
        }
        static void Update(string entity)
        {
            try
            {
                Console.Write($"Enter {entity.ToLower()}'s ID to update: ");
                string id = Console.ReadLine();

                switch (entity)
                {
                    case "Phone":
                        Phone p = rest.Get<Phone>(id, entity.ToLower());
                        Console.Write($"Enter {entity.ToLower()}'s new name (was {p.Name}): ");
                        string name = Console.ReadLine();
                        p.Name = name;
                        rest.Put(p, entity.ToLower());
                        Console.WriteLine($"'{name}' updated!");
                        break;
                    case "User":
                        User u = rest.Get<User>(id, entity.ToLower());
                        Console.Write($"Enter {entity.ToLower()}'s new name (was {u.Name}): ");
                        string uName = Console.ReadLine();
                        u.Name = uName;
                        rest.Put(u, entity.ToLower());
                        Console.WriteLine($"'{uName}' updated!");
                        break;
                    case "Manufacturer":
                        Manufacturer m = rest.Get<Manufacturer>(id, entity.ToLower());
                        Console.Write($"Enter {entity.ToLower()}'s new name (was {m.Name}): ");
                        string mName = Console.ReadLine();
                        m.Name = mName;
                        rest.Put(m, entity.ToLower());
                        Console.WriteLine($"'{mName}' updated!");
                        break;
                }

                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }
        }
        static void Delete(string entity)
        {
            try
            {
                Console.Write($"Enter {entity.ToLower()}'s ID to delete: ");

                string strId = Console.ReadLine();
                rest.Delete(strId, entity.ToLower());

                Console.WriteLine($"Entity '{strId}' deleted!");
                Console.ReadLine();
            }
            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadLine(); }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:29971/", "phone");

            var phoneSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Phone"))
                .Add("Read", () => Read("Phone"))
                .Add("Create", () => Create("Phone"))
                .Add("Delete", () => Delete("Phone"))
                .Add("Update", () => Update("Phone"))
                .Add("Exit", ConsoleMenu.Close);

            var userSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("User"))
                .Add("Read", () => Read("User"))
                .Add("Create", () => Create("User"))
                .Add("Delete", () => Delete("User"))
                .Add("Update", () => Update("User"))
                .Add("Exit", ConsoleMenu.Close);

            var manufacturerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Manufacturer"))
                .Add("Read", () => Read("Manufacturer"))
                .Add("Create", () => Create("Manufacturer"))
                .Add("Delete", () => Delete("Manufacturer"))
                .Add("Update", () => Update("Manufacturer"))
                .Add("Exit", ConsoleMenu.Close);

            var nonCrudMenu = new ConsoleMenu(args, level: 1)
                .Add("Get all iPhones", () => GetAlliPhones())
                .Add("Get colored phones", () => GetPreferredColorPhones())
                .Add("Get user phone count", () => GetPhoneCountByUser())
                .Add("Get screentime breakdown", () => GetScreenTimeBd())
                .Add("Get phones by specific location", () => PhonesBySpecificLocation())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Phones", () => phoneSubMenu.Show())
                .Add("Users", () => userSubMenu.Show())
                .Add("Manufacturers", () => manufacturerSubMenu.Show())
                .Add("Non Crud Menu", () => nonCrudMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        
        }
    }
}

