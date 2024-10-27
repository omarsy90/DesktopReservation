import service from "../service";
import moment from "moment";
const myReservation = {
  namespaced: true,
  state: {
    bookingsArray: [],
    curentDesk: null,
    error: "",
    isloading: false,
  },
  mutations: {
    bookingsArray(state, bookingsArray) {
      state.bookingsArray = bookingsArray;
    },
    curentDesk(state, curentDesk) {
      state.curentDesk = curentDesk;
    },
    error(state, error) {
      state.error = error;
    },
    isloading(state, isloading) {
      state.isloading = isloading;
    },
  },
  actions: {
    // return curent and next reservation
    async getNextReservations(context) {
      context.commit("isloading", true);
      const response = await service.getNextReservation();
      if (response.status == 200) {
        // this code down to determine  activ desk from next(not activ) desk
        const today = moment().format("YYYY-MM-DD");
        response.bookings.forEach((booking) => {
          const dateStart = moment(booking.dateStart).format("YYYY-MM-DD");
          if (dateStart <= today) {
            booking.isActive = true; // booking is activ
          } else {
            booking.isActive = false; // booking is coming but not started
          }
          //-------------------------------------------------------------
        });
        context.commit("bookingsArray", response.bookings);
      } else {
        context.commit("error", response.message);
      }
      context.commit("isloading", false);
    },
    async setComment(context, payload) {
      const response = await service.setComment(
        payload.deskId,
        payload.comment
      );
      return response;
    },
    async bookingDelete(context, bookingId) {
      const response = await service.bookingDelete(bookingId);
      if (response.status == 204) {
        context.dispatch("getNextReservations");
      }
      return response;
    },
  },
  getters: {
    bookingsArray(state) {
      return state.bookingsArray;
    },
    curentDesk(state) {
      return state.curentDesk;
    },
    error(state) {
      return state.error;
    },
  },
};
export default myReservation;