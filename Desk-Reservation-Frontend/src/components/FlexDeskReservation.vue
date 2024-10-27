<template>
  <div>
    <!-- !-->
    <not-found v-if="isNotFound"> </not-found>
     <div class="status">
    
    
    <error-item v-if="bookingError" :msg="bookingError"></error-item>
    <success-item v-else-if="successMessage" :msg="successMessage"></success-item>
     </div>
    <!--    !-->


    <button class="back-button"><router-link :to="{ name: `alldesks`}">Zurück</router-link></button>
    <div class="desk-container"  v-if="!isNotFound">
      <div class="flex-container">
        <p>Bitte beachten das Flex Desk nur für 3 Tage gebucht werden kann!</p>
        <h5>Bitte deine Resevierungsdauer auswählen:</h5>
        <Datepicker format="YYYY-MM-DD" :disabled-date="(date) => date < new Date(Date.now())  " v-model="dateStart">
        </Datepicker>
        <select v-model="period">
          <option value="1">1 Tag</option>
          <option value="2">2 Tage</option>
          <option value="3">3 Tage</option>
        </select>
        <button class="forward-button" @click="book()">
          Bestätigen
        </button>
         <spinner v-if="isLoading"> </spinner>

        <div class="checkbox">
          <input type="checkbox"   @change="onChange()"  v-if="!favourited" />
          <label v-if="!favourited">Als Favorit speichern</label>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Datepicker from "vue2-datepicker"
  import "vue2-datepicker/scss/index.scss"
  import moment from "moment";
  // import DatePicker from './DatePicker.vue'
import NotFound from "./NotFound.vue";
import Spinner from "./Spinner.vue";
import ErrorItem from "./ErrorItem.vue";
import SuccessItem from "./SuccessItem.vue";

  export default {
    name: 'felxdeskreservation',
    components: {
      Datepicker,
      NotFound,
      Spinner,
      ErrorItem,
      SuccessItem,
    },

    data() {
      return {
        dateStart: "",
        period: 1,
        isNotFound: false,
        isFavourited:false,
        
           favourited:false
      };
    },
    props: ['deskId'],
    computed: {
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

    methods: {

      
      onChange(){
       
          this.isFavourited = !this.isFavourited ;
          

      },

      book() {
        const dateStart = moment(this.dateStart).format('YYYY-MM-DD')
        let dateEnd = Date.parse(dateStart) + Math.max(parseInt(this.period) - 1, 0) * 24 * 3600 * 1000;
        dateEnd = moment(new Date(dateEnd)).format("YYYY-MM-DD");
        this.$store.dispatch("booking/dateStart",dateStart);
        this.$store.dispatch("booking/dateEnd",dateEnd) ;

       this.$store.dispatch("booking/bookingFlixDesk",this.isFavourited);

      }

    },
    mounted() {
       
      // this.favourited = this.$router.query.favourited
        this.favourited =  this.$router.history.current.query.favourited
      const id = this.deskId;

      this.$store.dispatch("booking/getSelectedDesk", id).then(() => {

        if (!this.$store.getters["booking/selectedDesk"]) {
          // desk with id given in url is not valid
          this.isNotFound = true;
        }
       
     this.$store.dispatch('booking/bookingError','');
     this.$store.dispatch('booking/successMessage','');
      });


    }
  };
</script>

<style lang="scss" scoped>

   .status{
  display: flex;
    justify-content: center;
    align-items: center;
    

   }
  .desk-container {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 8%;
  }

  .flex-container {
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

  select {
    margin-top: 10px;
    width: 52%;
    padding: 5px;
    border-radius: 5px;
    text-align: center;
  }

  .checkbox {
    font-size: 14px;
    text-align: center;

    label {
      margin-left: 5px;
    }
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