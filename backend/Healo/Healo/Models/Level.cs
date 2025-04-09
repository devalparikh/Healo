namespace Healo.Models;

[Flags]
public enum Level
{
    None = 0,

    // Physician Levels
    Attending = 1 << 0,
    Fellow = 1 << 1,
    Chief = 1 << 2,

    // Pharmacy Levels
    StaffPharmacist = 1 << 3,
    ClinicalPharmacist = 1 << 4,
    PharmacyManager = 1 << 5,

    // Dentistry Levels
    AssociateDentist = 1 << 6,
    SeniorDentist = 1 << 7,
    DentalDirector = 1 << 8,

    // Group masks
    PhysicianLevels = Attending | Fellow | Chief,
    PharmacyLevels = StaffPharmacist | ClinicalPharmacist | PharmacyManager,
    DentistryLevels = AssociateDentist | SeniorDentist | DentalDirector,
    All = PhysicianLevels | PharmacyLevels | DentistryLevels
}