<template>
  <div>
   <h2>Fixe Reservierungen</h2>
    <div class="reservation-container">
      <table>
        <thead>
          <th>Platz</th>
          <th>Stiege</th>
          <th>Kommentieren</th>
        </thead>
        
        <thead   v-for="(desk,index) in fixReservations" :key="desk.id">
        <tr  v-if="index < count"   >
          <td>   {{desk.label}} </td>
          <td>{{desk.office.name}}</td>
           <td><input type="text"  @input="rewrite($event,desk.id)" :id="desk.id"  maxlength="50" ><img class="comment" src="../assets/comments.png"
                @click="setComment(desk.id)"></td>
        </tr>

        </thead>
      </table>
      <div class="more-button"  @click="count+=3">
        <button>Mehr Sehen</button>
      </div>
    </div>
  </div>
</template>

<script>

import Swal from 'sweetalert2'

  export default {
    data() {
      return{
         count:5,
         commentsArray:[]
      }
    },

    computed:{
      
    fixReservations(){

      return this.$store.getters['fixreservation/fixReservations'] ;
    }

    },methods:{

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



         setComment(deskId) {

        const comment = this.commentsArray.find(commentobj => commentobj.deskId ==deskId)?.comment
        
         const   payload ={deskId :deskId , comment:comment}
        this.$store.dispatch('fixreservation/setComment',payload).then(res => {
            const titel = res.message
          if (res.status == 204) {

            Swal.fire({
              position: 'top-end',
              icon: 'success',
              title: titel,
              showConfirmButton: false,
              timer: 1500
            }) ;

             document.getElementById(`${deskId}`).value ="" ;


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

    mounted(){

      const   userId = window.localStorage.getItem('userId') || window.sessionStorage.getItem('userId') ;
      this.$store.dispatch('fixreservation/getFixreservation',userId) ;

    }
  } 
</script>

<style lang="scss" scoped>

 .comment {
    background: #A9B7D6;
    border: 2px solid #A9B7D6;
    margin: 5px;
    padding: 4px;

    &:hover {
      background: #536EAD;
    }
  }
h2{
    margin-top: 10px;
    background: #536EAD;
    color: #F3F3F3;
    padding: 10px;
    border-top-right-radius: 5px;
    border-top-left-radius: 5px;
}

.reservation-container{
  background: #A9B7D6;
  padding: 10px;
  border-radius: 5px;

  table{
    width: 100%;
  }

  thead{
    text-align: center;
    padding: 5px;
  }

  tr{
    background: #F3F3F3;
    color: #424242;

    td{
      text-align: center;
      border: 2px solid #A9B7D6;
    }
  }
  input{
    border: none;
    height: 30px;
    width: 80%;
  }
}

.more-button{
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

    
       &:hover{
        background:#536EAD;
        transition: 0.5s;
        }
    }
}
</style>