import service from "../service";
const desks = {
    namespaced: true,
    state: {
        desksaray: [],
        officesArray:[],
        selectedStiegeId: '',
        selectedMinDatum: '',
        selectedMaxDatum: ''
    },
    mutations: {
        desks(state, desks) {
            state.desksaray = desks;
        },
        setMinDatum(state, datum) {
            state.selectedMinDatum = datum
        },
        setMaxDatum(state, datum) {
            state.selectedMaxDatum = datum
        },
        setStiegeId(state, stiegeId) {
            state.selectedStiegeId = stiegeId
        },
        officesArray(state,offices){
            state.officesArray = offices ;
        }
    },
    actions: {
        async getAllOffices(context){
            
            const offices = await service.getAllofice() ;
            context.commit('officesArray',offices) ;
         },
        async setDesks(context) {
            //payload hold token
            context.rootState.isloading = true;
            const response = await service.getAlldesks();
            if (response == 400) {
                context.rootState.error = "Opps !! Es ist leider ein Fehler entstanden, bitte die Seite neu laden";
                context.rootState.successMessag = "";
            } else if (response == 401) {
                context.rootState.error = "Bitte loggen Sie sich ein";
                context.rootState.successMessag = "";
            } else if (response) { // response ia arrry of desks
                context.rootState.error = "";
                context.commit('desks', response);
            }
            context.rootState.isloading = false;

        },
        async felterDesks(context, payload) {
            let desks = '';
              console.log(payload.from, payload.to)
             
                if (payload.from == null) {
                    payload.from = payload.to
                   // context.commit('setMinDatum', payload.to);
                } else if (payload.to == null) {
                    payload.to = payload.from
                  //  context.commit('setMaxDatum', payload.from);
                } else if (payload.to < payload.from) {
                    payload.to = payload.from
                    context.commit('setMaxDatum', payload.from)
                }
                desks = await service.filterDesks( payload.from, payload.to, payload.stiegeId);
            
           
            context.commit('desks', desks)
        },
        setStiege(context,stiegeId){
        context.commit('setStiegeId',stiegeId) ;
            
            
        }
    },
    getters: {
        desks(state) {
            return state.desksaray;
        },
        MinDatum(state) {
            return state.selectedMinDatum
        },
        MaxDatum(state) {
            return state.selectedMaxDatum
        },
        StiegeId(state) {
            return state.selectedStiegeId;
        },
        officesArray(state){
            return state.officesArray
        }
    },
}
export default desks