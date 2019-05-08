using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FantasyManager.Core.Models;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;

namespace FantasyManager.Infrastructure.Context
{
    public class FantasyManagerContext : DbContext
    {
        #region Constructors

        private readonly IDbContextSettings _settings;

        private readonly ILoggerFactory _loggerFactory;

        public FantasyManagerContext( DbContextOptions<FantasyManagerContext> options )
            : base( options ) { }

        public FantasyManagerContext( DbContextOptions<FantasyManagerContext> options, IOptions<DbContextSettings> settings, ILoggerFactory loggerFactory )
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
                    optionsBuilder.UseInMemoryDatabase(
                        databaseName: "FantasyManager" );
                    optionsBuilder.UseQueryTrackingBehavior( QueryTrackingBehavior.NoTracking );
                }
            }

            // enable logging 
            optionsBuilder.UseLoggerFactory( _loggerFactory );

            // disable lazy loading 
            // lazy loading can hurt performance, so we will use Eager Loading over lazy loading
            // by making use of .Include and .IncludeMultiple operations that are available 
            optionsBuilder.UseLazyLoadingProxies( false );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            // let base take care of any initial items 
            base.OnModelCreating( modelBuilder );

            // apply any custom conventions 
            // TODO

            // configure each entity 
            ConfigureEntities( modelBuilder );
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
                options.Property( k => k.Id)
                    .HasValueGenerator<InMemoryIntegerValueGenerator<long>>();

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

        #endregion
    }
}
