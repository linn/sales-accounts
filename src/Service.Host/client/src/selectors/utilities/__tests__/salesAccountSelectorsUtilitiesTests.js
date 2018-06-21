import { getActivityEmployeeUris, getSalesAccountItem, getSalesAccountName, getSalesAccountId, getDiscountSchemeUri, getSalesAccountActivityTurnoverBandName, getSalesAccountActivityDiscountSchemeName, getActivityEmployeeName } from '../salesAccountSelectorUtilities';

describe('when selecting activity employee Uris', () => {
    test('should retrun employee uris', () => {
        const salesAccount = {
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                discountSchemeUri: '/sales/discounting/schemes/4',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '2018-06-14T11:08:01.9950000Z',
                address: {},
                links: [
                    {
                        href: '/sales/accounts/1',
                        rel: 'self'
                    }
                ]
            },
            activities: [
                {
                    discountSchemeUri: '/sales/discounting/schemes/4',
                    activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                    changedOn: '2018-06-14T14:47:18.3256500Z',
                    updatedByUri: '/employees/33067',
                    links: null
                },
                {
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                    activityType: 'SalesAccountUpdateTurnoverBandUriActivity',
                    changedOn: '2018-06-14T14:47:18.3264860Z',
                    updatedByUri: '/employees/123',
                    links: null
                },
            ]
        };

        const expected = ['/employees/33067', '/employees/123'];

        expect(getActivityEmployeeUris(salesAccount)).toEqual(expected);
    });

});

describe('when selecting sales account item', () => {
    test('should return item', () => {
        const salesAccount = {
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                discountSchemeUri: '/sales/discounting/schemes/4',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '2018-06-14T11:08:01.9950000Z',
                address: {},
                links: [
                    {
                        href: '/sales/accounts/1',
                        rel: 'self'
                    }
                ]
            },
            activities: [
                {
                    discountSchemeUri: '/sales/discounting/schemes/4',
                    activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                    changedOn: '2018-06-14T14:47:18.3256500Z',
                    updatedByUri: '/employees/33067',
                    links: null
                }
            ]
        };

        const expected = {
            id: 1,
            name: 'TeddyBartons',
            turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
            discountSchemeUri: '/sales/discounting/schemes/4',
            eligibleForGoodCreditDiscount: false,
            eligibleForRebate: false,
            growthPartner: false,
            closedOn: '2018-06-14T11:08:01.9950000Z',
            address: {},
            links: [
                {
                    href: '/sales/accounts/1',
                    rel: 'self'
                }
            ]
        };

        expect(getSalesAccountItem(salesAccount)).toEqual(expected);
    });
});

describe('when selecting sales account name', () => {
    test('should return name', () => {

        const salesAccount = {
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                discountSchemeUri: '/sales/discounting/schemes/4',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '2018-06-14T11:08:01.9950000Z',
                address: {},
                links: [
                    {
                        href: '/sales/accounts/1',
                        rel: 'self'
                    }
                ]
            }
        };

        const expectedResult = 'TeddyBartons';

        expect(getSalesAccountName(salesAccount)).toEqual(expectedResult);
    });
});

describe('when selecting discount scheme uri', () => {
    test('should return uri', () => {

        const salesAccount = {
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                discountSchemeUri: '/sales/discounting/schemes/4',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '2018-06-14T11:08:01.9950000Z',
                address: {},
                links: [
                    {
                        href: '/sales/accounts/1',
                        rel: 'self'
                    }
                ]
            }
        };

        const expectedResult = '/sales/discounting/schemes/4';

        expect(getDiscountSchemeUri(salesAccount)).toEqual(expectedResult);
    });
});

