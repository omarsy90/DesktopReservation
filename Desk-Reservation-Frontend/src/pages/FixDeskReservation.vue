<template>
  <div>
        <button class="back-button"><router-link :to="{ name: `alldesks`}">Zurück</router-link></button>
      <div class="reservation-container">
        <not-found v-if="isNotFound"> </not-found>
        
          
        <error-item v-else-if="bookingError" :msg="bookingError"></error-item>
        <success-item v-else-if="successMessage"  :msg="successMessage"></success-item>

        <div class="fix-container">
        <p>Bitte beachten das Fix Desk auf 3 Monate gebucht werden kann!</p>
        <div class="fix-booking-bestätigen">
        <input type="checkbox" checked   @change="onChange()">I want to declare this desk as my fix desk:
        </div>
        <span v-if="!isChecked"> Sie sollen checkbox aktivieren </span>
        <button class="forward-button" @click="book()">
          Bestätigen
        </button>
        <spinner v-if="isLoading" > </spinner>
        </div>
      </div>
  </div>
</template>

<script>
//import Datepicker from "vue2-datepicker";
import "vue2-datepicker/scss/index.scss";
import Spinner from '../components/Spinner.vue'
import ErrorItem from '../components/ErrorItem.vue'
import SuccessItem from '../components/SuccessItem.vue'
//import moment from "moment";
// import NotFound from "../components/NotFound.vue"
import NotFound from "../components/NotFound.vue";

export default {
  components: {
    //Datepicker,
    Spinner,
    ErrorItem,
    SuccessItem ,
     NotFound
  },

  data() {
    return {
     
      isNotFound: false,
      isChecked:true
      
    };
  },
  props: ["deskId"],

  methods: {

    onChange(){
        
        this.isChecked = !this.isChecked ;
    },
    book() {
      if (this.isChecked) {
        // booking as fix platz
           const comment = "I want to declare this desk as my fix desk:"
        this.$store.dispatch("booking/fixDeskBooking",comment);
      } 
    },
  },
  
  computed: {
    getDate() {
      return this.dateStart;
    },
    getStatus() {
      return this.isNotFound;
    },

    isLoading() {
      return this.$store.getters["booking/bookingIsloading"];
    },

    bookingError() {
      return this.$store.getters["booking/bookingError"];
    },

    successMessage() {
      return this.$store.getters["booking/bookingSuccessMessage"];
    },
  },
  mounted() {
    
    const id = this.deskId;
    
    
    
      //intialdata for fix-reservation

      this.$store.dispatch("booking/getSelectedDesk", id).then(() => {
        if (!this.$store.getters["booking/selectedDesk"]) {
          // desk with id given in url is not valid
          this.isNotFound = true;
        }
      });   
  },


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
