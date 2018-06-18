import { getSalesAccount, getSalesAccountName, getSalesAccountId, getDiscountSchemeName, getSalesAccountTurnoverBandName, getTurnoverBands, getActivities, getSalesAccountLoading } from '../salesAccountSelectors';

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

describe('when selecting sales account loading', () => {    

    test('should return true when sales account loading', () => {

        const state = {
            salesAccount: {
                loading: true,
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
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                    discountSchemeUri: '/sales/discounting/schemes/9',
                    eligibleForGoodCreditDiscount: true,
                    eligibleForRebate: true,
                    growthPartner: true,
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    address: {
                        line1: 'Line 1',
                        line2: 'Line 2',
                        line3: 'Line 3',
                        line4: 'Line 4 ',
                        countryUri: '/countries/AF',
                        postcode: 'Postcode',
                        countryName: 'Afghanistan'
                    },
                    links: [
                        {
                            href: '/sales/accounts/1',
                            rel: 'self'
                        }
                    ]
                },
                activities: [
                    {
                        closedOn: '2018-06-14T11:08:01.9950000Z',
                        activityType: 'SalesAccountCloseActivity',
                        changedOn: '2018-06-14T10:08:02.1340160Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                    {
                        turnoverBandUri: null,
                        activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                        changedOn: '2018-06-13T13:14:00.6373120Z',
                        links: null,
                        updatedByUri: '/employees/987'
                    },
                    {
                        discountSchemeUri: '/sales/discounting/schemes/9',
                        activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                        changedOn: '2018-06-13T13:20:27.0841650Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                ],
            },
            discountSchemes: [
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
                },
                {
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
            ],
            turnoverBandSets: [
                {
                    name: 'CI',
                    turnoverBands: [
                        {
                            name: 'CI Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'CI High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/1/turnover-bands/2',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/1',
                            rel: 'self'
                        }
                    ]
                },
                {
                    name: 'Distributor',
                    turnoverBands: [
                        {
                            name: 'Distributor Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/2/turnover-bands/3',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'Distributor High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/2',
                            rel: 'self'
                        }
                    ]
                },
                {
                    name: 'Retailer',
                    turnoverBands: [
                        {
                            name: 'Retailer Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'Retailer High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/3/turnover-bands/6',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/3',
                            rel: 'self'
                        }
                    ]
                }
            ],
            employees: {
                items: [
                    {
                        loading: false,
                        id: 987,
                        firstName: 'Peter',
                        lastName: 'Beardsley',
                        userName: 'peterb',
                        emailAddress: 'peter.beardsley@linn.co.uk',
                        href: '/employees/987',
                        fullName: 'Peter Beardsley',
                        links: [
                            {
                                href: '/employees/987',
                                rel: 'self'
                            }
                        ]
                    }
                ]
            }
        }

        expect(getSalesAccountLoading(state)).toEqual(true);
    });

    test('should return true when other loading', () => {
        const state = {
            salesAccount: {
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
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                    discountSchemeUri: '/sales/discounting/schemes/9',
                    eligibleForGoodCreditDiscount: true,
                    eligibleForRebate: true,
                    growthPartner: true,
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    address: {
                        line1: 'Line 1',
                        line2: 'Line 2',
                        line3: 'Line 3',
                        line4: 'Line 4 ',
                        countryUri: '/countries/AF',
                        postcode: 'Postcode',
                        countryName: 'Afghanistan'
                    },
                    links: [
                        {
                            href: '/sales/accounts/1',
                            rel: 'self'
                        }
                    ]
                },
                activities: [
                    {
                        closedOn: '2018-06-14T11:08:01.9950000Z',
                        activityType: 'SalesAccountCloseActivity',
                        changedOn: '2018-06-14T10:08:02.1340160Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                    {
                        turnoverBandUri: null,
                        activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                        changedOn: '2018-06-13T13:14:00.6373120Z',
                        links: null,
                        updatedByUri: '/employees/987'
                    },
                    {
                        discountSchemeUri: '/sales/discounting/schemes/9',
                        activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                        changedOn: '2018-06-13T13:20:27.0841650Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                ],
            },
            discountSchemes: [
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
                },
                {
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
            ],
            turnoverBandSets: [
                {
                    name: 'CI',
                    turnoverBands: [
                        {
                            name: 'CI Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'CI High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/1/turnover-bands/2',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/1',
                            rel: 'self'
                        }
                    ]
                },
                {
                    name: 'Distributor',
                    turnoverBands: [
                        {
                            name: 'Distributor Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/2/turnover-bands/3',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'Distributor High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/2',
                            rel: 'self'
                        }
                    ]
                },
                {
                    name: 'Retailer',
                    turnoverBands: [
                        {
                            name: 'Retailer Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'Retailer High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/3/turnover-bands/6',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/3',
                            rel: 'self'
                        }
                    ]
                }
            ],
            employees: {
                items: [
                    {
                        loading: true,
                        id: 987,
                        firstName: 'Peter',
                        lastName: 'Beardsley',
                        userName: 'peterb',
                        emailAddress: 'peter.beardsley@linn.co.uk',
                        href: '/employees/987',
                        fullName: 'Peter Beardsley',
                        links: [
                            {
                                href: '/employees/987',
                                rel: 'self'
                            }
                        ]
                    }
                ]
            }
        }

        expect(getSalesAccountLoading(state)).toEqual(true);
    });

    test('should return true when no turnover bands', () => {

        const state = {
            salesAccount: {
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
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                    discountSchemeUri: '/sales/discounting/schemes/9',
                    eligibleForGoodCreditDiscount: true,
                    eligibleForRebate: true,
                    growthPartner: true,
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    address: {
                        line1: 'Line 1',
                        line2: 'Line 2',
                        line3: 'Line 3',
                        line4: 'Line 4 ',
                        countryUri: '/countries/AF',
                        postcode: 'Postcode',
                        countryName: 'Afghanistan'
                    },
                    links: [
                        {
                            href: '/sales/accounts/1',
                            rel: 'self'
                        }
                    ]
                },
                activities: [
                    {
                        closedOn: '2018-06-14T11:08:01.9950000Z',
                        activityType: 'SalesAccountCloseActivity',
                        changedOn: '2018-06-14T10:08:02.1340160Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                    {
                        turnoverBandUri: null,
                        activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                        changedOn: '2018-06-13T13:14:00.6373120Z',
                        links: null,
                        updatedByUri: '/employees/987'
                    },
                    {
                        discountSchemeUri: '/sales/discounting/schemes/9',
                        activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                        changedOn: '2018-06-13T13:20:27.0841650Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                ],
            },
            discountSchemes: [
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
                },
                {
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
            ],
            employees: {
                items: [
                    {
                        loading: false,
                        id: 987,
                        firstName: 'Peter',
                        lastName: 'Beardsley',
                        userName: 'peterb',
                        emailAddress: 'peter.beardsley@linn.co.uk',
                        href: '/employees/987',
                        fullName: 'Peter Beardsley',
                        links: [
                            {
                                href: '/employees/987',
                                rel: 'self'
                            }
                        ]
                    }
                ]
            }
        }

        expect(getSalesAccountLoading(state)).toEqual(true);
    });

    test('should return false when none are loading', () => {

        const state = {
            salesAccount: {
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
                    turnoverBandUri: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                    discountSchemeUri: '/sales/discounting/schemes/9',
                    eligibleForGoodCreditDiscount: true,
                    eligibleForRebate: true,
                    growthPartner: true,
                    closedOn: '2018-06-14T11:08:01.9950000Z',
                    address: {
                        line1: 'Line 1',
                        line2: 'Line 2',
                        line3: 'Line 3',
                        line4: 'Line 4 ',
                        countryUri: '/countries/AF',
                        postcode: 'Postcode',
                        countryName: 'Afghanistan'
                    },
                    links: [
                        {
                            href: '/sales/accounts/1',
                            rel: 'self'
                        }
                    ]
                },
                activities: [
                    {
                        closedOn: '2018-06-14T11:08:01.9950000Z',
                        activityType: 'SalesAccountCloseActivity',
                        changedOn: '2018-06-14T10:08:02.1340160Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                    {
                        turnoverBandUri: null,
                        activityType: 'SalesAccountApplyTurnoverBandProposalActivity',
                        changedOn: '2018-06-13T13:14:00.6373120Z',
                        links: null,
                        updatedByUri: '/employees/987'
                    },
                    {
                        discountSchemeUri: '/sales/discounting/schemes/9',
                        activityType: 'SalesAccountUpdateDiscountSchemeUriActivity',
                        changedOn: '2018-06-13T13:20:27.0841650Z',
                        links: null,
                        updatedByUri: '/employees/123'
                    },
                ],
            },
            discountSchemes: [
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
                },
                {
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
            ],
            turnoverBandSets: [
                {
                    name: 'CI',
                    turnoverBands: [
                        {
                            name: 'CI Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/1/turnover-bands/1',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'CI High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/1/turnover-bands/2',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/1',
                            rel: 'self'
                        }
                    ]
                },
                {
                    name: 'Distributor',
                    turnoverBands: [
                        {
                            name: 'Distributor Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/2/turnover-bands/3',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'Distributor High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/2/turnover-bands/4',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/2',
                            rel: 'self'
                        }
                    ]
                },
                {
                    name: 'Retailer',
                    turnoverBands: [
                        {
                            name: 'Retailer Low',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/3/turnover-bands/5',
                                    rel: 'self'
                                }
                            ]
                        },
                        {
                            name: 'Retailer High',
                            boundaries: [],
                            links: [
                                {
                                    href: '/sales/discounting/turnover-band-sets/3/turnover-bands/6',
                                    rel: 'self'
                                }
                            ]
                        }
                    ],
                    links: [
                        {
                            href: '/sales/discounting/turnover-band-sets/3',
                            rel: 'self'
                        }
                    ]
                }
            ],
            employees: {
                items: [
                    {
                        loading: false,
                        id: 987,
                        firstName: 'Peter',
                        lastName: 'Beardsley',
                        userName: 'peterb',
                        emailAddress: 'peter.beardsley@linn.co.uk',
                        href: '/employees/987',
                        fullName: 'Peter Beardsley',
                        links: [
                            {
                                href: '/employees/987',
                                rel: 'self'
                            }
                        ]
                    }
                ]
            }
        }

        expect(getSalesAccountLoading(state)).toEqual(false);
    });
});
