<template>
  <div>
    <h2>Meine Reservierungen</h2>

    <div class="reservation-container">
      <table>
        <thead>
          <th>Datum</th>
          <th>Stiege</th>
          <th>Tisch</th>
          <th>Kommentieren/Störnieren</th>
        </thead>

        <thead v-for="(booking, index) in myBookings" :key="booking.id">
          <tr v-if="index < count">
            <td>
              {{ "von : " + booking.dateStart }}
              {{ "bis : " + booking.dateEnd }}
            </td>
            <td>{{ booking.desk.office.name }}</td>
            <td>{{ booking.desk.label }}</td>

            <td v-if="booking.isActive">
              <input
                type="text"
                @input="rewrite($event, booking.desk.id)"
                maxlength="50"
                :id="booking.desk.id"
              /><img
                class="comment"
                src="../assets/comments.png"
                @click="setComment(booking.desk.id)"
              />
            </td>

            <td v-else>
              <button class="remove" @click="remove(booking.id)">
                Störnieren<img
                  src="../assets/remove.png"
                />
              </button>
            </td>
          </tr>
        </thead>
      </table>
      <div class="more-button">
        <button @click="count += 3">Mehr Sehen</button>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
export default {
  data() {
    return {
      count: 5,
      commentsArray: [],
    };
  },

  computed: {
    myBookings() {
      const reservationArry =
        this.$store.getters["myreservation/bookingsArray"];
      let sortedArray = reservationArry.sort((a, b) => {
        if (
          new Date(a.dateEnd).getTime() - new Date(b.dateEnd).getTime() ===
          0
        ) {
          return (
            new Date(a.dateStart).getTime() - new Date(b.dateStart).getTime()
          );
        }

        return new Date(a.dateEnd).getTime() - new Date(b.dateEnd).getTime();
      });

      return sortedArray.reverse();
    },
  },

  methods: {
    remove(bookingId) {
      Swal.fire({
        title: "Möchten Sie ihr Tisch wirklich störnieren?",
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: "Yes",
        // denyButtonText: `Don't save`,
      }).then((result) => {
        if (result.isConfirmed) {
          this.$store
            .dispatch("myreservation/bookingDelete", bookingId)
            .then((response) => {
              if (response.status == 204) {
                // deleted
                Swal.fire({
                  position: "top-end",
                  icon: "success",
                  title: response.message,
                  showConfirmButton: false,
                  timer: 1500,
                });
              } else {
                // there is wrong

                Swal.fire({
                  icon: "error",
                  title: "Oops...",
                  text: response.message,
                });
              }
            });
        }
      });
    },

    rewrite(event, deskId) {
      const comment = this.commentsArray.find(
        (commentobj) => commentobj.deskId == deskId
      );
      if (comment) {
        this.commentsArray.splice(this.commentsArray.indexOf(comment), 1);
      }

      const txt = event.target.value;
      if (txt) {
        this.commentsArray.push({
          deskId: deskId,
          comment: txt,
        });
      }
    },
    //--------------------------
    setComment(deskId) {
      const comment = this.commentsArray.find(
        (commentobj) => commentobj.deskId == deskId
      )?.comment;

      const payload = { deskId: deskId, comment: comment };
      this.$store.dispatch("myreservation/setComment", payload).then((res) => {
        const titel = res.message;
        if (res.status == 204) {
          Swal.fire({
            position: "top-end",
            icon: "success",
            title: titel,
            showConfirmButton: false,
            timer: 1500,
          });

          document.getElementById(`${deskId}`).value = "";
        } else {
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: titel,
          });
        }
      });
    },

    //------------------------
  },
  mounted() {
    this.$store.dispatch("myreservation/getNextReservations");
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


.comment {
  background: #a9b7d6;
  border: 2px solid #a9b7d6;
  margin: 5px;
  padding: 4px;

  &:hover {
    background: #536ead;
  }
}

.reservation-container {
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
      padding: 10px;
      text-align: center;
      border: 2px solid #a9b7d6;
    }
  }

  input {
    border: none;
    height: 30px;
    width: 80%;
  }
}

.more-button {
  display: flex;
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

.remove {
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
