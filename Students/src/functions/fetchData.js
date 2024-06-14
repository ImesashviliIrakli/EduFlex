import axios from "axios";

export async function fetchData(url, config) {
  try {
    const response = await axios(url, {
      ...config,
    });
  } catch (e) {
    console.error(e);
    throw new Error(e);
  }
}
