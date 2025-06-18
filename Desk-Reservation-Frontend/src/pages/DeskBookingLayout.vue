<template>
  <div>
 
    <header :class="{ scrollednav: scrolledNav }">
      <nav>
        <h1 class="branding">Desk Booking</h1>
        <ul v-show="!mobile" class="navigation">
          <li>
            <router-link class="link" :to="{ name: `alldesks`}"
              >Startseite</router-link
            >
          </li>
          <li>
            <router-link class="link" :to="{ name: `profile` }"
              >Profil</router-link
            >
          </li>
          <li>
            <router-link class="link" :to="{ name: `reservation` }"
              >Meine Reservierungen</router-link
            >
          </li>

          <li>
            <router-link
              v-if="isAdmin"
              class="link"
              :to="{ name: `admincomment` }"
              >Kommentare</router-link
            >
          </li>

          <li>
            <router-link
              v-if="isAdmin"
              class="link"
              :to="{ name: `adminrequest` }"
              >FixAnfrage</router-link
            >
          </li>

          <li>
            <router-link class="link" :to="{ name: `logout` }"
              >Ausloggen</router-link
            >
          </li>
        </ul>
        <div class="icon">
          <i
            @click="toggleMobileNav"
            v-show="mobile"
            class="far fa-bars"
            :class="{ iconactive: mobileNav }"
          ></i>
        </div>

        <transition name="mobile-nav">
          <ul v-show="mobileNav" class="dropdown-nav">
            <li>
              <router-link class="link" :to="{ name: `alldesks` }"
                >Startseite</router-link
              >
            </li>
            <li>
              <router-link class="link" :to="{ name: `profile` }"
                >Profil</router-link
              >
            </li>
            <li>
              <router-link class="link" :to="{ name: `reservation` }"
                >Reservierungen</router-link
              >
            </li>
            <li>
              <router-link
                v-if="isAdmin"
                class="link"
                :to="{ name: `admincomment` }"
                >Kommentare</router-link
              >
            </li>

            <li>
              <router-link
                v-if="isAdmin"
                class="link"
                :to="{ name: `adminrequest` }"
                >FixAnfrage</router-link
              >
            </li>
            
              <li>
                <router-link class="link" :to="{ name: `logout` }"
                  >Ausloggen</router-link
                >
              </li>
            
          </ul>
        </transition>
      </nav>
    </header>
    <router-link
      :to="{
        name: 'profile',
        params: { useid: `${$store.state.authentication.userId}` },
      }"
    >
    </router-link>
    
 

    <router-view />
       <div >
      
    </div>
  </div>
</template>

<script>
//import SideFilter from "../components/SideFilter.vue"
//import Spinner from "../components/Spinner.vue";
//import ErrorItem from "../components/ErrorItem.vue";



export default {
  components: {
    //  SideFilter
    //Spinner,
    //ErrorItem,
    
    
  },
  name: "navigation",
  data() {
    return {
      scrolledNav: null,
      mobile: null,
      mobileNav: null,
      windowWidth: null,
    };
  },
  props: ["userId"],
  created() {
    window.addEventListener(`resize`, this.checkScreen);
    this.checkScreen();
  },

 
  methods: {
    toggleMobileNav() {
      this.mobileNav = !this.mobileNav;
    },

    checkScreen() {
      this.windowWidth = window.innerWidth;
      if (this.windowWidth <= 850) {
        this.mobile = true;
        return;
      }
      this.mobile = false;
      this.mobileNav = false;
      return;
    },
  },

  computed: {
    isAdmin() {
     return false
    },
  },
};
</script>

<style lang="scss" scoped>
.sidebar-filter {
  position: fixed;
  left: 20px;
  top: 100px;
}

header {
  background: #eaedf5;
  width: 100%;
  transition: 0.5s ease all;
  border-bottom-left-radius: 25px;
  border-bottom-right-radius: 25px;

  nav {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 12px 0;
    transition: 0.5s ease all;

    ul,
    .link {
      font-family: "Montserrat", Regular;
      color: #7e92c1;
      list-style: none;
      text-decoration: none;
    }

    li {
      text-transform: uppercase;
      padding: 16px;
      margin-left: 16px;
    }

    .link:hover {
      border-style: outset;
      padding: 8px;
      color: #eaedf5;
      border-radius: 25px;
      background: #536ead;
      border: none;
      font-family: "Montserrat", Semibold;
    }

    .navigation {
      display: flex;
      align-items: center;
      justify-content: flex-end;
    }

    .branding {
      color: #7e92c1;
      display: flex;
      font-weight: bold;
      align-items: center;
      margin-left: 5px;
    }

    .icon {
      display: flex;
      align-items: center;
      position: absolute;
      right: 40px;

      i {
        cursor: pointer;
        font-size: 24px;
        transition: 0.8s ease all;
      }
    }

    .iconactive {
      transform: rotate(180deg);
    }

    .dropdown-nav {
      display: flex;
      flex-direction: column;
      position: fixed;
      width: 100%;
      max-width: 250px;
      height: 100%;
      background: #eaedf5;
      border-radius: 0 25px;
      margin-top: 0;
      top: 0;
      left: 0;
      z-index: 1000;
       

      li {
        margin-left: 0;
      }
    }

    .mobile-nav-enter-active,
    .mobile-nav-leave-active {
      transition: 1.5s ease all;
    }

    .mobile-nav-enter-from {
      transform: translateX(-350px);
    }

    .mobile-nav-leave-to {
      transform: translateX(-350px);
    }

    .mobile-nav-enter-to {
      transform: translateX(0);
    }

    .dropdown-social {
      display: flex;

      img {
        margin: 5px;
      }
    }
  }
}
.div-load{
  
  text-align: center;
  position: absolute;
   width:fit-content;
   left: 45%;
   top: 20%;
   z-index: 1000;

}


</style>
