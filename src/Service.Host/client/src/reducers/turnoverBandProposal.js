import * as actionTypes from '../actions';
import { getSelfHref, getHref } from '../helpers/utilities';

const getProposedTurnoverBand = (data) => (
    {
        calculatedTurnoverBandUri: data.calculatedTurnoverBandUri,
        proposedTurnoverBandUri: data.proposedTurnoverBandUri,
        includeInUpdate: data.includeInUpdate,
        appliedToAccount: data.appliedToAccount,
        salesValueCurrency: data.salesValueCurrency,
        salesAccountUri: getHref(data, 'sales-account'),
        uri: getSelfHref(data)
    });

const turnoverBandProposal = (state = {}, action) => {
    switch (action.type) {
        case actionTypes.REQUEST_TURNOVER_BAND_PROPOSAL:
            return {
                financialYear: action.payload.financialYear,
                loading: true,
                proposedTurnoverBands: [],
                uri : null
            };

        case actionTypes.RECEIVE_TURNOVER_BAND_PROPOSAL:
            if (!state.financialYear || state.financialYear === action.payload.data.financialYear) {
                return {
                    financialYear: action.payload.data.financialYear,
                    loading: false,
                    proposedTurnoverBands: action.payload.data.proposedTurnoverBands.map(t => getProposedTurnoverBand(t)),
                    uri: getSelfHref(action.payload.data),
                    applyUri: getHref(action.payload.data, 'apply-proposal')
                }
            }

            return state;
        case actionTypes.REQUEST_UPDATE_PROPOSED_TURNOVER_BAND:
            return state;

        case actionTypes.RECEIVE_UPDATE_PROPOSED_TURNOVER_BAND:
        {
            const index = state.proposedTurnoverBands.findIndex(p => p.uri === action.payload.uri);
            return {
                ...state,
                proposedTurnoverBands: index > -1
                    ? [
                        ...state.proposedTurnoverBands.slice(0, index),
                        getProposedTurnoverBand(action.payload.data),
                        ...state.proposedTurnoverBands.slice(index + 1)
                    ]
                    : [
                        ...state.proposedTurnoverBands,
                        getProposedTurnoverBand(action.payload.data)
                    ]
            }
        }

        default:
            return state;
    }
}

export default turnoverBandProposal;