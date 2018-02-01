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
