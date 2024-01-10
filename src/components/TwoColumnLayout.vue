<template>
  <div class="container-fluid">
    <div class="row two-column-layout">
      <aside class="col-sm-3">
        <slot name="aside"></slot>
      </aside>
      <main class="col-12 col-sm">
        <slot></slot>
      </main>
    </div>
  </div>
</template>

<script>
export default {
  name: "twocolumnlayout",

  mounted() {},

  methods: {
    dosome(event) {
      event.preventDefault();
    },
   
    
  },
};
</script>

<style scoped></style>
