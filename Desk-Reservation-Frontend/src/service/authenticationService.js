import axios from "axios";
const basiUrl = "https://localhost:5001/api";

class authenticatonService
{
    // return  token  if ther credentials are right
        // return 400 if payload not correct
        // return 401 if email or password is unvalid
    async logIn(auth) {
        
        const response = await axios
          .post(`${basiUrl}/User/login`, {
            email: auth.email,
            password: auth.password,
          }).then((res)=>{
            return res ;
          }).catch((err)=>
          {
             return  err.response 
          }
          );
         
        return response ;
      }


}

const  authService = new authenticatonService();
export default authService ;


