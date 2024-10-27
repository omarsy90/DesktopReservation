<template>
  <div id="regist--form" class="register-container">
    <button class="back-button"><router-link to="/login">Zurück</router-link></button>
    <form @submit.prevent="register()">
    <div class="head-container">
      <h2>Registierung</h2>
     </div>
      <div class="input-container">
        <label>Vornamen:</label>
        <input type="text" v-model="$store.state.authentication.firstname" required />
        <span v-if="checkFirstname">Tragen Sie Bitte Ihren Vornamen ein</span>

        <label>Nachnamen:</label>
        <input type="text" v-model="$store.state.authentication.lastname" required />
        <span v-if="checkLastname">Tragen Sie Bitte Ihren Nachnamen ein</span>

        <label>Abteilung:</label>
        <select type="text" v-model="$store.state.authentication.department" required>
          <option value="android">Android</option>
          <option value="web development">Web Development</option>
        </select>
        <span v-if="checkdepartment">Wählen Sie Bitte ihre Abteilung aus</span>

        <label>Email:</label>
        <input type="email" v-model="$store.state.authentication.email" required />
        <span v-if="checkEmail">Tragen Sie Bitte Ihre Email ein</span>
        <span v-else-if="checkTypeEmail">Ungültige Email-Addresse</span>

        <label>Passwort:</label>
        <input type="password" v-model="$store.state.authentication.password" required />
        <span v-if="checkpassword">Bitte Tragen Sie Ihr Passwort ein</span>
        <span v-else-if="checkTypePassword">Passwort muss mindestens 8 Charachter,ein groß
          Buchstabe und eine Zahl haben</span>

        <label>Passwort Wiederholen:</label>
        <input type="password" v-model="confirmPassword" required />
        <span v-if="checkConfirmPassword">Ihr Passwort muss übereinstimmen</span>
      </div>
      <div class="register-button">
        <button type="submit">Registieren</button>
      </div>
      <spinner v-if="$store.state.isloading" class="spinner"> </spinner>
      <erroritem v-else-if="$store.state.error" :msg="$store.state.error"> </erroritem>


    </form>
  </div>
</template>

<script>
  import Spinner from './Spinner.vue'
  import erroritem from './ErrorItem.vue';

  // import Swal from 'sweetalert2'
  export default {
    name: "register",
    components: {
      Spinner,
      erroritem,

    },
    data() {
      return {

        confirmPassword: null,

      };
    },
    methods: {

      register() {
        //event.preventDefault();
        if (this.$store.state.isloading === false) {

          if (!this.checkFirstname && !this.checkLastname && !this.checkdepartment && !this.checkEmail && !this
            .checkTypeEmail && !this.checkpassword && !this.checkTypePassword && !this.checkConfirmPassword) {

            
            this.$store.dispatch('authentication/register').then(
              () => {
               
                if (this.$store.state.isloading === false && this.$store.state.successMessage != '') {
                  this.$router.push('/login');
                }

              }
            );





          }


        }

      }
    },
    computed: {



      checkFirstname: function () {
        
        if (!this.$store.getters["authentication/firstname"] == "") {
          return false;
        }

        return true; //this.isvorname = true
      },
      checkLastname: function () {
        
        if (!this.$store.getters["authentication/lastname"] == "") {
          return false;
        }
        return true;
      },

      checkdepartment: function () {
        if (!this.$store.getters["authentication/department"] == "") {
          return false;
        }
        return true;
      },

      checkEmail: function () {
        if (!this.$store.getters["authentication/email"] == "") {
          return false;
        }
        return true;
      },

      checkTypeEmail: function () {
        const regex = new RegExp("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$");

        const result = String(this.$store.getters['authentication/email']).match(regex);
       

       
        if (result) {
          return false;
        }
        return true



      },

      checkpassword: function () {

        if (!this.$store.getters['authentication/password'] == "") {
          return false
        }
        return true

      },

      checkTypePassword: function () {



        const password = this.$store.getters['authentication/password'];
        var mediumRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})");;
        const result = password.match(mediumRegex);

        if (result) {
          return false
        }
        return true

      },

      checkConfirmPassword: function () {
        if (this.confirmPassword !== this.$store.getters['authentication/password']) {
          return true
        }
        return false
      }
    },
  };
</script>

<style lang="scss" scoped>
  .spinner {
    position: absolute;
    top: 1045px;
  }

  span {
    color: red;
    font-size: 14px;
    margin-bottom: 8px;
  }

  .register-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    padding: 10px;
  }

  form {
    max-width: 700px;
    width: 100%;
    background: #fff;
    border-radius: 5px;
    padding: 25px 30px;
  }

  .head-container{
    font-size: 25px;
    font-weight: bolder;
    color: #f3f3f3;
    background: #536EAD;
    padding: 10px;
    border-top-right-radius: 5px;
    border-top-left-radius: 5px;
    box-shadow: 8px 8px 10px #536ead;
  }

  .input-container {
    margin: 10px 0;
    background: #eaedf5;
    color: #424242;
    padding: 30px;
    border-radius: 5px;
    box-shadow: 8px 8px 10px #536ead;
  }

   label {
      display: flex;
      margin: 8px 0 10px;
      text-transform: uppercase;
      letter-spacing: 1px;
    }

    input, select{
    display: block;
    padding: 10px 6px;
    width: 100%;
    box-sizing: border-box;
    border: none;
    border-radius: 5px;
    color: #424242;
    background: #d4dbea;
}

   .register-button {
    display: flex;
    justify-content: center;

    button {
      background: #A9B7D6;
      color: #f3f3f3;
      margin: 10px;
      padding: 10px;
      width: 20%;
      border: 3px solid #536EAD;
      border-radius: 25px;
      font-size: 18px;
      font-family: "Montserrat", regular;


      &:hover {
        background: #536EAD;
        transition: 0.5s;
      }
    }
  }

  .back-button{
    background: #A9B7D6;
    padding: 10px;
    border: 3px solid #536EAD;
    border-radius: 25px;
    font-size: 18px;
    font-family: "Montserrat", regular;

    a{
      color: #f3f3f3;
      text-decoration: none;
    }

      &:hover {
        background: #536EAD;
        transition: 0.5s;
      }
    }

</style>