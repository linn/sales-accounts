import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestTurnoverBandSets = () => ({
    type: actionTypes.REQUEST_TURNOVER_BAND_SETS,
    payload: { }
});

const receiveTurnoverBandSets = (data) => ({
    type: actionTypes.RECEIVE_TURNOVER_BAND_SETS,
    payload: { data }
});

export const fetchTurnoverBandSets = () => async (dispatch) => {
    dispatch(requestTurnoverBandSets());
    try {
        const data = await fetchJson(`${config.proxyRoot}/sales/discounting/turnover-band-sets`, { headers: { 'Accept': 'application/json' } });
        dispatch(receiveTurnoverBandSets(data));
    } catch (e) {
        alert(`Failed to fetch turnover band sets. Error: ${e.message}`);
    }
};

const requestTurnoverBand = () => ({
    type: actionTypes.REQUEST_TURNOVER_BAND,
    payload: { }
});

const receiveTurnoverBand = (data) => ({
    type: actionTypes.RECEIVE_TURNOVER_BAND,
    payload: { data }
});

export const fetchTurnoverBand = (salesAccount) => async (dispatch) => {
    
    const turnoverBandUri = salesAccount ? salesAccount.turnoverBandUri : null;
    if (turnoverBandUri) {
      
        try {
            const data = await fetchJson(`${config.proxyRoot}${turnoverBandUri}`, { headers: { 'Accept': 'application/json' } });
            dispatch(receiveTurnoverBand(data));
        } catch (e) {
            alert(`Failed to fetch turnover band. Error: ${e.message}`);
        }
    }
};

