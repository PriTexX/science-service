using Science.Config;
using Serilog;
using Serilog.Events;

namespace Science.Logging;

public static class Logging
{
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(
            (_, loggerCfg) =>
            {
                loggerCfg
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Override(
                        "Microsoft.EntityFrameworkCore.Database",
                        LogEventLevel.Warning
                    )
                    .MinimumLevel.Override(
                        "Microsoft.AspNetCore.Hosting.Diagnostics",
                        LogEventLevel.Warning
                    )
                    .MinimumLevel.Override(
                        "Microsoft.AspNetCore.StaticFiles",
                        LogEventLevel.Warning
                    )
                    .Enrich.WithProperty("Environment", Cfg.Environment)
                    .Enrich.FromLogContext();

                if (Cfg.IsProduction())
                {
                    loggerCfg.MinimumLevel.Override(
                        "Microsoft.EntityFrameworkCore.Query",
                        LogEventLevel.Error
                    );
                }
            }
        );
    }
}
