using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;

namespace Infrastructure
{
    public static class DefaultSeed
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Coffees.Any()) return;
            var now = DateTime.Now;

            var brands = new Brand[]
            {
                new Brand
                {
                    CreateDate = now,
                    MutationDate = now,
                    Name = "IEF & IDO",
                    Coffees = new List<Coffee>
                    {
                        new Coffee {
                            
                            CreateDate = now,
                            MutationDate = now,
                            CoffeeType = CoffeeType.Arabica,
                            Country = "Mexico"
                        },
                        new Coffee {

                            CreateDate = now,
                            MutationDate = now,
                            CoffeeType = CoffeeType.Arabica,
                            Country = "Guatemala"
                        },

                    }
                },
                new Brand
                {
                    CreateDate = now,
                    MutationDate = now,
                    Name = "Boon",
                    Coffees = new List<Coffee>
                    {
                        new Coffee {

                            CreateDate = now,
                            MutationDate = now,
                            CoffeeType = CoffeeType.Arabica,
                            Country = "Kenya"
                        }
                    }
                },
                new Brand
                {
                    CreateDate = now,
                    MutationDate = now,
                    Name = "Giraffe",
                    Coffees = new List<Coffee>
                    {
                        new Coffee {

                            CreateDate = now,
                            MutationDate = now,
                            CoffeeType = CoffeeType.Arabica,
                            Country = "Ethiopia"
                        }
                    }
                },
                new Brand
                {
                    CreateDate = now,
                    MutationDate = now,
                    Name = "Douwe Egberts",
                    Coffees = new List<Coffee>
                    {
                        new Coffee {

                            CreateDate = now,
                            MutationDate = now,
                            CoffeeType = CoffeeType.Robusto,
                            Country = "Bagger"
                        }
                    }
                }
            };

            context.Brands.AddRange(brands);

            var flavors = new Flavor[]
            {
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Creamy"
               },
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Watery"
               },
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Full"
               },
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Acid"
               },
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Sweet"
               },
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Bitter"
               },
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Fruity"
               },
               new Flavor
               {
                   CreateDate = now,
                   MutationDate = now,
                   Name = "Spices"
               }
           };
            context.Flavors.AddRange(flavors);

            context.SaveChanges();
        }
    }
}
