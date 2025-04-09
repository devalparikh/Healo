using System.ComponentModel.DataAnnotations;

namespace Healo.Configuration.Options;

public class DatabasesOptions
{
    public const string SectionName = "Databases";

    [Required]
    public IDictionary<string, DatabaseOptions> Databases { get; set; } = new Dictionary<string, DatabaseOptions>();
}

public class DatabaseOptions
{
    [Required]
    public string DatabaseName { get; set; } = null!;
}