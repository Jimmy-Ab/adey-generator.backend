using System;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace PrivacyPolicyGeneratorBackend.Application.Configurations
{
    public class AppConfiguration
    {
        public string Secret { get; set; }
    }
}
