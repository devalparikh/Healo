export interface EmployerType {
  id: string;
  name: string;
}

export interface LevelType {
  id: string;
  name: string;
}

export interface JobData {
  id: string;
  employerTypeId: string;
  levelId: string;
  salary: number;
  workLifeBalanceScore: number;
}
