namespace FinancialSharp

open System

type PaymentDuePeriod =
    | Begin
    | End

type Financial =

    static member private PaymentDuePeriodMult =
        function | Begin -> 1.0
                 | End -> 0.0

    /// Compute the future value
    static member FV(rate:double, nper:double, pmt:double, pv:double, ?paymentDuePeriod0:PaymentDuePeriod) =
        if rate = 0.0 then
            -(pv + pmt * nper) 
        else
            let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
            let temp = (1.0 + rate) ** nper
            (-pv * temp - pmt * (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) / rate * (temp - 1.0))

    /// Compute the payment against loan principal plus interest
    static member PMT(rate:double, nper:double, pv:double, ?fv0:double, ?paymentDuePeriod0:PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let temp = (1.0 + rate) ** nper
        let maskedRate = 
            if rate = 0.0 then
                1.0
            else
                rate
        let fact = 
            if rate = 0.0 then
                nper
            else
                let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
                (1.0 + maskedRate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) * (temp - 1.0) / maskedRate
        -(fv + pv * temp) / fact

    /// Compute the number of periodic payments
    static member NPER(rate:double, pmt:double, pv:double, ?fv0:double, ?paymentDuePeriod0:PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        if rate = 0.0 then
            -(fv + pv) / pmt 
        else
            let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
            let z = pmt * (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) / rate
            Math.Log((-fv + z) / (pv + z)) / Math.Log(1.0 + rate)

    /// Compute the interest portion of a payment
    static member IPMT(rate:double, per:double, nper:double, pv:double, ?fv0:double, ?paymentDuePeriod0:PaymentDuePeriod) =
        if per < 1.0 then
            None
        else
            let fv = defaultArg fv0 0.0
            let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
            match per, paymentDuePeriod with
            | 1.0, PaymentDuePeriod.Begin -> Some 0.0
            | _, pdp ->
                let totalPmt = Financial.PMT(rate, nper, pv, fv, paymentDuePeriod)
                let ipmt = Financial.FV(rate, (per - 1.0), totalPmt, pv, pdp) * rate
                match per, pdp with
                | p, PaymentDuePeriod.Begin when p > 1.0 -> Some (ipmt / (1.0 + rate))
                | _, _ -> Some ipmt

    /// Compute the payment against loan principal
    static member PPMT(rate:double, per:double, nper:double, pv:double, ?fv0:double, ?paymentDuePeriod0:PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
        let eval ipmt =
            let total = Financial.PMT(rate, nper, pv, fv, paymentDuePeriod)
            total - ipmt
        Financial.IPMT(rate, per, nper, pv, fv, paymentDuePeriod)
        |> Option.map eval

    /// Compute the present value
    static member PV(rate:double, nper:double, pmt:double, ?fv0:double, ?paymentDuePeriod0:PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let temp = (1.0 + rate) ** nper
        let fact = 
            if rate = 0.0 then
                nper
            else
                let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
                (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) * (temp - 1.0) / rate
        -(fv + pmt * fact) / temp

    /// Compute the NPV (Net Present Value) of a cash flow series
    static member NPV(rate:double, values:double seq) =
        values
        |> Seq.indexed
        |> Seq.fold (fun acc (i, curr) -> acc + (curr / (1.0 + rate) ** (double i))) 0.0

    /// Compute the modified internal rate of return
    static member MIRR(values:double seq, financeRate:double, reinvestRate:double) =
        let positives = values |> Seq.map (fun x -> if x > 0.0 then x else 0.0)
        let negatives = values |> Seq.map (fun x -> if x < 0.0 then x else 0.0)
        
        if not (Seq.exists (fun x -> x > 0.0) positives) || not (Seq.exists (fun x -> x < 0.0) negatives) then 
            None
        else
            let numer = Math.Abs(Financial.NPV(reinvestRate, positives))
            let denom = Math.Abs(Financial.NPV(financeRate, negatives))
            Some ((numer / denom) ** (1.0 / (double (Seq.length values) - 1.0)) * (1.0 + reinvestRate) - 1.0)
