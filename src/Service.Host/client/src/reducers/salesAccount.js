import * as actionTypes from '../actions';

const defaultState = {
    salesAccountUri: null,
    loading: false,
    item: null,
}

const salesAccount = (state = defaultState, action) => {
    switch (action.type) {
    case actionTypes.REQUEST_SALES_ACCOUNT:
        return {
            ...state,
            salesAccountUri: action.payload.salesAccountUri,
            loading: true
        }
       

    case actionTypes.RECEIVE_SALES_ACCOUNT:
        return {
            ...state,
            loading: false,
            item: action.payload.data
        }
    // case actionTypes.REQUEST_TURNOVER_BAND:
    //     return state;

    // case actionTypes.RECEIVE_TURNOVER_BAND:
    //     return action.payload.data;
    
    default:
        return state;
    }
}

export default salesAccount;