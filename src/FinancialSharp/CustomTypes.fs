namespace FinancialSharp

module CustomTypes =
    type PositiveDouble = PositiveDouble of double

    let createPositiveDouble d =
        if d >= 1.0 then
            Some (PositiveDouble d)
        else
            None