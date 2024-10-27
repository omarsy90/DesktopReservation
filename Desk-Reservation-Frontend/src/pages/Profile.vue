<template>
  <div>
    <div class="form_wrapper">
      <div class="form_container">
        <div class="title_container">
          <h2>Mein Profil</h2>
        </div>
        <div class="row clearfix">
          <div class="">
            <form>
              <div class="row clearfix" :style="{ display: readFormDispalyed }">
                <div class="col_half">
                  <div class="input_field">
                    <span><i aria-hidden="true" class="fa fa-user"></i></span>
                    <input
                      type="text"
                      name="name"
                      placeholder="Vorname"
                      disabled
                      :value="firstname"
                    />
                  </div>
                </div>
                <div class="col_half">
                  <div class="input_field">
                    <span><i aria-hidden="true" class="fa fa-user"></i></span>
                    <input
                      type="text"
                      name="name"
                      placeholder="Nachname"
                      disabled
                      :value="lastname"
                    />
                  </div>
                </div>
              </div>

              <!--  mein code !-->
              <div class="row clearfix" :style="{ display: isDisplayed }">
                <div class="col_half">
                  <div class="input_field">
                    <span><i aria-hidden="true" class="fa fa-user"></i></span>
                    <input
                      type="text"
                      name="name"
                      placeholder="Vorname"
                      v-model="firstname"
                    />
                    <b v-if="checkFirstname"
                      >Tragen Sie Bitte Ihren Vornamen ein</b
                    >
                  </div>
                </div>
                <div class="col_half">
                  <div class="input_field">
                    <span><i aria-hidden="true" class="fa fa-user"></i></span>
                    <input
                      type="text"
                      name="name"
                      placeholder="Nachname"
                      required
                      v-model="lastname"
                    />
                    <b v-if="checkLastname"
                      >Tragen Sie Bitte Ihren Nachnamen ein</b
                    >
                  </div>
                </div>
              </div>

              <div
                class="input_field select_option"
                :style="{ display: readFormDispalyed }"
              >
                <select disabled v-model="department">
                  <option value="">Bitte wählen Sie Ihre Abteilung aus</option>
                  <option value="Android">Android</option>
                  <option value="Web Development">Web Development</option>
                </select>
                <div class="select_arrow"></div>
              </div>

              <!--  select for editig!-->
              <div
                class="input_field select_option"
                :style="{ display: isDisplayed }"
              >
                <select v-model="department">
                  <option value="">Select a department</option>
                  <option value="Android">Android</option>
                  <option value="Web Development">Web Development</option>
                </select>
                <div class="select_arrow"></div>
                <b v-if="checkdepartment"
                  >Wählen Sie Bitte Ihre Abteilung aus</b
                >
              </div>

              <div class="input_field" :style="{ display: readFormDispalyed }">
                <span><i aria-hidden="true" class="fa fa-envelope"></i></span>
                <input
                  type="email"
                  name="email"
                  placeholder="Email"
                  :value="email"
                  disabled
                />
              </div>
              <!-- email for editing !-->
              <div class="input_field" :style="{ display: isDisplayed }">
                <span><i aria-hidden="true" class="fa fa-envelope"></i></span>
                <input
                  type="email"
                  name="email"
                  placeholder="Email"
                  v-model="email"
                  required
                />
                <b v-if="checkEmail"
                  >Tragen Sie Bitte Ihre Email-Addresse ein</b
                >
                <b v-else-if="checkTypeEmail"
                  >Diese Email-Addresse ist ungültig</b
                >
              </div>

              <div class="input_field" :style="{ display: readFormDispalyed }">
                <span><i aria-hidden="true" class="fa fa-lock"></i></span>
                <input
                  type="password"
                  name="password"
                  placeholder="Password"
                  value="********"
                  disabled
                />
              </div>
              <!-- password for editing !-->

              <div class="input_field" :style="{ display: isDisplayed }">
                <span><i aria-hidden="true" class="fa fa-lock"></i></span>
                <input
                  type="password"
                  name="password"
                  placeholder="Password"
                  v-model="password"
                />
                <b v-if="checkpassword">Tragen Sie Bitte die nue Passwort ein</b>
                <b v-else-if="checkTypePassword">
                  Ihr Passwort muss mindestens 8 Charachter,ein groß Buchstabe
                  und aus einer Zahl bestehen</b
                >
              </div>

              <!--  to confirm password  !-->
             

              <input
                class="button"
                type="submit"
                value="Daten ändern"
                :style="{ display: readFormDispalyed }"
                @click="changeForm($event)"
              />
              <input
                class="button button--update"
                type="submit"
                value="Update"
                :style="{ display: isDisplayed }"
                @click="update($event)"
              />
              <input
                class="button button--back"
                type="submit"
                value="Back"
                :style="{ display: isDisplayed }"
                @click="backToReadForm($event)"
              />
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "profile",

  data() {
    return {
      displayEditForm: "none",
      displayReadForm: "block",

      firstname: "",
      lastname: "",
      department: "",
      email: "",
      password: "",
     
      isAdmin: false,
    };
  },
  methods: {
    update(event) {
      event.preventDefault();

      if (
        !this.checkFirstname &&
        !this.checkLastname &&
        !this.checkdepartment &&
        !this.checkEmail &&
        !this.checkTypeEmail &&
        !this.checkpassword &&
        !this.checkTypePassword 
        
      ) {
        if (this.$store.state.isloading === false) {
          const payload = {
          
            newInfo: {
              firstname: this.firstname,
              lastname: this.lastname,
              department: this.department,
              email: this.email,
              password: this.password,
            },
          };

          this.$store.dispatch("authentication/updateProfile", payload).then(()=>{

              setTimeout(()=>{
                 window.location.reload() ;
              },1500)
          })
        }
      }
    },

    changeForm(event) {
      event.preventDefault();

     
      this.displayEditForm = "block";
      this.displayReadForm = "none";
      this.getUserData();
    },
    backToReadForm(event) {
      event.preventDefault();
      this.displayEditForm = "none";
      this.displayReadForm = "block";
      this.getUserData();
    },

    getUserData() {
      this.firstname = this.$store.getters["authentication/firstname"];
      this.lastname = this.$store.getters["authentication/lastname"];
      this.department = this.$store.getters["authentication/department"];
      this.email = this.$store.getters["authentication/email"];
      
      this.isAdmin = this.$store.getters["authentication/isAdmin"];
     
    },
  },

  computed: {
    isDisplayed() {
      return this.displayEditForm;
    },
    readFormDispalyed() {
      return this.displayReadForm;
    },

    checkFirstname: function () {
      if (this.firstname) {
        return false;
      }

      return true; //this.isvorname = true
    },
    checkLastname: function () {
      if (this.lastname) {
        return false;
      }
      return true;
    },

    checkdepartment: function () {
      if (this.department) {
        return false;
      }
      return true;
    },

    checkEmail: function () {
      if (this.email) {
        return false;
      }
      return true;
    },

    checkTypeEmail: function () {
      const regex = new RegExp("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$");

      const ismatched = String(this.email).match(regex);

      if (ismatched) {
        return false;
      }
      return true;
    },

    checkpassword: function () {
      if (!this.password == "") {
        return false;
      }
      return true;
    },

    checkTypePassword: function () {
      const password = this.password;
      var mediumRegex = new RegExp(
        "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})"
      );
      const ismatched = password.match(mediumRegex);

      if (ismatched) {
        return false;
      }
      return true;
    },

   
  },

  mounted() {
    this.$store.dispatch("authentication/getProfile").then(() => {
      this.getUserData();
     
    });
  },
};
</script>
<style scoped lang="scss">
$yellow: #f5ba1a;
$black: #000000;
$grey: #cccccc;

