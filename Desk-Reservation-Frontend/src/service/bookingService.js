import axios from "axios";
import storageService from "./strorageService";
const basiUrl = process.env.VUE_APP_API_BASE_URL;
class bookingService {
  async deskBooking(deskId, dateStart, dateEnd) {
    const response = await axios
      .post(
        `${basiUrl}/booking`,
        {
          deskId: deskId,
          dateStart: dateStart,
          dateEnd: dateEnd,
        },
        {
          headers: {
            Authorization: this.getToken(),
          },
        }
      )
      .then((res) => {
        if (res) {
          //
          return {
            status: 204,
            message: "Flextisch wurde gebucht",
          };
        }
      })
      .catch((err) => {
        if (String(err).match(400)) {
          return {
            status: 400,
            message: "Ihre Nachfrage ist ungültig",
          };
        } else if (String(err).match(401)) {
          return {
            status: 401,
            message: "Die Authentifizierung ist ungültig",
          };
        } else if (String(err).match(404)) {
          return {
            status: 404,
            message:
              "Leider könnten wir kein Tisch mit dem angegeben Daten finden",
          };
        } else {
          return {
            status: 409,
            message:
              "Der gesuchte Tisch entweder schon gebucht als Flextisch oder ihr Reservierungsdauer ist mehr als 3 Tage",
          };
        }
      });

    return response;
  }

  async fixDeskBooking(deskId, dtStart, isFavourited) {
    const response = await axios
      .post(
        `${basiUrl}/Fixreservation/request`,
        {
          deskId: deskId,
          dtStart: dtStart,
          isFavourited: isFavourited,
        },
        {
          headers: {
            authorization: "Bearer " + storageService.getToken(),
            "Content-Type": "application/json",
          },
        }
      )
      .then((res) => {
        return res;
      })
      .catch((err) => {
        return err.response;
      });

    return response;
  }
}

const bookService = new bookingService();
export default bookService;
