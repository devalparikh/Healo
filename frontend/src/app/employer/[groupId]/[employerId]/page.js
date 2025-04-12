"use client";

import { useState } from "react";
import { useEntries } from "../../../../hooks/useEntries";
import {
  useJobGroups,
  useEmployerTypesByEnum,
  useLevelTypesByEnum,
} from "../../../../hooks/useTypes";
import {
  normalizeString,
  camelCaseToSpace,
} from "../../../../utils/stringUtils";

export default function EmployerDetail({ params }) {
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;

  // Decode the URL parameters
  const groupName = decodeURIComponent(params.groupId).replace(/-/g, " ");
  const employerName = decodeURIComponent(params.employerId).replace(/-/g, " ");

  const {
    entries,
    isLoading: entriesLoading,
    error: entriesError,
  } = useEntries();
  const { jobGroups, isLoading: groupsLoading } = useJobGroups();
  const { employerTypes, isLoading: employerTypesLoading } =
    useEmployerTypesByEnum(groupName);
  const { levelTypes, isLoading: levelTypesLoading } =
    useLevelTypesByEnum(groupName);

  // Filter data using normalized string comparison with decoded names
  const employerData = entries.filter((item) => {
    const normalizedItemGroup = normalizeString(item.jobGroup);
    const normalizedItemEmployer = normalizeString(item.employerType);
    return (
      normalizedItemGroup === normalizeString(groupName) &&
      normalizedItemEmployer === normalizeString(employerName)
    );
  });

  const totalPages = Math.ceil(employerData.length / itemsPerPage);
  const startIndex = (currentPage - 1) * itemsPerPage;
  const paginatedData = employerData.slice(
    startIndex,
    startIndex + itemsPerPage
  );

  const isLoading =
    entriesLoading ||
    groupsLoading ||
    employerTypesLoading ||
    levelTypesLoading;

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="text-xl text-gray-600">Loading...</div>
      </div>
    );
  }

  if (entriesError) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="text-xl text-red-600">Error: {entriesError}</div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="mb-6">
          <h2 className="text-lg font-medium text-gray-600">{groupName}</h2>
          <h1 className="text-3xl font-bold text-gray-900">
            {employerName} Data
          </h1>
        </div>

        <div className="bg-white overflow-hidden shadow-lg rounded-lg">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Level
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Salary
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Work/Life Balance
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-200">
              {paginatedData.map((item, index) => (
                <tr key={index} className="hover:bg-gray-50">
                  <td className="px-6 py-4 text-sm text-gray-900 font-medium">
                    {camelCaseToSpace(item.level)}
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

        {totalPages > 1 && (
          <div className="mt-4 flex justify-center">
            <nav className="flex items-center space-x-2">
              {Array.from({ length: totalPages }, (_, i) => i + 1).map(
                (page) => (
                  <button
                    key={page}
                    onClick={() => setCurrentPage(page)}
                    className={`px-3 py-1 rounded ${
                      currentPage === page
                        ? "bg-indigo-600 text-white"
                        : "bg-white text-gray-700 hover:bg-gray-50"
                    }`}
                  >
                    {page}
                  </button>
                )
              )}
            </nav>
          </div>
        )}
      </div>
    </div>
  );
}
