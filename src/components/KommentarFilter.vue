<template>
  <div>
    <div class="filter-container">
        <h1>Filter</h1>
        <select name="desk" id="desk" @input="deskSelect($event)">
        <option value="">all</option>
        <option v-for="desk in desks" :key="desk.id" :value="desk.id">
            {{ desk.label + "  in Office " + desk.office.name }}
        </option>
        </select>
    </div>
  </div>
</template>

<script>
export default {
  name: "kommentarFilter",

  data() {
    return {};
  },

  methods: {
    deskSelect(event) {
      this.$store.dispatch("adminbranch/getComments", event.target.value);
    },
  },

  computed: {
    desks() {
      return this.$store.getters["adminbranch/desks"];
    },
  },

  mounted() {
    this.$store.dispatch("adminbranch/getAlldesks").then(() => {
      this.$store.dispatch("adminbranch/getComments", "");
    });
  },
};
</script>

<style lang="scss" scoped>
.filter-container{
    display: flex;
    flex-direction: column;
    background: #536ead;
    //margin: 20%;
    padding: 10px;
    border-radius: 5px;
    color: #f3f3f3;
    width: 90%;
    text-align: center;
    margin-top: 5px;

    select {
        margin: 10px;
        padding: 10px;
        border-radius: 5px;
        text-align: center;
    }
}

</style>
