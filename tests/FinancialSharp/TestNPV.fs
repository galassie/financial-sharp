namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestNPV() = 
    
    [<Test>]
    member _this.``npv``() = 
        let npv = Financial.NPV(0.05, [|-15000.0; 1500.0; 2500.0; 3500.0; 4500.0; 6000.0|])
        Assert.AreEqual(122.89485495093959, npv)