"use client";

import { useState } from "react";
import {
  mockData,
  employerTypesByGroup,
  levelTypesByGroup,
  healthcareGroups,
} from "../../../../data/mockData";

export default function EmployerDetail({ params }) {
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;

  const { groupId, employerId } = params;

  // Get the healthcare group and employer type names
  const healthcareGroup = healthcareGroups.find((hg) => hg.id === groupId);
  const employerType = employerTypesByGroup[groupId]?.find(
    (et) => et.id === employerId
  );

  // Filter data for the specific group and employer
  const employerData = mockData.filter(
    (item) => item.groupId === groupId && item.employerTypeId === employerId
  );

  const totalPages = Math.ceil(employerData.length / itemsPerPage);
  const startIndex = (currentPage - 1) * itemsPerPage;
  const paginatedData = employerData.slice(
    startIndex,
    startIndex + itemsPerPage
  );

  // Get level name based on groupId and levelId
  const getLevelName = (levelId) => {
    return (
      levelTypesByGroup[groupId]?.find((l) => l.id === levelId)?.name || ""
    );
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="mb-6">
          <h2 className="text-lg font-medium text-gray-600">
            {healthcareGroup?.name}
          </h2>
          <h1 className="text-3xl font-bold text-gray-900">
            {employerType?.name} Data
          </h1>
        </div>

        <div className="bg-white overflow-hidden shadow-lg rounded-lg">
          <table className="min-w-full divide-y divide-gray-200">
            {/* ...existing thead... */}
            <tbody className="divide-y divide-gray-200">
              {paginatedData.map((item, index) => (
                <tr key={item.id || index} className="hover:bg-gray-50">
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

        {/* ...existing pagination code... */}
      </div>
    </div>
  );
}
