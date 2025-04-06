"use client";

import { useState } from "react";
import { mockData, employerTypes, levelTypes } from "../../../data/mockData";

export default function EmployerDetail({ params }) {
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;

  const employerType = employerTypes.find((et) => et.id === params.id);
  const employerData = mockData.filter(
    (item) => item.employerTypeId === params.id
  );

  const totalPages = Math.ceil(employerData.length / itemsPerPage);
  const startIndex = (currentPage - 1) * itemsPerPage;
  const paginatedData = employerData.slice(
    startIndex,
    startIndex + itemsPerPage
  );

  const getLevelName = (levelId) => {
    return levelTypes.find((l) => l.id === levelId)?.name || "";
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold mb-6 text-gray-900">
          {employerType?.name} Data
        </h1>

        <div className="bg-white overflow-hidden shadow-lg rounded-lg">
          <table className="min-w-full divide-y divide-gray-200">
            <thead>
              <tr className="bg-gray-100">
                <th className="px-6 py-4 text-left text-sm font-semibold text-gray-900">
                  Level
                </th>
                <th className="px-6 py-4 text-left text-sm font-semibold text-gray-900">
                  Salary
                </th>
                <th className="px-6 py-4 text-left text-sm font-semibold text-gray-900">
                  Work-Life Balance
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-200">
              {paginatedData.map((item) => (
                <tr key={item.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 text-sm text-gray-900 font-medium">
                    {getLevelName(item.levelId)}
                  </td>
                  <td className="px-6 py-4 text-sm text-gray-900">
                    ${item.salary.toLocaleString()}
                  </td>
                  <td className="px-6 py-4 text-sm text-gray-900">
                    {item.workLifeBalanceScore}/10
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        <div className="mt-6 flex justify-center space-x-2">
          {Array.from({ length: totalPages }, (_, i) => (
            <button
              key={i + 1}
              onClick={() => setCurrentPage(i + 1)}
              className={`px-4 py-2 rounded-md text-sm font-medium shadow-sm ${
                currentPage === i + 1
                  ? "bg-indigo-600 text-white hover:bg-indigo-700"
                  : "bg-white text-gray-700 hover:bg-gray-50 border border-gray-300"
              }`}
            >
              {i + 1}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
}
