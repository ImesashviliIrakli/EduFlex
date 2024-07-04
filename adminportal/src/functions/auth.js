import axios from "axios";

export async function auth(config) {
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
    throw e;
  }
}
