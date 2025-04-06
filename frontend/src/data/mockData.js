export const healthcareGroups = [
  { id: "hg-1", name: "Physician" },
  { id: "hg-2", name: "Pharmacy" },
  { id: "hg-3", name: "Dentistry" },
  { id: "hg-4", name: "Physical Therapy" },
  { id: "hg-5", name: "Nursing" },
];

export const employerTypesByGroup = {
  "hg-1": [
    { id: "et-1-1", name: "Hospital" },
    { id: "et-1-2", name: "Private Practice" },
    { id: "et-1-3", name: "Clinic" },
  ],
  "hg-2": [
    { id: "et-2-1", name: "Retail Pharmacy" },
    { id: "et-2-2", name: "Hospital Pharmacy" },
    { id: "et-2-3", name: "Pharmaceutical Company" },
  ],
  "hg-3": [
    { id: "et-3-1", name: "Dental Clinic" },
    { id: "et-3-2", name: "Dental Hospital" },
    { id: "et-3-3", name: "Private Practice" },
  ],
  // Add more employer types for other groups as needed
};

export const levelTypesByGroup = {
  "hg-1": [
    { id: "l-1-1", name: "Attending" },
    { id: "l-1-2", name: "Fellow" },
    { id: "l-1-3", name: "Chief" },
  ],
  "hg-2": [
    { id: "l-2-1", name: "Staff Pharmacist" },
    { id: "l-2-2", name: "Clinical Pharmacist" },
    { id: "l-2-3", name: "Pharmacy Manager" },
  ],
  "hg-3": [
    { id: "l-3-1", name: "Associate Dentist" },
    { id: "l-3-2", name: "Senior Dentist" },
    { id: "l-3-3", name: "Dental Director" },
  ],
  // Add more levels for other groups as needed
};

export const mockData = [
  // Physician Data (hg-1)
  {
    id: "job-1-1",
    groupId: "hg-1",
    employerTypeId: "et-1-1",
    levelId: "l-1-1",
    salary: 280000,
    workLifeBalanceScore: 6,
  },
  {
    id: "job-1-2",
    groupId: "hg-1",
    employerTypeId: "et-1-1",
    levelId: "l-1-2",
    salary: 180000,
    workLifeBalanceScore: 7,
  },
  {
    id: "job-1-3",
    groupId: "hg-1",
    employerTypeId: "et-1-1",
    levelId: "l-1-3",
    salary: 400000,
    workLifeBalanceScore: 5,
  },
  {
    id: "job-1-4",
    groupId: "hg-1",
    employerTypeId: "et-1-2",
    levelId: "l-1-1",
    salary: 320000,
    workLifeBalanceScore: 8,
  },
  {
    id: "job-1-5",
    groupId: "hg-1",
    employerTypeId: "et-1-2",
    levelId: "l-1-3",
    salary: 450000,
    workLifeBalanceScore: 7,
  },

  // Pharmacy Data (hg-2)
  {
    id: "job-2-1",
    groupId: "hg-2",
    employerTypeId: "et-2-1",
    levelId: "l-2-1",
    salary: 120000,
    workLifeBalanceScore: 8,
  },
  {
    id: "job-2-2",
    groupId: "hg-2",
    employerTypeId: "et-2-1",
    levelId: "l-2-3",
    salary: 140000,
    workLifeBalanceScore: 7,
  },
  {
    id: "job-2-3",
    groupId: "hg-2",
    employerTypeId: "et-2-2",
    levelId: "l-2-2",
    salary: 135000,
    workLifeBalanceScore: 6,
  },
  {
    id: "job-2-4",
    groupId: "hg-2",
    employerTypeId: "et-2-3",
    levelId: "l-2-3",
    salary: 160000,
    workLifeBalanceScore: 8,
  },

  // Dentistry Data (hg-3)
  {
    id: "job-3-1",
    groupId: "hg-3",
    employerTypeId: "et-3-1",
    levelId: "l-3-1",
    salary: 150000,
    workLifeBalanceScore: 8,
  },
  {
    id: "job-3-2",
    groupId: "hg-3",
    employerTypeId: "et-3-1",
    levelId: "l-3-2",
    salary: 200000,
    workLifeBalanceScore: 7,
  },
  {
    id: "job-3-3",
    groupId: "hg-3",
    employerTypeId: "et-3-2",
    levelId: "l-3-3",
    salary: 300000,
    workLifeBalanceScore: 6,
  },
  {
    id: "job-3-4",
    groupId: "hg-3",
    employerTypeId: "et-3-3",
    levelId: "l-3-2",
    salary: 250000,
    workLifeBalanceScore: 8,
  },
];

export const jobPostings = [
  {
    id: "post-1",
    groupId: "hg-1",
    title: "Senior Attending Physician",
    employer: "Central Hospital",
    location: "New York, NY",
    estimatedSalary: "300,000 - 400,000",
    type: "Full-time",
    employerTypeId: "et-1-1",
  },
  {
    id: "post-2",
    groupId: "hg-2",
    title: "Clinical Pharmacist Lead",
    employer: "Memorial Healthcare",
    location: "Boston, MA",
    estimatedSalary: "130,000 - 150,000",
    type: "Full-time",
    employerTypeId: "et-2-2",
  },
  {
    id: "post-3",
    groupId: "hg-3",
    title: "Senior Dentist",
    employer: "Bright Smile Dental",
    location: "San Francisco, CA",
    estimatedSalary: "200,000 - 250,000",
    type: "Full-time",
    employerTypeId: "et-3-1",
  },
  {
    id: "post-4",
    groupId: "hg-1",
    title: "Chief of Medicine",
    employer: "County General Hospital",
    location: "Chicago, IL",
    estimatedSalary: "380,000 - 450,000",
    type: "Full-time",
    employerTypeId: "et-1-1",
  },
  {
    id: "post-5",
    groupId: "hg-2",
    title: "Pharmacy Manager",
    employer: "CVS Health",
    location: "Los Angeles, CA",
    estimatedSalary: "140,000 - 160,000",
    type: "Full-time",
    employerTypeId: "et-2-1",
  },
];
