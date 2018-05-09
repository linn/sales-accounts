export const getFinancialYear = (turnoverBandProposal) => {
    if (!turnoverBandProposal || !turnoverBandProposal.financialYear){
        return null;
    }

    return turnoverBandProposal.financialYear;
}

export const getProposedTurnoverBands = (turnoverBandProposal) => {
    if (!turnoverBandProposal || !turnoverBandProposal.proposedTurnoverBands) {
        return null;
    }

    return turnoverBandProposal.proposedTurnoverBands;
}

export const getLoading = (turnoverBandProposal) => {
    if (!turnoverBandProposal || !turnoverBandProposal.loading) {
        return null;
    }

    return turnoverBandProposal.loading;
}
