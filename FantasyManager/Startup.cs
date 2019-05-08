using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;

using FantasyManager.Infrastructure.Context;
using FantasyManager.Infrastructure.Interfaces;
using FantasyManager.Infrastructure.Managers;
using FantasyManager.Infrastructure.Repository;
using System;
using FantasyManager.Core.Models;
using FantasyManager.Core.Extensions;


namespace FantasyManager.Web
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_2 );

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles( configuration =>
             {
                 configuration.RootPath = "ClientApp/build";
             } );

            #region Dependency Injection

            services.AddDbContext<FantasyManagerContext>( options =>
            {
                options.UseInMemoryDatabase( databaseName: "FantasyManager" );
                options.UseQueryTrackingBehavior( QueryTrackingBehavior.NoTracking );
            } );

            // repository  
            services.AddScoped<IDbRepository, DbRepository>();

            // managers 
            services.AddTransient<IPlayerManager, PlayerManager>();
            services.AddTransient<ITeamManager, TeamManager>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseStaticFiles();

            app.UseMvc( routes =>
             {
                 routes.MapRoute(
                     name: "default",
                     template: "{controller}/{action=Index}/{id?}" );
             } );

            app.UseSpa( spa =>
             {
                 spa.Options.SourcePath = "ClientApp";

                 if ( env.IsDevelopment() )
                 {
                     spa.UseReactDevelopmentServer( npmScript: "start" );
                 }
             } );

            var context = serviceProvider.GetService<FantasyManagerContext>();

            SeedData( context );
        }

        #region Seed Data
        protected void SeedData( FantasyManagerContext context )
        {
            #region Teams

            /*
            var teams = new List<Team>() {
                new Team { Id = 1, Name = "Arizona Cardinals" },
                new Team { Id = 2, Name = "Atlanta Falcons" },
                new Team { Id = 3, Name = "Baltimore Ravens" },
                new Team { Id = 4, Name = "Buffalo Bills" },
                new Team { Id = 5, Name = "Carolina Panthers" },
                new Team { Id = 6, Name = "Chicago Bears" },
                new Team { Id = 7, Name = "Cincinnati Bengals" },
                new Team { Id = 8, Name = "Cleveland Browns" },
                new Team { Id = 9, Name = "Dallas Cowboyws" },
                new Team { Id = 10, Name = "Denver Broncos" },
                new Team { Id = 11, Name = "Detroit Lions" },
                new Team { Id = 12, Name = "Green Bay Packers" },
                new Team { Id = 13, Name = "Houston Texans" },
                new Team { Id = 14, Name = "Indiana Colts" },
                new Team { Id = 15, Name = "Jacksonville Jaguars" },
                new Team { Id = 16, Name = "Kansas City Chiefs" },
                new Team { Id = 17, Name = "Los Angeles Chargers" },
                new Team { Id = 18, Name = "Los Angeles Rams" },
                new Team { Id = 19, Name = "Miami Dolphins" },
                new Team { Id = 20, Name = "Minnesota Vikings" },
                new Team { Id = 21, Name = "New England Patriots" },
                new Team { Id = 22, Name = "New Orleans Saints" },
                new Team { Id = 23, Name = "New York Giants" },
                new Team { Id = 24, Name = "New York Jets" },
                new Team { Id = 25, Name = "Oakland Raiders" },
                new Team { Id = 26, Name = "Philadelphia Eagles" },
                new Team { Id = 27, Name = "Pittsburgh Steeler" },
                new Team { Id = 28, Name = "San Francisco 49ers" },
                new Team { Id = 29, Name = "Seattle Seahawks" },
                new Team { Id = 30, Name = "Tampa Bay Buccaneers" },
                new Team { Id = 31, Name = "Tennessee Titans" },
                new Team { Id = 32, Name = "Washington Redskins" }
            };
            */

            var teams = new List<Team>()
            {
                new Team { Id = 1, Name = "Atlanta Falcons" },
                new Team { Id = 2, Name = "Philadelphia Eagles" },
                new Team { Id = 3, Name = "Seattle Seahawks" },
                new Team { Id = 4, Name = "Tennessee Titans" }
            };

            foreach ( var team in teams )
            {
                context.AddAsync( team );
            }

            string filePath = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar.png" );
            byte[] avatar = File.ReadAllBytes( filePath );

            var players = new List<Player>
            {
                new Player { Id = 1, Name = "Julio Jones", TeamId = 1, DateOfBirth = new DateTime( 1989, 2, 8 ), Status = "Active", Weight = 220, Height = 75, Position = PlayerPosition.WideReceiver.GetDescription(), StatsId = 1, Avatar = avatar },
                new Player { Id = 2, Name = "Matt Ryan", TeamId = 1, DateOfBirth = new DateTime(1985, 5, 17 ), Status = "Active", Weight = 217, Height = 76, Position = PlayerPosition.Quarterback.GetDescription(), StatsId = 2, Avatar = avatar },
                new Player { Id = 3, Name = "Matt Bosher", TeamId = 1, DateOfBirth = new DateTime(1987, 10, 18), Status = "Active", Weight = 208, Height = 72, Position = PlayerPosition.Kicker.GetDescription(), StatsId = 3, Avatar = avatar  }
            };

            foreach ( var player in players )
            {
                context.AddAsync( player );
            }

            var stats = new List<Statistics>
            {
                // Receiving Stats Only
                new Statistics{ Id = 1, Season = 2018, GamesPlayed = 16, Receptions = 113, ReceivingTargets = 170, ReceivingYards = 1677, YardsPerReception = 14.8, ReceivingTouchdowns = 8, LongReception = 58, ReceivingFirstDowns = 80, FumblesRecovered = 2, ReceivingFumblesLost = 2 },
                new Statistics{ Id = 2, Season = 2018, GamesPlayed = 16, Receptions = 1, ReceivingTargets = 1, ReceivingYards = 5, YardsPerReception = 5, ReceivingTouchdowns = 1, LongReception = 5, ReceivingFirstDowns = 1, FumblesRecovered = 0, ReceivingFumblesLost = 0 },

                // Defensive Stats Only
                new Statistics{ Id = 3, Season = 2018, GamesPlayed = 16, TotalTackles = 3, SoloTackles = 2, AssistTackles = 1, Sacks = 0, ForcedFumbles = 0, FumblesRecovered = 0, FumblesRecoveredYards = 0, Interceptions = 0, InterceptionYards = 0, AverageInterceptionYards = 0, InterceptionTouchdowns = 0, LongInterception = 0, PassesDefended = 0, Stuffs = 0, YardsLost = 0, KicksBlocked = 0 }
            };

            foreach (var stat in stats)
            {
                context.AddAsync( stat );
            }

            context.SaveChangesAsync();

            #endregion
        }

        #endregion
    }
}
