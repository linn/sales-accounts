import turnoverBandSets from '../turnoverBandSets';
import deepFreeze from 'deep-freeze';
import * as actionTypes from '../../actions';

describe('turnover band sets reducer', () => {
    test('when requesting turnover band sets', () => {
        const state = [];

        const action = {
            type: actionTypes.REQUEST_TURNOVER_BAND_SETS,
            payload: {}
        };

        const expected = []

        deepFreeze(state);

        expect(turnoverBandSets(state, action)).toEqual(expected);
    });

    test('when receiving turnover band sets', () => {
        const state = [];

        const action = {
            type: actionTypes.RECEIVE_TURNOVER_BAND_SETS,
            payload:{
                data: [
                    {
                        name: 'Retailer',
                        turnoverBands: [],
                        links: []
                    },
                    {
                        name: 'CI',
                        turnoverBands: [],
                        links: []
                    },
                ]
            }
        };

        const expected =  [
            {
                name: 'Retailer',
                turnoverBands: [],
                links: []
            },
            {
                name: 'CI',
                turnoverBands: [],
                links: []
            },
        ]

        deepFreeze(state);

        expect(turnoverBandSets(state, action)).toEqual(expected);
    });
});

    