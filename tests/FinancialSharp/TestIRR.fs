namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestIRR() = 
    
    static member data () : obj[][] = 
        [|
            [| [|-150000.0; 15000.0; 25000.0; 35000.0; 45000.0; 60000.0|]; 0.052432888859414113 |]
            [| [|-100.0; 0.0; 0.0; 74.0|]; -0.095495830348972424 |]
            [| [|-100.0; 39.0; 59.0; 55.0; 20.0|]; 0.2809484211599611 |]
            [| [|-100.0; 100.0; 0.0; -7.0 |]; -0.083299666184932633 |]
            [| [|-100.0; 100.0; 0.0; 7.0 |]; 0.062058485629929605 |]
            [| [|-5.0; 10.5; 1.0; -8.0; 1.0 |]; 0.088598338527755408 |]
        |]

    [<TestCaseSource("data")>]
    member _this.``irr``(values:double seq,  expectedResult:double) = 
        match Financial.IRR(values) with
        | Some irr -> Assert.AreEqual(expectedResult, irr)
        | None -> Assert.Fail("irr should be Some!")