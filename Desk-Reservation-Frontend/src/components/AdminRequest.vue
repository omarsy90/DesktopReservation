<template>
  <div>
    <h2></h2>
    <div class="request-container">
      <table>
        <thead>
          <th>Datum</th>
          <th>Name</th>
          <th>Stiege</th>
          <th>Tisch</th>
        </thead>
        <tr v-for="req in requests" :key="req.id">
          <td>{{ req.requestedAt }}</td>
          <td>{{ req.user.firstname + " " + req.user.lastname }}</td>
          <td>{{ req.desk.office.name }}</td>
          <td>{{ req.desk.label }}</td>
          <td>
            <button class="check-button" @click="approve(req.id)">
              Zustimmen
              <img
                src="../assets/check(2).png"
              />
            </button>
            <button class="remove-button" @click="refuse(req.id)">
              Ablehnen
              <img
                src="../assets/remove.png"
              />
            </button>
          </td>
        </tr>
      </table>
      <div class="more-button">
        <button>Mehr Sehen</button>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";

export default {
  name: "adminrequest",
  data() {
    return {};
  },

  computed: {
    requests() {
      const requestArray = this.$store.getters["adminbranch/fixDeskRequest"];

      requestArray.forEach((req) => {
        // to prette date format
        const datetime = req.requestedAt;

        const date = datetime.split("T")[0];
        let time = datetime.split("T")[1];
        time = time.split(".")[0];
        req.requestedAt = date + " " + time + " " + "GMC";
      });

      const sortedArray = requestArray.sort((a, b) => {
        const A_date = a.requestedAt.split(" ")[0];
        const A_year = A_date.split("-")[0];
        const A_month = A_date.split("-")[1];
        const A_day = A_date.split("-")[2];
        const A_time = a.requestedAt.split(" ")[1];
        const A_hour = A_time.split(":")[0];
        const A_minute = A_time.split(":")[1];
        const A_second = A_time.split(":")[2];

        const B_date = b.requestedAt.split(" ")[0];
        const B_year = B_date.split("-")[0];
        const B_month = B_date.split("-")[1];
        const B_day = B_date.split("-")[2];
        const B_time = b.requestedAt.split(" ")[1];
        const B_hour = B_time.split(":")[0];
        const B_minute = B_time.split(":")[1];
        const B_second = B_time.split(":")[2];

        return (
          new Date(
            A_year,
            A_month,
            A_day,
            A_hour,
            A_minute,
            A_second,
            0
          ).getTime() -
          new Date(
            B_year,
            B_month,
            B_day,
            B_hour,
            B_minute,
            B_second,
            0
          ).getTime()
        );
      });

      return sortedArray.reverse();
    },
  },

  methods: {
    approve(requestId) {
      Swal.fire({
        title: "Sind Sie damit einverstanden ?",
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: "Yes",
        // denyButtonText: `Don't save`,
      }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
          this.$store
            .dispatch("adminbranch/fixDeskApprove", requestId)
            .then((res) => {
              if (res.status == 204) {
                Swal.fire({
                  position: "top-end",
                  icon: "success",
                  title: res.message,
                  showConfirmButton: false,
                  timer: 1500,
                });
              } else {
                Swal.fire({
                  position: "top-end",
                  icon: "error",
                  title: res.message,
                  showConfirmButton: false,
                  timer: 1500,
                });
              }
            });
        } else if (result.isDenied) {
          // Swal.fire('Changes are not saved', '', 'info')
        }
      });
    },
    refuse(requestId) {
      Swal.fire({
        title: "Sind sie damit einverstanden ?",
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: "Yes",
        // denyButtonText: `Don't save`,
      }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
          this.$store
            .dispatch("adminbranch/fixDeskDecline", requestId)
            .then((res) => {
              if (res.status == 204) {
                Swal.fire({
                  position: "top-end",
                  icon: "success",
                  title: res.message,
                  showConfirmButton: false,
                  timer: 1500,
                });
              } else {
                Swal.fire({
                  position: "top-end",
                  icon: "error",
                  title: res.message,
                  showConfirmButton: false,
                  timer: 1500,
                });
              }
            });
        } else if (result.isDenied) {
          // Swal.fire('Changes are not saved', '', 'info')
        }
      });
    },
  },

  mounted() {
    this.$store.dispatch("adminbranch/getAllrequest");
  },
};
</script>

<style lang="scss" scoped>
h2 {
  margin-top: 10px;
  background: #536ead;
  color: #f3f3f3;
  padding: 10px;
  border-top-right-radius: 5px;
  border-top-left-radius: 5px;
}

.request-container {
  background: #a9b7d6;
  padding: 10px;
  border-radius: 5px;

  table {
    width: 100%;
  }

  thead {
    text-align: center;
    padding: 5px;
  }

  tr {
    background: #f3f3f3;
    color: #424242;

    td {
      padding: 20px;
      text-align: center;
      border: 2px solid #a9b7d6;
    }
  }
}

.more-button {
  display: flex;
  align-items: center;
  justify-content: center;

  button {
    background: #a9b7d6;
    color: #f3f3f3;
    margin: 15px;
    padding: 10px;
    width: 20%;
    border: 3px solid #536ead;
    border-radius: 25px;
    font-size: 18px;
    font-family: "Montserrat", regular;

    &:hover {
      background: #536ead;
      transition: 0.5s;
    }
  }
}

  .check-button {
    color: #f3f3f3;
    background: #00ba00;
    border: none;
    border-radius: 5px;
    padding: 4px;

    &:hover {
      border-radius: 25px;
      transition: 0.5s;
    }
  }

  .remove-button {
    color: #f3f3f3;
    background: #f44336;
    margin-left: 30px;
    border: none;
    border-radius: 5px;
    padding: 4px;

    &:hover {
      border-radius: 25px;
      transition: 0.5s;
    }
  }
</style>
