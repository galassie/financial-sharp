namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestIpmt() = 

    [<TestCase(0.008333333333333333, 1.0, 24.0, 2000.0, 0.0, 0.0)>]
    [<TestCase(0.001988079518355057, 1.0, 360.0, 300000.0, 0.0, 0.0)>]
    [<TestCase(0.001988079518355057, 2.0, 360.0, 300000.0, 0.0, -594.10715770470836)>]
    [<TestCase(0.001988079518355057, 3.0, 360.0, 300000.0, 0.0, -592.97159217484057)>]
    member _this.``ipmt with paymentDuePeriod = PaymentDuePeriod.Begin``(rate:double, per:double, nper:double, pv:double, fv:double, expectedResult:double) = 
        match Financial.Ipmt(rate, per, nper, pv, fv, PaymentDuePeriod.Begin) with
        | Some ipmt -> Assert.AreEqual(expectedResult, ipmt)
        | None -> Assert.Fail("ipmt should be Some!")

    [<TestCase(0.008333333333333333, 1.0, 24.0, 2000.0, 0.0, -16.666666666666668)>]
    [<TestCase(0.008333333333333333, 2.0, 24.0, 2000.0, 0.0, -16.036473449930281)>]
    [<TestCase(0.008333333333333333, 3.0, 24.0, 2000.0, 0.0, -15.401028623054424)>]
    [<TestCase(0.008333333333333333, 4.0, 24.0, 2000.0, 0.0, -14.760288422621272)>]
    member _this.``ipmt with paymentDuePeriod = PaymentDuePeriod.End``(rate:double, per:double, nper:double, pv:double, fv:double, expectedResult:double) = 
        match Financial.Ipmt(rate, per, nper, pv, fv, PaymentDuePeriod.End) with
        | Some ipmt -> Assert.AreEqual(expectedResult, ipmt)
        | None -> Assert.Fail("ipmt should be Some!")