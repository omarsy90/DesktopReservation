import service from "../service";

const favourite = {
  namespaced: true,
  state: {
    favouritedesks: [],
  },
  mutations: {
    favouritedesks(state, desks) {
      state.favouritedesks = desks;
    },
  },
  actions: {
    async getFavourities(context) {
      const response = await service.gitUserFavourities();
      if (response.status == 200) {
        context.commit("favouritedesks", response.favourities);
      }
    },

    async deletefavourite(context, deskId) {
      const response = await service.deleteFavourite(deskId);
      if (response.status == 204) {
        context.dispatch("getFavourities");
      }

      return response;
    },
  },

  getters: {
    favouritedesks(state) {
      return state.favouritedesks;
    },
  },
};

export default favourite;
