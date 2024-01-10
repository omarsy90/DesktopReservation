<template>
    <div>
        <div class="sidebar-container">
            <button @click="activeTab = `MyReservation`">Meine Reservierungen</button>
            <button @click="activeTab = `OldReservation`">Alte Reservierungen</button>
            <button @click="activeTab = `Favorite`">Meine Favoriten</button>
            <button @click="activeTab = `FixReservation`">Fixe Reservierungen</button>
        </div>
            <MyReservation v-if="activeTab === `MyReservation`"/>
            <OldReservation v-if="activeTab === `OldReservation`"/>
            <Favorite v-if="activeTab === `Favorite`"/>
            <FixDeskReservation v-if="activeTab === `FixReservation`"/>
    </div>
</template>

<script>
import MyReservation from "../components/MyReservation.vue"
import OldReservation from "../components/OldReservation.vue"
import Favorite from "../components/Favorite.vue"
import FixDeskReservation from "../components/FixDeskReservation.vue"

export default {
     data() {
    return {
      activeTab: "MyReservation"
    }
  },
    components: {
        MyReservation,
        OldReservation,
        Favorite,
        FixDeskReservation
       
    }

}
</script>

<style lang="scss" scoped>

h2{
font-family: "Montserrat", bold;
font-size: 24px;
color: #F3F3F3 ;
}

.sidebar-container{
  margin: 10px;
  border-radius: 8px;
  padding: 6px 30px;
  text-align: center;
}

button{
    margin: 5px;
    background: #A9B7D6;
    color: #F3F3F3;
    font-family: "Montserrat", Semibold;
    font-size: 18px;
    border-radius: 15px;
    padding: 10px;
    border:none
}

</style>