b {
  color: red;
}

h2 {
  background-color: black;
  color: #f3f3f3;
}

.button--back {
  background-color: red;
}

.clearfix {
  &:after {
    content: "";
    display: block;
    clear: both;
    visibility: hidden;
    height: 0;
  }
}

.form_wrapper {
  background: #fff;
  box-sizing: border-box;
  padding: 35px;
  margin: 8% auto;
  width: 50%;
  border-top: 5px solid $yellow;
  -webkit-box-shadow: 0 0 3px rgba(0, 0, 0, 0.1);
  -moz-box-shadow: 0 0 3px rgba(0, 0, 0, 0.1);
  box-shadow: 0 0 3px rgba(0, 0, 0, 0.1);
  -webkit-transform-origin: 50% 0%;
  transform-origin: 50% 0%;
  -webkit-transform: scale3d(1, 1, 1);
  transform: scale3d(1, 1, 1);
  -webkit-transition: none;
  transition: none;
  -webkit-animation: expand 0.8s 0.6s ease-out forwards;
  animation: expand 0.8s 0.6s ease-out forwards;
  opacity: 0;

  h2 {
    font-size: 1.5em;
    line-height: 1.5em;
    margin: 0;
  }

  .title_container {
    text-align: center;
    padding-bottom: 15px;
  }

  h3 {
    font-size: 1.1em;
    font-weight: normal;
    line-height: 1.5em;
    margin: 0;
  }

  label {
    font-size: 12px;
  }

  .row {
    margin: 10px -15px;

    > div {
      padding: 0 15px;
      box-sizing: border-box;
    }
  }

  .col_half {
    width: 50%;
    float: left;
  }

  .input_field {
    position: relative;
    margin-bottom: 20px;
    -webkit-animation: bounce 0.6s ease-out;
    animation: bounce 0.6s ease-out;

    > span {
      position: absolute;
      left: 0;
      top: 0;
      color: #333;
      height: 90%;
      //   border-right: 1px solid $grey;
      text-align: center;
      width: 30px;

      > i {
        padding-top: 10px;
      }
    }
  }

  .textarea_field {
    > span {
      > i {
        padding-top: 10px;
      }
    }
  }

  input {
    &[type="text"],
    &[type="email"],
    &[type="password"] {
      width: 100%;
      padding: 8px 10px 9px 35px;
      height: 35px;
      border: 1px solid $grey;
      box-sizing: border-box;
      outline: none;
      -webkit-transition: all 0.3s ease-in-out;
      -moz-transition: all 0.3s ease-in-out;
      -ms-transition: all 0.3s ease-in-out;
      transition: all 0.3s ease-in-out;
    }

    &[type="text"]:hover,
    &[type="email"]:hover,
    &[type="password"]:hover {
      background: #fafafa;
    }

    &[type="text"]:focus,
    &[type="email"]:focus,
    &[type="password"]:focus {
      -webkit-box-shadow: 0 0 2px 1px rgba(255, 169, 0, 0.5);
      -moz-box-shadow: 0 0 2px 1px rgba(255, 169, 0, 0.5);
      box-shadow: 0 0 2px 1px rgba(255, 169, 0, 0.5);
      border: 1px solid $yellow;
      background: #fafafa;
    }

    &[type="submit"] {
      // background: $yellow;
      height: 35px;
      line-height: 35px;
      width: 100%;
      border: none;
      outline: none;
      cursor: pointer;
      color: #fff;
      font-size: 1.1em;
      margin-bottom: 10px;
      -webkit-transition: all 0.3s ease-in-out;
      -moz-transition: all 0.3s ease-in-out;
      -ms-transition: all 0.3s ease-in-out;
      transition: all 0.3s ease-in-out;

      &:hover {
        background: darken($yellow, 7%);
      }

      &:focus {
        background: darken($yellow, 7%);
      }
    }

    &[type="checkbox"],
    &[type="radio"] {
      border: 0;
      clip: rect(0 0 0 0);
      height: 1px;
      margin: -1px;
      overflow: hidden;
      padding: 0;
      position: absolute;
      width: 1px;
    }
  }
}

