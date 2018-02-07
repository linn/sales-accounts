import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';
import { getTurnoverBandName } from '../selectors/salesAccountsSelectors';

const requestSalesAccountDetail = salesAccountUri => ({
    type: actionTypes.REQUEST_SALES_ACCOUNT,
    payload: { salesAccountUri }
});

const receiveSalesAccountDetail = data => ({
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

export const hideEditModal = () => ({
    type: actionTypes.HIDE_EDIT_MODAL,
    payload: {}
});

export const closeAccount = id => async (dispatch) => {
    try {
        const data = await deleteJson(`${config.appRoot}${'sales/accounts/'}${id}`, { headers: { 'Accept': 'application/json' } });
        //reset page here?
    } catch (e) {
        alert(`Failed to delete sales account. Error: ${e.message}`);
    }
};

export const editDiscountScheme = discountSchemeUri => ({
    type: actionTypes.EDIT_DISCOUNT_SCHEME,
    payload: { discountSchemeUri }
});


export const setDiscountScheme = discountSchemeUri => ({
    type: actionTypes.SET_DISCOUNT_SCHEME,
    payload: { discountSchemeUri }
});

export const editTurnoverBand = () => ({
    type: actionTypes.EDIT_TURNOVER_BAND,
    payload: {}
});

export const setTurnoverBand = turnoverBandUri => ({
    type: actionTypes.SET_TURNOVER_BAND,
    payload: { turnoverBandUri }
});

export const editEligibleForGoodCreditDiscount = () => ({
    type: actionTypes.EDIT_ELIGIBLE_FOR_GOOD_CREDIT_DISCOUNT,
    payload: {}
});

export const setEligibleForGoodCreditDiscount = eligible => ({
    type: actionTypes.SET_ELIGIBLE_FOR_GOOD_CREDIT_DISCOUNT,
    payload: { eligible }
});

export const editGrowthPartner = () => ({
    type: actionTypes.EDIT_GROWTH_PARTNER,
    payload: { }
});

export const setGrowthPartner = eligible => ({
    type: actionTypes.SET_GROWTH_PARTNER,
    payload: { eligible }
});

export const editEligibleForRebate = () => ({
    type: actionTypes.EDIT_ELIGIBLE_FOR_REBATE,
    payload: { }
});

export const setEligibleForRebate = eligible => ({
    type: actionTypes.SET_ELIGIBLE_FOR_REBATE,
    payload: { eligible }
});