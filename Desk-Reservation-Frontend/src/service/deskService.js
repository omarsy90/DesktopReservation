import axios from "axios";
import storageService from "./strorageService";
const basiUrl = process.env.VUE_APP_API_BASE_URL;

class deskService {
  async getAlldesks() {
    const response = await axios
      .get(`${basiUrl}/Desk`, {
        headers: {
          authorization: "Bearer " + storageService.getToken(),
        },
      })
      .then((res) => {
        return res;
      })
      .catch((err) => {
        return err.response;
      });

    return response;
  }

  // retun desk-pbject if the request right
  // return null if request unautherized or desk with id given not found
  async getDeskById(deskId) {
    const response = await axios
      .get(`${basiUrl}/Desk/${deskId}`, {
        headers: {
          Authorization: "Bearer " + storageService.getToken(),
        },
      })
      .then((res) => {
        if (res) {
          return res;
        }
      })
      .catch((err) => {
        return err.response;
      });

    return response;
  }
}

const deskSer = new deskService();
export default deskSer;
