using apiTckets.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities
{
    public class ApiTicketDbContext : DbContext
    {
        public DbSet<Promoteur> Promoteurs { get; set; }
        public DbSet<TypeEvent> TypeEvents { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTicket> UsersTickets { get; set; }

        public ApiTicketDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
