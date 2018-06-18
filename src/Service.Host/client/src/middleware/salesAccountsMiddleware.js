import * as actionTypes from '../actions';
import { fetchActivities, fetchCountry, fetchSalesAccount } from '../actions/salesAccounts';
import { fetchDiscountSchemes } from '../actions/discountSchemes';
import { fetchTurnoverBandSets } from '../actions/turnoverBandSets';

export const salesAccountsMiddleware = ({dispatch, getState}) => next => action => { 
    const result = next(action);
    
    switch(action.type) {
        case actionTypes.RECEIVE_SALES_ACCOUNT:
            if (action.payload.data.address && action.payload.data.address.countryUri) {
                dispatch(fetchCountry(action.payload.data.address.countryUri));
            }
            break;

        case actionTypes.RECEIVE_CLOSE_SALES_ACCOUNT:
            dispatch(fetchSalesAccount(action.payload.salesAccountUri));
            dispatch(fetchDiscountSchemes());
            dispatch(fetchTurnoverBandSets());
            dispatch(fetchActivities(action.payload.salesAccountUri));
            break;
    }

    return result;
}