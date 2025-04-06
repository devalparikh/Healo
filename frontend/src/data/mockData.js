export const employerTypes = [
  { id: "et-1", name: "Hospital" },
  { id: "et-2", name: "Private Practice" },
  { id: "et-3", name: "Clinic" },
];

export const levelTypes = [
  { id: "l-1", name: "Attending" },
  { id: "l-2", name: "Fellow" },
  { id: "l-3", name: "Chief" },
];

export const mockData = [
  {
    id: "job-1",
    employerTypeId: "et-1",
    levelId: "l-1",
    salary: 80000,
    workLifeBalanceScore: 7,
  },
  {
    id: "job-2",
    employerTypeId: "et-1",
    levelId: "l-2",
    salary: 120000,
    workLifeBalanceScore: 6,
  },
  {
    id: "job-3",
    employerTypeId: "et-1",
    levelId: "l-3",
    salary: 200000,
    workLifeBalanceScore: 5,
  },
  {
    id: "job-4",
    employerTypeId: "et-2",
    levelId: "l-1",
    salary: 90000,
    workLifeBalanceScore: 8,
  },
  {
    id: "job-5",
    employerTypeId: "et-2",
    levelId: "l-2",
    salary: 150000,
    workLifeBalanceScore: 8,
  },
  {
    id: "job-6",
    employerTypeId: "et-2",
    levelId: "l-3",
    salary: 250000,
    workLifeBalanceScore: 7,
  },
  {
    id: "job-7",
    employerTypeId: "et-3",
    levelId: "l-1",
    salary: 75000,
    workLifeBalanceScore: 9,
  },
  {
    id: "job-8",
    employerTypeId: "et-3",
    levelId: "l-2",
    salary: 110000,
    workLifeBalanceScore: 8,
  },
  {
    id: "job-9",
    employerTypeId: "et-3",
    levelId: "l-3",
    salary: 180000,
    workLifeBalanceScore: 8,
  },
  {
    id: "job-9",
    employerTypeId: "et-1",
    levelId: "l-3",
    salary: 880000,
    workLifeBalanceScore: 2,
  },
];

export const jobPostings = [
  {
    id: "post-1",
    title: "Senior Attending Physician",
    employer: "Central Hospital",
    location: "New York, NY",
    estimatedSalary: "300,000 - 400,000",
    type: "Full-time",
    employerTypeId: "et-1",
  },
  {
    id: "post-2",
    title: "Fellow Position",
    employer: "Private Practice Group",
    location: "Los Angeles, CA",
    estimatedSalary: "200,000 - 250,000",
    type: "Full-time",
    employerTypeId: "et-2",
  },
  {
    id: "post-3",
    title: "Medical Director",
    employer: "Community Clinic",
    location: "Chicago, IL",
    estimatedSalary: "180,000 - 220,000",
    type: "Full-time",
    employerTypeId: "et-3",
  },
];
