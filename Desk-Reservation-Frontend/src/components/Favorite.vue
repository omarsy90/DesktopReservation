<template>
  <div>
    <h2>Meine Favoriten</h2>
    <div class="reservation-container">
      <table>
        <thead>
          <th>Stiege</th>
          <th>Tisch</th>
          <th>Reservieren</th>
          <th>Favorittisch löschen</th>
        </thead>
        <tr v-for="desk in favourities" :key="desk.id">
          <td>{{ desk.office.name }}</td>
          <td>{{ desk.label }}</td>
          <td>
            <button class="reservation-button" @click="book(desk.id)">
              Tisch reservieren
            </button>
          </td>
          <td>
            <button class="remove-button" @click="deletefavourite(desk.id)">
              Favorittisch löschen<img src="../assets/remove.png" alt="" />
            </button>
          </td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
export default {
  data() {
    return {};
  },
  computed: {
    favourities() {
      return this.$store.getters["favourite/favouritedesks"];
    },
  },

  methods: {
    book(deskId) {
      this.$router.push({
        name: "flexdeskreservation",
        params: { deskId: deskId },
        query: { favourited: true },
      });
    },

    deletefavourite(deskId) {
      Swal.fire({
        title: "Möchten Sie wirklich ihr Tisch als Favorit löschen?",
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: "Yes",
        // denyButtonText: `Don't save`,
      }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
          this.$store
            .dispatch("favourite/deletefavourite", deskId)
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
    this.$store.dispatch("favourite/getFavourities");
  },
};
</script>

<style lang="scss" scoped>
h2 {
  margin-top: 10px;
  background: #536ead;
  padding: 10px;
  color: #f3f3f3;
  border-top-right-radius: 5px;
  border-top-left-radius: 5px;
}

.reservation-container {
  background: #a9b7d6;
  padding: 10px;
  border-radius: 5px;

  table {
    width: 100%;
  }

  thead {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 5px;
  }

  tr {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background: #f3f3f3;
    color: #424242;
    border-radius: 5px;
    margin-bottom: 20px;

    td {
      padding: 10px;
    }
  }
}

.reservation-button {
  display: flex;
  justify-content: center;
  background: #a9b7d6;
  color: #f3f3f3;
  padding: 10px;
  border: 3px solid #536ead;
  border-radius: 25px;

  &:hover {
    background: #536ead;
    transition: 0.5s;
  }
}

.remove-button {
  padding: 5px;
  color: #f3f3f3;
  background: #f44336;
  border: none;
  border-radius: 5px;

  &:hover {
    border-radius: 25px;
    transition: 0.5s;
  }
}
</style>
