using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FantasyManager.Core.Models;

namespace FantasyManager.Infrastructure.Context
{
    public class TeamManagerContext : DbContext
    {
        #region Constructors

        private readonly IDbContextSettings _settings;

        private readonly ILoggerFactory _loggerFactory;

        public TeamManagerContext( DbContextOptions<TeamManagerContext> options )
            : base( options ) { }

        public TeamManagerContext( DbContextOptions<TeamManagerContext> options, IOptions<DbContextSettings> settings, ILoggerFactory loggerFactory )
            : base( options )
        {
            _loggerFactory = loggerFactory;
            _settings = settings?.Value;
        }

        #endregion

        #region Initialization Methods

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            // let base take care of initial items 
            base.OnConfiguring( optionsBuilder );

            // if not previously configured 
            if ( !optionsBuilder.IsConfigured )
            {
                // enable in memory database 
                if ( !string.IsNullOrEmpty( _settings?.ConnectionString ) )
                {
                    optionsBuilder.UseInMemoryDatabase( databaseName: "FantasyManager" );
                }
            }

            // enable logging 
            optionsBuilder.UseLoggerFactory( _loggerFactory );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            // let base take care of any initial items 
            base.OnModelCreating( modelBuilder );

            // apply any custom conventions 
            // TODO

            // configure each entity 
            ConfigureEntities( modelBuilder );

            // seed data
            SeedData( modelBuilder );
        }

        #region Configure Entities 

        protected virtual void ConfigureEntities( ModelBuilder modelBuilder )
        {
            #region Conventions (Notes) 

            // By convention, a property named Id or <type name>Id will be configured as the key of an entity.
            // If you want to exclude a property from the model use .Ignore

            #region Player

            modelBuilder.Entity<Player>( options =>
            {
                // use identity
                options.HasKey( k => k.Id );

                // set property values 
                options.Property( p => p.Name ).HasMaxLength( 50 ).IsRequired();
                options.Property( p => p.Status ).HasMaxLength( 50 ).IsRequired();

                // map table relationships
                options
                    .HasOne( o => o.Team )
                    .WithMany( o => o.Roster )
                    .HasForeignKey( o => o.TeamId );


            } );

            #endregion

            #region Statistics

            modelBuilder.Entity<Statistics>( options =>
            {
                // use identity
                options.HasKey( k => k.Id );

                // map table relationships
                //options
                //    .HasOne( o => o.Player )
                //    .WithOne( o => o.Statistics );
            } );

            #endregion

            #region Team

            modelBuilder.Entity<Team>( options =>
            {
                // use identity
                options.HasKey( k => k.Id );

                // set property values 
                options.Property( p => p.Name ).HasMaxLength( 50 ).IsRequired();
            } );

            #endregion

            #region User

            modelBuilder.Entity<User>( options =>
            {
                // use identity
                options.HasKey( k => k.Id );

                // set property values
                options.Property( p => p.Username ).HasMaxLength( 50 ).IsRequired();
                options.Property( p => p.FirstName ).HasMaxLength( 50 ).IsRequired();
                options.Property( p => p.LastName ).HasMaxLength( 50 ).IsRequired();
                options.Property( p => p.Email ).HasMaxLength( 100 ).IsRequired();
            } );

            #endregion

            #endregion
        }

        #endregion

        #region Seed Data
        protected void SeedData( ModelBuilder modelBuilder )
        {
            #region Teams

            modelBuilder.Entity<Team>().HasData(
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
            );

            #endregion
        }

        #endregion

        #endregion
    }
}
