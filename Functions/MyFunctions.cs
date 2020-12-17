﻿using apiTckets.Entities;
using apiTckets.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Functions
{
    public class MyFunctions
    {
        private readonly ApiTicketDbContext _context;
        public MyFunctions(ApiTicketDbContext context)
        {
            _context = context;
        }
        public string uploadImage(string dossier, string file)
        {
            return "";
        }

        public static string GenererCodeEvent(DbSet<Event> events)
        {
            string code = "EVENT." + DateTime.Now.Year + "." + RandomString(6);
            var verifCode = from e in events
                            where e.CodeEvent == code
                            select new { e.Id, e.Nom };
            if (verifCode.Count() > 0)
            {
                GenererCodeEvent(events);
            }
            return code;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
