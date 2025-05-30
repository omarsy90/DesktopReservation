<template>
  <div class="desks-wrapper row">
    <div class="list-desks">
      <desk-item
      v-for="deskelement in getDesks"
      :key="deskelement.DeskID"
      :desk="deskelement"
    >
    </desk-item>
   
     </div>
  </div>
</template>

<script>
//import TwoColumnLayout from "../components/TwoColumnLayout.vue"
import DeskItem from "./DeskItem.vue";

export default {
  name: "listdesksitem",
  components: {
    DeskItem,
  //  TwoColumnLayout
  },
  computed: {
    getDesks() {
      
      console.log(this.$store.getters["desks/desks"]);
      return this.$store.getters["desks/desks"];
    },
  },

  mounted() {
    this.$store.dispatch("desks/getDesks");
  },
};
</script>

<style scoped>
.list-desks {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;;
 flex-direction: row;
 
}
.desks-wrapper
{
 padding-left: 5%;
 padding-right: 5%;
  
  
}
</style>
