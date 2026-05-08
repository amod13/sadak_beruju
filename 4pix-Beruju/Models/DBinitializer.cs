using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using _4pix_Beruju.Models;
using _4pix_Beruju.Models.Setups;

namespace _4pix_Beruju.Models
{
    public class DBinitializer
    {

    }

    public class BerujuEntities : DbContext
    {


        //public DbSet<ProgramSetup> ProgramSetup { get; set; }
        //public DbSet<SubProgramMaster> SubProgramMaster { get; set; }

        //public DbSet<OfficeDetails> OfficeDetails { get; set; }
        //public DbSet<AspNetCustomUserRoles> AspNetCustomUserRoles { get; set; }
        public DbSet<ApplicationDetail> ApplicationDetail { get; set; }

        public DbSet<SetBerujuTargetValue> SetBerujuTargetValue { get; set; }
        public DbSet<SMSStatus> SMSStatus { get; set; }
        public DbSet<CurrentOfficeChiefDetails> CurrentOfficeChiefDetails { get; set; }

        public DbSet<FiscalYearRecord> FiscalYearRecord { get; set; }

        public DbSet<MergeOfficeMaster> MergeOfficeMaster { get; set; }
        public DbSet<MergeOfficeDetails> MergeOfficeDetails { get; set; }

        public DbSet<BerujuFilesByDafa> BerujuFilesByDafa { get; set; }
        public DbSet<BerujuFilesByDafaDocument> BerujuFilesByDafaDocument { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<BerujuFilesByDafa>()
          .HasMany(x => x.Documents)
          .WithRequired(x => x.BerujuFilesByDafa)
          .HasForeignKey(x => x.BerujuFilesByDafaId)
          .WillCascadeOnDelete(true);
        }

    }
}