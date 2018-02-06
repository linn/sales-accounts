import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';
import { fetchTurnoverBand } from './turnoverBands';

const requestSalesAccountDetail = (salesAccountUri) => ({
    type: actionTypes.REQUEST_SALES_ACCOUNT,
    payload: { salesAccountUri }
});

const receiveSalesAccountDetail = (data) => ({
    type: actionTypes.RECEIVE_SALES_ACCOUNT,
    payload: { data }
});

export const fetchSalesAccount = salesAccountUri => async (dispatch) => {
    dispatch(requestSalesAccountDetail(salesAccountUri));
    try {
        const data = await fetchJson(`${config.appRoot}${salesAccountUri}`, { headers: { 'Accept': 'application/json' } });
        dispatch(receiveSalesAccountDetail(data));
        dispatch(fetchTurnoverBand(data.turnoverBandUri));
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

export const editDiscountScheme = (discountSchemeUri) => ({
    type: actionTypes.EDIT_DISCOUNT_SCHEME,
    payload: { discountSchemeUri }
});

export const setDiscountScheme = (discountSchemeUri) => ({
    type: actionTypes.SET_DISCOUNT_SCHEME,
    payload: { discountSchemeUri }
});
