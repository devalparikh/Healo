"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import {
  mockData,
  healthcareGroups,
  employerTypesByGroup,
  levelTypesByGroup,
} from "../data/mockData";
import SalaryCard from "../components/SalaryCard";
import WorkLifeBalanceBar from "../components/WorkLifeBalanceBar";
import Sidebar from "../components/Sidebar";
import JobPostings from "../components/JobPostings";

export default function App() {
  const router = useRouter();
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);
  const [selectedGroup, setSelectedGroup] = useState(healthcareGroups[0].id);

  // Get current employer types based on selected group
  const currentEmployerTypes = employerTypesByGroup[selectedGroup] || [];
  const currentLevelTypes = levelTypesByGroup[selectedGroup] || [];

  // Calculate average work-life balance score for each employer type
  const getAverageScore = (employerTypeId) => {
    const scores = mockData
      .filter(
        (item) =>
          item.groupId === selectedGroup &&
          item.employerTypeId === employerTypeId
      )
      .map((item) => item.workLifeBalanceScore);
    return scores.length > 0
      ? scores.reduce((a, b) => a + b, 0) / scores.length
      : 0;
  };

  // Get average salary for a specific employer type and level
  const getAverageSalary = (employerTypeId, levelId) => {
    const salaries = mockData
      .filter(
        (item) =>
          item.groupId === selectedGroup &&
          item.employerTypeId === employerTypeId &&
          item.levelId === levelId
      )
      .map((item) => item.salary);
    return salaries.length > 0
      ? salaries.reduce((a, b) => a + b, 0) / salaries.length
      : 0;
  };

  return (
    <div className="min-h-screen bg-gray-100">
      <header className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto px-4 py-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center">
            <h1 className="text-3xl font-extrabold bg-clip-text text-transparent bg-gradient-to-r from-indigo-600 to-indigo-400">
              Healo
            </h1>
            <button
              onClick={() => setIsSidebarOpen(true)}
              className="p-2 rounded-md hover:bg-gray-100"
            >
              <svg
                className="w-6 h-6"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M4 6h16M4 12h16M4 18h16"
                />
              </svg>
            </button>
          </div>
        </div>
      </header>

      <Sidebar isOpen={isSidebarOpen} onClose={() => setIsSidebarOpen(false)} />

      <main className="max-w-7xl mx-auto px-4 py-8 sm:px-6 lg:px-8">
        <div className="mb-8 border-b border-gray-200">
          <nav className="-mb-px flex space-x-8" aria-label="Healthcare Groups">
            {healthcareGroups.map((group) => (
              <button
                key={group.id}
                onClick={() => setSelectedGroup(group.id)}
                className={`
                  whitespace-nowrap py-4 px-1 border-b-2 font-medium text-sm
                  ${
                    selectedGroup === group.id
                      ? "border-indigo-500 text-indigo-600"
                      : "border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300"
                  }
                `}
              >
                {group.name}
              </button>
            ))}
          </nav>
        </div>

        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {currentEmployerTypes.map((employerType) => (
            <div
              key={employerType.id}
              className="bg-white p-6 rounded-lg shadow"
            >
              <h2 className="text-2xl font-bold mb-4 text-indigo-800 pb-2 border-b border-indigo-200">
                {employerType.name}
              </h2>
              <WorkLifeBalanceBar score={getAverageScore(employerType.id)} />
              <div className="space-y-4">
                {currentLevelTypes.map((level) => {
                  const avgSalary = getAverageSalary(employerType.id, level.id);
                  return (
                    avgSalary > 0 && (
                      <SalaryCard
                        key={level.id}
                        level={level.name}
                        salary={avgSalary}
                      />
                    )
                  );
                })}
              </div>
              <button
                onClick={() =>
                  router.push(`/employer/${selectedGroup}/${employerType.id}`)
                }
                className="mt-4 w-full bg-indigo-600 text-white py-2 px-4 rounded hover:bg-indigo-700"
              >
                View All Data
              </button>
            </div>
          ))}
        </div>

        <JobPostings groupId={selectedGroup} />
      </main>
    </div>
  );
}
