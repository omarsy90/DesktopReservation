<template>
  <div class="overflow-auto">
    <h2>Kommentare</h2>
    <div class="comment-container">
      <table>
        <thead>
          <th>Datum</th>
          <th>Name</th>
          <th>Stiege</th>
          <th>Tisch</th>
          <th>Kommentare</th>
        </thead>
        
            <tr  v-for="comm in Allcomments " :key="comm.id"> 
          <td>{{comm.commentedAt}}</td>
          <td>{{comm.user.firstname}}</td>
          <td>{{comm.desk.office.name}}</td>
          <td>{{comm.desk.label}}</td>
          <td>{{comm.comment}}</td>
            </tr>
        
      </table>
      
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
    };
  },

  computed: {
    Allcomments() {
      const commentsArray = this.$store.getters["adminbranch/comments"];
            
            const allComments= [] ;

        for( let i = 0 ; i< commentsArray.length ; i++ ){
          for  (let j=0 ; j < commentsArray[i].length ; j++){
                
                allComments.push(commentsArray[i][j]);

             }
        }

             const sortedArray = allComments.sort((a,b)=> {
              
              const A_date = a.commentedAt.split("  ")[0];
              const A_year = A_date.split('-')[0] ;
              const A_month = A_date.split('-')[1] ;
              const A_day = A_date.split('-')[2] ;
              const A_time = a.commentedAt.split("  ")[1];
              const A_hour = A_time.split(':')[0] ;
              const A_minute = A_time.split(':')[1] ;
              const A_second = A_time.split(':')[2] ;



              const B_date = b.commentedAt.split("  ")[0];
              const B_year = B_date.split('-')[0] ;
              const B_month = B_date.split('-')[1] ;
              const B_day = B_date.split('-')[2] ;
              const B_time = b.commentedAt.split("  ")[1];
              const B_hour = B_time.split(':')[0] ;
              const B_minute = B_time.split(':')[1] ;
              const B_second = B_time.split(':')[2] ;






             return  new Date(A_year,A_month,A_day,A_hour,A_minute,A_second,0).getTime() - new Date(B_year,B_month,B_day,B_hour,B_minute,B_second,0).getTime()
           })  
        
        return sortedArray.reverse()
        
        
    },
  },
  mounted() {},
};
</script>

<style lang="scss" scoped>
.pagination-button {
  padding: 5px;
  margin: 2px;
  border-radius: 25px;
  border: 2px solid #536ead;
  cursor: pointer;
}
h2 {
  margin-top: 10px;
  background: #536ead;
  color: #f3f3f3;
  padding: 10px;
  border-top-right-radius: 5px;
  border-top-left-radius: 5px;
}
.comment-container {
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
</style>
