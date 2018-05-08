import { getSalesAccount } from '../salesAccountsSelectors';

describe('when getting sales account', () => {
    test('should return account', () => {

        const salesAccounts = {
            items: [
                { item: { id: 1, uri: '/1' } },
                { item: { id: 2, uri: '/2' } }
            ]
        };


        const expectedResult = { item: { id: 1, uri: '/1' }  };

        expect(getSalesAccount(salesAccounts, '/1')).toEqual(expectedResult);
    });
});

describe('when getting account before data is loaded', () => {
    test('should return null', () => {
        expect(getSalesAccount(null, '/1')).toEqual(null);
    });
});