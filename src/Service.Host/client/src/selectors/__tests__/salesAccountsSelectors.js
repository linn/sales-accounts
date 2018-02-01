import { getSalesAccount, getSalesAccountName, getSalesAccountsLoading } from '../salesAccountsSelectors';

describe('when selecting sales account', () => {
    test('should return sales account', () => {
        const salesAccountId = 33;
        const salesAccounts = [
            {
                salesAccountId: 11,
                loading: false,
                item: {
                    name: '1'
                }
            },
            {
                salesAccountId: 33,
                loading: false,
                item: {
                    name: '2'
                }
            }
        ];

        const expectedResult = {
            name: '2'
        };
        expect(getSalesAccount(salesAccountId, salesAccounts)).toEqual(expectedResult);
    });
});

describe('when selecting sales account but not found', () => {
    test('should return null', () => {
        const salesAccountId = 1;
        const salesAccounts = [
            {
                salesAccountId: 2,
                loading: false,
                item: {
                    name: '2'
                }
            },
            {
                salesAccountId: 3,
                loading: false,
                item: {
                    name: '3'
                }
            }
        ];

        expect(getSalesAccount(salesAccountId, salesAccounts)).toEqual(null);
    });
});

describe('when loading', () => {
    test('should be true when loading', () => {
        const salesAccounts = [
            {
                salesAccountId: 11,
                loading: true,
                item: {
                    name: '1'
                }
            },
            {
                salesAccountId: 33,
                loading: false,
                item: {
                    name: '2'
                }
            }
        ];

        expect(getSalesAccountsLoading(salesAccounts)).toEqual(true);
    });
});

describe('when not loading', () => {
    test('should be false when not loading', () => {
        const salesAccounts = [
            {
                salesAccountId: 11,
                loading: false,
                item: {
                    name: '1'
                }
            },
            {
                salesAccountId: 33,
                loading: false,
                item: {
                    name: '2'
                }
            }
        ];

        expect(getSalesAccountsLoading(salesAccounts)).toEqual(false);
    });
});