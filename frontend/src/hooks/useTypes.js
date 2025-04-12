import { useState, useEffect } from "react";
import { typeService } from "../services/typeService";

export const useJobGroups = () => {
  const [jobGroups, setJobGroups] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchJobGroups = async () => {
      try {
        setIsLoading(true);
        const data = await typeService.getJobGroups();
        setJobGroups(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };

    fetchJobGroups();
  }, []);

  return { jobGroups, isLoading, error };
};

export const useEmployerTypesByEnum = (jobGroup) => {
  const [employerTypes, setEmployerTypes] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchEmployerTypes = async () => {
      if (!jobGroup) return;

      try {
        setIsLoading(true);
        const data = await typeService.getEmployerTypesByJobGroupEnum(jobGroup);
        setEmployerTypes(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };

    fetchEmployerTypes();
  }, [jobGroup]);

  return { employerTypes, isLoading, error };
};

export const useLevelTypesByEnum = (jobGroup) => {
  const [levelTypes, setLevelTypes] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchLevelTypes = async () => {
      if (!jobGroup) return;

      try {
        setIsLoading(true);
        const data = await typeService.getLevelTypesByJobGroupEnum(jobGroup);
        setLevelTypes(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };

    fetchLevelTypes();
  }, [jobGroup]);

  return { levelTypes, isLoading, error };
};
