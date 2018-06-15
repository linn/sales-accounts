import config from '../config';
import * as actionTypes from './index';
import { CALL_API } from 'redux-api-middleware';

export const fetchTurnoverBandSets = () => ({
    [CALL_API]: {
        endpoint: `${config.proxyRoot}/sales/discounting/turnover-band-sets`,
        method: 'GET',
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_TURNOVER_BAND_SETS,
                payload: { }
            },
            {
                type: actionTypes.RECEIVE_TURNOVER_BAND_SETS,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `turnover band sets - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});