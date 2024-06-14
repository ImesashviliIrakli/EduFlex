import { useQuery } from "@tanstack/react-query";
import { fetchData } from "../functions/fetchData";

export function useGetAll(url, queryKey) {
  const queryFn = fetchData(url, { method: "get" });
  const getAllQuery = useQuery({ queryKey: [queryKey], queryFn: queryFn });

  return getAllQuery;
}
