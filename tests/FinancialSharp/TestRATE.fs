namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestRate() =

    [<Test>]
    member _this.``rate with paymentDuePeriod = PaymentDuePeriod.Begin``() = 
        match Financial.Rate(10.0, 0.0, -3500.0, 10000.0, PaymentDuePeriod.Begin) with
        | Some rate -> Assert.AreEqual(0.11069085371426901, rate)
        | None -> Assert.Fail("rate should be Some!")

    [<Test>]
    member _this.``rate with paymentDuePeriod = PaymentDuePeriod.End``() = 
        match Financial.Rate(10.0, 0.0, -3500.0, 10000.0, PaymentDuePeriod.End) with
        | Some rate -> Assert.AreEqual(0.11069085371426901, rate)
        | None -> Assert.Fail("rate should be Some!")

    [<Test>]
    member _this.``rate returns None when it cannot be calculated``() = 
        match Financial.Rate(12.0, 400.0, 10000.0, 5000.0) with
        | Some _ -> Assert.Fail("rate should be None!")
        | None -> Assert.Pass()