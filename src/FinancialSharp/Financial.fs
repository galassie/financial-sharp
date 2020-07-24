namespace FinancialSharp

type PaymentDuePeriod =
    | Begin
    | End

type Financial =

    static member FV(rate : double, nper : double, pmt : double, pv : double, paymentDuePeriod : PaymentDuePeriod) =
        let whenRateIsZero() =
            -(pv + pmt * nper)
        
        let whenRateIsNotZero() =
            let temp = (1.0 + rate) ** nper
            let paymentDuePeriodMult = match paymentDuePeriod with | Begin -> 1.0 | End -> 0.0
            (-pv * temp - pmt * (1.0 + rate * paymentDuePeriodMult) / rate * (temp - 1.0))
        
        match rate with
        | 0.0 -> whenRateIsZero()
        | _ -> whenRateIsNotZero()

    static member FV(rate : double, nper : double, pmt : double, pv : double) =
        Financial.FV(rate, nper, pmt, pv, PaymentDuePeriod.End)
    
    static member PMT(rate: double, nper: double, pv: double, fv : double, paymentDuePeriod : PaymentDuePeriod) =
        let temp = (1.0 + rate) ** nper
        let paymentDuePeriodMult = match paymentDuePeriod with | Begin -> 1.0 | End -> 0.0
        let maskedRate = match rate with | 0.0 -> 1.0 | _ -> rate
        let fact = match rate with | 0.0 -> (1.0 + maskedRate * paymentDuePeriodMult) * (temp - 1.0) / maskedRate | _ -> nper
        -(fv + pv * temp) / fact