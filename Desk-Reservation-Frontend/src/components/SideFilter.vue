<template>
  <div class="filter-container">
    <h2>Filter</h2>
    
    <select    id="select"    @input="filter()">
      <option  value=""> all </option>
         <option  v-for="office in getOffices" :key="office.id" :value="office.id"> {{office.name}}</option>
      <option>KL</option>
    </select>
   <b> von : </b> <Datepicker v-model="$store.state.desks.selectedMinDatum"
     :disabled-date="date => (date)<= new Date(Date.now())" 
     format="YYYY-MM-DD"    @input="filter()">
     </Datepicker>
      <br>

   <b> bis : </b>
   <Datepicker v-model="$store.state.desks.selectedMaxDatum"
     :disabled-date="date => (date)<= new Date(Date.now())|| date <  getmindDate " 
     format="YYYY-MM-DD"   @input="filter()">
     </Datepicker>

  </div>
</template>

<script>
  import Datepicker from "vue2-datepicker"
  import "vue2-datepicker/scss/index.scss"

export default {
  components:{
    Datepicker
  },

  data() {
    return {
         
    }
  },

  methods:{

 
     
      filter(){
         
         //  :)->-<

        const  stiegeId = document.getElementById('select').value
           this.$store.dispatch('desks/setStiege',stiegeId) ;
           const payload = {
     
  
      from: this.$store.getters['desks/MinDatum'] ,
      to: this.$store.getters['desks/MaxDatum'],
      stiegeId: this.$store.getters['desks/StiegeId'],


    }   
       
    
        this.$store.dispatch('desks/felterDesks',payload) ;

      },

    
  },

  computed:{

   
   getmindDate(){
     return this.$store.getters['desks/MinDatum'] ;
   },


   getOffices(){

     return this.$store.getters['desks/officesArray'] ;
   }

  },

  mounted(){

    this.$store.dispatch('desks/getAllOffices') ;
  }
  

}
</script>

<style lang="scss" scoped>

h2{
font-family: "Montserrat", bold;
font-size: 24px;
color: #F3F3F3 ;
}

.filter-container{
  margin: 10px;
  display: flex;
  flex-direction: column;
  background: #959aa7;
  border-radius: 10px;
  padding: 10px;
  text-align: center;
  width: 300px;
}

select{
  margin-bottom: 25px;
  padding: 6px;
  font-family: "Montserrat",SemiBold ;
  font-size: 1rem;
  border-radius: 4px;
}

b{
  color: #F3F3F3;
}
</style>