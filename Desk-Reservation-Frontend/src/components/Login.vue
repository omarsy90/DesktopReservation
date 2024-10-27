<template>
  <form @submit.prevent="logIn()">
    <success-item
      v-if="$store.state.successMessage"
      :msg="$store.state.successMessage"
    >
    </success-item>
    <error-item v-else-if="$store.state.error" :msg="$store.state.error">
    </error-item>

    <div class="input-container">
      <label>Email:</label>
      <input
        type="email"
        v-model="$store.state.authentication.email"
        required
      />
      <span v-if="checkEmail">Tragen Sie Bitte Ihre Email ein</span>
      <span v-else-if="checkTypeEmail">Ungültige Email-Addresse</span>

      <label>Passwort:</label>
      <input
        type="password"
        v-model="$store.state.authentication.password"
        required
      />
      <span v-if="checkpassword">Tragen Sie Bitte Ihr Passwort ein</span>
      <span v-else-if="checkTypePassword"
        >Passwort muss mindestens 8 Charachter,ein groß Buchstabe und eine Zahl
        haben</span
      >
    </div>

    <div class="checkbox-container">
      <p>Eingeloggt bleiben</p>
      <toggle
        @toggle="changeRemeber()"
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
import SuccessItem from "./SuccessItem.vue";
import ErrorItem from "./ErrorItem.vue";
import Spinner from "./Spinner.vue";
import Toggle from "./Toggle.vue";
import service from "../service/index.js";
export default {
  components: {
    SuccessItem,
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
      if (
        this.$store.state.isloading === false &&
        !this.checkEmail &&
        !this.checkTypeEmail &&
        !this.checkpassword &&
        !this.checkTypePassword
      ) {
        // there is no request running and email and password right wroten

        this.$store.dispatch("authentication/logIn").then((res) => {
          if (res) {
            //res hold token and userId

            if (this.remember) {
              service.saveDataLocalStorage({
                key: "token",
                value: res.token,
              });

              service.saveDataLocalStorage({
                key: "userId",
                value: res.userId,
              });
            } else {
              service.saveDataSessionStorage({
                key: "token",
                value: res.token,
              });

              service.saveDataSessionStorage({
                key: "userId",
                value: res.userId,
              });
            }

            // this.$router.push({
            //   path: `/homepage`
            // }).catch(()=>{   });

            window.location.href = "/homepage";
          } else {
            // there is error in login
          }
        });
      }
    },

    changeRemeber(result) {
      this.remember = result;
    },
  },

  computed: {
    checkEmail: function () {
      if (!this.$store.getters["authentication/email"] == "") {
        return false;
      }
      return true;
    },

    checkTypeEmail: function () {
      const regex = new RegExp("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$");

      const result = String(this.$store.getters["authentication/email"]).match(
        regex
      );

      if (result) {
        return false;
      }
      return true;
    },

    checkpassword: function () {
      if (!this.$store.getters["authentication/password"] == "") {
        return false;
      }
      return true;
    },

    checkTypePassword: function () {
      const password = this.$store.getters["authentication/password"];
      var mediumRegex = new RegExp("^(?=.*[a-z])(?=.*[0-9])(?=.{8,})");
      const result = password.match(mediumRegex);

      if (result) {
        return false;
      }
      return true;
    },
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
