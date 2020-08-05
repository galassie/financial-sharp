namespace FinancialSharp

module CustomTypes =
    type PaymentPeriod = PaymentPeriod of double

    let createPaymentPeriod d =
        if d >= 1.0 then
            Some (PaymentPeriod d)
        else
            None