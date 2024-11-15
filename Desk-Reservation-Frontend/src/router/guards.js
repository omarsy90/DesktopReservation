import service from "../service/strorageService.js";

export async function isAuthorised(to, from, next) {
    const token = service.getDataLocalStorage('token') || service.getDataSessionStorage('token')
    if (token) {
        next()
}
else{
    next({name:'login'});
}



}



