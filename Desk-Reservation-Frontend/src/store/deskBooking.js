import Deskservice from "../service/deskService";
import bookingService from "../service/bookingService";
const deskBooking = {
  namespaced: true,
  state: {
    bookingError: "",
    bookingSuccessMessage: "",
  },
  mutations: {
    bookingSuccessMessage(state, successMessage) {
      state.bookingSuccessMessage = successMessage;
    },
    bookingError(state, errorMessage) {
      state.bookingError = errorMessage;
    },
  },
  actions: {
    async getDeskInfo(context, deskId) {
      const deskInfo = await Deskservice.getDeskById(deskId);
      if (deskInfo.status === 200) {
        return deskInfo.data;
      }
    },

    async bookFixDesk(context, fixBookingData) {
      context.rootState.isloading = true;

      if (!fixBookingData.dtStart) {
        context.commit("bookingError", "Srart datum soll ausgewählt werden");
        context.rootState.isloading = false;
        return;
      }
      const res = await bookingService.fixDeskBooking(
        fixBookingData.deskId,
        fixBookingData.dtStart,
        fixBookingData.isFavourited
      );
      console.log(res);
      if (res.status == 200) {
        context.commit(
          "bookingSuccessMessage",
          "deine Anfordarung wurde erforfreich geschickt"
        );

        context.commit("bookingError", "");
      } else {
        context.commit(
          "bookingError",
          "deine Anfordarung wurde nicht geschickt, Propieren Sie später"
        );
        context.commit("bookingSuccessMessage", "");
      }

      context.rootState.isloading = false;
    },

    reset(context) {
      context.commit("bookingSuccessMessage", "");
      context.commit("bookingError", "");
      context.rootState.isloading = false;
    },
  },
  getters: {
    bookingSuccessMessage(state) {
      return state.bookingSuccessMessage;
    },
    bookingError(state) {
      return state.bookingError;
    },
  },
};
export default deskBooking;
