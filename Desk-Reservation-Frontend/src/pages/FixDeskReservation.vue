<template>
  <div>
        <button class="back-button"><router-link :to="{ name: `alldesks`}">Zurück</router-link></button>
      <div class="reservation-container">
        <not-found v-if="isNotFound"> </not-found>
        
          
        <error-item v-else-if="bookingError" :msg="bookingError"></error-item>
        <success-item v-else-if="successMessage"  :msg="successMessage"></success-item>

        <div class="fix-container">
        <p>Bitte beachten das Fix Desk auf 3 Monate gebucht werden kann!</p>
            <div>
              <label>label :</label>  <label> {{deskLabel}} </label> <br/>
              <label>office :</label>  <label> {{officeName}} </label>  
             </div>
        <div>
           <Datepicker  confirm format="YYYY-MM-DD" :disabled-date="(date) => date < new Date(Date.now())  " v-model="dateStart">
        </Datepicker>
        </div>
        <div class="fix-booking-bestätigen">
        <input type="checkbox"    @change="onChange()"> als favourite Platz reservieren
        </div>
        
        <button class="forward-button" @click="book()">
          Bestätigen
        </button>
        <spinner v-if="isLoading" > </spinner>
        </div>
      </div>
  </div>
</template>

<script>

import Datepicker from "vue2-datepicker"
import "vue2-datepicker/scss/index.scss";
import Spinner from '../components/Spinner.vue'
import ErrorItem from '../components/ErrorItem.vue'
import SuccessItem from '../components/SuccessItem.vue'
//import moment from "moment";
// import NotFound from "../components/NotFound.vue"
import NotFound from "../components/NotFound.vue";

export default {
  components: {
    Datepicker,
    Spinner,
    ErrorItem,
    SuccessItem ,
     NotFound
  },

  data() {
    return {
     dateStart:"",
      isNotFound: false,
      isFavourited:false,
      deskLabel : "",
      officeName : ""
      
    };
  },
  props: ["deskId"],

  methods: {
    
     getDeskInfo()
    {
      console.log("Desk ID "+this.deskId);
      this.$store.dispatch("deskBooking/getDeskInfo",this.deskId).then(res=>{
         this.deskLabel = res.Label;
         this.officeName = res.Office.Name;
      });
     
    },
    onChange(){
        
        this.isFavourited = !this.isFavourited ;
    },
    book() {
        
        this.$store
        .dispatch(
        "deskBooking/bookFixDesk",{deskId:this.deskId,dtStart:this.dateStart,isFavourited:this.isFavourited});
    },
  },
  
  computed: {

   
  
    isLoading() {
      return this.$store.getters["booking/bookingIsloading"];
    },

    bookingError() {
      return this.$store.getters["deskBooking/bookingError"];
    },

    successMessage() {
      return this.$store.getters["deskBooking/bookingSuccessMessage"];
    },
  },
  mounted() {
      this.$store.dispatch("deskBooking/reset")
  },
  created(){
    console.log("created excuted !!");
    this.getDeskInfo();
  }

};
</script>

<style lang="scss" scoped>
    span{
  
  color: red;

    }
.fix-booking-bestätigen{

  display: flex;
 // background-color: red;
  justify-content: space-between;
  input{

    margin-right: 5px;
    margin-top: 4px;
  }
}
.reservation-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.fix-container {
  display: flex;
  flex-direction: column;
  width: 480px;
  height: 480px;
  margin: 20px;
  background: #eaedf5;
  text-align: center;
  padding: 40px;
  border-radius: 10px;
  box-shadow: 8px 8px 10px #536ead;
  align-items: center;
  justify-content: space-between;

  h5 {
    font-weight: bold;
    margin: 20px;
    padding: 20px;
  }

  p {
    font-weight: bold;
    color: #536ead;
  }
}

.forward-button {
  background: #a9b7d6;
  color: #f3f3f3;
  margin: 20px;
  padding: 10px;
  border: none;
  border-radius: 5px;
  font-size: 20px;

  a {
    text-decoration: none;
    color: #f3f3f3;
  }

  &:hover {
    background: #536ead;
    transition: 0.5s;
  }
}

img {
  margin: 20px;
  width: 50%;
  border-radius: 15px;
  box-shadow: 8px 8px 10px #536ead;
}
.back-button {
    background: #a9b7d6;
    color: #f3f3f3;
    margin: 15px;
    padding: 10px;
    width: 10%;
    border: none;
    border-radius: 15px;
    font-size: 18px;
    font-family: "Montserrat", regular;
    
     a{
        text-decoration: none;
        color: #f3f3f3;
    }


     &:hover{
        border: 2px solid #a9b7d6;
        background:#536EAD;
    }
}
</style>
