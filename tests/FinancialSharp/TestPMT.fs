namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestPMT() = 

    [<Test>]
    member _.``pmt with default values``() = 
        let result = Financial.PMT(0.08 / 12.0, 5.0 * 12.0, 15000.0)
        Assert.AreEqual(-304.14591432620773, result)
        
    [<Test>]
    member _.``pmt with paymentDuePeriod = PaymentDuePeriod.Begin``() = 
        let result = Financial.PMT(0.08 / 12.0, 5.0 * 12.0, 15000.0, 0.0, PaymentDuePeriod.Begin)
        Assert.AreEqual(-302.13170297305402, result)

    [<Test>]
    member _.``pmt with rate = 0.0``() =         
        let result = Financial.PMT(0.0, 5.0 * 12.0, 15000.0)
        Assert.AreEqual(-250.0, result)