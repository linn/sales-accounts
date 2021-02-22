import config from '../config';
import * as actionTypes from './index';
import { RSAA } from 'redux-api-middleware';

export const fetchDiscountSchemes = () => ({
    [RSAA]: {
        endpoint: `${config.proxyRoot}/sales/discounting/schemes?includeClosedSchemes=true`,
        method: 'GET',
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_DISCOUNT_SCHEMES,
                payload: { }
            },
            {
                type: actionTypes.RECEIVE_DISCOUNT_SCHEMES,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) => res ? `discount schemes - ${res.status} ${res.statusText}` : `Network request failed`,
            }
        ]
    }
});