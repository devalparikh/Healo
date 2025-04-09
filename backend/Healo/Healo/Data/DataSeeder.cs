using Healo.Models;
using Healo.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Healo.Data;

public class DataSeeder
{
    private readonly EntryDbContext _context;
    private readonly ILogger<DataSeeder> _logger;

    // Healthcare Group ID mappings
    private static readonly Dictionary<string, Guid> GroupIds = new()
    {
        { "hg-1", Guid.Parse("7f5f1222-b52b-4f1d-9819-54c04e8e80c1") }, // Physician
        { "hg-2", Guid.Parse("9c5f1333-c63c-5f2e-0920-65c15e9e91d2") }, // Pharmacy
        { "hg-3", Guid.Parse("8d6f1444-d74d-6f3f-1a21-76d26f0fa2e3") }, // Dentistry
    };

    // Employer Type ID mappings
    private static readonly Dictionary<string, Guid> EmployerTypeIds = new()
    {
        // Physician employer types
        { "et-1-1", Guid.Parse("1a1f2222-b52b-4f1d-9819-54c04e8e80c1") }, // Hospital
        { "et-1-2", Guid.Parse("1b2f2222-b52b-4f1d-9819-54c04e8e80c2") }, // Private Practice
        { "et-1-3", Guid.Parse("1c3f2222-b52b-4f1d-9819-54c04e8e80c3") }, // Clinic

        // Pharmacy employer types
        { "et-2-1", Guid.Parse("2a1f3333-c63c-5f2e-0920-65c15e9e91d1") }, // Retail Pharmacy
        { "et-2-2", Guid.Parse("2b2f3333-c63c-5f2e-0920-65c15e9e91d2") }, // Hospital Pharmacy
        { "et-2-3", Guid.Parse("2c3f3333-c63c-5f2e-0920-65c15e9e91d3") }, // Pharmaceutical Company

        // Dentistry employer types
        { "et-3-1", Guid.Parse("3a1f4444-d74d-6f3f-1a21-76d26f0fa2e1") }, // Dental Clinic
        { "et-3-2", Guid.Parse("3b2f4444-d74d-6f3f-1a21-76d26f0fa2e2") }, // Dental Hospital
        { "et-3-3", Guid.Parse("3c3f4444-d74d-6f3f-1a21-76d26f0fa2e3") }, // Private Practice
    };

    // Level ID mappings
    private static readonly Dictionary<string, Guid> LevelIds = new()
    {
        // Physician levels
        { "l-1-1", Guid.Parse("4a1f2222-b52b-4f1d-9819-54c04e8e80c1") }, // Attending
        { "l-1-2", Guid.Parse("4b2f2222-b52b-4f1d-9819-54c04e8e80c2") }, // Fellow
        { "l-1-3", Guid.Parse("4c3f2222-b52b-4f1d-9819-54c04e8e80c3") }, // Chief

        // Pharmacy levels
        { "l-2-1", Guid.Parse("5a1f3333-c63c-5f2e-0920-65c15e9e91d1") }, // Staff Pharmacist
        { "l-2-2", Guid.Parse("5b2f3333-c63c-5f2e-0920-65c15e9e91d2") }, // Clinical Pharmacist
        { "l-2-3", Guid.Parse("5c3f3333-c63c-5f2e-0920-65c15e9e91d3") }, // Pharmacy Manager

        // Dentistry levels
        { "l-3-1", Guid.Parse("6a1f4444-d74d-6f3f-1a21-76d26f0fa2e1") }, // Associate Dentist
        { "l-3-2", Guid.Parse("6b2f4444-d74d-6f3f-1a21-76d26f0fa2e2") }, // Senior Dentist
        { "l-3-3", Guid.Parse("6c3f4444-d74d-6f3f-1a21-76d26f0fa2e3") }, // Dental Director
    };

