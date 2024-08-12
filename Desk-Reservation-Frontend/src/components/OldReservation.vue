<template>
  <div>
    <h2>Alte Reservierungen</h2>
    <div class="reservation-container">
      <table>
        <thead>
          <th>Datum</th>
          <th>Stiege</th>
          <th>Tisch</th>
          <th>Kommentieren</th>
        </thead>

        <thead v-for=" (booking ,index ) in altreservation" :key="booking.id">
          <tr v-if="index < count">

            <td> {{ 'von :'+ booking.dateStart}} &nbsp; {{ 'bis :'+booking.dateEnd}} </td>
            <td>{{booking.desk.office.name}}</td>
            <td>{{booking.desk.label}}</td>
            <td><input type="text"  @input="rewrite($event,booking.desk.id)" :id="booking.desk.id"   maxlength="50" ><img class="comment" src="../assets/comments.png"
                @click="setComment(booking.desk.id)"></td>


          </tr>
        </thead>
      </table>
      <div class="more-button">
        <button @click="increase()">Mehr Sehen</button>
      </div>
    </div>
  </div>
</template>

<script scoped>
  import Swal from 'sweetalert2'
  //import moment from 'moment'

  export default {
    data() {
      return {

        count: 5,
        
        commentsArray:[]
      }
    },

    methods: {

      rewrite(event,deskId){
        
        const comment = this.commentsArray.find(commentobj => commentobj.deskId ==deskId);
        if(comment){

          this.commentsArray.splice(this.commentsArray.indexOf(comment),1);
        
        }
        const txt = event.target.value ;
             if(txt){
                  this.commentsArray.push({deskId:deskId,comment:txt});
             }
      },

      increase() {

        this.count += 3;
      },

      setComment(deskId) {

        const comment = this.commentsArray.find(commentobj => commentobj.deskId ==deskId)?.comment
     
         const   payload ={deskId :deskId , comment:comment}
        this.$store.dispatch('altreservation/setComment',payload).then(res => {
            const titel = res.message
          if (res.status == 204) {

            Swal.fire({
              position: 'top-end',
              icon: 'success',
              title: titel,
              showConfirmButton: false,
              timer: 1500
            })

                 document.getElementById(`${deskId}`).value='';
          } else {

            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: titel,
              
            })


          }
        }) ;

       

      }


    },

    computed: {
      altreservation() {
        
        const oldReservationArry= this.$store.getters['altreservation/altreservationarray'] ;
        let sortedArray = oldReservationArry.sort ((a ,b) => 
        {
           if(new Date(a.dateEnd).getTime() - new Date(b.dateEnd).getTime() ===0){
           
            return new Date(a.dateStart).getTime() - new Date(b.dateStart).getTime()

           }

          return   new Date(a.dateEnd).getTime() - new Date(b.dateEnd).getTime()   
           
           
        }) ;
            
        return  sortedArray.reverse();
      }

    },

    mounted() {

      this.$store.dispatch('altreservation/getAltebooking', 'a705133c-e329-439a-b504-7bb3a5dfe362')

    }
  }
</script>

<style lang="scss" scoped>
  h2 {
    margin-top: 10px;
    background: #536EAD;
    color: #F3F3F3;
    padding: 10px;
    border-top-right-radius: 5px;
    border-top-left-radius: 5px;
  }

  .comment {
    background: #A9B7D6;
    border: 2px solid #A9B7D6;
    margin: 5px;
    padding: 4px;

    &:hover {
      background: #536EAD;
    }
  }

  .reservation-container {
    background: #A9B7D6;
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
      background: #F3F3F3;
      color: #424242;

      td {
        padding: 1px;
        text-align: center;
        border: 2px solid #A9B7D6;
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
      background: #A9B7D6;
      color: #f3f3f3;
      margin: 15px;
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
</style>