"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import {
  useJobGroups,
  useEmployerTypesByEnum,
  useLevelTypesByEnum,
} from "../../hooks/useTypes";
import { entryService } from "../../services/entryService";
import { normalizeString } from "../../utils/stringUtils";

export default function AddEntry() {
  const router = useRouter();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState(null);

  const [formData, setFormData] = useState({
    jobGroup: "",
    employerType: "",
    level: "",
    salary: "",
    workLifeBalanceScore: "",
  });

  const { jobGroups, isLoading: jobGroupsLoading } = useJobGroups();
  const selectedGroup = jobGroups.find(
    (group) =>
      normalizeString(group.name) === normalizeString(formData.jobGroup)
  );

  const { employerTypes, isLoading: employerTypesLoading } =
    useEmployerTypesByEnum(selectedGroup?.name);

  const { levelTypes, isLoading: levelTypesLoading } = useLevelTypesByEnum(
    selectedGroup?.name
  );

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]:
        name === "salary"
          ? parseFloat(value)
          : name === "workLifeBalanceScore"
          ? parseInt(value)
          : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError(null);

    try {
      await entryService.createEntry(formData);
      router.push("/");
    } catch (err) {
      setError(err.message || "Failed to submit entry");
    } finally {
      setIsSubmitting(false);
    }
  };

  // Add this helper function to check loading state
  const isLoadingData = () => {
    if (jobGroupsLoading) return true;
    if (formData.jobGroup && (employerTypesLoading || levelTypesLoading))
      return true;
    return false;
  };

  if (isLoadingData()) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-100">
        <div className="text-2xl text-gray-600">Loading...</div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-white">
      <div className="max-w-2xl mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-8">
          Add Your Entry
        </h1>

        {error && (
          <div className="bg-red-50 border border-red-200 text-red-600 px-4 py-3 rounded mb-6">
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Healthcare Field
            </label>
            <select
              name="jobGroup"
              value={formData.jobGroup}
              onChange={handleChange}
              required
              className="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 bg-white text-gray-900 text-base py-2 px-3"
            >
              <option value="">Select Field</option>
              {jobGroups.map((group) => (
                <option key={group.id} value={group.name}>
                  {group.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Employer Type
            </label>
            <select
              name="employerType"
              value={formData.employerType}
              onChange={handleChange}
              required
              disabled={!formData.jobGroup}
              className="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 bg-white text-gray-900 text-base py-2 px-3 disabled:bg-gray-100 disabled:text-gray-500"
            >
              <option value="">Select Employer Type</option>
              {employerTypes.map((type) => (
                <option key={type.id} value={type.name}>
                  {type.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Level
            </label>
            <select
              name="level"
              value={formData.level}
              onChange={handleChange}
              required
              disabled={!formData.jobGroup}
              className="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 bg-white text-gray-900 text-base py-2 px-3 disabled:bg-gray-100 disabled:text-gray-500"
            >
              <option value="">Select Level</option>
              {levelTypes.map((level) => (
                <option key={level.id} value={level.name}>
                  {level.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Annual Salary (USD)
            </label>
            <input
              type="number"
              name="salary"
              value={formData.salary}
              onChange={handleChange}
              required
              min="0"
              className="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 bg-white text-gray-900 text-base py-2 px-3"
              placeholder="Enter salary"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Work-Life Balance Score (1-10)
            </label>
            <input
              type="number"
              name="workLifeBalanceScore"
              value={formData.workLifeBalanceScore}
              onChange={handleChange}
              required
              min="1"
              max="10"
              className="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 bg-white text-gray-900 text-base py-2 px-3"
              placeholder="Rate from 1 to 10"
            />
          </div>

          <button
            type="submit"
            disabled={isSubmitting}
            className="w-full bg-indigo-600 text-white py-3 px-4 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 disabled:bg-indigo-300 disabled:cursor-not-allowed text-base font-medium"
          >
            {isSubmitting ? "Submitting..." : "Submit Entry"}
          </button>
        </form>
      </div>
    </div>
  );
}
