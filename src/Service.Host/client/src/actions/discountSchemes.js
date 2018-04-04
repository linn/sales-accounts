import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestDiscountSchemes = () => ({
    type: actionTypes.REQUEST_DISCOUNT_SCHEMES,
    payload: { }
});

const receiveDiscountSchemes = data => ({
    type: actionTypes.RECEIVE_DISCOUNT_SCHEMES,
    payload: { data }
});

export const fetchDiscountSchemes = () => async dispatch => {
    dispatch(requestDiscountSchemes());
    try {
        const data = await fetchJson(`${config.proxyRoot}/sales/discounting/schemes?includeClosedSchemes=true`, { headers: { 'Accept': 'application/json' } });
        dispatch(receiveDiscountSchemes(data));
    } catch (e) {
        alert(`Failed to fetch discount Schemes. Error: ${e.message}`);
    }
};
