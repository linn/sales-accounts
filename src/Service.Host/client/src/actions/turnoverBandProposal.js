import { fetchJson, postJson, putJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const financialYearQueryString = (financialYear) => (financialYear ? `?financialYear=${financialYear}` : '');

const requestTurnoverBandProposal = (financialYear) => ({
    type: actionTypes.REQUEST_TURNOVER_BAND_PROPOSAL,
    payload: { financialYear }
});

const receiveTurnoverBandProposal = data => ({
    type: actionTypes.RECEIVE_TURNOVER_BAND_PROPOSAL,
    payload: { data }
});

const requestUpdateProposedTurnoverBand = (uri, turnoverBandUri) => ({
    type: actionTypes.REQUEST_UPDATE_PROPOSED_TURNOVER_BAND,
    payload: { uri, turnoverBandUri }
});

const receiveUpdateProposedTurnoverBand = (uri, data) => ({
    type: actionTypes.RECEIVE_UPDATE_PROPOSED_TURNOVER_BAND,
    payload: { uri, data }
});

export const fetchTurnoverBandProposal = (financialYear) => async dispatch => {
    dispatch(requestTurnoverBandProposal(financialYear));
    try {
        const data = await fetchJson(`${config.appRoot}/sales/accounts/turnover-band-proposals${financialYearQueryString(financialYear)}`);
        dispatch(receiveTurnoverBandProposal(data));
    } catch (e) {
        alert(`Failed to fetch turnover band proposal. Error: ${e.message}`);
    }
};

export const calculateTurnoverBandProposal = (financialYear) => async dispatch => {
    dispatch(requestTurnoverBandProposal(financialYear));
    try {
        const data = await postJson(`${config.appRoot}/sales/accounts/turnover-band-proposals`, { financialYear : financialYear ? financialYear : null});
        dispatch(receiveTurnoverBandProposal(data));
    } catch (e) {
        alert(`Failed to calculate turnover band proposal. Error: ${e.message}`);
    }
};

export const updateProposedTurnoverBand = (uri, turnoverBandUri) => async dispatch => {
    dispatch(requestUpdateProposedTurnoverBand(uri, turnoverBandUri));
    try {
        const data = await putJson(`${config.appRoot}${uri}`, { turnoverBandUri });
        dispatch(receiveUpdateProposedTurnoverBand(uri, data));
    } catch (e) {
        alert(`Failed to update proposed turnover band. Error: ${e.message}`);
    }
};

export const applyTurnoverBandProposal = (uri, financialYear) => async dispatch => {
    dispatch(requestTurnoverBandProposal(financialYear));
    try {
        const data = await postJson(`${config.appRoot}${uri}`, {});
        dispatch(receiveTurnoverBandProposal(data));
    } catch (e) {
        alert(`Failed to apply turnover band proposal. Error: ${e.message}`);
    }
};