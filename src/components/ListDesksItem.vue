<template>
  <TwoColumnLayout>
  <div class="list-desks">
      <desk-item
      v-for="deskelement in getDesks"
      :key="deskelement.id"
      :desk="deskelement"
    >
    </desk-item>
  </div>
  </TwoColumnLayout>
</template>

<script>
import TwoColumnLayout from "../components/TwoColumnLayout.vue"
import DeskItem from "./DeskItem.vue";

export default {
  name: "listdesksitem",
  components: {
    DeskItem,
    TwoColumnLayout
  },
  computed: {
    getDesks() {
      return this.$store.getters["desks/desks"];
    },
  },

  mounted() {
  

    

    this.$store.dispatch("desks/setDesks");

    // test

   
    const payload = { 
      from: "",
      to: "",
      stiege: "",
    };
    this.$store.dispatch("desks/felterDesks", payload);
  },
};
</script>

<style scoped>
.list-desks {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}
</style>
