import { fetchJson, putJson, deleteJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';
import { CALL_API } from 'redux-api-middleware';

// const requestSalesAccountDetail = salesAccountUri => ({
//     type: actionTypes.REQUEST_SALES_ACCOUNT,
//     payload: { salesAccountUri }
// });

// const receiveSalesAccountDetail = data => ({
//     type: actionTypes.RECEIVE_SALES_ACCOUNT,
//     payload: { data }
// });

const requestSalesAccounts = ()=> ({
    type: actionTypes.REQUEST_SALES_ACCOUNTS,
    payload: {}
});

const receiveSalesAccounts = data => ({
    type: actionTypes.RECEIVE_SALES_ACCOUNTS,
    payload: { data }
});

export const fetchAllOpenSalesAccounts = () => async dispatch => {
    dispatch(requestSalesAccounts());
    try {
        const data = await fetchJson(`${config.appRoot}/sales/accounts`);
        dispatch(receiveSalesAccounts(data));
    } catch (e) {
        alert(`Failed to fetch open sales accounts. Error: ${e.message}`);
    }
};

// export const fetchSalesAccount = salesAccountUri => async dispatch => {    
//     dispatch(requestSalesAccountDetail(salesAccountUri));
//     try {
//         const data = await fetchJson(`${config.appRoot}${salesAccountUri}`);
//         dispatch(receiveSalesAccountDetail(data));
//         if (data.address) {
//             dispatch(fetchCountry(data.address.countryUri));
//         }
//     } catch (e) {
//         alert(`Failed to fetch sales account. Error: ${e.message}`);
//     }
// };

export const fetchSalesAccount = salesAccountUri => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}${salesAccountUri}`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_SALES_ACCOUNT,
                payload: salesAccountUri,
            },
            {
                type: actionTypes.RECEIVE_SALES_ACCOUNT,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `Sales Account - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});

export const fetchSalesAccounts = (salesAccountUris = []) => async dispatch => {
    salesAccountUris.forEach(salesAccountUri => {
        dispatch(fetchSalesAccount(salesAccountUri));
    });
};

export const hideEditModal = () => ({
    type: actionTypes.HIDE_EDIT_MODAL,
    payload: {}
});

// TODO is this needed?
// do this later - uncomment component code and test
export const closeAccount = id => async dispatch => {
    const body = { closedOn: new Date() };
    try {
        const data = await deleteJson(`${config.appRoot}${'/sales/accounts/'}${id}`, body);
        window.location.reload();
    } catch (e) {
        alert(`Failed to delete sales account. Error: ${e.message}`);
    }
};

export const saveAccountUpdate = salesAccount => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}/sales/accounts/${salesAccount.id}`,
        method: 'PUT',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            TurnoverBandUri : salesAccount.turnoverBandUri,
            DiscountSchemeUri : salesAccount.discountSchemeUri,
            EligibleForGoodCreditDiscount : salesAccount.eligibleForGoodCreditDiscount,
            EligibleForRebate : salesAccount.eligibleForRebate,
            GrowthPartner : salesAccount.growthPartner
        }),
        types: [
            {
                type: actionTypes.START_SAVE,
                payload: {}
            },
            {
                type: actionTypes.SAVE_COMPLETE,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            // TODO create new error type for this
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `save - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
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

export const fetchCountry = countryUri => ({
    [CALL_API]: {
        endpoint: `${config.proxyRoot}${countryUri}`,
        method: 'GET',
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_COUNTRY,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_COUNTRY,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `country - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
})

export const fetchActivities = salesAccountUri => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}${salesAccountUri}/activities`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_ACTIVITIES,
                payload: { salesAccountUri }
            },
            {
                type: actionTypes.RECEIVE_ACTIVITIES,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `Sales Account activities - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});