import axios from "axios";

export async function fetchData(config) {
  try {
    const response = await axios({
      headers: {
        "Content-Type": "application/json",
      },
      ...config,
    });

    const data = await response.data;
    return data;
  } catch (e) {
    console.error(e);
    throw {
      message: e.message || "An error occurred",
      status: e.response?.status,
      data: e.response?.data,
    };
  }
}
