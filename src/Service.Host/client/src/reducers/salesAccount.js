import * as actionTypes from '../actions';

const defaultState = {
    salesAccountUri: null,
    loading: false,
    editDiscountSchemeVisible: false,
    editTurnoverBandVisible: false,
    editGoodCreditVisible: false,
    editGrowthPartnerVisible: false,
    editEligibleForRebateVisible: false,
    dirty: false,
    saving: false,
    item: null,
}

const salesAccount = (state = defaultState, action) => {
    switch (action.type) {
        case actionTypes.REQUEST_SALES_ACCOUNT:
            return {
                ...state,
                salesAccountUri: action.payload.salesAccountUri,
                loading: true
            }

        case actionTypes.RECEIVE_SALES_ACCOUNT:
            return {
                ...state,
                loading: false,
                item: action.payload.data
            }


        case actionTypes.HIDE_EDIT_MODAL:
            return {
                ...state,
                editDiscountSchemeVisible: false,
                editTurnoverBandVisible: false,
                editGoodCreditVisible: false,
                editGrowthPartnerVisible: false,
                editEligibleForRebateVisible: false,
            }

        case actionTypes.EDIT_DISCOUNT_SCHEME:
            return {
                ...state,
                editDiscountSchemeVisible: true
            }

        case actionTypes.SET_DISCOUNT_SCHEME:
            return {
                ...state,
                dirty: true,
                item: {
                    ...state.item,
                    discountSchemeUri: action.payload.discountSchemeUri,
                    turnoverBandUri: null,
                }
            }

        case actionTypes.EDIT_TURNOVER_BAND:
            return {
                ...state,
                editTurnoverBandVisible: true
            }

        case actionTypes.SET_TURNOVER_BAND:
            return {
                ...state,
                dirty: true,
                item: {
                    ...state.item,
                    turnoverBandUri: action.payload.turnoverBandUri,
                }
            }

        case actionTypes.EDIT_ELIGIBLE_FOR_GOOD_CREDIT_DISCOUNT:
            return {
                ...state,
                editGoodCreditVisible: true
            }

        case actionTypes.SET_ELIGIBLE_FOR_GOOD_CREDIT_DISCOUNT:
            return {
                ...state,
                dirty: true,
                item: {
                    ...state.item,
                    eligibleForGoodCreditDiscount: action.payload.eligible,
                }
            }

        case actionTypes.EDIT_GROWTH_PARTNER:
            return {
                ...state,
                editGrowthPartnerVisible: true
            }

        case actionTypes.SET_GROWTH_PARTNER:
            return {
                ...state,
                dirty: true,
                item: {
                    ...state.item,
                    growthPartner: action.payload.eligible,
                }
            }

        case actionTypes.EDIT_ELIGIBLE_FOR_REBATE:
            return {
                ...state,
                editEligibleForRebateVisible: true
            }

        case actionTypes.SET_ELIGIBLE_FOR_REBATE:
            return {
                ...state,
                dirty: true,
                item: {
                    ...state.item,
                    eligibleForRebate: action.payload.eligible,
                }
            }

        case actionTypes.REQUEST_COUNTRY:
            return {
                ...state,
                loading: true,
                item: {
                    ...state.item,
                    address: {
                        ...state.item.address,
                        countryName: null
                    }
                }
            }

        case actionTypes.RECEIVE_COUNTRY:
            return {
                ...state,
                loading: false,
                item: {
                    ...state.item,
                    address: {
                        ...state.item.address,
                        countryName: action.payload.data.countryName
                    }
                }
            }

        case actionTypes.START_SAVE:
            return {
                ...state,
                saving: true
            }

        case actionTypes.SAVE_COMPLETE:
            return {
                ...state,
                saving: false,
                dirty: false
            }

        case actionTypes.REQUEST_ACTIVITIES:
            return {
                ...state
            }

        case actionTypes.RECEIVE_ACTIVITIES:
            return {
                ...state,
                activities: action.payload.data.activities
            }

        default:
            return state;
    }
}

export default salesAccount;