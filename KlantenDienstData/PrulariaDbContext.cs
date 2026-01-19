using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData
{
    public class PrulariaDbContext : DbContext
    {
        public PrulariaDbContext(DbContextOptions<PrulariaDbContext> options) : base(options)
        {
        }

        public PrulariaDbContext()
        {
        }
    }
}
