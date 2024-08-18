using System.ComponentModel.DataAnnotations;

namespace Science.Config;

file sealed class ExternalCfgValues
{
    public const string SectionName = "App";

    [Required]
    public required string ConnectionString { get; init; }

    [Required]
    public required string RsaPublicKey { get; init; }

    [Required]
    public required string SeqApiKey { get; init; }
}

public static class Cfg
{
    public static void InitCfg(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddOptions<ExternalCfgValues>()
            .BindConfiguration(ExternalCfgValues.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var externalCfgValues = builder
            .Configuration.GetSection(ExternalCfgValues.SectionName)
            .Get<ExternalCfgValues>();

        ArgumentNullException.ThrowIfNull(externalCfgValues);

        Environment = builder.Environment.EnvironmentName;
        ConnectionString = externalCfgValues.ConnectionString;
        RsaPublicKey = externalCfgValues.RsaPublicKey;
        SeqApiKey = externalCfgValues.SeqApiKey;
    }

    public static bool IsProduction() => !IsDevelopment();

    public static bool IsDevelopment() => Environment == "Development";

    public static string Environment { get; private set; } = null!;
    public static string ConnectionString { get; private set; } = null!;
    public static string RsaPublicKey { get; private set; } = null!;
    public static string SeqApiKey { get; private set; } = null!;
}
