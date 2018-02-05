import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestSalesAccountDetail = (salesAccountId) => ({
    type: actionTypes.REQUEST_SALES_ACCOUNT,
    payload: { salesAccountId }
});

const receiveSalesAccountDetail = (salesAccountId, data) => ({
    type: actionTypes.RECEIVE_SALES_ACCOUNT,
    payload: { salesAccountId, salesAccount: data }
});

export const fetchSalesAccount = salesAccountId => async (dispatch) => {
    dispatch(requestSalesAccountDetail(salesAccountId));
    try {
        const data = await fetchJson(`${config.appRoot}/sales/accounts/${salesAccountId}`, { headers: { 'Accept': 'application/json' } });
        dispatch(receiveSalesAccountDetail(salesAccountId, data));
    } catch (e) {
        alert(`Failed to fetch sales account. Error: ${e.message}`);
    }
};

export const fetchSalesAccounts = (salesAccountUris = []) => async dispatch => {
    salesAccountUris.forEach(salesAccountUri => {
        dispatch(fetchSalesAccount(salesAccountUri));
    });
};

export const showEditModal = () => ({
    type: actionTypes.SHOW_EDIT_MODAL,
    payload: {}
});

export const hideEditModal = () => ({
    type: actionTypes.HIDE_EDIT_MODAL,
    payload: {}
});
