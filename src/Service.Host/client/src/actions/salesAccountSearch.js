import config from '../config';
import * as actionTypes from './index';
import { CALL_API } from 'redux-api-middleware';

const receiveSalesAccounts = (searchTerm, salesAccounts) => ({
    type: actionTypes.RECEIVE_SALES_ACCOUNTS_SEARCH,
    payload: { searchTerm, salesAccounts }
});

export const clearSalesAccountSearch = () => ({
    type: actionTypes.CLEAR_SALES_ACCOUNT_SEARCH,
    payload: {}
});

const performSearchSalesAccounts = searchTerm => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}/sales/accounts?searchTerm=${searchTerm}`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_SALES_ACCOUNTS_SEARCH,
                payload: { searchTerm }
            },
            {
                type: actionTypes.RECEIVE_SALES_ACCOUNTS_SEARCH,
                payload: async (action, state, res) => ({ salesAccounts: await res.json(), searchTerm })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `Sales Account search - ${res.status} ${res.statusText}` : `Network request failed`,       
            }
        ]
    }
});

export const searchSalesAccounts = searchTerm => async dispatch => {
    if (searchTerm) {
        dispatch(performSearchSalesAccounts(searchTerm));
    } else {
        dispatch(receiveSalesAccounts('', []));
    }
};