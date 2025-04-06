import { jobPostings } from '../data/mockData';

export default function JobPostings() {
  return (
    <div className="mt-8 bg-gray-50 p-6 rounded-lg">
      <h2 className="text-2xl font-semibold mb-4">Suggested Job Postings</h2>
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
        {jobPostings.map((job) => (
          <div key={job.id} className="bg-white p-4 rounded-lg border hover:shadow-md transition-shadow">
            <h3 className="font-semibold text-lg mb-2 text-indigo-600">{job.title}</h3>
            <p className="text-gray-600 mb-2">{job.employer}</p>
            <p className="text-gray-500 text-sm mb-2">{job.location}</p>
            <p className="text-blue-600 font-medium">${job.estimatedSalary}</p>
            <p className="text-gray-500 text-sm mt-2">{job.type}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
