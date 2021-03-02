namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestNper() = 
    
    [<Test>]
    member _this.``nper with paymentDuePeriod = PaymentDuePeriod.Begin``() = 
        let nper = Financial.Nper(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriod.Begin)
        Assert.AreEqual(20.761564405189535, nper)
    
    [<TestCase(0.0, -2000.0, 0.0, 100000.0, 50.0)>]
    [<TestCase(0.075, -2000.0, 0.0, 100000.0, 21.544944197323339)>]
    [<TestCase(0.1, 0.0, -500.0, 1500.0, 11.526704607247604)>]
    member _this.``nper with paymentDuePeriod = PaymentDuePeriod.End``(rate:double, pmt:double, pv:double, fv:double, expectedResult:double) = 
        let nper = Financial.Nper(rate, pmt, pv, fv, PaymentDuePeriod.End)
        Assert.AreEqual(expectedResult, nper)

    [<Test>]
    member _this.``nper with rate 0.0``() = 
        let nper = Financial.Nper(0.0, -100.0, 1000.0)
        Assert.AreEqual(10.0, nper)
    
    [<Test>]
    member _this.``nper with infinite nper``() = 
        let nper = Financial.Nper(0.0, -0.0, 1000.0)
        Assert.AreEqual(infinity, nper)