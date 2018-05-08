import salesAccounts from '../salesAccounts';
import deepFreeze from 'deep-freeze';
import * as actionTypes from '../../actions';

describe('sales accounts reducer', () => {
    test('when requesting sales accounts', () => {
        const state =  {
            loading: false,
            items: []
        };

        const action = {
            type: actionTypes.REQUEST_SALES_ACCOUNTS,
            payload: {}
        };

        const expected = {
            loading: true,
            items: []
        };

        deepFreeze(state);

        expect(salesAccounts(state, action)).toEqual(expected);
    });

    test('when receiving sales accounts', () => {
        const state = {
            loading: true,
            items: []
        };

        const action = {
            type: actionTypes.RECEIVE_SALES_ACCOUNTS,
            payload: {
                data: [
                    { 'id': 1, name: 'name', links: [ { rel: 'self', href: '/sa/1' } ]},
                    { 'id': 2, name: 'name2', links: [ { rel: 'self', href: '/sa/2' } ]}
                ]         
            }
        };

        const expected = {
            loading: false,
            items:
            [
                { item: { id: 1, name: 'name', uri: '/sa/1', links: [{ rel: 'self', href: '/sa/1' }] } },
                { item: { id: 2, name: 'name2', uri: '/sa/2', links: [{ rel: 'self', href: '/sa/2' }] } }
            ]
        };

        deepFreeze(state);

        expect(salesAccounts(state, action)).toEqual(expected);
    });
});