    // Entry ID mappings
    private static readonly Dictionary<string, Guid> EntryIds = new()
    {
        // Physician entries
        { "job-1-1", Guid.Parse("8a1f2222-b52b-4f1d-9819-54c04e8e80c1") },
        { "job-1-2", Guid.Parse("8b2f2222-b52b-4f1d-9819-54c04e8e80c2") },
        { "job-1-3", Guid.Parse("8c3f2222-b52b-4f1d-9819-54c04e8e80c3") },
        { "job-1-4", Guid.Parse("8d1f2222-b52b-4f1d-9819-54c04e8e80c4") },
        { "job-1-5", Guid.Parse("8e1f2222-b52b-4f1d-9819-54c04e8e80c5") },
        
        // Pharmacy entries
        { "job-2-1", Guid.Parse("9a1f3333-c63c-5f2e-0920-65c15e9e91d1") },
        { "job-2-2", Guid.Parse("9b2f3333-c63c-5f2e-0920-65c15e9e91d2") },
        { "job-2-3", Guid.Parse("9c3f3333-c63c-5f2e-0920-65c15e9e91d3") },
        { "job-2-4", Guid.Parse("9d1f3333-c63c-5f2e-0920-65c15e9e91d4") },
        
        // Dentistry entries
        { "job-3-1", Guid.Parse("0a1f4444-d74d-6f3f-1a21-76d26f0fa2e1") },
        { "job-3-2", Guid.Parse("0b2f4444-d74d-6f3f-1a21-76d26f0fa2e2") },
        { "job-3-3", Guid.Parse("0c3f4444-d74d-6f3f-1a21-76d26f0fa2e3") },
        { "job-3-4", Guid.Parse("0d1f4444-d74d-6f3f-1a21-76d26f0fa2e4") }
    };

