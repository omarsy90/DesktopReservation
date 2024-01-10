import service from "../service";
const fixReservation = {
  namespaced: true,
  state: {
    fixReservations: [],
  },
  mutations: {
    fixReservations(state, fixReservations) {
      state.fixReservations = fixReservations;
    },
  },
  actions: {
    async getFixreservation(context, userId) {
      const fixreservationsArray = [];
      const response = await service.getAlldesks();
      if (response != 400 && response != 401) {
        response.forEach((booking) => {
          if (booking.fixDeskUserId) {
            if (booking.fixDeskUserId == userId) {
              fixreservationsArray.push(booking);
            }
          }
        });
      }
      context.commit("fixReservations", fixreservationsArray);
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
    fixReservations(state) {
      return state.fixReservations;
    },
  },
};
export default fixReservation;