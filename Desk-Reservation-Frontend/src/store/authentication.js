import service from "../service/authenticationService.js";
const authenticaton = {
  namespaced: true,
  state: () => ({
    token: "",
    firstname: "",
    lastname: "",
    department: "",
    email: "",
    password: "",
    isAdmin: false,
  }),
  mutations: {
    token(state, token) {
      state.token = token;
    },
    firstname(state, firstname) {
      state.firstname = firstname;
    },
    lastname(state, lastname) {
      state.lastname = lastname;
    },
    department(state, department) {
      state.department = department;
    },
    email(state, email) {
      state.email = email;
    },
    password(state, password) {
      state.password = password;
    },
    isAdmin(state, isAdmin) {
      state.isAdmin = isAdmin;
    },
    message(state, message) {
      state.message = message;
    },
    error(state, message) {
      state.error = message;
    },
  },
  actions: {
    async register(context) {
      context.rootState.isloading = true;
      const user = {
        firstname: context.getters.firstname,
        lastname: context.getters.lastname,
        department: context.getters.department,
        email: context.getters.email,
        password: context.getters.password,
      };
      const respnse = service
        .userRegistering(user)
        .then((res) => {
          if (res.status == 204) {
            context.rootState.successMessage =
              "Sie sind nun registriert und können sich einloggen";
            context.rootState.error = "";
            return true;
          }
        })
        .catch((err) => {
          context.rootState.successMessage = "";
          if (String(err).match(400)) {
            context.rootState.error = "Eines von den Felder ist ungültig !";
            return false;
          } else if (String(err).match(409)) {
            context.rootState.error =
              "Die Email-Adresse ist bereits in Verwendung !";
            return false;
          }
        })
        .finally(() => {
          context.rootState.isloading = false;
        });
      return respnse;
    },
    setUserinfo(context, user) {
      context.commit("firstname", user.firstname);
      context.commit("lastname", user.lastname);
      context.commit("department", user.department);
      context.commit("email", user.email);
      context.commit("isAdmin", user.isAdmin);
      context.commit("token", user.token);
    },
    async logIn(context) {
      
      context.rootState.isloading = true;
      const auth = {
        email: context.getters.email,
        password: context.getters.password,
      };
      const response = await service.logIn(auth);

      context.rootState.isloading = false;
      if (response?.status === 200) {
        context.rootState.error = "";
        context.commit("token", response.data.token);
        console.log(context.state.token);
        
      } else if( response?.status === 401) {
        context.rootState.error = "email address or password not correct";
        console.log(context.state.token);
      }else if(response?.status ==400)
      {
         context.rootState.error ="email and password must be filled";
      }
      return null;
    },
    async getProfile(context) {
      // payload have token and userId
      context.rootState.isloading = true;
      const user = await service.getUserProfile();
      // user could be false or object
      if (user) {
        // user.password = payload.password
        context.dispatch("setUserinfo", user);
      }
      context.rootState.isloading = false;
    },
    async updateProfile(context, payload) {
      context.rootState.isloading = true;
      const response = await service.updateProfile(
        payload.token,
        payload.newInfo
      );
      if (response == 400) {
        context.rootState.error =
          "Eines von Ihnen angegeben Felder ist ungültig ! ";
        context.rootState.successMessage = "";
      } else if (response == 409) {
        context.rootState.error = "Die Email-Addresse ist bereits regestriert";
        context.rootState.successMessage = "";
      } else {
        // set data of user in store
        response.password = payload.newInfo.password;
        context.dispatch("setUserinfo", response);
        context.rootState.successMessage = "Profil würde geupdated";
        context.rootState.error = "";
      }
      context.rootState.isloading = false;
    },
  },
  getters: {
    firstname(state) {
      return state.firstname;
    },
    lastname(state) {
      return state.lastname;
    },
    department(state) {
      return state.department;
    },
    email(state) {
      return state.email;
    },
    password(state) {
      return state.password;
    },
    isAdmin(state) {
      return state.isAdmin;
    },
    userId(state) {
      return state.userId;
    },
  },
};
export default authenticaton;