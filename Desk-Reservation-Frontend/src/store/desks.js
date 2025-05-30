import deskService from "../service/deskService";
const desks = {
  namespaced: true,
  state: {
    desksaray: [],
    officesArray: [],
    selectedStiegeId: "",
    selectedMinDatum: "",
    selectedMaxDatum: "",
  },
  mutations: {
    desks(state, desks) {
      state.desksaray = desks;
    },
    setMinDatum(state, datum) {
      state.selectedMinDatum = datum;
    },
    setMaxDatum(state, datum) {
      state.selectedMaxDatum = datum;
    },
    setStiegeId(state, stiegeId) {
      state.selectedStiegeId = stiegeId;
    },
    officesArray(state, offices) {
      state.officesArray = offices;
    },
  },
  actions: {
    async getDesks(context) {
      context.rootState.isloading = true;
      const res = await deskService.getAlldesks();
      if (res.status == 200) {
        const desks = res.data;

        context.commit("desks", desks);
      }

      context.rootState.isloading = false;
    },
  },
  getters: {
    desks(state) {
      return state.desksaray;
    },
    MinDatum(state) {
      return state.selectedMinDatum;
    },
    MaxDatum(state) {
      return state.selectedMaxDatum;
    },
    StiegeId(state) {
      return state.selectedStiegeId;
    },
    officesArray(state) {
      return state.officesArray;
    },
  },
};
export default desks;
