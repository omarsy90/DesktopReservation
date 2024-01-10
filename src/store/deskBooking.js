import service from "../service";
const booking = {
  namespaced: true,
  state: {
    selectedDeskId: "",
    dateStart: null,
    dateEnd: null,
    bookingError: "",
    bookingSuccessMessage: "",
    bookingIsloading: false,
    selectedDesk: null,
  },
  mutations: {
    selectedDeskId(state, deskId) {
      state.selectedDeskId = deskId;
    },
    dateStart(state, dateStart) {
      state.dateStart = dateStart;
    },
    dateEnd(state, dateEnd) {
      state.dateEnd = dateEnd;
    },
    bookingIsloading(state, isloading) {
      state.bookingIsloading = isloading;
    },
    bookingSuccessMessage(state, successMessage) {
      state.bookingSuccessMessage = successMessage;
    },
    bookingError(state, errorMessage) {
      state.bookingError = errorMessage;
    },
    selectedDesk(state, desk) {
      //selectedDesk
      state.selectedDesk = desk;
    },
  },
  actions: {
    async addFavourit(context, deskId) {
      context.commit("bookingIsloading", true);
      const response = await service.addFavourite(deskId);
      if (response.status == 204) {
        context.commit(
          "bookingSuccessMessage",
          "Der Tisch wurde gebucht und als ihr Favorite gespeichert"
        );
      }
      context.comment("bookingIsloading", false);
    },
    async bookingFlixDesk(context, isFavourited) {
      context.commit("bookingIsloading", true);
      const deskId = context.getters.selectedDeskId;
      const dateStart = context.getters.dateStart;
      const dateEnd = context.getters.dateEnd;
      const checkStatus = await service.getStatusFixDeskRequest(deskId);
      // check if this desk als fix-desk requested or booked
      if (String(checkStatus) === "no existing") {
        // desk selected is free
        const response = await service.deskBooking(deskId, dateStart, dateEnd);
        if (response.status == 204) {
          context.commit("bookingSuccessMessage", response.message);
          context.commit("bookingError", "");
          if (isFavourited === true) {
            context.dispatch("addFavourit", deskId);
          }
        } else {
          context.commit("bookingError", response.message);
          context.commit("bookingSuccessMessage", "");
        }
      } else {
        // the desk selected is either booked or asked as fix-desk
        context.commit("bookingError", checkStatus);
        context.commit("bookingSuccessMessage", "");
      }
      context.commit("bookingIsloading", false);
    },
    async fixDeskBooking(context, comment) {
      context.commit("bookingIsloading", true);
      const deskId = context.getters.selectedDeskId;
      const checkStatus = await service.getStatusFixDeskRequest(deskId);
      // check if this desk als fix-desk requested or booked
      if (String(checkStatus) === "no existing") {
        const response = await service.fixDeskbooking(deskId, comment);
        if (response.status == 204) {
          context.commit("bookingSuccessMessage", response.message);
          context.commit("bookingError", "");
        } else {
          context.commit("bookingSuccessMessage", "");
          context.commit("bookingError", response.message);
        }
      } else {
        //  this desk requested or booked as fix-desk
        context.commit("bookingError", checkStatus);
        context.commit("bookingSuccessMessage", "");
      }
      context.commit("bookingIsloading", false);
    },
    async getSelectedDesk(context, deskId) {
      const desk = await service.getDeskBeiId(deskId);
      if (desk) {
        context.commit("selectedDesk", desk);
        context.commit("selectedDeskId", desk.id);
      }
    },
    dateStart(context, datum) {
      context.commit("dateStart", datum);
    },
    dateEnd(context, datum) {
      context.commit("dateEnd", datum);
    },
    deskId(context, id) {
      context.commit("selectedDeskId", id);
    },
    bookingError(context, error) {
      context.commit("bookingError", error);
    },
    successMessage(context, message) {
      context.commit("bookingSuccessMessage", message);
    },
  },
  getters: {
    selectedDeskId(state) {
      return state.selectedDeskId;
    },
    dateStart(state) {
      return state.dateStart;
    },
    dateEnd(state) {
      return state.dateEnd;
    },
    bookingIsloading(state) {
      return state.bookingIsloading;
    },
    bookingSuccessMessage(state) {
      return state.bookingSuccessMessage;
    },
    bookingError(state) {
      return state.bookingError;
    },
    selectedDesk(state) {
      return state.selectedDesk;
    },
  },
};
export default booking;
