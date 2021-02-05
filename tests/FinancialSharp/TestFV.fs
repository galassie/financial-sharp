namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestFV() = 
    
    [<Test>]
    member _.``fv with paymentDuePeriod = PaymentDuePeriod.End``() = 
        let result = Financial.FV(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.End)
        Assert.AreEqual(86609.362673042924, result)
    
    [<Test>]
    member _.``fv with paymentDuePeriod = PaymentDuePeriod.Begin``() = 
        let result = Financial.FV(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.Begin)
        Assert.AreEqual(93105.064873521143, result)
    
    [<Test>]
    member _.``fv with rate 0.0``() = 
        let result = Financial.FV(0.0, 5.0, 100.0, 0.0)
        Assert.AreEqual(-500, result)