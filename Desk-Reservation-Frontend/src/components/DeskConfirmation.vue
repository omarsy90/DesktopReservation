<template>
  <div>
    <button class="back-button">
      <router-link to="/deskreservation">Zurück</router-link>
    </button>

    <not-found v-if="getStatus"> </not-found>
    <spinner v-else-if="isLoading"> </spinner>
    <error-item v-else-if="bookingError" :msg="bookingError"></error-item>
    <success-item
      v-else-if="successMessage"
      :msg="successMessage"
    ></success-item>
    <div class="desk-container">
      <img src="../assets/tisch.jpg" />
      <div class="flex-container">
        <p>Dieser Platz wird von {{}} reserviert.</p>
        <p>Bitte bestätige ?</p>
        <button class="confirmation-button" @click="book()">Bestätigung</button>
        <div class="checkbox">
          <input type="checkbox" />
          <label>Als Favorit speichern</label>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import moment from "moment";
import NotFound from "./NotFound.vue";
import Spinner from "./Spinner.vue";
import ErrorItem from "./ErrorItem.vue";
import SuccessItem from "./SuccessItem.vue";

export default {
  name: "deskconfirmation",
  components: {
    NotFound,
    Spinner,
    ErrorItem,
    SuccessItem,
  },

  data() {
    return {
      isNotFound: false,
      type: "",
      comment: null,
    };
  },
  props: ["deskId"],

  methods: {
    book() {
      if (this.type == "fix") {
        // booking as fix platz

        this.$store.dispatch("booking/fixDeskBooking", this.comment);
      } else {
        //booking as flix platz
        this.$store.dispatch("booking/bookingFlixDesk");
      }
    },
  },
  computed: {
    getStatus() {
      return this.isNotFound;
    },

    isLoading() {
      return this.$store.getters["booking/bookingIsloading"];
    },

    bookingError() {
      return this.$store.getters["booking/bookingError"];
    },

    successMessage() {
      return this.$store.getters["booking/bookingSuccessMessage"];
    },
  },
  mounted() {
    this.type = this.$route.query.type;
    const id = this.deskId;
    const dateStart = moment(this.$route.query.date).format("YYYY-MM-DD");
    let period = "";
    if (this.type == "fix") {
      //intialdata for fix-reservation

      this.$store.dispatch("booking/getSelectedDesk", id).then(() => {
       
        if (!this.$store.getters["booking/selectedDesk"]) {
          // desk with id given in url is not valid
          this.isNotFound = true;
        } else {
          // desk with id given is valid

          const comment = ` ich möchte erklären dass ich diese Platz ab ${dateStart}  buchen möchte`;

          this.comment = comment;
        }
      });
    } else {
      // initial data for flex-reservation
      period = this.$route.query.period;

      this.$store.dispatch("booking/getSelectedDesk", id).then(() => {
     
        if (!this.$store.getters["booking/selectedDesk"]) {
          // desk with id given in url is not valid
          this.isNotFound = true;
        } else {
          // desk with id given is valid

          let dateEnd =
            Date.parse(dateStart) +
            Math.max(parseInt(period) - 1, 0) * 24 * 3600 * 1000;
          dateEnd = moment(new Date(dateEnd)).format("YYYY-MM-DD");
          this.$store.dispatch("booking/dateStart", dateStart);
          this.$store.dispatch("booking/dateEnd", dateEnd);
        }
      });
    }
  },
};
</script>

<style lang="scss" scoped>
.desk-container {
  display: flex;
  justify-content: center;
  align-items: center;
}

.flex-container {
  display: flex;
  flex-direction: column;
  max-width: 480px;
  margin: 20px;
  background: #eaedf5;
  text-align: center;
  padding: 40px;
  border-radius: 10px;
  box-shadow: 8px 8px 10px #536ead;

  h5 {
    font-weight: bold;
    margin: 20px;
    padding: 50px;
  }

  p {
    font-weight: bold;
    color: #536ead;
  }
}

.confirmation-button {
  background: #a9b7d6;
  color: #f3f3f3;
  margin: 30px;
  padding: 10px;
  border: none;
  border-radius: 5px;
  font-size: 20px;

  a {
    text-decoration: none;
    color: #f3f3f3;
  }

  &:hover {
    background: #536ead;
    transition: 0.5s;
  }
}

img {
  margin: 10px;
  height: 20%;
  width: 20%;
  border: #536ead 2px solid;
  border-radius: 15px;
  box-shadow: 8px 8px 10px #536ead;
}

.back-button {
  background: #a9b7d6;
  color: #f3f3f3;
  margin: 15px;
  padding: 10px;
  border: none;
  border-radius: 5px;
  font-size: 18px;
  font-family: "Montserrat", regular;

  a {
    text-decoration: none;
    color: #f3f3f3;
  }

  &:hover {
    border: 3px solid #a9b7d6;
    background: #536ead;
    transition: 0.5s;
  }
}

.checkbox {
  font-size: 14px;
  text-align: center;

  label {
    margin-left: 5px;
  }
}
</style>
