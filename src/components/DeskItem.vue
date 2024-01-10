<template>
    <div class="card"> 
        <img :src="desk.map" alt="Avatar" style="width:100%" />
        <div class="container">
            <h4><b>{{desk.label}} </b></h4>
            <div class="eqipments"> 
            <p v-for="(featre,index) in desk.equipment" :key="index" > {{featre}} </p>
            
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
        computed: {},
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
    background: #A9B7D6;
    color: white;
    font-size: 14px;
    margin: 5px;
    padding: 2px;
    border-radius: 5px;
  }
}
.card {
  box-shadow: 4px 4px 5px #536ead;
  transition: 0.3s;
  width: 250px;
  border-radius: 5px;
  padding: 10px;
  margin: 10px;
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
</style>