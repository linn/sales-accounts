import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

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
        const queryString = financialYear ? `?financialYear=${financialYear}` : '';
        const data = await fetchJson(`${config.appRoot}/sales/accounts/turnover-band-proposals${queryString}`);
        dispatch(receiveTurnoverBandProposal(data));
    } catch (e) {
        alert(`Failed to fetch turnover band proposal. Error: ${e.message}`);
    }
};