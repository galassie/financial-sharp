namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestPpmt() = 

    [<TestCase(0.008333333333333333, 1.0, 60.0, 55000.0, 0.0, -1158.9297115237273)>]
    [<TestCase(0.006666666666666667, 1.0, 60.0, 15000.0, 0.0, -302.13170297305402)>]
    member _this.``ppmt with paymentDuePeriod = PaymentDuePeriod.Begin``(rate:double, per:double, nper:double, pv:double, fv:double, expectedResult:double) = 
        match Financial.Ppmt(rate, per, nper, pv, fv, PaymentDuePeriod.Begin) with
        | Some ppmt -> Assert.AreEqual(expectedResult, ppmt)
        | None -> Assert.Fail("ppmt should be Some!")
        
    [<TestCase(0.008333333333333333, 1.0, 60.0, 55000.0, 0.0, -710.25412578642499)>]
    [<TestCase(0.006666666666666667, 1.0, 60.0, 15000.0, 0.0, -204.14591432620773)>]
    member _this.``ppmt with paymentDuePeriod = PaymentDuePeriod.End``(rate:double, per:double, nper:double, pv:double, fv:double, expectedResult:double) = 
        match Financial.Ppmt(rate, per, nper, pv, fv, PaymentDuePeriod.End) with
        | Some ppmt -> Assert.AreEqual(expectedResult, ppmt)
        | None -> Assert.Fail("ppmt should be Some!")