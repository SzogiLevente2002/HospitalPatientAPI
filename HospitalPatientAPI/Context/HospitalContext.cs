using Castle.Core.Resource;
using HospitalPatientAPI.Entities;
using Microsoft.EntityFrameworkCore;



public class HospitalContext : DbContext
{
    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
}
