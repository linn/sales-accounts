import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestSalesAccountDetail = (salesAccountUri) => ({
    type: actionTypes.REQUEST_SALES_ACCOUNT,
    payload: { salesAccountUri }
});

const receiveSalesAccountDetail = (salesAccountUri, data) => ({
    type: actionTypes.RECEIVE_SALES_ACCOUNT,
    payload: { salesAccountUri, salesAccount: data }
});

export const fetchSalesAccount = salesAccountUri => async (dispatch, getState) => {
    const { salesAccounts } = getState();

    if (!salesAccounts.some(c => c.salesAccountUri === salesAccountUri)) {
        dispatch(requestSalesAccountDetail(salesAccountUri));
        try {
            const data = await fetchJson(`${config.appRoot}${salesAccountUri}`, { headers: { 'Accept': 'application/json' } });
            dispatch(receiveSalesAccountDetail(salesAccountUri, data));
        } catch (e) {
            alert(`Failed to fetch sales account. Error: ${e.message}`);
        }
    }
};

export const fetchSalesAccounts = (salesAccountUris = []) => async dispatch => {
    salesAccountUris.forEach(salesAccountUri => {
        dispatch(fetchSalesAccount(salesAccountUri));
    });
};
