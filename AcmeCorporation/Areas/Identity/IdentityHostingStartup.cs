﻿using System;
using AcmeCorporation.Areas.Identity.Data;
using AcmeCorporation.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AcmeCorporation.Areas.Identity.IdentityHostingStartup))]
namespace AcmeCorporation.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AcmeIdentityContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("AcmeIdentityContext")));
            });
        }
    }
}