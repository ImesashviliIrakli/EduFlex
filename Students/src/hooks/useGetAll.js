import { useQuery } from "@tanstack/react-query";
import { useRootContext } from "./useRootContext";
import { fetchData } from "../functions/fetchData";

export const useGetAll = (queryKey, urlParam) => {
  const { baseUrl } = useRootContext();

  const query = useQuery({
    queryKey: queryKey,
    queryFn: () =>
      fetchData({
        url: baseUrl + urlParam,
        method: "get",
      }),
  });

  return query;
};
