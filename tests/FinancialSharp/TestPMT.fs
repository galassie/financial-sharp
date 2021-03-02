namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestPmt() = 

    [<Test>]
    member _this.``pmt with default values``() = 
        let pmt = Financial.Pmt(0.08 / 12.0, 5.0 * 12.0, 15000.0)
        Assert.AreEqual(-304.14591432620773, pmt)
        
    [<Test>]
    member _this.``pmt with paymentDuePeriod = PaymentDuePeriod.Begin``() = 
        let pmt = Financial.Pmt(0.08 / 12.0, 5.0 * 12.0, 15000.0, 0.0, PaymentDuePeriod.Begin)
        Assert.AreEqual(-302.13170297305402, pmt)

    [<Test>]
    member _this.``pmt with rate = 0.0``() =         
        let pmt = Financial.Pmt(0.0, 5.0 * 12.0, 15000.0)
        Assert.AreEqual(-250.0, pmt)