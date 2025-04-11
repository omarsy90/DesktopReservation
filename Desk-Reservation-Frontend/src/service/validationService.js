

class  validationService {

    validateEmail(email){

     const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      let isValid = emailRegex.test(email) ;
      return isValid ;
    }

    validatePassword(password)
    {
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        let isValid = passwordRegex.test(password);
        return isValid
    }
    
}

const validator = new validationService();
export default validator