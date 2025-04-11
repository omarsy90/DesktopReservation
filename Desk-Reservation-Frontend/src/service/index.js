import axios from "axios";
import moment from "moment";

const basiUrl = "https://deskbooking.dev.webundsoehne.com/api";

class Service {
 
  async getStatusFixDeskRequest(deskId) {
    const response = await axios
      .get(`${basiUrl}/desk/${deskId}/fix`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        // desk is as fix-desk requested

        if (String(res.data.status) === "NonExisting") {
          // this desk not as fix-desk requested
          return "no existing";
        } else if (String(res.data.status) === "Pending") {
          // // it is as fix-desk requested but untill now ther is no answer from admin
          return "Es wird nach einem Fixtisch nachgefragt";
        } else {
          // this desk as fix-desk booked
          return "Der Tisch wurde gebucht";
        }
      })
      .catch((err) => {
        if (String(err).match(404)) {
          // desk is not exist not more found
          return "Leider ist der Tisch nicht mehr verfügbar";
        }
      });

    return response;
  }

  // desk booking as flix-desk   retun object {stats,message}
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
            message: "Leider könnten wir kein Tisch mit dem angegeben Daten finden",
          };
        } else {
          return {
            status: 409,
            message:
            "Der gesuchte Tisch entweder schon gebucht als Flextisch oder ihr Reservierungsdauer ist mehr als 3 Tage"
          };
        }
      });

    return response;
  }

  // fix-desk booking   retun object {status,message}
  async fixDeskbooking(deskId, comment) {
    const response = await axios
      .post(
        `${basiUrl}/desk/${deskId}/fix`,
        {
          comment: comment,
        },
        {
          headers: {
            authorization: this.getToken(),
          },
        }
      )
      .then((res) => {
        if (res) {
          return {
            status: 204,
            message: "Ihre Anfrage wurde nachgereicht",
          };
        }
      })
      .catch((err) => {
        if (String(err).match("400")) {
          return {
            status: 400,
            message: "Ihre Nachfrage ist ungültig",
          };
        } else if (String(err).match("401")) {
          return {
            status: 401,
            message: "Authentifizierung ist ungültig",
          };
        } else if (String(err).match("404")) {
          return {
            status: 404,
            message: "Der gesuchte Tisch wurde nicht gefunden",
          };
        } else {
          return {
            status: 409,
            message:
              "Entweder haben Sie schon den Tisch als Fixtisch gebucht oder es wird noch nachgefragt"
          };
        }
      });

    return response;
  }

  // get all  fixreservation --------------------

  async getFixReservation() {}

  //-------------------------------------------

  async getFlexdeskBooking() {
    // return arrays of object-booking  if request right
    // return 401 if unathreized (token not valid)
    //return 400 ther is no booking for  userId and deskId give

    const response = await axios
      .get(`${basiUrl}/booking`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        return res.data; // aray of booking-object
      })
      .catch((err) => {
        if (String(err).match(400)) {
          return 400;
        } else if (String(err).match(401)) {
          return 401;
        }
      });

    return response;
  }

  //---------------------------------------alte reservation-Teil

  // this function retun alt flex reservation and
  // return an {arry of reservationen ,stauts:200}of all right
  // return object{ status:401, message} if token or UserId unvaild

  async getAltereservation(userId) {
    const nowdate = moment().format("YYYY-MM-DD");

    const altbookings = [];
    const response = await axios
      .get(`${basiUrl}/booking?userId=${userId}`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        // nowdate =  new Date(nowdate.getFullYear(),nowdate.getMonth()-1,nowdate.getDay(),0,0,0) ;

        res.data.forEach((booking) => {
          const dateEnd = moment(booking.dateEnd).format("YYYY-MM-DD");

          if (dateEnd < nowdate) {
            altbookings.push(booking);
          }
        });

        return {
          booking: altbookings,
          status: 200,
        };
      })
      .catch(() => {
        return {
          status: 401,
          message: "Ihre Daten sind ungültig",
        };
      });
    return response;
  }

  // this function return object {status,mesage} based on the sitaution of response
  async setComment(deskId, comment) {
    const response = await axios
      .post(
        `${basiUrl}/desk/${deskId}/comment`,
        {
          comment: comment,
        },
        {
          headers: {
            authorization: this.getToken(),
          },
        }
      )
      .then((res) => {
        if (res) {
          return {
            status: 204,
            message: "Ihr Kommentar wurde gesendet",
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
            message: "Ihre Daten sind ungültig",
          };
        } else {
          return {
            status: 404,
            message: "Bitte fügen Sie ein Kommentar ein",
          };
        }
      });

    return response;
  }

  //------------------------------------------------

  //--------------my reservation teil----------------------------

  // return all next and current  bookings   for user
  async getNextReservation() {
    const userId = this.getUserId();
    let bookingsArray = [];
    const nowdate = moment().format("YYYY-MM-DD");

    const response = await axios
      .get(`${basiUrl}/booking?userId=${userId}`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        res.data.forEach((booking) => {
          const endDate = moment(booking.dateEnd).format("YYYY-MM-DD");

          if (endDate >= nowdate) {
            bookingsArray.push(booking);
          }
        });

        return {
          status: 200,
          bookings: bookingsArray,
        };
      })
      .catch(() => {
        return {
          status: 404,
          message: "Sie sind leider nicht berechtig um die auszuführen oder ihre Daten sind ungültig",
        };
      });

    return response;
  }

  async bookingDelete(bookingId) {
    const response = axios
      .delete(`${basiUrl}/booking/${bookingId}`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then(() => {
        return { status: 204, message: "Die Reservierung wurde störniert" };
      })
      .catch((err) => {
        if (String(err).match(401)) {
          return { status: 401, message: "Ihre Daten sind ungültig" };
        } else if (String(err).match(403)) {
          return {
            status: 403,
            message:
              "Sie sind leider nicht berechtigt um die auszuführen",
          };
        } else if (String(err).match(404)) {
          return {
            status: 404,
            message: "Es wurde keine Reservierung unter ihre Suche gefunden",
          };
        } else {
          return {
            status: 409,
            message:
              "Ihre Reservierung hat schon angefangen, deshalb ist es nicht möglich diese Reservierung zu störnieren",
          };
        }
      });
    return response;
  }

  //--------------------------------------------------------------

  //----------------------favoriteTeil---------------------
  async addFavourite(deskId) {
    const response = await axios
      .post(
        `${basiUrl}/favourite`,
        {
          deskId: deskId,
        },
        {
          headers: {
            authorization: this.getToken(),
          },
        }
      )
      .then(() => {
        return {
          status: 204,
          message: "Der Tisch wurde als Favorit gespeichert",
        };
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
            message: "Ihre Daten sind ungültig",
          };
        } else {
          return {
            status: 404,
            message: "Der gesuchte Tisch wurde nicht gefunden",
          };
        }
      });
    return response;
  }

  async deleteFavourite(deskId) {
    const response = axios
      .delete(`${basiUrl}/favourite`, {
        headers: {
          authorization: this.getToken(),
        },
        data: {
          deskId: deskId,
        },
      })
      .then(() => {
        return { status: 204, message: "Der Tisch wurde als Favorit gelöscht" };
      })
      .catch((err) => {
        if (String(err).match(400)) {
          return { status: 400, message: "Ihre Nachfrage ist ungültig" };
        } else if (String(err).match(401)) {
          return { status: 4001, message: " Ihre Daten sind ungültig" };
        } else {
          return {
            status: 409,
            message: "Der gesuchte Tisch wurde nicht gefunden",
          };
        }
      });

    return response;
  }

  // this function return {status.200, favourities:[]} if connection authreized
  // if there is wrong retun  {status: 401 , []:emptyarray}
  async gitUserFavourities() {
    const response = await axios
      .get(`${basiUrl}/favourite`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        return {
          status: 200,
          favourities: res.data,
        };
      })
      .catch(() => {
        return {
          status: 401,
          favourities: [],
        };
      });

    return response;
  }

  //--------------------------------------------------------------------------

  //--------------------------------admin-bereich----------

  async getFixRequest() {
    const response = await axios
      .get(`${basiUrl}/admin/fix-desk-request`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        return {
          status: 200,
          requests: res.data,
        };
      })
      .catch((err) => {
        if (String(err).match(401)) {
          return {
            status: 401,
            message: "Ihre Daten sind ungültig",
          };
        } else if (String(err).match(403)) {
          return {
            status: 403,
            message:
              "Sie haben für diese Aktion keine Rechte",
          };
        } else {
          return {
            status: 404,
            message:
              "Der gesuchte Tisch oder Benutzer wurde nicht gefunden",
          };
        }
      });

    return response;
  }

  async getUserBeiId(userId) {
    const response = await axios
      .get(`${basiUrl}/user/${userId}`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        return res.data;
      })
      .catch(() => {
        return false;
      });

    return response;
  }

  async fixDeskrequestApprove(requsetId) {
    const response = await axios
      .post(
        `${basiUrl}/admin/fix-desk-request/${requsetId}/approve`,
        {},
        {
          headers: {
            Authorization: this.getToken(),
          },
        }
      )
      .then(() => {
        return {
          status: 204,
          message: " Fixtisch Anfrage wurde bestätigt",
        };
      })
      .catch((err) => {
        if (String(err).match(401)) {
          return {
            status: 401,
            message: "Ihre Daten sind ungültig",
          };
        } else if (String(err).match(403)) {
          return {
            status: 403,
            message:
              "Sie haben für diese Aktion keine Rechte",
          };
        } else {
          return {
            status: 404,
            message: "Es wurde kein Anfrage gefunden",
          };
        }
      });

    return response;
  }

  async fixDeskRequestDecline(requestId) {
    const response = await axios
      .post(
        `${basiUrl}/admin/fix-desk-request/${requestId}/decline`,
        {},
        {
          headers: {
            authorization: this.getToken(),
          },
        }
      )
      .then(() => {
        return {
          status: 204,
          message: " Fixtisch Anfrage wurde abgelehnt",
        };
      })
      .catch((err) => {
        if (String(err).match(401)) {
          return {
            status: 401,
            message: "Ihre Daten sind ungültig",
          };
        } else if (String(err).match(403)) {
          return {
            status: 403,
            message:
              "Sie haben für die Aktion kein Rechte",
          };
        } else {
          return {
            status: 404,
            message: "Es wurde kein Anfrage gefunden",
          };
        }
      });

    return response;
  }

  //-----------------------------------------------------

  async getAllofice() {
    const response = await axios.get(`${basiUrl}/office`, {
      headers: {
        authorization: this.getToken(),
      },
    });
    return response.data;
  }

  async getDeskBeiofficeId(officeId) {
    let desks = [];
    await axios
      .get(`${basiUrl}/desk`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        res.data.forEach((desk) => {
          if (desk.office.id == officeId) {
            desks.push(desk);
          }
        });
      });

    return desks;
  }

  async getComments(deskId) {
    const comments = await axios
      .get(`${basiUrl}/desk/${deskId}/comment`, {
        headers: {
          authorization: this.getToken(),
        },
      })
      .then((res) => {
        return res.data;
      });
    comments.forEach((comment) => {
      const date = comment.commentedAt.split("T")[0];
      let time = comment.commentedAt.split("T")[1];
      time = time.split(".")[0];
      comment.commentedAt = date + "  " + time + "  GMT";
    });
    return comments;
  }

  getToken() {
    const token =
      window.localStorage.getItem("token") ||
      window.sessionStorage.getItem("token");

    return `Bearer ${token}`;
  }

  getUserId() {
    return (
      window.localStorage.getItem("userId") ||
      window.sessionStorage.getItem("userId")
    );
  }

  saveDataLocalStorage(data) {
    window.localStorage.setItem(data.key, data.value);
  }

  saveDataSessionStorage(data) {
    window.sessionStorage.setItem(data.key, data.value);
  }

  getDataLocalStorage(key) {
    return window.localStorage.getItem(key);
  }
  getDataSessionStorage(key) {
    return window.sessionStorage.getItem(key);
  }
}

const service = new Service();

export default service;
