export default function WorkLifeBalanceBar({ score }) {
  return (
    <div className="mb-4">
      <div className="flex justify-between mb-1">
        <span className="text-sm font-medium text-indigo-600">Work-Life Balance</span>
        <span className="text-sm font-medium text-indigo-600">{score.toFixed(1)}/10</span>
      </div>
      <div className="w-full bg-green-100 rounded-full h-2.5">
        <div
          className="bg-green-500 h-2.5 rounded-full transition-all duration-300"
          style={{ width: `${(score / 10) * 100}%` }}
        ></div>
      </div>
    </div>
  );
}
