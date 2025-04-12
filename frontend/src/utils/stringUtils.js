export const normalizeString = (str) => {
  if (!str) return "";
  return str.split(" ").join("").toLowerCase();
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
