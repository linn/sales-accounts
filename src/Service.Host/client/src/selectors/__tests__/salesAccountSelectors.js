import { getSalesAccount, getSalesAccountName, getSalesAccountId, getDiscountSchemeName, getSalesAccountTurnoverBandName, getTurnoverBands } from '../salesAccountSelectors';

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