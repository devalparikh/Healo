"use client";

import { useState } from 'react';

export default function Sidebar({ isOpen, onClose }) {
  const [filters, setFilters] = useState({
    location: '',
    jobType: 'all'
  });

  const locations = ['All', 'New York', 'California', 'Texas', 'Florida'];
  const jobTypes = ['all', 'Hospital', 'Private', 'Urgent Care'];

  return (
    <div className={`fixed inset-y-0 left-0 z-50 w-64 bg-white shadow-lg transform ${
      isOpen ? 'translate-x-0' : '-translate-x-full'
    } transition-transform duration-300 ease-in-out`}>
      <div className="p-4">
        <div className="flex justify-between items-center mb-6">
          <h2 className="text-xl font-semibold">Filters</h2>
          <button onClick={onClose} className="text-gray-500 hover:text-gray-700">
            ✕
          </button>
        </div>

        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Location
            </label>
            <select 
              className="w-full border rounded-md p-2"
              value={filters.location}
              onChange={(e) => setFilters({...filters, location: e.target.value})}
            >
              {locations.map(location => (
                <option key={location} value={location.toLowerCase()}>
                  {location}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Job Type
            </label>
            <select 
              className="w-full border rounded-md p-2"
              value={filters.jobType}
              onChange={(e) => setFilters({...filters, jobType: e.target.value})}
            >
              {jobTypes.map(type => (
                <option key={type} value={type}>
                  {type.charAt(0).toUpperCase() + type.slice(1)}
                </option>
              ))}
            </select>
          </div>
        </div>
      </div>
    </div>
  );
}
