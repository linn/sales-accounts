import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestSalesAccounts = searchTerm => ({
    type: actionTypes.REQUEST_SALES_ACCOUNTS,
    payload: { searchTerm }
});

const receiveSalesAccounts = (searchTerm, salesAccounts) => ({
    type: actionTypes.RECEIVE_SALES_ACCOUNTS,
    payload: { searchTerm, salesAccounts }
});

export const clearSalesAccountSearch = () => ({
    type: actionTypes.CLEAR_SALES_ACCOUNT_SEARCH,
    payload: {}
});

export const searchSalesAccounts = searchTerm => async dispatch => {
    if (searchTerm) {
        dispatch(requestSalesAccounts(searchTerm));
        try
        {
            const data = await fetchJson(`${config.appRoot}/sales/accounts?searchTerm=${searchTerm}`);
            dispatch(receiveSalesAccounts(searchTerm, data));
        } catch (e) {
            alert(`Failed to search for sales accounts. Error: ${e.message}`);
        }
    } else {
        dispatch(receiveSalesAccounts('', []));
    }
};