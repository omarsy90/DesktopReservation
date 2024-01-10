import Vue from "vue";
import VueRouter from "vue-router";
import Login from "../components/Login.vue"
import Register from "../components/Register.vue"
import DeskBookingLayout from "../pages/DeskBookingLayout.vue"
//import Profile from "../pages/Profile.vue"
import Profile from "../pages/Profile.vue"
import LogOut from "../components/LogOut.vue"

import {isAuthorised} from './guards.js'
import ListDesksItem from '../components/ListDesksItem.vue'
import ReservationPage from "../pages/ReservationPage.vue"
import FixDeskReservation from "../pages/FixDeskReservation.vue"
import AdminComment from "../pages/AdminComment.vue"
//import AdminDeskRequest from "../pages/AdminDeskRequest.vue"
import AdminRequest from '../components/AdminRequest.vue'

import SideFilter from '../components/SideFilter.vue'
import FlexDeskReservation from "../components/FlexDeskReservation.vue"
import KommentarFilter from "../components/KommentarFilter" 

Vue.use(VueRouter);




const routes = [
  {
    path: "/login",
    name: "login",
    component: Login,
  },
  {
    path: "/register",
    name: "register",
    component: Register,
  },
  {
    path: "/homepage",
    name: "deskbookinglayout",
    beforeEnter: isAuthorised ,
    component: DeskBookingLayout ,
      
       
    children:[
      {
        path:"",
        name:"alldesks",
        components:{
          default:ListDesksItem,
          filter: SideFilter ,
        }

      },  
      {
        path:"profile",
        name:"profile",
        component:Profile ,
      },  
      {
        path: "flexdeskreservation/:deskId",
        name: "flexdeskreservation",
        component: FlexDeskReservation,
        props:true 
        
      },
      {
        path: "fixdeskreservation/:deskId",
        name: "fixdeskreservation",
        component: FixDeskReservation,
        props:true
        },
     
      {
        path: "admincommentsection",
        name: "admincomment",
        components:{
         default:  AdminComment ,
         filter :KommentarFilter 
                   } 
    },
      {
        path: "adminrequest",
        name: "adminrequest",
        component: AdminRequest
      },
      {
        path: "reservation",
        name: "reservation",
        component: ReservationPage
      },      
    ]
  }, 
  {
    path: "/logout",
    name: "logout",
    component: LogOut
  } 
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
