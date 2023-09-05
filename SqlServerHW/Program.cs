using Microsoft.Extensions.Configuration;






static string GetConnectionString(string connectionString = "Default")
{
    string output = "";

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(connectionString);

    var config = builder.Build();

    output = config.GetConnectionString(connectionString);

    return output;
}