    public DataSeeder(
        EntryDbContext context,
        ILogger<DataSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Checking if database needs seeding...");

            if (await _context.Entries.AnyAsync())
            {
                _logger.LogInformation("Database already contains data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Beginning database seeding...");

            var entries = new List<EntryEntity>
            {
                // Physician entries (job-1-*)
                new EntryEntity
                {
                    Id = EntryIds["job-1-1"],
                    JobGroup = JobGroup.Physician,
                    JobGroupId = GroupIds["hg-1"],
                    EmployerType = EmployerType.Hospital,
                    EmployerTypeId = EmployerTypeIds["et-1-1"],
                    Level = Level.Attending,
                    LevelId = LevelIds["l-1-1"],
                    Salary = 280000,
                    WorkLifeBalanceScore = 6
                },
                new EntryEntity
                {
                    Id = EntryIds["job-1-2"],
                    JobGroup = JobGroup.Physician,
                    JobGroupId = GroupIds["hg-1"],
                    EmployerType = EmployerType.Hospital,
                    EmployerTypeId = EmployerTypeIds["et-1-1"],
                    Level = Level.Fellow,
                    LevelId = LevelIds["l-1-2"],
                    Salary = 180000,
                    WorkLifeBalanceScore = 7
                },
                new EntryEntity
                {
                    Id = EntryIds["job-1-3"],
                    JobGroup = JobGroup.Physician,
                    JobGroupId = GroupIds["hg-1"],
                    EmployerType = EmployerType.Hospital,
                    EmployerTypeId = EmployerTypeIds["et-1-1"],
                    Level = Level.Chief,
                    LevelId = LevelIds["l-1-3"],
                    Salary = 400000,
                    WorkLifeBalanceScore = 5
                },
                new EntryEntity
                {
                    Id = EntryIds["job-1-4"],
                    JobGroup = JobGroup.Physician,
                    JobGroupId = GroupIds["hg-1"],
                    EmployerType = EmployerType.PhysicianPrivatePractice,
                    EmployerTypeId = EmployerTypeIds["et-1-2"],
                    Level = Level.Attending,
                    LevelId = LevelIds["l-1-1"],
                    Salary = 320000,
                    WorkLifeBalanceScore = 8
                },
                new EntryEntity
                {
                    Id = EntryIds["job-1-5"],
                    JobGroup = JobGroup.Physician,
                    JobGroupId = GroupIds["hg-1"],
                    EmployerType = EmployerType.PhysicianPrivatePractice,
                    EmployerTypeId = EmployerTypeIds["et-1-2"],
                    Level = Level.Chief,
                    LevelId = LevelIds["l-1-3"],
                    Salary = 450000,
                    WorkLifeBalanceScore = 7
                },

                // Pharmacy entries (job-2-*)
                new EntryEntity
                {
                    Id = EntryIds["job-2-1"],
                    JobGroup = JobGroup.Pharmacy,
                    JobGroupId = GroupIds["hg-2"],
                    EmployerType = EmployerType.RetailPharmacy,
                    EmployerTypeId = EmployerTypeIds["et-2-1"],
                    Level = Level.StaffPharmacist,
                    LevelId = LevelIds["l-2-1"],
                    Salary = 120000,
                    WorkLifeBalanceScore = 8
                },
                new EntryEntity
                {
                    Id = EntryIds["job-2-2"],
                    JobGroup = JobGroup.Pharmacy,
                    JobGroupId = GroupIds["hg-2"],
                    EmployerType = EmployerType.RetailPharmacy,
                    EmployerTypeId = EmployerTypeIds["et-2-1"],
                    Level = Level.PharmacyManager,
                    LevelId = LevelIds["l-2-3"],
                    Salary = 140000,
                    WorkLifeBalanceScore = 7
                },
                new EntryEntity
                {
                    Id = EntryIds["job-2-3"],
                    JobGroup = JobGroup.Pharmacy,
                    JobGroupId = GroupIds["hg-2"],
                    EmployerType = EmployerType.HospitalPharmacy,
                    EmployerTypeId = EmployerTypeIds["et-2-2"],
                    Level = Level.ClinicalPharmacist,
                    LevelId = LevelIds["l-2-2"],
                    Salary = 135000,
                    WorkLifeBalanceScore = 6
                },
                new EntryEntity
                {
                    Id = EntryIds["job-2-4"],
                    JobGroup = JobGroup.Pharmacy,
                    JobGroupId = GroupIds["hg-2"],
                    EmployerType = EmployerType.PharmaceuticalCompany,
                    EmployerTypeId = EmployerTypeIds["et-2-3"],
                    Level = Level.PharmacyManager,
                    LevelId = LevelIds["l-2-3"],
                    Salary = 160000,
                    WorkLifeBalanceScore = 8
                },

                // Dentistry entries (job-3-*)
                new EntryEntity
                {
                    Id = EntryIds["job-3-1"],
                    JobGroup = JobGroup.Dentistry,
                    JobGroupId = GroupIds["hg-3"],
                    EmployerType = EmployerType.DentalClinic,
                    EmployerTypeId = EmployerTypeIds["et-3-1"],
                    Level = Level.AssociateDentist,
                    LevelId = LevelIds["l-3-1"],
                    Salary = 150000,
                    WorkLifeBalanceScore = 8
                },
                new EntryEntity
                {
                    Id = EntryIds["job-3-2"],
                    JobGroup = JobGroup.Dentistry,
                    JobGroupId = GroupIds["hg-3"],
                    EmployerType = EmployerType.DentalClinic,
                    EmployerTypeId = EmployerTypeIds["et-3-1"],
                    Level = Level.SeniorDentist,
                    LevelId = LevelIds["l-3-2"],
                    Salary = 200000,
                    WorkLifeBalanceScore = 7
                },
                new EntryEntity
                {
                    Id = EntryIds["job-3-3"],
                    JobGroup = JobGroup.Dentistry,
                    JobGroupId = GroupIds["hg-3"],
                    EmployerType = EmployerType.DentalHospital,
                    EmployerTypeId = EmployerTypeIds["et-3-2"],
                    Level = Level.DentalDirector,
                    LevelId = LevelIds["l-3-3"],
                    Salary = 300000,
                    WorkLifeBalanceScore = 6
                },
                new EntryEntity
                {
                    Id = EntryIds["job-3-4"],
                    JobGroup = JobGroup.Dentistry,
                    JobGroupId = GroupIds["hg-3"],
                    EmployerType = EmployerType.DentistryPrivatePractice,
                    EmployerTypeId = EmployerTypeIds["et-3-3"],
                    Level = Level.SeniorDentist,
                    LevelId = LevelIds["l-3-2"],
                    Salary = 250000,
                    WorkLifeBalanceScore = 8
                }
            };

            _logger.LogInformation($"Seeding {entries.Count} entries to database...");
            await _context.Entries.AddRangeAsync(entries);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}
