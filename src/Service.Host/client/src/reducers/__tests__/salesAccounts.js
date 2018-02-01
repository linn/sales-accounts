import salesAccounts from '../salesAccounts';
import deepFreeze from 'deep-freeze';
import * as actionTypes from '../../actions';

describe('sales accounts reducer', () => {
    test('when requesting an account', () => {
        const state = [];

        const action = {
            type: actionTypes.REQUEST_SALES_ACCOUNT,
            payload: {
                salesAccountUri: '/s/a/1'
            }
        };

        const expected = [
            {
                salesAccountUri: '/s/a/1',
                loading: true,
                item: null
            }];

        deepFreeze(state);

        expect(salesAccounts(state, action)).toEqual(expected);
    });

    test('when receiving a sales account', () => {
        const state = [
            {
                salesAccountUri: '/s/a/1',
                loading: true,
                item: null
            }];

        const action = {
            type: actionTypes.RECEIVE_SALES_ACCOUNT,
            payload: {
                salesAccountUri: '/s/a/1',
                salesAccount: {
                    name: '1'
                }
            }
        };

        const expected = [
            {
                salesAccountUri: '/s/a/1',
                loading: false,
                item: {
                    name: '1'
                }
            }];

        deepFreeze(state);

        expect(salesAccounts(state, action)).toEqual(expected);
    });
});
