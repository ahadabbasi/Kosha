using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Kosha.CustomerManager.Web.Infrastructure.Configurations.Options;

public abstract class ConfigureOptions<TOptions>(IConfiguration configuration) : IConfigureOptions<TOptions>
    where TOptions : class
{
    protected abstract string SectionName { get; }

    public virtual void Configure(TOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}