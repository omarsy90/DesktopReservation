<template>
    <div class="card "> 
        <img :src="`${ApiBaseUrl}/File/${desk.Map}`" alt="Avatar" style="width:100%" />
        <div class="container">
            <h4><b>{{desk.Label}} </b></h4>
            <div class="eqipments"> 
            <p v-for="(equipment,index) in desk.Equipments" :key="index" > {{equipment.Feature}} </p>
            
            </div>
        </div>
        <div class="button-container">
          <router-link :to="{name:'fixdeskreservation',params:{deskId:desk.id}} " class="button">Fix Reservierung </router-link>
          <router-link :to="{name:'flexdeskreservation',params:{deskId:desk.id}} " class="button">Flex Reservierung </router-link>
        </div>    
    </div>
</template>


<script> 
// register vue-auth-image directive
    export default {
        name: "deskitem",
        props: ['desk'],
        computed: {
          ApiBaseUrl()
          {
            console.log(`${process.env.VUE_APP_API_BASE_URL}/File/${this.desk.Map}`);
            return process.env.VUE_APP_API_BASE_URL
          }
        },
        methods: {},

    }
</script>

<style scoped lang="scss">
.container {
    display: flex;
    flex-direction: column;
    align-items: center;
    font-size: 20px;
    font-weight: bold;
}
.eqipments {
    display: flex;
  p {
    background: green;
    color: white;
    font-size: 14px;
    margin: 5px;
    padding: 5px;
    border-radius: 5px;
  }
}
.card {
  box-shadow: 4px 4px 5px #536ead;
  transition: 0.3s;
  width: 300px;
  border-radius: 5px;
  padding: 10px;
  margin-top: 30px ;

   ;
}
.card:hover {
  box-shadow: 8px 8px 10px #536ead;
}
img {
  border-radius: 5px 5px 0 0;
}

.button-container{
display: flex;
flex-direction: column;

  .button {
    margin-bottom: 5px;
    background: #A9B7D6;
    color: #F3F3F3;
    padding: 10px;
    border: none;
    border-radius: 5px;
    font-size: 18px;
    font-family: "Montserrat", regular;
    list-style: none;
    text-decoration: none;

    &:hover{
      background:#536EAD;
      transition: 0.5s;
    }
  }
}

@media(max-width: 730px)
{
  .card{
   
    margin: 30px auto;
    
  }
}
</style>