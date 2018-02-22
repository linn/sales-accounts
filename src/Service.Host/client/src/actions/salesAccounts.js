import { fetchJson, putJson, deleteJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestSalesAccountDetail = salesAccountUri => ({
    type: actionTypes.REQUEST_SALES_ACCOUNT,
    payload: { salesAccountUri }
});

const receiveSalesAccountDetail = data => ({
    type: actionTypes.RECEIVE_SALES_ACCOUNT,
    payload: { data }
});

export const fetchSalesAccount = salesAccountUri => async dispatch => {
    dispatch(requestSalesAccountDetail(salesAccountUri));
    try {
        const data = await fetchJson(`${config.appRoot}${salesAccountUri}`);
        dispatch(receiveSalesAccountDetail(data));
        dispatch(fetchCountry(data.address.countryUri));
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

export const closeAccount = id => async dispatch => {
    const body = { closedOn: new Date() };
    try {
        const data = await deleteJson(`${config.appRoot}${'/sales/accounts/'}${id}`, body);
        window.location.reload();
    } catch (e) {
        alert(`Failed to delete sales account. Error: ${e.message}`);
    }
};

export const saveAccountUpdate = salesAccount => async dispatch => {
    dispatch(startSave());
    const body = {
        TurnoverBandUri : salesAccount.turnoverBandUri,
        DiscountSchemeUri : salesAccount.discountSchemeUri,
        EligibleForGoodCreditDiscount : salesAccount.eligibleForGoodCreditDiscount,
        EligibleForRebate : salesAccount.eligibleForRebate,
        GrowthPartner : salesAccount.growthPartner
    }
    try {
        await putJson(`${config.appRoot}/sales/accounts/${salesAccount.id}`, body);
        dispatch(saveComplete());
    } catch (e) {
        alert(`Failed to update sales account. Error: ${e.message}`);
    }
};

export const startSave = () => ({
    type: actionTypes.START_SAVE,
    payload: {}
});

export const saveComplete = () => ({
    type: actionTypes.SAVE_COMPLETE,
    payload: {}
});

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

const requestCountry = () => ({
    type: actionTypes.REQUEST_COUNTRY,
    payload: { }
});

const receiveCountry = data => ({
    type: actionTypes.RECEIVE_COUNTRY,
    payload: { data }
});

export const fetchCountry = (countryUri) => async dispatch => {
        dispatch(requestCountry());
    try {
        const data = await fetchJson(`${config.countryRoot}${countryUri}`, { headers: { 'Accept': 'application/json' } });
        dispatch(receiveCountry(data));
    } catch (e) {
        alert(`Failed to fetch country. Error: ${e.message}`);
    }
};