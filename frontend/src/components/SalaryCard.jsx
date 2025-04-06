export default function SalaryCard({ level, salary }) {
  const formatSalary = (amount) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      maximumFractionDigits: 0
    }).format(amount);
  };

  return (
    <div className="p-4 border rounded-lg hover:shadow-md transition-shadow bg-white">
      <h3 className="font-bold text-xl mb-2 text-indigo-600">{level}</h3>
      <div className="text-gray-800">
        <p>{formatSalary(salary)}</p>
      </div>
    </div>
  );
}
