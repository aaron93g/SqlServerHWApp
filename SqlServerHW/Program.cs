using Microsoft.Extensions.Configuration;
using DataAccessLibrary;


SqlCrud sql = new SqlCrud(GetConnectionString());

sql.GetEmployerInformation(2);


//GetPeoplesNames(sql);


static void GetPeoplesNames(SqlCrud sql)
{
    var rows = sql.GetAll();

    foreach ( var row in rows)
    {
        Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
    }
}


static string GetConnectionString(string connectionString = "Default")
{
    string output = "";

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("AppSettings.json");

    var config = builder.Build();

    output = config.GetConnectionString(connectionString);

    return output;
}