using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SomeUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurais();
            SimpleSamuraiQuery();
            LikeQuery();
            InsertBattle();
            UpdateDisconnected();
            Console.ReadLine();
        }

        private static void UpdateDisconnected()
        {
            using (var context = new SamuaiContext())
            {
                var battle = context.Battles.FirstOrDefault();
                battle.EndDate = new DateTime(1560, 06, 30);
                using (var context2 = new SamuaiContext())
                {
                    context2.Battles.Update(battle);
                    context2.SaveChanges();
                }
            }
        }

        private static void InsertBattle()
        {
            using (var context = new SamuaiContext())
            {
                context.Battles.Add(
                    new Battle
                    {
                        Name = "Battle of Okehazama",
                        StartDate = new DateTime(1560, 5, 1),
                        EndDate = new DateTime(1560, 06, 15)
                    });
                context.SaveChanges();
            }
        }

        private static void LikeQuery()
        {
            using (var context = new SamuaiContext())
            {
                var samurai = context.Samurais
                    .FirstOrDefault(x => EF.Functions.Like(x.Name, "S%"));

                Console.WriteLine(samurai.Name);
            }
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuaiContext())
            {
                var samurais = context.Samurais.ToList();

                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Nathan" };
            var samurai2 = new Samurai { Name = "Sampson" };
            using (var context = new SamuaiContext())
            {
                context.Samurais.AddRange(samurai, samurai2);
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Nathan" };
            using (var context = new SamuaiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }
    }
}