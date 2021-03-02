namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestPv() =

    [<TestCase(0.07, 20.0, 12000.0, 0.0, -136027.14291242755)>]
    [<TestCase(0.07, 21.0, 12000.0, 0.0, -139128.17094619398)>]
    [<TestCase(0.07, 22.0, 12000.0, 0.0, -142026.32798709717)>]
    [<TestCase(0.07, 23.0, 12000.0, 0.0, -144734.88596924971)>]
    [<TestCase(0.07, 24.0, 12000.0, 0.0, -147266.24856939228)>]
    [<TestCase(0.07, 25.0, 12000.0, 0.0, -149632.0080087778)>]
    member _this.``pv with paymentDuePeriod = PaymentDuePeriod.Begin``(rate:double, nper:double, pv:double, fv:double, expectedResult:double) = 
        let pv = Financial.Pv(rate, nper, pv, fv, PaymentDuePeriod.Begin)
        Assert.AreEqual(expectedResult, pv)
        
    [<TestCase(0.07, 20.0, 12000.0, 0.0, -127128.17094619398)>]
    [<TestCase(0.07, 21.0, 12000.0, 0.0, -130026.32798709719)>]
    [<TestCase(0.07, 22.0, 12000.0, 0.0, -132734.88596924968)>]
    [<TestCase(0.07, 23.0, 12000.0, 0.0, -135266.24856939225)>]
    [<TestCase(0.07, 24.0, 12000.0, 0.0, -137632.0080087778)>]
    [<TestCase(0.07, 25.0, 12000.0, 0.0, -139842.99813904468)>]
    member _this.``pv with paymentDuePeriod = PaymentDuePeriod.End``(rate:double, nper:double, pv:double, fv:double, expectedResult:double) = 
        let pv = Financial.Pv(rate, nper, pv, fv, PaymentDuePeriod.End)
        Assert.AreEqual(expectedResult, pv)
    
    [<Test>]
    member _this.``pv with rate 0.0``() = 
        let pv = Financial.Pv(0.0, 20.0, 12000.0, 0.0)
        Assert.AreEqual(-240000.0, pv)
    
    [<Test>]
    member _this.``pv with rate not 0.0``() = 
        let pv = Financial.Pv(0.0, 20.0, 12000.0, 1000.0)
        Assert.AreEqual(-241000.0, pv)