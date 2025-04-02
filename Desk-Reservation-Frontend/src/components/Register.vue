<template>
  <div id="regist--form" class="register-container">
    <button class="back-button"><router-link to="/login">Zurück</router-link></button>
    <form @submit.prevent="register()">
    <div class="head-container">
      <h2>Registier</h2>
     </div>
      <div class="dev-form-container">
        <div class="dev-input">
        <label>First Name: <span>* </span></label>
        <input type="text" v-model="$store.state.authentication.firstname" required />
        </div>
        
        <div class="dev-input">
        <label>Last Name: <span> *</span> </label>
        <input type="text" v-model="$store.state.authentication.lastname" required />
        </div>
         
      <div class="dev-input">
        <label>Department: <span>*</span> </label>
        <select type="text" v-model="$store.state.authentication.department" required>
          <option value="android">Android</option>
          <option value="web development">Web Development</option>
        </select>
         </div>

         <div class="dev-input">
        <label>Email: <span>*</span> </label>
        <input type="email" 
        v-model="$store.state.authentication.email" 
        @input="emailChanged"
        required />
        <span v-if="emailError">email is not correct</span>
         </div>

      <div class="dev-input">
        <label>Password: <span>*</span> </label>
        <input type="password"
         v-model="$store.state.authentication.password"
        @input="passwordChanged"
          required />
        <span v-if="passwordError">password must have at least 8 letters ,One sign and one capital letter </span>
      </div>
        <div class="dev-input">
        <label>Password Confirmation: <span>* </span> </label>
        <input type="password" 
        v-model="confirmPassword" 
        required />
       
        </div>
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
  import utility from '../service/validationService.js'
  export default {
    name: "register",
    components: {
      Spinner,
      erroritem,
      

    },
    data() {
      return {

        confirmPassword: null,
        isEmailEntered : false,
        isPasswordEntered : false,


      };
    },
    methods: {

      register() {
        //event.preventDefault();
        // if (this.$store.state.isloading === false) {

        //   if (!this.checkFirstname && !this.checkLastname && !this.checkdepartment && !this.checkEmail && !this
        //     .checkTypeEmail && !this.checkpassword && !this.checkTypePassword && !this.checkConfirmPassword) {

            
        //     this.$store.dispatch('authentication/register').then(
        //       () => {
               
        //         if (this.$store.state.isloading === false && this.$store.state.successMessage != '') {
        //           this.$router.push('/login');
        //         }

        //       }
        //     );
         // }


        

      },
      emailChanged(){
        this.isEmailEntered = true;
      },
       passwordChanged(){
        this.isPasswordEntered = true;
       }
     
    },
    computed: {

     
   

     

      emailError() {
       let email = this.$store.getters["authentication/email"]  ;
       let isValid = utility.validateEmail(email);
       return !isValid && this.isEmailEntered
       
      },

     

      passwordError() {

       let pass = this.$store.getters['authentication/password']  ;
        let isValid = utility.validatePassword(pass);
        return !isValid && this.isPasswordEntered

      },

     
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
   
   .dev-input{
    margin-top: 30px;
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

  .dev-form-container {
    margin: 10px 0;
    background: #eaedf5;
    color: #424242;
    padding: 30px;
    border-radius: 5px;
    box-shadow: 8px 8px 10px #536ead;
  }

   label {
      display: flex;
      
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