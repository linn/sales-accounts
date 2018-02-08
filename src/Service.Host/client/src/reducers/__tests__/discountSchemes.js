import discountSchemes from '../discountSchemes';
import deepFreeze from 'deep-freeze';
import * as actionTypes from '../../actions';

describe('discount schemes reducer', () => {
    test('when requesting discount schemes', () => {
        const state = [];

        const action = {
            type: actionTypes.REQUEST_DISCOUNT_SCHEMES,
            payload: {}
        };

        const expected = []

        deepFreeze(state);

        expect(discountSchemes(state, action)).toEqual(expected);
    });

    test('when receiving discount schemes', () => {
        const state = [];

        const action = {
            type: actionTypes.RECEIVE_DISCOUNT_SCHEMES,
            payload:{
                data: [
                    {
                        name: 'Retailer',
                        rules: [],
                        links: []
                    },
                    {
                        name: 'CI',
                        rules: [],
                        links: []
                    },
                ]
            }
        };

        const expected =  [
            {
                name: 'Retailer',
                rules: [],
                links: []
            },
            {
                name: 'CI',
                rules: [],
                links: []
            },
        ]

        deepFreeze(state);

        expect(discountSchemes(state, action)).toEqual(expected);
    });
});

    