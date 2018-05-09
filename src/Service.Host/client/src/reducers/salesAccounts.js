import * as actionTypes from '../actions';
import { getSelfHref} from '../helpers/utilities';

const defaultState = {
    items: [],
    loading: false
};

const setSalesAccount = (salesAccount) => ({
    item: {
        ...salesAccount,
        uri: getSelfHref(salesAccount)
    }
});

const salesAccounts = (state = defaultState, action) => {
    switch (action.type) {
        case actionTypes.REQUEST_SALES_ACCOUNTS:
            return {
                ...state,
                loading: true
            }

        case actionTypes.RECEIVE_SALES_ACCOUNTS:
            return {
                ...state,
                loading: false,
                items: action.payload.data ? action.payload.data.map(a => setSalesAccount(a)) : []
            }

        default:
            return state;
    }
}

export default salesAccounts;