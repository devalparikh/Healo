namespace Healo.Models;

[Flags]
public enum EmployerType
{
    None = 0,

    // Physician (1-7)
    Hospital = 1 << 0,
    PhysicianPrivatePractice = 1 << 1,
    Clinic = 1 << 2,

    // Pharmacy (8-63)
    RetailPharmacy = 1 << 3,
    HospitalPharmacy = 1 << 4,
    PharmaceuticalCompany = 1 << 5,

    // Dentistry (64-511)
    DentalClinic = 1 << 6,
    DentalHospital = 1 << 7,
    DentistryPrivatePractice = 1 << 8,

    // Group masks
    Physician = Hospital | PhysicianPrivatePractice | Clinic,
    Pharmacy = RetailPharmacy | HospitalPharmacy | PharmaceuticalCompany,
    Dentistry = DentalClinic | DentalHospital | DentistryPrivatePractice,
    All = Physician | Pharmacy | Dentistry
}