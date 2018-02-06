import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestTurnoverBand = () => ({
    type: actionTypes.REQUEST_TURNOVER_BAND,
    payload: { }
});

const receiveTurnoverBand = (data) => ({
    type: actionTypes.RECEIVE_TURNOVER_BAND,
    payload: { data }
});

export const fetchTurnoverBand = (turnoverBandUri) => async (dispatch) => {
    if (turnoverBandUri) {
      
        try {
            const data = await fetchJson(`${config.proxyRoot}${turnoverBandUri}`, { headers: { 'Accept': 'application/json' } });
            dispatch(receiveTurnoverBand(data));
        } catch (e) {
            alert(`Failed to fetch turnover band. Error: ${e.message}`);
        }
    }
};