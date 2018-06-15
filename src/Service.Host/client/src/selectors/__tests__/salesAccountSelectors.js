import { getSalesAccount, getSalesAccountName, getSalesAccountId, getDiscountSchemeName, getSalesAccountTurnoverBandName, getTurnoverBands, getActivities } from '../salesAccountSelectors';

describe('when selecting sales account', () => {
    test('should return sales account', () => {

        const salesAccount = {
            salesAccountUri: '/sales/accounts/1',
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: ''
            }
        };

        const expectedResult = {
            id: 1,
            name: 'TeddyBartons',
            turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
            discountSchemeUri: '/sales/discounting/schemes/1',
            eligibleForGoodCreditDiscount: false,
            eligibleForRebate: false,
            growthPartner: false,
            closedOn: ''
        }
        expect(getSalesAccount(salesAccount)).toEqual(expectedResult);
    });
});

describe('when selecting sales account but not found', () => {
    test('should return null', () => {
        const salesAccount = {}

        expect(getSalesAccount(salesAccount)).toEqual(null);
    });
});

describe('when selecting sales account name', () => {
    test('should return name', () => {

        const salesAccount = {
            id: 1,
            name: 'Name'
        };

        const expectedResult = 'Name';

        expect(getSalesAccountName(salesAccount)).toEqual(expectedResult);
    });
});

describe('when selecting sales account id', () => {
    test('should return id', () => {

        const salesAccount = {
            id: 1,
            name: 'Name'
        };

        const expectedResult = 1;

        expect(getSalesAccountId(salesAccount)).toEqual(expectedResult);
    });
});

describe('when selecting discount scheme name', () => {
    test('should return name', () => {

        const salesAccount = {
            id: 1,
            name: 'TeddyBartons',
            turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
            discountSchemeUri: '/sales/discounting/schemes/1',
            eligibleForGoodCreditDiscount: false,
            eligibleForRebate: false,
            growthPartner: false,
            closedOn: ''
        }

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

        const expectedResult = 'Retailer';

        expect(getDiscountSchemeName(salesAccount, discountSchemes)).toEqual(expectedResult);
    });
});

describe('when selecting turnover band name', () => {
    test('should return name', () => {

        const salesAccount = {
            id: 1,
            name: 'TeddyBartons',
            turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
            discountSchemeUri: '/sales/discounting/schemes/1',
            eligibleForGoodCreditDiscount: false,
            eligibleForRebate: false,
            growthPartner: false,
            closedOn: ''
        }

        const turnoverBandSets = [
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

        const expectedResult = 'Retailer Low'

        expect(getSalesAccountTurnoverBandName(salesAccount, turnoverBandSets)).toEqual(expectedResult);
    });
});

describe('when selecting turnover bands', () => {
    test('should return band for the sales account\'s current discount scheme', () => {

        const salesAccount = {
            id: 1,
            name: 'TeddyBartons',
            turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
            discountSchemeUri: '/sales/discounting/schemes/1',
            eligibleForGoodCreditDiscount: false,
            eligibleForRebate: false,
            growthPartner: false,
            closedOn: ''
        };

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

        const turnoverBandSets = [
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

        const expectedResult = [
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
        ];

        expect(getTurnoverBands(salesAccount, turnoverBandSets, discountSchemes)).toEqual(expectedResult);
    });
});

describe('when selecting sales account activities', () => {
    test('should return activities when no employees', () => {

        const salesAccount = {
            salesAccountUri: '/sales/accounts/1',
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: ''
            },
            activities: [
                {
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    activityType: 'SalesAccountCloseActivity',
                    changedOn: '2018-06-14T10:08:02.1340160Z',
                    links: null
                },
                {
                    turnoverBandUri: null,
                    activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                    changedOn: '2018-06-13T13:14:00.6373120Z',
                    links: null
                }
            ]
        };

        const expectedResult = [
            {
                closedOn: '2018-06-14T11:08:01.9950000Z',
                activityType: 'SalesAccountCloseActivity',
                changedOn: '2018-06-14T10:08:02.1340160Z',
                links: null
            },
            {
                turnoverBandUri: null,
                activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                changedOn: '2018-06-13T13:14:00.6373120Z',
                links: null
            }
        ]

        expect(getActivities(salesAccount)).toEqual(expectedResult);
    });

    test('should return activities with employee names', () => {

        const salesAccount = {
            salesAccountUri: '/sales/accounts/1',
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
                turnoverBandUri: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                discountSchemeUri: '/sales/discounting/schemes/1',
                eligibleForGoodCreditDiscount: false,
                eligibleForRebate: false,
                growthPartner: false,
                closedOn: ''
            },
            activities: [
                {
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    activityType: 'SalesAccountCloseActivity',
                    changedOn: '2018-06-14T10:08:02.1340160Z',
                    links: null,
                    updatedByUri: '/employees/3306'
                },
                {
                    turnoverBandUri: null,
                    activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                    changedOn: '2018-06-13T13:14:00.6373120Z',
                    links: null,
                    updatedByUri: '/employees/123'
                }
            ]
        };

        const employees = {
            loading: false,
            items: [
                {
                    id: 3306,
                    firstName: 'Teddy',
                    lastName: 'Barton',
                    userName: 'teddyb',
                    emailAddress: 'teddy.barton@linn.co.uk',
                    href: '/employees/3306',
                    fullName: 'Teddy Barton',
                    links: [
                        {
                            href: '/employees/3306',
                            rel: 'self'
                        }
                    ]
                },
                {
                    id: 123,
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
                    ]
                }
            ]
        }

        const expectedResult = [
            {
                closedOn: '2018-06-14T11:08:01.9950000Z',
                activityType: 'SalesAccountCloseActivity',
                changedOn: '2018-06-14T10:08:02.1340160Z',
                links: null,
                updatedByUri: '/employees/3306',
                updatedByName: 'Teddy Barton'
            },
            {
                turnoverBandUri: null,
                activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                changedOn: '2018-06-13T13:14:00.6373120Z',
                links: null,
                updatedByUri: '/employees/123',
                updatedByName: 'Peter Beardsley'
            }
        ]

        expect(getActivities(salesAccount, employees)).toEqual(expectedResult);
    });
});