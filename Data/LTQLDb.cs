using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Testthi.Models;

    public class LTQLDb : DbContext
    {
        public LTQLDb (DbContextOptions<LTQLDb> options)
            : base(options)
        {
        }

        public DbSet<Testthi.Models.NhaCungCap> NhaCungCap { get; set; } = default!;

        public DbSet<Testthi.Models.SanPham> SanPham { get; set; } = default!;
    }
