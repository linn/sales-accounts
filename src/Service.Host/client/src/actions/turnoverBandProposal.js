import { fetchJson, postJson, putJson, deleteJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';
import { CALL_API } from 'redux-api-middleware';

const financialYearQueryString = (financialYear) => (financialYear ? `?financialYear=${financialYear}` : '');

export const fetchTurnoverBandProposal = financialYear => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}/sales/accounts/turnover-band-proposals${financialYearQueryString(financialYear)}`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_TURNOVER_BAND_PROPOSAL,
                payload: { financialYear }
            },
            {
                type: actionTypes.RECEIVE_TURNOVER_BAND_PROPOSAL,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `turnover band proposal - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});

export const calculateTurnoverBandProposal = financialYear => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}/sales/accounts/turnover-band-proposals`,
        method: 'POST',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            financialYear : financialYear ? financialYear : null
        }),
        types: [
            {
                type: actionTypes.REQUEST_TURNOVER_BAND_PROPOSAL,
                payload: { financialYear }
            },
            {
                type: actionTypes.RECEIVE_TURNOVER_BAND_PROPOSAL,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `calculate turnover band proposal - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});

export const updateProposedTurnoverBand = (uri, turnoverBandUri) => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}${uri}`,
        method: 'PUT',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            turnoverBandUri
        }),
        types: [
            {
                type: actionTypes.REQUEST_UPDATE_PROPOSED_TURNOVER_BAND,
                payload: { uri, turnoverBandUri }
            },
            {
                type: actionTypes.RECEIVE_UPDATE_PROPOSED_TURNOVER_BAND,
                payload: async (action, state, res) => ({ data: await res.json(), uri })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `update proposed turnover band - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});

export const excludeProposedTurnoverBand = uri => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}${uri}`,
        method: 'DELETE',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({}),
        types: [
            {
                type: actionTypes.REQUEST_UPDATE_PROPOSED_TURNOVER_BAND,
                payload: { uri }
            },
            {
                type: actionTypes.RECEIVE_UPDATE_PROPOSED_TURNOVER_BAND,
                payload: async (action, state, res) => ({ data: await res.json(), uri })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `exclude proposed turnover band - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});

export const applyTurnoverBandProposal = (uri, financialYear) => ({
    [CALL_API]: {
        endpoint: `${config.appRoot}${uri}`,
        method: 'POST',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({}),
        types: [
            {
                type: actionTypes.REQUEST_TURNOVER_BAND_PROPOSAL,
                payload: { financialYear }
            },
            {
                type: actionTypes.RECEIVE_TURNOVER_BAND_PROPOSAL,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `apply turnover band proposal - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});