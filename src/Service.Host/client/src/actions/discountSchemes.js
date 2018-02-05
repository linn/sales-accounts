import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestDiscountScheme = () => ({
    type: actionTypes.REQUEST_DISCOUNT_SCHEMES,
    payload: { }
});

const receiveDiscountScheme = (data) => ({
    type: actionTypes.RECEIVE_DISCOUNT_SCHEMES,
    payload: { data }
});

export const fetchDiscountSchemes = () => async (dispatch) => {
    dispatch(requestDiscountScheme());
    try {
        const data = await fetchJson(`${config.proxyRoot}/sales/discounting/schemes`, { headers: { 'Accept': 'application/json' } });
        dispatch(receiveDiscountScheme(data));
    } catch (e) {
        alert(`Failed to fetch discount Schemes. Error: ${e.message}`);
    }
};
