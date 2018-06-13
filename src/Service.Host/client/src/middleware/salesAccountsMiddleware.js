import * as actionTypes from '../actions';
import { fetchCountry } from '../actions/salesAccounts';

export const salesAccountsMiddleware = ({dispatch, getState}) => next => action => { 
    const result = next(action);
    
    if (action.type === actionTypes.RECEIVE_SALES_ACCOUNT) {
        console.log(action.payload.data);
        dispatch(fetchCountry(action.payload.data.address.countryUri))
    }

    return result;
}