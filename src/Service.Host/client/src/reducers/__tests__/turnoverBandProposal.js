import turnoverBandProposal from '../turnoverBandProposal';
import deepFreeze from 'deep-freeze';
import * as actionTypes from '../../actions';

describe('turnover band proposal reducer tests', () => {
    test('when requesting turnover band proposal', () => {
        const state = [];

        const action = {
            type: actionTypes.REQUEST_TURNOVER_BAND_PROPOSAL,
            payload: { financialYear : '2030'}
        };

        const expected = {
            financialYear: '2030',
            uri : null,
            proposedTurnoverBands: [],
            loading : true
        }

        deepFreeze(state);

        expect(turnoverBandProposal(state, action)).toEqual(expected);
    });

    test('when receiving turnover band proposal', () => {
        const state = {};

        const action = {
            type: actionTypes.RECEIVE_TURNOVER_BAND_PROPOSAL,
            payload: {
                data: {
                    financialYear : '2030',
                    proposedTurnoverBands: [{
                        proposedTurnoverBandUri: '/1',
                        calculatedTurnoverBandUri: '/1',
                        includeInUpdate: true,
                        appliedToAccount: false,
                        salesValueCurrency: 12,
                        links: [{ rel: 'sales-account', href: '/sales/accounts/1' }, { rel: 'self', href: '/tbp/1' }]
                    }],
                    links: [{ rel: 'self', href: '/sales/accounts/turnover-band-proposals?financialYear=2030' }]
                }
            }
        };

        const expected = {
            financialYear: '2030',
            proposedTurnoverBands: [
                {
                    salesAccountUri: '/sales/accounts/1',
                    proposedTurnoverBandUri: '/1',
                    calculatedTurnoverBandUri: '/1',
                    includeInUpdate: true,
                    appliedToAccount: false,
                    salesValueCurrency: 12,
                    uri: '/tbp/1'
                }],
            uri : '/sales/accounts/turnover-band-proposals?financialYear=2030',
            loading : false
        }

        deepFreeze(state);

        expect(turnoverBandProposal(state, action)).toEqual(expected);
    });

    test('when receiving not requested turnover band proposal', () => {
        const state = {
            financialYear : '2031'
        };

        const action = {
            type: actionTypes.RECEIVE_TURNOVER_BAND_PROPOSAL,
            payload: {
                data: {
                    financialYear: '2030',
                    proposedTurnoverBands: [{ id: 1 }],
                    links: [{ rel: 'self', href: '/sales/accounts/turnover-band-proposals?financialYear=2030' }]
                }
            }
        };

        const expected = {
            financialYear: '2031'
        }

        deepFreeze(state);

        expect(turnoverBandProposal(state, action)).toEqual(expected);
    });
});

    