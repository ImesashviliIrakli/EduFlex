import axios from "axios";

export async function fetchData(config) {
  try {
    const response = await axios({
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${JSON.parse(sessionStorage.getItem("token"))}`,
      },
      ...config,
    });

    const data = await response.data;
    return data;
  } catch (e) {
    console.error(e); 
    throw e;
  }
}
