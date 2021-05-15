using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCFNMembers.Domain.Repositories.Concrete {
    public class MyDbContext : DbContext {
        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> myOptions) : base(myOptions) { }

       

    }
}
