import service from "../service/authenticationService.js";
const authenticaton = {
  namespaced: true,
  state: () => ({
    token: "",
    firstname: "",
    lastname: "",
    username :"",
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
    username(state, username){
      state.username = username;

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
      const newUser = {
        firstName: context.getters.firstname,
        lastName: context.getters.lastname,
        userName: context.getters.username,
        email: context.getters.email,
        password: context.getters.password,
        department: context.getters.department,
      };
      service
        .register(newUser)
        .then((res) => {
          
          if (res.status >= 200 && res.status <= 204) 
            {
            context.rootState.successMessage =
              "user has been successfully registered";
            context.rootState.error = "";
          }else
          {
          context.rootState.error = res?.data?.message;
          context.rootState.successMessage=""; 
          }
        })
        .finally(() => {
          context.rootState.isloading = false;
        });
    },
    setUserinfo(context, user) {
      context.commit("firstname", user.firstname);
      context.commit("lastname", user.lastname);
      context.commit("department", user.department);
      context.commit("email", user.email);
      context.commit("isAdmin", user.isAdmin);
      context.commit("token", user.token);
    },
    async logIn(context ) {
      
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
        return true ;
      } else if( response?.status === 401) {
        context.rootState.error = "email address or password not correct";
      }
      return false;
    },


  },
  getters: {
    firstname(state) {
      return state.firstname;
    },
    lastname(state) {
      return state.lastname;
    },

    username(state){
      return state.username
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
    token(state) {
      return state.token;
    },
  },
};
export default authenticaton;