.form_container {
  .row {
    .col_half.last {
      border-left: 1px solid $grey;
    }
  }
}

.checkbox_option {
  label {
    margin-right: 1em;
    position: relative;

    &:before {
      content: "";
      display: inline-block;
      width: 0.5em;
      height: 0.5em;
      margin-right: 0.5em;
      vertical-align: -2px;
      border: 2px solid $grey;
      padding: 0.12em;
      background-color: transparent;
      background-clip: content-box;
      transition: all 0.2s ease;
    }

    &:after {
      border-right: 2px solid $black;
      border-top: 2px solid $black;
      content: "";
      height: 20px;
      left: 2px;
      position: absolute;
      top: 7px;
      transform: scaleX(-1) rotate(135deg);
      transform-origin: left top;
      width: 7px;
      display: none;
    }
  }

  input {
    &:hover + label:before {
      border-color: $black;
    }

    &:checked + label {
      &:before {
        border-color: $black;
      }

      &:after {
        -moz-animation: check 0.8s ease 0s running;
        -webkit-animation: check 0.8s ease 0s running;
        animation: check 0.8s ease 0s running;
        display: block;
        width: 7px;
        height: 20px;
        border-color: $black;
      }
    }
  }
}

.radio_option {
  label {
    margin-right: 1em;

    &:before {
      content: "";
      display: inline-block;
      width: 0.5em;
      height: 0.5em;
      margin-right: 0.5em;
      border-radius: 100%;
      vertical-align: -3px;
      border: 2px solid $grey;
      padding: 0.15em;
      background-color: transparent;
      background-clip: content-box;
      transition: all 0.2s ease;
    }
  }

  input {
    &:hover + label:before {
      border-color: $black;
    }

    &:checked + label:before {
      background-color: $black;
      border-color: $black;
    }
  }
}

