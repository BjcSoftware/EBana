using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SQLite.CodeFirst;
using EBana.Domain.Models;
using EBana.Security.Hash;
using System;

namespace EBana.EfDataAccess
{
	public class EBanaContext : DbContext
    {
        private readonly IHash hash;

        public EBanaContext(IHash hash) : base("name=EBanaDB") 
        {
            if (hash == null)
                throw new ArgumentNullException("hash");

            this.hash = hash;
        }

        // définition des tables utilisables
        public DbSet<Article> Article { get; set; }
        public DbSet<TypeArticle> TypeArticle { get; set; }
        public DbSet<TypeEpi> TypeEpi { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new MyDbContextInitializer(hash, modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);

            // les noms de tables sont au singulier
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // relations table/classe
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Banalise>().ToTable("Banalise");
            modelBuilder.Entity<EPI>().ToTable("EPI");
            modelBuilder.Entity<SEL>().ToTable("SEL");
            modelBuilder.Entity<Credentials>().ToTable("Credentials");
            modelBuilder.Entity<TypeEpi>().ToTable("TypeEpi");
        }
    }


    /// <summary>
    /// Initialisateur de context basé sur le SqliteCreateDatabaseIfNotExists
    /// Permet de fournir des données initiales à la base
    /// </summary>
    public class MyDbContextInitializer : SqliteCreateDatabaseIfNotExists<EBanaContext>
    {
        private readonly IHash hash;

        public MyDbContextInitializer(IHash hash, DbModelBuilder modelBuilder)
            : base(modelBuilder)
        {
            if (hash == null)
                throw new ArgumentNullException("hash");

            this.hash = hash;
        }

        protected override void Seed(EBanaContext context)
        {
        	// définition des types d'articles disponibles
            context.TypeArticle.Add(new TypeArticle { Libelle = "Banalisé" });
            context.TypeArticle.Add(new TypeArticle { Libelle = "SEL" });
            
            // définition du mot de passe par défaut
            string defaultPassword = "admin";
            string hashedPassword = hash.Hash(defaultPassword);
            
            context.Credentials.Add(new Credentials { Password = hashedPassword } );
        }
    }
}
