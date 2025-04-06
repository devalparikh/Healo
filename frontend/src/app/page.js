"use client";

import { useState } from "react";
import { mockData } from "../data/mockData";
import SalaryCard from "../components/SalaryCard";
import WorkLifeBalanceBar from "../components/WorkLifeBalanceBar";
import Sidebar from "../components/Sidebar";
import JobPostings from "../components/JobPostings";

export default function App() {
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

  // Get unique employer types and levels
  const employerTypes = [...new Set(mockData.map((item) => item.employerType))];
  const levels = [...new Set(mockData.map((item) => item.level))];

  // Calculate average work-life balance score for each employer type
  const getAverageScore = (employerType) => {
    const scores = mockData
      .filter((item) => item.employerType === employerType)
      .map((item) => item.workLifeBalanceScore);
    return scores.reduce((a, b) => a + b, 0) / scores.length;
  };

  // Get data for a specific employer type and level
  const getData = (employerType, level) => {
    return mockData.find(
      (item) => item.employerType === employerType && item.level === level
    );
  };

  // Get average salary for a specific employer type and level
  const getAverageSalary = (employerType, level) => {
    const salaries = mockData
      .filter(
        (item) => item.employerType === employerType && item.level === level
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
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {employerTypes.map((employerType) => (
            <div key={employerType} className="bg-white p-6 rounded-lg shadow">
              <h2 className="text-2xl font-bold mb-4 text-indigo-800 pb-2 border-b border-indigo-200">
                {employerType}
              </h2>
              <WorkLifeBalanceBar score={getAverageScore(employerType)} />
              <div className="space-y-4">
                {levels.map((level) => {
                  const avgSalary = getAverageSalary(employerType, level);
                  return (
                    avgSalary > 0 && (
                      <SalaryCard
                        key={level}
                        level={level}
                        salary={avgSalary}
                      />
                    )
                  );
                })}
              </div>
            </div>
          ))}
        </div>

        <JobPostings />
      </main>
    </div>
  );
}
