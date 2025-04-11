
class storageService
{

    saveDataLocalStorage(data) {
        window.localStorage.setItem(data.key, data.value);
      }
    
      saveDataSessionStorage(data) {
        window.sessionStorage.setItem(data.key, data.value);
      }
    
      getDataLocalStorage(key) {
        return window.localStorage.getItem(key);
      }
      getDataSessionStorage(key) {
        return window.sessionStorage.getItem(key);
      }

      getToken()
      {
        let token = this.getDataLocalStorage("token") 
         if(!token)
         {
            token = this.getDataSessionStorage("token");
         }
        return token;
      }

}

const storage = new storageService();
export default storage ; 
