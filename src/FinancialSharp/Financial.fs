namespace FinancialSharp

open System
open FinancialSharp.CustomTypes

type PaymentDuePeriod =
    | Begin
    | End

type Financial =

    static member private PaymentDuePeriodMult =
        function | Begin -> 1.0
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

    static member NPER(rate: double, pmt: double, pv: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End

        if rate = 0.0 then
            -(fv + pv) / pmt 
        else
            let z = pmt * (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) / rate
            Math.Log((-fv + z) / (pv + z)) / Math.Log(1.0 + rate)

    static member IPMT (rate: double, per: double, nper: double, pv: PositiveDouble, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End

        match pv, paymentDuePeriod with
        | (PositiveDouble pvv), PaymentDuePeriod.Begin when pvv = 1.0 -> 0.0
        | (PositiveDouble pvv), pdp ->
            let totalPmt = Financial.PMT(rate, nper, pvv, fv, pdp)
            let ipmt = Financial.FV(rate, (per - 1.0), totalPmt, pvv, pdp) * rate

            match per, pdp with
            | perv, PaymentDuePeriod.Begin when perv > 1.0 -> ipmt / (1.0 + rate)
            | _, _ -> ipmt

    static member PV(rate: double, nper: double, pmt: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End

        let temp = (1.0 + rate) ** nper
        let fact = if rate = 0.0 then nper else (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) * (temp - 1.0) / rate
        -(fv + pmt * fact) / temp