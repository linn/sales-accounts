import { getTurnoverBandSetUri, getDiscountScheme } from '../discountSchemeSelectorUtilities';

describe('when selecting turnover band set uri', () => {
    test('should return turnover band set uri', () => {

        const discountScheme = {
            name: 'Retailer',
            rules: null,
            closedOn: '',
            links: [
                {
                    href: '/sales/discounting/schemes/9',
                    rel: 'self'
                },
                {
                    href: '/sales/discounting/turnover-band-sets/3',
                    rel: 'turnover-band-set'
                }
            ]
        }

        const expectedResult = '/sales/discounting/turnover-band-sets/3';

        expect(getTurnoverBandSetUri(discountScheme)).toEqual(expectedResult);
    });
});

describe('when selecting discountScheme', () => {
    test('should return discountScheme', () => {
        
        const discountSchemes = [
            {
                name: 'Retailer',
                rules: [],
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

        const discountSchemeUri = '/sales/discounting/schemes/1';

        const expectedResult = {
            name: 'Retailer',
            rules: [],
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
        };

        expect(getDiscountScheme(discountSchemes, discountSchemeUri)).toEqual(expectedResult);
    });
});
