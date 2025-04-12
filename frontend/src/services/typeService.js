import apiClient from "./apiClient";

export const typeService = {
  async getJobGroups() {
    const response = await apiClient.get("/JobGroup");
    return response.data;
  },

  async getEmployerTypesByJobGroup(jobGroupId) {
    const response = await apiClient.get(
      `/EmployerType/byJobGroup/${jobGroupId}`
    );
    return response.data;
  },

  async getLevelTypesByJobGroup(jobGroupId) {
    const response = await apiClient.get(`/LevelType/byJobGroup/${jobGroupId}`);
    return response.data;
  },

  async getEmployerTypesByJobGroupEnum(jobGroup) {
    const response = await apiClient.get(
      `/EmployerType/byJobGroupType/${jobGroup}`
    );
    return response.data;
  },

  async getLevelTypesByJobGroupEnum(jobGroup) {
    const response = await apiClient.get(
      `/LevelType/byJobGroupType/${jobGroup}`
    );
    return response.data;
  },
};
