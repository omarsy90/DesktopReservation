import Vue from "vue";
import Vuex from "vuex";
import authentication from "./authentication";
import desks from "./desks"
import booking from './deskBooking';
import altreservation from './altreservation' ;
import myreservation from './current-future-reservation'
import fixreservation from './fixReservation'
import favourite from './favourite.js'
import adminbranch from './adminbranch.js'

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    isloading: false,
    successMessage: "",
    error: "",
  },
  mutations: {},
  actions: {},
  modules: {
    authentication,
    desks,
    booking,
    altreservation,
    myreservation,
    fixreservation,
    favourite ,
    adminbranch,
  },
});
