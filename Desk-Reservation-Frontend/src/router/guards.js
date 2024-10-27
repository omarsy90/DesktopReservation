import service from "../service/index.js";

export async function isAuthorised(to, from, next) {
    const token = service.getDataLocalStorage('token') || service.getDataSessionStorage('token')
    const userId = service.getDataLocalStorage('userId') || service.getDataSessionStorage('userId')
    if (token && userId) {
       
        next()

}else{
    next({name:'login'});
}



}