.select_option {
  position: relative;
  width: 100%;

  select {
    display: inline-block;
    box-sizing: border-box;
    margin: 0;
    width: 100%;
    height: 40px;
    font-size: 16px;
    font-family: Arial, Helvetica, sans-serif;

    cursor: pointer;
    color: #7b7b7b;
    border: 1px solid $grey;
    border-radius: 0;
    background: #fff;
    appearance: none;
    -webkit-appearance: none;
    -moz-appearance: none;
    transition: all 0.2s ease;

    &::-ms-expand {
      display: none;
    }

    &:hover,
    &:focus {
      color: $black;
      background: #fafafa;
      border-color: $yellow;
      outline: none;
    }
  }
}

.select_arrow {
  position: absolute;
  top: calc(50% - 11px);
  right: 15px;
  width: 0;
  height: 0;
  pointer-events: none;
  border-width: 8px 5px 0 5px;
  border-style: solid;
  border-color: #7b7b7b transparent transparent transparent;
}

.select_option select {
  &:hover + .select_arrow,
  &:focus + .select_arrow {
    border-top-color: $black;
  }
}

.credit {
  position: relative;
  z-index: 1;
  text-align: center;
  padding: 15px;
  color: $yellow;

  a {
    color: darken($yellow, 7%);
  }
}

@-webkit-keyframes check {
  0% {
    height: 0;
    width: 0;
  }

  25% {
    height: 0;
    width: 7px;
  }

  50% {
    height: 20px;
    width: 7px;
  }
}

@keyframes check {
  0% {
    height: 0;
    width: 0;
  }

  25% {
    height: 0;
    width: 7px;
  }

  50% {
    height: 20px;
    width: 7px;
  }
}

@-webkit-keyframes expand {
  0% {
    -webkit-transform: scale3d(1, 0, 1);
    opacity: 0;
  }

  25% {
    -webkit-transform: scale3d(1, 1.2, 1);
  }

  50% {
    -webkit-transform: scale3d(1, 0.85, 1);
  }

  75% {
    -webkit-transform: scale3d(1, 1.05, 1);
  }

  100% {
    -webkit-transform: scale3d(1, 1, 1);
    opacity: 1;
  }
}

@keyframes expand {
  0% {
    -webkit-transform: scale3d(1, 0, 1);
    transform: scale3d(1, 0, 1);
    opacity: 0;
  }

  25% {
    -webkit-transform: scale3d(1, 1.2, 1);
    transform: scale3d(1, 1.2, 1);
  }

  50% {
    -webkit-transform: scale3d(1, 0.85, 1);
    transform: scale3d(1, 0.85, 1);
  }

  75% {
    -webkit-transform: scale3d(1, 1.05, 1);
    transform: scale3d(1, 1.05, 1);
  }

  100% {
    -webkit-transform: scale3d(1, 1, 1);
    transform: scale3d(1, 1, 1);
    opacity: 1;
  }
}

@-webkit-keyframes bounce {
  0% {
    -webkit-transform: translate3d(0, -25px, 0);
    opacity: 0;
  }

  25% {
    -webkit-transform: translate3d(0, 10px, 0);
  }

  50% {
    -webkit-transform: translate3d(0, -6px, 0);
  }

  75% {
    -webkit-transform: translate3d(0, 2px, 0);
  }

  100% {
    -webkit-transform: translate3d(0, 0, 0);
    opacity: 1;
  }
}

@keyframes bounce {
  0% {
    -webkit-transform: translate3d(0, -25px, 0);
    transform: translate3d(0, -25px, 0);
    opacity: 0;
  }

  25% {
    -webkit-transform: translate3d(0, 10px, 0);
    transform: translate3d(0, 10px, 0);
  }

  50% {
    -webkit-transform: translate3d(0, -6px, 0);
    transform: translate3d(0, -6px, 0);
  }

  75% {
    -webkit-transform: translate3d(0, 2px, 0);
    transform: translate3d(0, 2px, 0);
  }

  100% {
    -webkit-transform: translate3d(0, 0, 0);
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
}

@media (max-width: 600px) {
  .form_wrapper {
    .col_half {
      width: 100%;
      float: none;
    }
  }

  .bottom_row {
    .col_half {
      width: 50%;
      float: left;
    }
  }

  .form_container {
    .row {
      .col_half.last {
        border-left: none;
      }
    }
  }

  .remember_me {
    padding-bottom: 20px;
  }
}

.button {
  background-color: $yellow;

  &:hover {
    background-color: #6b5824;
  }
}

.button--back {
  background-color: red;

  &:hover {
    background-color: rgb(224, 86, 86);
  }
}

.button--update {
  background-color: green;

  &:hover {
    background-color: rgb(56, 145, 90);
  }
}
</style>
