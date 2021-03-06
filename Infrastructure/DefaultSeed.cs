﻿using Core.Entities;
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
                    ImageUri = "../assets/Logos/IefIdo_CMYK_V2-Hersteld_Rondo-Mexico.png",
                    Coffees = new List<Coffee>
                    {
                        new Coffee {
                            
                            CreateDate = now,
                            MutationDate = now,
                            CoffeeType = CoffeeType.Arabica,
                            Country = "Mexico",
                            Rating = 4
                        },
                        new Coffee {

                            CreateDate = now,
                            MutationDate = now,
                            CoffeeType = CoffeeType.Arabica,
                            Country = "Guatemala",
                            Rating = 3.1m,
                            loggedRecords = 312
                        },
                    }
                },
                new Brand
                {
                    CreateDate = now,
                    MutationDate = now,
                    ImageUri = "../assets/Logos/IefIdo_CMYK_V2-Hersteld_Rondo-Mexico.png",
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
                    ImageUri = "../assets/Logos/IefIdo_CMYK_V2-Hersteld_Rondo-Mexico.png",
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
                    ImageUri = "../assets/Logos/IefIdo_CMYK_V2-Hersteld_Rondo-Mexico.png",
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
            context.SaveChanges();

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

            var record = new Record
            {
                CoffeeId = 1,
                DoseIn = 17.5m,
                DoseOut = 32.5m,
                Time = 30,
                Rating = 3.5m,
            };

            context.Add(record);
            context.SaveChanges();

            var recordFlavors = new RecordFlavor[]
            {
                new RecordFlavor{FlavorId = 1, RecordId = 1},
                new RecordFlavor{FlavorId = 2, RecordId = 1},
                new RecordFlavor{FlavorId = 3, RecordId = 1},
            };

            context.AddRange(recordFlavors);
            context.SaveChanges();

        }
    }
}
