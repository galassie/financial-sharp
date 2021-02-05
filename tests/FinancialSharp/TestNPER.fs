namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestNPER() = 
    
    [<Test>]
    member _.``nper with paymentDuePeriod = PaymentDuePeriod.Begin``() = 
        let result = Financial.NPER(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriod.Begin)
        Assert.AreEqual(20.761564405189535, result)
    
    [<TestCase(0.0, -2000.0, 0.0, 100000.0, 50.0)>]
    [<TestCase(0.075, -2000.0, 0.0, 100000.0, 21.544944197323339)>]
    [<TestCase(0.1, 0.0, -500.0, 1500.0, 11.526704607247604)>]
    member _.``nper with paymentDuePeriod = PaymentDuePeriod.End``(rate:double, pmt:double, pv:double, fv:double, expectedResult:double) = 
        let result = Financial.NPER(rate, pmt, pv, fv, PaymentDuePeriod.End)
        Assert.AreEqual(expectedResult, result)

    [<Test>]
    member _.``nper with rate 0.0``() = 
        let result = Financial.NPER(0.0, -100.0, 1000.0)
        Assert.AreEqual(10.0, result)
    
    [<Test>]
    member _.``nper with infinite result``() = 
        let result = Financial.NPER(0.0, -0.0, 1000.0)
        Assert.AreEqual(infinity, result)