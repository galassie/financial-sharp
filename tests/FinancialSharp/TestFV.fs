namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestFV() = 
    
    [<Test>]
    member _this.``fv with paymentDuePeriod = PaymentDuePeriod.Begin``() = 
        let fv = Financial.FV(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.Begin)
        Assert.AreEqual(93105.064873521143, fv)

    [<Test>]
    member _this.``fv with paymentDuePeriod = PaymentDuePeriod.End``() = 
        let fv = Financial.FV(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.End)
        Assert.AreEqual(86609.362673042924, fv)
    
    [<Test>]
    member _this.``fv with rate 0.0``() = 
        let fv = Financial.FV(0.0, 5.0, 100.0, 0.0)
        Assert.AreEqual(-500.0, fv)