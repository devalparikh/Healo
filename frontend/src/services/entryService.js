import apiClient from "./apiClient";
import { normalizeString } from "../utils/stringUtils";

export const entryService = {
  getAllEntries: async () => {
    try {
      const response = await apiClient.get("/Entry");
      return response.data;
    } catch (error) {
      console.error("Error fetching entries:", error);
      throw error;
    }
  },

  createEntry: async (entryData) => {
    try {
      const normalizedData = {
        ...entryData,
        jobGroup: normalizeString(entryData.jobGroup),
        employerType: normalizeString(entryData.employerType),
        level: normalizeString(entryData.level),
      };

      const response = await apiClient.post("/Entry", normalizedData);
      return response.data;
    } catch (error) {
      console.error("Error creating entry:", error);
      throw error;
    }
  },
};
