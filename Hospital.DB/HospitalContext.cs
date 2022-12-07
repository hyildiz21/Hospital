using Hospital.DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB
{
    //Miras alma: Inheritence
    //Daha önce yazılmış kural seti varsa o kurallara göre sınıfı yazabiliyoruz

    public partial class HospitalContext : DbContext
    {
        //list yapısı gibi tablolarımızı contexte tanıttık
        public virtual DbSet<User> Users { get; set; } = null;
        public virtual DbSet<Doctor> Doctors { get; set; } = null;
        public virtual DbSet<UserType> UserTypes { get; set; } = null;
        public virtual DbSet<Patient> Patients { get; set; } = null;
        public virtual DbSet<Polyclinic> Polyclinics { get; set; } = null;
        public virtual DbSet<Appointment> Appointments { get; set; } = null;
        public virtual DbSet<Insurance> Insurances { get; set; } = null;
        public virtual DbSet<DoctorTitle> DoctorTitles { get; set; } = null;
        public virtual DbSet<DoctorRoom> DoctorRooms { get; set; } = null;
        public virtual DbSet<Room> Rooms { get; set; } = null;
        public virtual DbSet<Analyz> Analyzes { get; set; } = null;
        public virtual DbSet<Medicine> Medicines { get; set; } = null;
        
        
        
        
        //başka yerde kullanılamayan protected
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppConfiguration appConfiguration = new AppConfiguration();
            //konfigure olmadıysa
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(appConfiguration.ConnectionString);
            }
        }

        //Model yapıyı getirirken ilişkiler
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