describe('when selecting sales account activity turnover band name', () => {
    test('should return turnover band name', () => {

        const activity = {
            turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
            activityType: 'SalesAccountUpdateTurnoverBandUriActivity',
            changedOn: '2018-04-26T14:37:22.5157640Z',
            updatedByUri: null,
            links: null
        }

        const turnoverBands = [
            {
                name: 'Retailer',
                turnoverBands: [
                    {
                        name: 'Retailer Low',
                        links: [
                            {
                                rel: 'self',
                                href: '/sales/discounting/turnover-band-sets/1/turnover-bands/1'
                            }
                        ]
                    },
                    {
                        name: 'Retailer High',
                        links: [
                            {
                                rel: 'self',
                                href: '/sales/discounting/turnover-band-sets/1/turnover-bands/2'
                            }
                        ]
                    }
                ],
                links: [
                    {
                        rel: 'self',
                        href: '/sales/discounting/turnover-band-sets/1'
                    }
                ]
            },
            {
                name: 'CI',
                turnoverBands: [
                    {
                        name: 'CI Low',
                        links: [
                            {
                                rel: 'self',
                                href: '/sales/discounting/turnover-band-sets/2/turnover-bands/3'
                            }
                        ]
                    },
                    {
                        name: 'CI High',
                        links: [
                            {
                                rel: 'self',
                                href: '/sales/discounting/turnover-band-sets/2/turnover-bands/4'
                            }
                        ]
                    }
                ],
                links: [
                    {
                        rel: 'self',
                        href: '/sales/discounting/turnover-band-sets/2'
                    }
                ]
            }
        ];

        const expectedResult = 'CI High';

        expect(getSalesAccountActivityTurnoverBandName(activity, turnoverBands)).toEqual(expectedResult);
    });
});

describe('when selecting sales account activity discount scheme name', () => {
    test('should return discount scheme name', () => {

        const activity = {
            discountSchemeUri: '/sales/discounting/schemes/4',
            activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
            changedOn: '2018-06-14T14:47:18.3256500Z',
            updatedByUri: '/employees/33067',
            links: null
        }

        const discountSchemes = [
            {
                name: 'CI',
                rules: null,
                closedOn: '',
                links: [
                    {
                        href: '/sales/discounting/schemes/3',
                        rel: 'self'
                    },
                    {
                        href: '/sales/discounting/turnover-band-sets/1',
                        rel: 'turnover-band-set'
                    }
                ]
            },
            {
                name: 'Distributor',
                rules: null,
                closedOn: '',
                links: [
                    {
                        href: '/sales/discounting/schemes/4',
                        rel: 'self'
                    },
                    {
                        href: '/sales/discounting/turnover-band-sets/2',
                        rel: 'turnover-band-set'
                    }
                ]
            }
        ];

        const expectedResult = 'Distributor';

        expect(getSalesAccountActivityDiscountSchemeName(activity, discountSchemes)).toEqual(expectedResult);
    });
});

describe('when selecting activity employee name', () => {
    test('should return employee name', () => {
        const activity = {
            discountSchemeUri: '/sales/discounting/schemes/4',
            activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
            changedOn: '2018-06-14T14:47:18.3256500Z',
            updatedByUri: '/employees/123',
            links: null
        }

        const employees = {
            loading: false,
            items: [
                {
                    id: 33067,
                    firstName: 'Peter',
                    lastName: 'Beardsley',
                    userName: 'peterb',
                    emailAddress: 'peter.beardsley@linn.co.uk',
                    href: '/employees/123',
                    fullName: 'Peter Beardsley',
                    links: [
                        {
                            href: '/employees/123',
                            rel: 'self'
                        }
                    ],
                    loading: false
                }
            ]
        }

        const expectedResult = 'Peter Beardsley';

        expect(getActivityEmployeeName(activity, employees)).toEqual(expectedResult);
    });
});

describe('when selecting sales account id', () => {
    test('should return id', () => {
        const salesAccount = {
            loading: false,
            editDiscountSchemeVisible: false,
            editTurnoverBandVisible: false,
            editGoodCreditVisible: false,
            editGrowthPartnerVisible: false,
            editEligibleForRebateVisible: false,
            dirty: false,
            saving: false,
            item: {
                id: 1,
                name: 'TeddyBartons',
                turnoverBandUri: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                discountSchemeUri: '/sales/discounting/schemes/4',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: '2018-06-14T11:08:01.9950000Z',
                address: {},
                links: [
                    {
                        href: '/sales/accounts/1',
                        rel: 'self'
                    }
                ]
            }
        }

        const expectedResult = 1;

        expect(getSalesAccountId(salesAccount)).toEqual(expectedResult);
    });
});