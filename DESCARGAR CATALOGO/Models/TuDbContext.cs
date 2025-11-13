using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DESCARGAR_CATALOGO.Models
{
    public partial class TuDbContext : DbContext
    {
        public TuDbContext()
        {
        }

        public TuDbContext(DbContextOptions<TuDbContext> options)
            : base(options)
        {
        }

        // --- Definiciones de tus tablas (DbSet) ---
        // Asumo que tienes los modelos de datos Oitm, Oitb, Itm1 y Oitw creados.

        public virtual DbSet<Itm1> Itm1s { get; set; }

        public virtual DbSet<Oitb> Oitbs { get; set; }

        public virtual DbSet<Oitm> Oitms { get; set; }

        public virtual DbSet<Oitw> Oitws { get; set; } // La tabla de Stock

        // --- Fin de Definiciones de tablas ---


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // El código de esta sección (OnModelCreating) es muy largo y depende 
            // de cómo Entity Framework mapeó las claves primarias y las relaciones 
            // en tu base de datos. Como este código se autogenera, 
            // ASUMO que no lo borraste cuando moviste el código. 
            // Si lo borraste, tendrás que ejecutar Scaffold-DbContext de nuevo.

            // --- DEBE HABER MÁS CÓDIGO AQUÍ GENERADO POR EL COMANDO SCAFFOLD ---

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}