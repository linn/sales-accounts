export const getSalesAccount = (salesAccounts, salesAccountUri) => {
    if(!salesAccounts || !salesAccounts.items){
        return null;
    }

    const account = salesAccounts.items.find(s => s.item && s.item.uri === salesAccountUri);
    return account ? account.item : null;
}
