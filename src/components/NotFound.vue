<template>
  <div></div>
</template>

<script>
export default {
  name: "notfound",
};
</script>

<style scoped lang="scss">
div {
  position: absolute;
  width: 90%;
  height: 800px;
  left: 15%;
  right: 5%;
  top: 100px;

  background-image: url("../assets/notfound.jpg");
  background-position: cover;
  background-repeat: no-repeat;
  @media only screen and (max-width: 600px) {
    div {
      width: 100%;
    }
  }
}
</style>
