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
            if (!state.financialYear || state.financialYear === action.payload.financialYear) {
                return {
                    financialYear: action.payload.data.financialYear,
                    loading: false,
                    proposedTurnoverBands: action.payload.data.proposedTurnoverBands.map(t => getProposedTurnoverBand(t)),
                    uri: getSelfHref(action.payload.data)
                }
            }

            return state;
    default:
        return state;
    }
}

export default turnoverBandProposal;