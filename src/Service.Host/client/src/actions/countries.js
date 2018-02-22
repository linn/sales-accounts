import { fetchJson } from '../helpers/fetchJson';
import config from '../config';
import * as actionTypes from './index';

const requestCountry = () => ({
    type: actionTypes.REQUEST_COUNTRY,
    payload: { }
});

const receiveCountry = data => ({
    type: actionTypes.RECEIVE_COUNTRY,
    payload: { data }
});

export const fetchCountry = (countryUri) => async dispatch => {
    dispatch(requestCountry());
    try {
        const data = await fetchJson(`${config.proxyRoot}${countryUri}`, { headers: { 'Accept': 'application/json' } });
        dispatch(receiveCountry(data));
    } catch (e) {
        alert(`Failed to fetch country. Error: ${e.message}`);
    }
};