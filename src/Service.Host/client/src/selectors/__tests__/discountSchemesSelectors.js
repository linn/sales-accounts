import { getDiscountSchemes } from '../discountSchemesSelectors';

describe('when selecting discount schemes', () => {
    test('should return only open discount schemes', () => {

        const discountSchemes = [
            {
                name: 'Retailer',
                rules: [],
                closedOn: '',
                links: [
                    {
                        rel: 'self',
                        href: '/sales/discounting/schemes/1'
                    },
                    {
                        rel: 'turnover-band-set',
                        href: '/sales/discounting/turnover-band-sets/1'
                    }
                ]
            },
            {
                name: 'CI',
                rules: [],
                closedOn: '2008-09-15T15:53:00.0000000',
                links: [
                    {
                        rel: 'self',
                        href: '/sales/discounting/schemes/2'
                    },
                    {
                        rel: 'turnover-band-set',
                        href: '/sales/discounting/turnover-band-sets/2'
                    }
                ]
            }
        ];

        const expectedResult = [
            {
                name: 'Retailer',
                rules: [],
                closedOn: '',
                links: [
                    {
                        rel: 'self',
                        href: '/sales/discounting/schemes/1'
                    },
                    {
                        rel: 'turnover-band-set',
                        href: '/sales/discounting/turnover-band-sets/1'
                    }
                ]
            }
        ];
        
        expect(getDiscountSchemes(discountSchemes)).toEqual(expectedResult);
    });
});