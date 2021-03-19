open FinancialSharp

[<EntryPoint>]
let main argv  =
    printfn "FinancialSharp!\n"

    Financial.Nper(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriod.Begin)
    |> printfn "Number of periodic payments: %f"

    Financial.Fv(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.End)
    |> printfn "Future value: %f"

    Financial.Npv(0.05, [|-15000.0; 1500.0; 2500.0; 3500.0; 4500.0; 6000.0|])
    |> printfn "Net present value of a cash flow series: %f"

    Financial.Pv(0.0, 20.0, 12000.0, 0.0)
    |> printfn "Present value: %f"
    0