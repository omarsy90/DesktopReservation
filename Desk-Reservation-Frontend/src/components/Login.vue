<template>
  <form @submit.prevent="logIn()">
    
   
    <error-item v-if="$store.state.error" :msg="$store.state.error">
    </error-item>

    <div class="input-container">
      <label>Email:</label>
      <input
        type="email"
        v-model="$store.state.authentication.email"
        required
      />
     <span v-if="emailError">email is not correct</span>
      

      <label>Passwort:</label>
      <input
        type="password"
        v-model="$store.state.authentication.password"
        required
      />
      <span v-if="passwordError">password must have at least 8 letters ,One sign and one capital letter </span>
      
      
    </div>

    <div class="checkbox-container">
      <p>Eingeloggt bleiben</p>
      <toggle
       
        :ischecked="remember"
        v-on:toggle="changeRemeber($event)"
      >
      </toggle>
    </div>
    <div class="login-button">
      <spinner class="spinner" v-if="$store.state.isloading"> </spinner>
      <button type="sumit">Login</button>
    </div>
    <div class="links">
      <p>
        Falls Sie noch kein Konto besitzen, dann gehts hier zur
        <router-link to="/register">Registierung ?</router-link>
      </p>
    </div>
  </form>
</template>

<script>

import ErrorItem from "./ErrorItem.vue";
import Spinner from "./Spinner.vue";
import Toggle from "./Toggle.vue";
import service from "../service/strorageService.js";
export default {
  components: {
    ErrorItem,
    Spinner,
    Toggle,
  },
  data() {
    return {
      remember: false,
      
    };
  },
  methods: {
    logIn() {
       {
        // there is no request running and email and password right wroten
      console.log("user try login ")
        this.$store.dispatch("authentication/logIn").then((isLogged) => {
          if (isLogged) {
           let token = this.$store.getters["authentication/token"];

            if (this.remember) {
              service.saveDataLocalStorage({
                key: "token",
                value: token
              });
            } else {
              service.saveDataSessionStorage({
                key: "token",
                value: token,
              });

            }
            window.location.href = "/";
          }
        });
      }
    },

    changeRemeber(isCheecked) {
      this.remember = isCheecked;
    },
  },

  computed: {
    emailError()
    {
      let email = this.$store.getters["authentication/email"];
      const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      let isValid = emailRegex.test(email);
      return !isValid
    } ,
    passwordError()
    {
      let pass = this.$store.getters["authentication/password"]
      const passwordRegex = /^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?":{}|<>])[A-Za-z\d!@#$%^&*(),.?":{}|<>]{8,}$/;
      let isvalid = passwordRegex.test(pass);
      return !isvalid
    }

     
  },
};
</script>

<style lang="scss" scoped>
.spinner {
  position: absolute;
  top: 58%;
  left: 47%;
  color: #d4dbea;
}

p {
  margin: 0;
}

span {
  color: red;
}

form {
  max-width: 480px;
  margin: 50px auto;
  background: #eaedf5;
  text-align: left;
  padding: 40px;
  border-radius: 10px;
  box-shadow: 8px 8px 10px #536ead;
}

.input-container {
  display: flex;
  flex-direction: column;

  input {
    margin-bottom: 20px;
    padding: 18px;
    border-radius: 5px;
    border: none;
    background: #d4dbea;
    color: #424242;
    font-family: "Montserrat", light;
    font-size: 24px;
    text-align: center;
  }

  label {
    color: #424242;
    display: inline-block;
    margin: 25px 0 15px;
    text-transform: uppercase;
    letter-spacing: 1px;
    font-weight: bold;
  }
}
.checkbox-container {
  display: flex;
  justify-content: end;
  align-items: center;
  font-family: "Montserrat", regular;
  font-size: 16px;
  margin: 20px 0;

  input {
    color: #424242;
    width: 15px;
    height: 15px;
  }
}

.login-button {
  text-align: center;

  button {
    margin: 10px;
    width: 165px;
    background: #536ead;
    color: #f3f3f3;
    font-family: "Montserrat", regular;
    font-size: 24px;
    border-radius: 15px;
    padding: 10px;
    border: none;
  }
}

.links {
  margin: 10px 0;
  list-style: none;
  text-align: center;
}
</style>
