using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TesteVeranine.Models;

public partial class HospitalContext : DbContext
{
    public HospitalContext()
    {
    }

    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FichaMed> FichaMeds { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Hospital;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FichaMed>(entity =>
        {
            entity.HasKey(e => e.Idfic).HasName("PK__FichaMed__926EB5C2426A8F8B");

            entity.ToTable("FichaMed");

            entity.Property(e => e.Idfic).HasColumnName("IDFIC");
            entity.Property(e => e.Idmed).HasColumnName("IDMED");
            entity.Property(e => e.Idpac).HasColumnName("IDPAC");

            entity.HasOne(d => d.IdmedNavigation).WithMany(p => p.FichaMeds)
                .HasForeignKey(d => d.Idmed)
                .HasConstraintName("FK__FichaMed__IDMED__3F466844");

            entity.HasOne(d => d.IdpacNavigation).WithMany(p => p.FichaMeds)
                .HasForeignKey(d => d.Idpac)
                .HasConstraintName("FK__FichaMed__IDPAC__403A8C7D");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Idmed).HasName("PK__Medico__941979DC34D44240");

            entity.ToTable("Medico");

            entity.Property(e => e.Idmed).HasColumnName("IDMED");
            entity.Property(e => e.Cpf).HasColumnName("CPF");
            entity.Property(e => e.Crm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CRM");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOME");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Idpac).HasName("PK__Paciente__98FEABE854F8578B");

            entity.ToTable("Paciente");

            entity.Property(e => e.Idpac).HasColumnName("IDPAC");
            entity.Property(e => e.Cpf).HasColumnName("CPF");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOME");
            entity.Property(e => e.Telefone).HasColumnName("TELEFONE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
