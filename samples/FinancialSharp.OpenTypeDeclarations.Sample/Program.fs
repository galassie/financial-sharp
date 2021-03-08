open type FinancialSharp.Financial
open type FinancialSharp.PaymentDuePeriod

[<EntryPoint>]
let main argv  =
    printfn "This is a series of examples on how to use FinancialSharp with Open Type declarations!\n"
    
    // PaymentDuePeriodBegin is the static property of Financial
    Nper(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriodBegin)
    |> printfn "Number of periodic payments: %f"

    // It is possible to open also Discriminated Unions like PaymentDuePeriod
    Fv(0.075, 20.0, -2000.0, 0.0, End)
    |> printfn "Future value: %f"

    Npv(0.05, [|-15000.0; 1500.0; 2500.0; 3500.0; 4500.0; 6000.0|])
    |> printfn "Net present value of a cash flow series: %f"

    Pv(0.0, 20.0, 12000.0, 0.0)
    |> printfn "Present value: %f"
    0