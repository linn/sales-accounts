import { fetchJson, postJson } from '../helpers/fetchJson';
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