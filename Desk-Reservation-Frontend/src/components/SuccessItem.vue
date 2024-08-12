<template>
  <div class="success">
    <img src="../assets/check(1).png" alt="" />{{ msg }}
  </div>
</template>

<script>
export default {
  name: "successitem",
  props: ["msg"],
};
</script>

<style scoped lang="scss">
.success {
  text-align: center;
  display: flex;
  justify-content: space-around;
  color: white;
  background-color: #1dd384;
  margin: 10px 0px;
  padding: 15px;
  align-content: center;
  border-radius: 5px 15px 5px;
  align-items: center;
}

img {
  margin: 5px;
}
</style>
