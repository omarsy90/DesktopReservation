import service from "../service";
const altreservation = {
  namespaced: true,
  state: {
    altreservationarray: [],
    currentDate: "",
    isloading: false,
  },
  mutations: {
    altreservationarray(state, altbooking) {
      state.altreservationarray = altbooking;
    },
    currentDate(state, date) {
      state.currentDate = date;
    },
    isloading(state, isloading) {
      state.isloading = isloading;
    },
  },
  actions: {
    async getAltebooking(context, userId) {
      context.commit("isloading", true);
      const response = await service.getAltereservation(userId);
      if (response.status == 200) {
        // no fehler
        context.commit("altreservationarray", response.booking);
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
  },
  getters: {
    altreservationarray(state) {
      return state.altreservationarray;
    },
    currentDate(state) {
      return state.currentDate;
    },
    isloading(state) {
      return state.isloading;
    },
  },
};
export default altreservation;