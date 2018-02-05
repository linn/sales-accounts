export const getSalesAccountsLoading = (salesAccounts) => {
    if (!salesAccounts) {
        return false;
    }

    return salesAccounts.some(p => p.loading === true);
}

export const getSalesAccount = (salesAccountId, salesAccounts) => {
    if (!salesAccountId || !salesAccounts) {
        return null;
    }

    var salesAccount = salesAccounts.find(p => p.salesAccountId === salesAccountId);

    return salesAccount ? salesAccount.item || null : null;
}

export const getDiscountSchemeName = (salesAccount, discountSchemes) => {
    if (!salesAccount || !discountSchemes) {
        return null;
    }

    var discountScheme = discountSchemes.find(s => s.links.find(l => l.href === salesAccount.discountSchemeUri));

    return discountScheme ? discountScheme.name : null;
}

export const getTurnoverBandName = (salesAccount, turnoverBandSets) => {
    if (!salesAccount || !turnoverBandSets) {
        return null;
    }

    var turnoverBandSet = turnoverBandSets.find(t => t.links.find(l => l.href === salesAccount.turnoverBandUri));

    return turnoverBandSet ? turnoverBandSet.name : null;
}