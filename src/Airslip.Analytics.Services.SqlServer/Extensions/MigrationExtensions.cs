using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Services.SqlServer.Data;
using Airslip.Common.Repository.Types.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Airslip.Analytics.Services.SqlServer.Extensions;

public static class MigrationExtensions
{
    public static void AddSqlFiles(this MigrationBuilder migrationBuilder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<string> sqlFiles = assembly.GetManifestResourceNames().
            Where(file => file.Contains("Database") && file.EndsWith(".sql"));
        foreach (string sqlFile in sqlFiles)
        {
            using Stream stream = assembly.GetManifestResourceStream(sqlFile);
            if (stream == null) continue;
            using StreamReader reader = new(stream);
            string sqlScript = reader.ReadToEnd();
            migrationBuilder.Sql($"EXEC(N'{sqlScript.Replace("'", "''")}')");
        }
    }
    
    public static ModelBuilder AddTableWithDefaults<TEntity>(this ModelBuilder modelBuilder, string? tableName = null)
        where TEntity : class, IEntityWithId
    {
        tableName ??= typeof(TEntity).Name.Pluralize();

        modelBuilder
            .Entity<TEntity>()
            .ToTable(tableName)
            .HasKey(b => b.Id)
            .HasName($"PK_{tableName}_Id");

        modelBuilder.Entity<TEntity>().Property(o => o.Id).HasColumnType(Constants.ID_DATA_TYPE);
        if (typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
            modelBuilder.Entity<TEntity>().Property("AuditInformationId").HasColumnType(Constants.ID_DATA_TYPE);
        if (typeof(IEntityWithOwnership).IsAssignableFrom(typeof(TEntity)))
        {
            modelBuilder.Entity<TEntity>().Property("EntityId").HasColumnType(Constants.ID_DATA_TYPE);
            modelBuilder.Entity<TEntity>().Property("UserId").HasColumnType(Constants.ID_DATA_TYPE);
        }
        if (typeof(IReportableWithOwnership).IsAssignableFrom(typeof(TEntity)))
        {
            modelBuilder.Entity<TEntity>().Property("EntityId").HasColumnType(Constants.ID_DATA_TYPE);
        }
        if (typeof(IReportableWithCurrency).IsAssignableFrom(typeof(TEntity)))
        {
            modelBuilder.Entity<TEntity>().Property("Currency").HasColumnType("varchar (5)");
        }
        if (typeof(IReportableWithIntegration).IsAssignableFrom(typeof(TEntity)))
        {
            modelBuilder.Entity<TEntity>().Property("IntegrationId").HasColumnType(Constants.ID_DATA_TYPE);
        }
        return modelBuilder;
    }
    public static ModelBuilder AddDatabaseGeneratedId<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : class, IEntityWithId
    {
        modelBuilder
            .Entity<TEntity>()
            .Property(b => b.Id)
            .HasDefaultValueSql("dbo.getId()");

        return modelBuilder;
    }
}