namespace FinancialSharp

open System

type PaymentDuePeriod =
    | Begin
    | End

type Financial =

    static member private PaymentDuePeriodMult =
        function | Begin -> 1.0
                 | End -> 0.0

    static member FV(rate : double, nper : double, pmt : double, pv : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        if rate = 0.0 then
            -(pv + pmt * nper) 
        else
            let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
            let temp = (1.0 + rate) ** nper
            (-pv * temp - pmt * (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) / rate * (temp - 1.0))

    static member PMT(rate: double, nper: double, pv: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
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

    static member NPER(rate: double, pmt: double, pv: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        if rate = 0.0 then
            -(fv + pv) / pmt 
        else
            let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
            let z = pmt * (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) / rate
            Math.Log((-fv + z) / (pv + z)) / Math.Log(1.0 + rate)

    static member IPMT(rate: double, per: double, nper: double, pv: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
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

    static member PPMT(rate: double, per: double, nper: double, pv: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
        let eval ipmt =
            let total = Financial.PMT(rate, nper, pv, fv, paymentDuePeriod)
            total - ipmt
        Financial.IPMT(rate, per, nper, pv, fv, paymentDuePeriod)
        |> Option.map eval

    static member PV(rate: double, nper: double, pmt: double, ?fv0 : double, ?paymentDuePeriod0 : PaymentDuePeriod) =
        let fv = defaultArg fv0 0.0
        let temp = (1.0 + rate) ** nper
        let fact = 
            if rate = 0.0 then
                nper
            else
                let paymentDuePeriod = defaultArg paymentDuePeriod0 PaymentDuePeriod.End
                (1.0 + rate * Financial.PaymentDuePeriodMult(paymentDuePeriod)) * (temp - 1.0) / rate
        -(fv + pmt * fact) / temp

    static member NPV(rate: double, values: double seq) =
        values
        |> Seq.indexed
        |> Seq.fold (fun acc (i, curr) -> acc + (curr / (1.0 + rate) ** (double i))) 0.0

    static member MIRR(values : double seq, financeRate : double, reinvestRate : double) =
        values
        |> Seq.exists (fun v -> v <> 0.0)
        |> function
            | false -> None
            | true ->
                let positives = values |> Seq.map (fun x -> if x > 0.0 then x else 0.0)
                let negatives = values |> Seq.map (fun x -> if x < 0.0 then x else 0.0)
                let numer = Financial.NPV(reinvestRate, positives) |> Math.Abs
                let denom = Financial.NPV(financeRate, negatives) |> Math.Abs
                Some ((numer / denom) ** (double (1 / ((Seq.length values) - 1))) * (1.0 + reinvestRate) - 1.0)
