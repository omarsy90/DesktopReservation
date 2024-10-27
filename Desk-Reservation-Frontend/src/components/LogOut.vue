<template>
  <div class="text-center">Bye bye! 👋 </div>
</template>

<script>
  export default {

    name: "logout",

    mounted() {
         
         window.localStorage.clear();
         window.sessionStorage.clear();
        window.location.href="/login"
    }


  }
</script>