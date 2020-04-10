using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence

{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.Activities.Any())
            {
                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Pact Activity 1",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        Category = "drinks",
                        City = "London",
                        Venue = "Pub",
                    },
                    new Activity
                    {
                        Title = "Pact Activity 2",
                        Date = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 months ago",
                        Category = "culture",
                        City = "Paris",
                        Venue = "Louvre",
                    },
                    new Activity
                    {
                        Title = "Future Activity 1",
                        Date = DateTime.Now.AddMonths(1),
                        Description = "Activity 1 month in the future",
                        Category = "culture",
                        City = "London",
                        Venue = "Natual History Museum",
                    },
                    new Activity
                    {
                        Title = "Future Activity 2",
                        Date = DateTime.Now.AddMonths(2),
                        Description = "Activity 2 months in the future",
                        Category = "drinks",
                        City = "Tennessee ",
                        Venue = "Culture Club Bar",
                    }

                };

                context.Activities.AddRange(activities);
                context.SaveChanges();
            }
        }
    }
}