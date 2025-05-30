import axios from "axios";
const basiUrl = process.env.VUE_APP_API_BASE_URL;

class authenticatonService {
  // return  token  if ther credentials are right
  // return 400 if payload not correct
  // return 401 if email or password is unvalid
  async logIn(auth) {
    const response = await axios
      .post(`${basiUrl}/User/login`, {
        email: auth.email,
        password: auth.password,
      })
      .then((res) => {
        return res;
      })
      .catch((err) => {
        return err.response;
      });

    return response;
  }

  async register(newUserInfo) {
    const axios = require("axios");
    let data = JSON.stringify(newUserInfo);
    console.log(data);
    let config = {
      method: "post",
      maxBodyLength: Infinity,
      url: `${basiUrl}/User/register`,
      headers: {
        "Content-Type": "application/json",
      },
      data: data,
    };

    const res = await axios
      .request(config)
      .then((response) => {
        console.log(JSON.stringify(response.data));
        return response;
      })
      .catch((error) => {
        console.log(error);
        return error.response;
      });

    return res;
  }
}

const authService = new authenticatonService();
export default authService;
