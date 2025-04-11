import axios from "axios";
import storageService from "./strorageService";
const basiUrl = "http://localhost:5008/api";

class deskService {
  async getAlldesks() {
  
    console.log(storageService.getToken());
    const response = await axios
      .get(`${basiUrl}/desk`, {
        headers: {
          authorization: storageService.getToken(),
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
  async getDeskBeiId(deskId) {
    const response = await axios
      .get(`${basiUrl}/desk/${deskId}`, {
        headers: {
          Authorization: this.getToken(),
        },
      })
      .then((res) => {
        if (res) {
          return res.data;
        }
      })
      .catch(() => {
        return null;
      });

    return response;
  }
}

const deskSer = new deskService();
export default deskSer;
