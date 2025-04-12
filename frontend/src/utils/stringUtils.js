export const normalizeString = (str) => {
  if (!str) return "";
  return str.replace(/\s+/g, "");
};

export const addSpacesToCamelCase = (str) => {
  if (!str) return "";
  return str
    .split("")
    .map((char, i) =>
      i > 0 && char === char.toUpperCase() ? ` ${char}` : char
    )
    .join("");
};

export const camelCaseToSpace = (str) => {
  return (
    str
      // Insert a space before all capital letters
      .replace(/([A-Z])/g, " $1")
      // Remove space at the start if exists
      .replace(/^./, (str) => str.toUpperCase())
      .trim()
  );
};
