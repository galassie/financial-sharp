namespace FinancialSharp

open System

type PaymentDuePeriod =
    | Begin
    | End

type Financial =

    static member private PaymentDuePeriodMult paymentDuePeriod =
        match paymentDuePeriod with
        | Begin -> 1.0
        | End -> 0.0

    /// <summary>Get the Begin value of PaymentDuePeriod type</summary>
    /// <returns>Begin value of PaymentDuePeriod type</returns>
    static member PaymentDuePeriodBegin = PaymentDuePeriod.Begin
    
    /// <summary>Get the End value of PaymentDuePeriod type</summary>
    /// <returns>End value of PaymentDuePeriod type</returns>
    static member PaymentDuePeriodEnd = PaymentDuePeriod.End
    
    /// <summary>Compute the future value</summary>
    /// <param name="rate">Rate of interest</param>
    /// <param name="nper">Number of compounding periods</param>
    /// <param name="pmt">Payment</param>
    /// <param name="pv">Present value</param>
    /// <param name="paymentDuePeriod">When payment are due (default is End)</param>
    /// <returns>Future value</returns>
    static member Fv(rate:double, nper:double, pmt:double, pv:double, ?paymentDuePeriod:PaymentDuePeriod) =
        if rate = 0.0 then
            -(pv + pmt * nper) 
        else
            let paymentDuePeriod = defaultArg paymentDuePeriod PaymentDuePeriod.End
            let temp = (1.0 + rate) ** nper
            (-pv * temp - pmt * (1.0 + rate * (Financial.PaymentDuePeriodMult paymentDuePeriod)) / rate * (temp - 1.0))

    /// <summary>Compute the payment against loan principal plus interest</summary>
    /// <param name="rate">Rate of interest</param>
    /// <param name="nper">Number of compounding periods</param>
    /// <param name="pv">Present value</param>
    /// <param name="fv">Future value (default is 0.0)</param>
    /// <param name="paymentDuePeriod">When payment are due (default is End)</param>
    /// <returns>Payment against loan plus interest</returns>
    static member Pmt(rate:double, nper:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod) =
        let fv = defaultArg fv 0.0
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
                let paymentDuePeriod = defaultArg paymentDuePeriod PaymentDuePeriod.End
                (1.0 + maskedRate * (Financial.PaymentDuePeriodMult paymentDuePeriod)) * (temp - 1.0) / maskedRate
        -(fv + pv * temp) / fact

    /// <summary>Compute the number of periodic payments</summary>
    /// <param name="rate">Rate of interest</param>
    /// <param name="pmt">Payment</param>
    /// <param name="pv">Present value</param>
    /// <param name="fv">Future value (default is 0.0)</param>
    /// <param name="paymentDuePeriod">When payment are due (default is End)</param>
    /// <returns>The number of periodic payments</returns>
    static member Nper(rate:double, pmt:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod) =
        let fv = defaultArg fv 0.0
        if rate = 0.0 then
            -(fv + pv) / pmt 
        else
            let paymentDuePeriod = defaultArg paymentDuePeriod PaymentDuePeriod.End
            let z = pmt * (1.0 + rate * (Financial.PaymentDuePeriodMult paymentDuePeriod)) / rate
            Math.Log((-fv + z) / (pv + z)) / Math.Log(1.0 + rate)

    /// <summary>Compute the interest portion of a payment</summary>
    /// <param name="rate">Rate of interest</param>
    /// <param name="per">Interest paid against the loan changes during the life or the loan</param>
    /// <param name="nper">Number of compounding periods</param>
    /// <param name="pv">Present value</param>
    /// <param name="fv">Future value (default is 0.0)</param>
    /// <param name="paymentDuePeriod">When payment are due (default is End)</param>
    /// <returns>The interest portion of a payment</returns>
    static member Ipmt(rate:double, per:double, nper:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod) =
        if per < 1.0 then
            None
        else
            let fv = defaultArg fv 0.0
            let paymentDuePeriod = defaultArg paymentDuePeriod PaymentDuePeriod.End
            match per, paymentDuePeriod with
            | 1.0, PaymentDuePeriod.Begin -> Some 0.0
            | _, pdp ->
                let totalPmt = Financial.Pmt(rate, nper, pv, fv, paymentDuePeriod)
                let ipmt = Financial.Fv(rate, (per - 1.0), totalPmt, pv, pdp) * rate
                match per, pdp with
                | p, PaymentDuePeriod.Begin when p > 1.0 -> Some (ipmt / (1.0 + rate))
                | _, _ -> Some ipmt

    /// <summary>Compute the payment against loan principal</summary>
    /// <param name="rate">Rate of interest</param>
    /// <param name="per">Interest paid against the loan changes during the life or the loan</param>
    /// <param name="nper">Number of compounding periods</param>
    /// <param name="pv">Present value</param>
    /// <param name="fv">Future value (default is 0.0)</param>
    /// <param name="paymentDuePeriod">When payment are due (default is End)</param>
    /// <returns>The payment against loan principal</returns>
    static member Ppmt(rate:double, per:double, nper:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod) =
        let fv = defaultArg fv 0.0
        let paymentDuePeriod = defaultArg paymentDuePeriod PaymentDuePeriod.End
        
        let eval ipmt =
            let total = Financial.Pmt(rate, nper, pv, fv, paymentDuePeriod)
            total - ipmt
        
        Financial.Ipmt(rate, per, nper, pv, fv, paymentDuePeriod)
        |> Option.map eval

    /// <summary>Compute the present value</summary>
    /// <param name="rate">Rate of interest</param>
    /// <param name="nper">Number of compounding periods</param>
    /// <param name="pmt">Payment</param>
    /// <param name="fv">Future value (default is 0.0)</param>
    /// <param name="paymentDuePeriod">When payment are due (default is End)</param>
    /// <returns>Present value</returns>
    static member Pv(rate:double, nper:double, pmt:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod) =
        let fv = defaultArg fv 0.0
        let temp = (1.0 + rate) ** nper
        let fact = 
            if rate = 0.0 then
                nper
            else
                let paymentDuePeriod = defaultArg paymentDuePeriod PaymentDuePeriod.End
                (1.0 + rate * (Financial.PaymentDuePeriodMult paymentDuePeriod)) * (temp - 1.0) / rate
        -(fv + pmt * fact) / temp

    /// <summary>Compute the rate of interest per period</summary>
    /// <param name="nper">Number of compounding periods</param>
    /// <param name="pmt">Payment</param>
    /// <param name="pv">Present value</param>
    /// <param name="fv">Future value</param>
    /// <param name="paymentDuePeriod">When payment are due (default is End)</param>
    /// <param name="guess">Starting guess for solving the rate of interest (default is 0.1)</param>
    /// <param name="tol">Required tolerance for the solution (default is 1e-6)</param>
    /// <param name="maxiter">Maximum iterations in finding the solution (default is 100)</param>
    /// <returns>The rate of interest per period</returns>
    static member Rate(nper:double, pmt:double, pv:double, fv:double, ?paymentDuePeriod:PaymentDuePeriod, ?guess:double, ?tol:double, ?maxiter:int) =
        let paymentDuePeriod = defaultArg paymentDuePeriod PaymentDuePeriod.End
        let guess = defaultArg guess 0.1
        let tol = defaultArg tol 1e-6
        let maxiter = defaultArg maxiter 100

        let mutable rate = guess
        let mutable iterator = 0
        let mutable close = false

        let g_div_gp r n p x y w =
            let t1 = (r+1.0) ** n
            let t2 = (r+1.0) ** (n-1.0)
            let g = y + t1*x + p*(t1-1.0)*(r*w+1.0) / r
            let gp = n*t2*x - p*(t1-1.0)*(r*w + 1.0) / (r ** 2.0) + n*p*t2 * (r*w + 1.0) / r + p*(t1-1.0)*w/r
            g / gp

        while (iterator < maxiter) && (not close) do
            let rnp1 = rate - g_div_gp rate nper pmt pv fv (Financial.PaymentDuePeriodMult paymentDuePeriod)
            let diff = Math.Abs(rnp1 - rate)
            close <- diff < tol
            iterator <- iterator + 1
            rate <- rnp1
        
        if not close then
            None
        else
            Some rate

    /// <summary>Compute the internal rate of return</summary>
    /// <param name="values">Input cash flows per time period</param>
    /// <param name="guess">Starting guess for solving the rate of interest (default is 0.1)</param>
    /// <param name="tol">Required tolerance for the solution (default is 1e-6)</param>
    /// <param name="maxiter">Maximum iterations in finding the solution (default is 100)</param>
    /// <returns>The internal rate of return</returns>
    static member Irr(values:double seq, ?guess:double, ?tol:double, ?maxiter:int) =
        let guess = defaultArg guess 0.1
        let tol = defaultArg tol 1e-9
        let maxiter = defaultArg maxiter 100
        
        let mutable irr0 = guess
        let mutable irr1 = 0.0
        let mutable iterator = 0
        let mutable close = false

        while (iterator < maxiter) && (not close) do
            let npv = 
                values
                |> Seq.indexed
                |> Seq.fold (fun state (index, value) -> state + value / ((irr0 + 1.0) ** (double index))) 0.0
            let ddx =
                values
                |> Seq.indexed
                |> Seq.fold (fun state (index, value) -> state + (value * double -index) / ((irr0 + 1.0) ** (double index + 1.0))) 0.0
            
            irr1 <- irr0 - npv / ddx
            
            close <- Math.Abs(irr1 - irr0) <= tol
            irr0 <- irr1
            iterator <- iterator + 1
            
        if not close then
            None
        else
            Some irr1

    /// <summary>Compute the net present value of a cash flow series</summary>
    /// <param name="rate">The discount rate</param>
    /// <param name="values">The values of the time series of cash flows</param>
    /// <returns>The net present value of a cash flow series</returns>
    static member Npv(rate:double, values:double seq) =
        values
        |> Seq.indexed
        |> Seq.fold (fun acc (i, curr) -> acc + (curr / (1.0 + rate) ** (double i))) 0.0

    /// <summary>Compute the modified internal rate of return</summary>
    /// <param name="values">Cash flows (must contain at least one positive and one negative value) </param>
    /// <param name="financeRate">Interest rate paid on the cash flows</param>
    /// <param name="reinvestRate">Interest rate received on the cash flows upon reinvestment</param>
    /// <returns>The modified internal rate of return</returns>
    static member Mirr(values:double seq, financeRate:double, reinvestRate:double) =
        let positives = values |> Seq.map (fun x -> if x > 0.0 then x else 0.0)
        let negatives = values |> Seq.map (fun x -> if x < 0.0 then x else 0.0)
        
        if not (Seq.exists (fun x -> x > 0.0) positives) || not (Seq.exists (fun x -> x < 0.0) negatives) then 
            None
        else
            let numer = Math.Abs(Financial.Npv(reinvestRate, positives))
            let denom = Math.Abs(Financial.Npv(financeRate, negatives))
            Some ((numer / denom) ** (1.0 / (double (Seq.length values) - 1.0)) * (1.0 + reinvestRate) - 1.0)
