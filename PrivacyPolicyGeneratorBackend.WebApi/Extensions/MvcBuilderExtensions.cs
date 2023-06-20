
using PrivacyPolicyGeneratorBackend.Application.Configurations;
using FluentValidation.AspNetCore;

namespace PrivacyPolicyGeneratorBackend.WebApi.Extensions
{
    public static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }
    }
}
