using System;
using System.Collections.Generic;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Entities.Context;

public partial class InterfellContext : DataContext
{
    public InterfellContext()
    {
    }

    public InterfellContext(DbContextOptions<InterfellContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ayuda> Ayudas { get; set; }

    public virtual DbSet<AyudasComuna> AyudasComunas { get; set; }

    public virtual DbSet<AyudasRegion> AyudasRegions { get; set; }

    public virtual DbSet<Comuna> Comunas { get; set; }

    public virtual DbSet<Logger> Loggers { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<PersonaAyuda> PersonaAyudas { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql("", ServerVersion.Parse("8.2.0-mysql"));
    } 
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Ayuda>(entity =>
        {
            entity.HasKey(e => e.IdAyuda).HasName("PRIMARY");

            entity.Property(e => e.Moneda).HasMaxLength(5);
            entity.Property(e => e.NomAyuda).HasMaxLength(50);
        });

        modelBuilder.Entity<AyudasComuna>(entity =>
        {
            entity.HasKey(e => new { e.ComunaId, e.AyudaId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Ayudas_Comuna");

            entity.HasIndex(e => e.AyudaId, "Ayudas_has_Comuna_FKIndex1");

            entity.HasIndex(e => e.ComunaId, "Ayudas_has_Comuna_FKIndex2");
        });

        modelBuilder.Entity<AyudasRegion>(entity =>
        {
            entity.HasKey(e => new { e.RegionId, e.AyudaId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Ayudas_Region");

            entity.HasIndex(e => e.AyudaId, "Ayudas_has_Region_FKIndex1");

            entity.HasIndex(e => e.RegionId, "Ayudas_has_Region_FKIndex2");
        });

        modelBuilder.Entity<Comuna>(entity =>
        {
            entity.HasKey(e => e.IdComuna).HasName("PRIMARY");

            entity.ToTable("Comuna");

            entity.HasIndex(e => e.CodRegion, "Comuna_FKIndex1");

            entity.Property(e => e.NomComuna).HasMaxLength(255);
        });

        modelBuilder.Entity<Logger>(entity =>
        {
            entity.HasKey(e => e.IdLogger).HasName("PRIMARY");

            entity.ToTable("Logger");

            entity.HasIndex(e => e.UserId, "Logger_FKIndex1");

            entity.Property(e => e.Accion).HasMaxLength(50);
            entity.Property(e => e.Entidad).HasMaxLength(255);
            entity.Property(e => e.Hora).HasColumnType("time");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PRIMARY");

            entity.Property(e => e.Moneda).HasMaxLength(5);
            entity.Property(e => e.NomPais).HasMaxLength(255);
            entity.Property(e => e.SiglaPais).HasMaxLength(3);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Identificacion).HasName("PRIMARY");

            entity.ToTable("Persona");

            entity.Property(e => e.PrimerApellido).HasMaxLength(50);
            entity.Property(e => e.PrimerNombre).HasMaxLength(50);
            entity.Property(e => e.SegundoApellido).HasMaxLength(50);
            entity.Property(e => e.SegundoNombre).HasMaxLength(50);
        });

        modelBuilder.Entity<PersonaAyuda>(entity =>
        {
            entity.HasKey(e => new { e.IdentificacionPersona, e.AyudaId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Persona_Ayudas");

            entity.HasIndex(e => new { e.Año, e.IdentificacionPersona, e.AyudaId }, "PersonaAyudas_UK").IsUnique();

            entity.HasIndex(e => e.IdentificacionPersona, "Persona_has_Ayudas_FKIndex1");

            entity.HasIndex(e => e.AyudaId, "Persona_has_Ayudas_FKIndex2");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.IdRegion).HasName("PRIMARY");

            entity.ToTable("Region");

            entity.HasIndex(e => e.PaisId, "Region_FKIndex1");

            entity.Property(e => e.NomRegion).HasMaxLength(255);
            entity.Property(e => e.SiglaRegion).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.Property(e => e.NomRol).HasMaxLength(50);
            entity.Property(e => e.SiglaRol).HasMaxLength(5);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.HasIndex(e => e.RolId, "Users_FKIndex1");

            entity.Property(e => e.FullName).HasMaxLength(4000);
            entity.Property(e => e.UserName).HasMaxLength(512);
            entity.Property(e => e.UserPsw).HasMaxLength(4000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
