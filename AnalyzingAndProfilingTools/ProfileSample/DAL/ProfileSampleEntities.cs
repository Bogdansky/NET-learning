using ProfileSample.Models;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ProfileSample.DAL
{
    public partial class ProfileSampleEntities : DbContext
    {
        public ProfileSampleEntities()
            : base("name=ProfileSampleEntities")
        {
        }

        public virtual DbSet<ImgSource> ImgSources { get; set; }
    }
}