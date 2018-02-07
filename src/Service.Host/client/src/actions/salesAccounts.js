import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';
import { getTurnoverBandName } from '../selectors/salesAccountsSelectors';

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
    } catch (e) {
        alert(`Failed to fetch sales account. Error: ${e.message}`);
    }
};

export const fetchSalesAccounts = (salesAccountUris = []) => async dispatch => {
    salesAccountUris.forEach(salesAccountUri => {
        dispatch(fetchSalesAccount(salesAccountUri));
    });
};

export const showDiscountSchemeEditModal = () => ({
    type: actionTypes.SHOW_DISCOUNT_SCHEME_EDIT_MODAL,
    payload: {}
});

export const showTurnoverBandEditModal = () => ({
    type: actionTypes.SHOW_TURNOVER_BAND_EDIT_MODAL,
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

export const setTurnoverBand = (turnoverBandUri) => ({
    type: actionTypes.SET_TURNOVER_BAND,
    payload: { turnoverBandUri }
});

export const editGoodCredit = () => ({
    type: actionTypes.EDIT_GOOD_CREDIT,
    payload: {}
});