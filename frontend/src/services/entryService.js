import apiClient from "./apiClient";

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
};
