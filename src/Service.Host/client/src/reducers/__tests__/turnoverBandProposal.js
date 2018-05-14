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
        const state = {
            financialYear: '2030'
        };

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
                    links: [
                        { rel: 'self', href: '/sales/accounts/turnover-band-proposals?financialYear=2030' },
                        { rel: 'apply-proposal', href: '/sales/accounts/turnover-band-proposals/apply?financialYear=2030' }]
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
            uri: '/sales/accounts/turnover-band-proposals?financialYear=2030',
            applyUri: '/sales/accounts/turnover-band-proposals/apply?financialYear=2030',
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

    test('when requesting update to proposed turnover band', () => {
        const state = {};

        const action = {
            type: actionTypes.REQUEST_UPDATE_PROPOSED_TURNOVER_BAND,
            payload: { uri : '/1', turnoverBandUri: '/2' }
        };

        const expected = {}

        deepFreeze(state);

        expect(turnoverBandProposal(state, action)).toEqual(expected);
    });

    test('when receiving update to proposed turnover band', () => {
        const state = {
            financialYear: '2030',
            uri: '/sales/accounts/turnover-band-proposals?financialYear=2030',
            proposedTurnoverBands: [{ uri: '/tbp/1' }, { uri: '/tbp/2' }, { uri: '/tbp/3' }]
        };

        const action = {
            type: actionTypes.RECEIVE_UPDATE_PROPOSED_TURNOVER_BAND,
            payload: {
                uri: '/tbp/2',
                data: {
                        proposedTurnoverBandUri: '/1',
                        calculatedTurnoverBandUri: '/1',
                        includeInUpdate: true,
                        appliedToAccount: false,
                        salesValueCurrency: 12,
                        links: [{ rel: 'sales-account', href: '/sales/accounts/1' }, { rel: 'self', href: '/tbp/2' }]
                }
            }
        };

        const expected = {
            financialYear: '2030',
            uri: '/sales/accounts/turnover-band-proposals?financialYear=2030',
            proposedTurnoverBands: [
                { uri: '/tbp/1' },
                {
                    salesAccountUri: '/sales/accounts/1',
                    proposedTurnoverBandUri: '/1',
                    calculatedTurnoverBandUri: '/1',
                    includeInUpdate: true,
                    appliedToAccount: false,
                    salesValueCurrency: 12,
                    uri: '/tbp/2'
                },
                { uri: '/tbp/3' }]
        }

        deepFreeze(state);

        expect(turnoverBandProposal(state, action)).toEqual(expected);
    });

});

    