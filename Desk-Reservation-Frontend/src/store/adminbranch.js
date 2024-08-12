import service from "../service";
const adminBrbanch = {
  namespaced: true,
  state: {
    fixDeskRequest: [],
    comments: [],
    //-----------------
    desks: [],
    deskIdselected: "",
    //-----------------
  },
  mutations: {
    fixDeskRequest(state, fixDeskRequest) {
      state.fixDeskRequest = fixDeskRequest;
    },
    desks(state, desks) {
      state.desks = desks;
    },
    deskIdselected(state, id) {
      state.deskIdselected = id;
    },
    comments(state, commentsArray) {
      state.comments.push(commentsArray);
    },
    resetComments(state) {
      state.comments = [];
    },
  },
  actions: {
    async getComments(context, deskId) {
      context.commit("resetComments");
      let commentsArray = [];
      if (deskId == "") {
        // all comments
        context.getters.desks.forEach((desk) => {
          service.getComments(desk.id).then((commentsArray) => {
            context.commit("comments", commentsArray);
          });
        });
      } else {
        // comment for desk
        commentsArray = await service.getComments(deskId);
        context.commit("comments", commentsArray);
      }
    },
    async getAlldesks(context) {
      const desks = await service.getAlldesks();
      context.commit("desks", desks);
    },
    async fixDeskDecline(context, requestId) {
      const response = await service.fixDeskRequestDecline(requestId);
      if (response.status == 204) {
        context.dispatch("getAllrequest"); // there for  to be  requestfixdesk automatik  updated
      }
      return response;
    },
    async fixDeskApprove(context, requestId) {
      const response = await service.fixDeskrequestApprove(requestId);
      if (response.status == 204) {
        context.dispatch("getAllrequest"); // there for  to be  requestfixdesk automatik  updated
      }
      return response;
    },
    async getAllrequest(context) {
      const response = await service.getFixRequest();
      if (response.status == 200) {
        context.commit("fixDeskRequest", response.requests);
      }
    },
    async getUserBeiId(context, userId) {
      if (context == "1");
      const user = await service.getUserBeiId(userId);
      return user;
    },
  },
  getters: {
    fixDeskRequest(state) {
      return state.fixDeskRequest;
    },
    desks(state) {
      return state.desks;
    },
    deskIdselected(state) {
      return state.deskIdselected;
    },
    comments(state) {
      return state.comments;
    },
  },
};
export default adminBrbanch;