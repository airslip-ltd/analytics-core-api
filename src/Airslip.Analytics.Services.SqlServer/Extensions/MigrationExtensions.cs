using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Airslip.Analytics.Services.SqlServer.Extensions;

public static class MigrationExtensions
{
    public static void AddSqlFiles(this MigrationBuilder migrationBuilder, string migrationName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<string> sqlFiles = assembly.GetManifestResourceNames().
            Where(file => file.Contains(migrationName) && file.EndsWith(".sql"));
        foreach (string sqlFile in sqlFiles)
        {
            using Stream stream = assembly.GetManifestResourceStream(sqlFile);
            if (stream == null) continue;
            using StreamReader reader = new(stream);
            string sqlScript = reader.ReadToEnd();
            migrationBuilder.Sql($"EXEC(N'{sqlScript.Replace("'", "''")}')");
        }
    }
}