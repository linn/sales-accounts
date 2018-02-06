export const getSalesAccountsLoading = (salesAccount) => {
    return salesAccount ? true : false;
}

export const getSalesAccount = (salesAccount) => {
    return salesAccount ? salesAccount.item  : null;
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