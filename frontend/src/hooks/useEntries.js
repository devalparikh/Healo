import { useState, useEffect } from "react";
import { entryService } from "../services/entryService";

export const useEntries = () => {
  const [entries, setEntries] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchEntries = async () => {
      try {
        setIsLoading(true);
        const data = await entryService.getAllEntries();
        setEntries(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };

    fetchEntries();
  }, []);

  return { entries, isLoading, error };
};
