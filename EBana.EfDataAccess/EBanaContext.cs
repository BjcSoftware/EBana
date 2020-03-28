using EBana.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBana.EfDataAccess
{
	public class EBanaContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseSqlite(@"Data Source=eBana.db;");
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new BanaliseEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new EpiEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new SelEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new CredentialsEntityTypeConfiguration());
		}
	}

	class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> articleConfiguration)
		{
			articleConfiguration
				.ToTable("articles")
				.HasKey(o => o.Id);
			articleConfiguration.OwnsOne(o => o.Reference);
		}
	}

	class BanaliseEntityTypeConfiguration : IEntityTypeConfiguration<Banalise>
	{
		public void Configure(EntityTypeBuilder<Banalise> banaliseConfiguration)
		{
			banaliseConfiguration.HasBaseType<Article>();
		}
	}

	class EpiEntityTypeConfiguration : IEntityTypeConfiguration<Epi>
	{
		public void Configure(EntityTypeBuilder<Epi> EpiConfiguration)
		{
			EpiConfiguration
				.HasBaseType<Banalise>()
				.OwnsOne(o => o.TypeEpi);
		}
	}

	class SelEntityTypeConfiguration : IEntityTypeConfiguration<Sel>
	{
		public void Configure(EntityTypeBuilder<Sel> SelConfiguration)
		{
			SelConfiguration.HasBaseType<Article>();
		}
	}

	class CredentialsEntityTypeConfiguration : IEntityTypeConfiguration<Credentials>
	{
		public void Configure(EntityTypeBuilder<Credentials> articleConfiguration)
		{
			articleConfiguration
				.ToTable("credentials")
				.HasKey(o => o.Id);
		}
	}
}
