namespace FinancialSharp

type PaymentDuePeriod =
    | Begin
    | End

type Financial =

    static member private PaymentDuePeriodMult(paymentDuePeriod : PaymentDuePeriod) =
        match paymentDuePeriod with 
        | Begin -> 1.0
        | End -> 0.0

    static member FV(rate : double, nper : double, pmt : double, pv : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End

        if rate = 0.0 then
            -(pv + pmt * nper) 
        else 
            let temp = (1.0 + rate) ** nper
            (-pv * temp - pmt * (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) / rate * (temp - 1.0))

    static member PMT(rate: double, nper: double, pv: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End

        let temp = (1.0 + rate) ** nper
        let maskedRate = if rate = 0.0 then 1.0 else rate
        let fact = if rate = 0.0 then (1.0 + maskedRate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) * (temp - 1.0) / maskedRate else nper
        -(fv + pv * temp) / fact