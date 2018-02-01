import salesAccountSearch from '../salesAccountSearch';
import deepFreeze from 'deep-freeze';
import * as actionTypes from '../../actions';

describe('sales account search reducer', () => {
    test('when requesting sales accounts', () => {
        const state = {
            loading: false,
            items: [
                {
                    name: '1'
                }
            ],
            visible: false,
            searchTerm: 'search'
        };

        const action = {
            type: actionTypes.REQUEST_SALES_ACCOUNTS,
            payload: {
                searchTerm: 'new search'
            }
        };

        const expected = {
            visible: false,
            loading: true,
            searchTerm: 'new search',
            items: []
        }

        deepFreeze(state);

        expect(salesAccountSearch(state, action)).toEqual(expected);
    });

    test('when receiving results', () => {
        const state = {
            visible: false,
            loading: true,
            searchTerm: 'new search',
            items: []
        }

        const action = {
            type: actionTypes.RECEIVE_SALES_ACCOUNTS,
            payload: {
                searchTerm: 'new search',
                salesAccounts: [
                    {
                        name: '1'
                    },
                    {
                        name: '2'
                    }
                ]
            }
        };

        const expected = {
            visible: false,
            loading: false,
            searchTerm: 'new search',
            items: [
                {
                    name: '1'
                },
                {
                    name: '2'
                }
            ]
        };

        deepFreeze(state);

        expect(salesAccountSearch(state, action)).toEqual(expected);
    });

    test('when showing search', () => {
        const state = {
            loading: false,
            items: [
                {
                    name: '1'
                }
            ],
            visible: false,
            searchTerm: 'search'
        };

        const action = {
            type: actionTypes.SHOW_SALES_ACCOUNT_SEARCH
        };

        const expected = {
            visible: true,
            loading: false,
            searchTerm: '',
            items: []
        }

        deepFreeze(state);

        expect(salesAccountSearch(state, action)).toEqual(expected);
    });

    test('when hiding search', () => {
        const state = {
            loading: false,
            items: [
                {
                    name: '1'
                }
            ],
            visible: false,
            searchTerm: 'search'
        };

        const action = {
            type: actionTypes.HIDE_SALES_ACCOUNT_SEARCH
        };

        const expected = {
            visible: false,
            loading: false,
            searchTerm: '',
            items: []
        };

        deepFreeze(state);

        expect(salesAccountSearch(state, action)).toEqual(expected);
    });
});