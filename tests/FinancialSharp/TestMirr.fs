namespace FinancialSharp.Tests

open NUnit.Framework
open FinancialSharp

[<TestFixture>]
type TestMirr() = 
    
    static member data () : obj[][] = 
        [|
            [| [|-4500.0; -800.0; 800.0; 800.0; 600.0; 600.0; 800.0; 800.0; 700.0; 3000.0|]; 0.08; 0.055; 0.06659717503155349 |]
            [| [|-120000.0; 39000.0; 30000.0; 21000.0; 37000.0; 46000.0|]; 0.10; 0.12; 0.12609413036590511 |]
            [| [|100.0; 200.0; -50.0; 300.0; -200.0|]; 0.05; 0.06; 0.34282338784217692 |]
        |]
    
    static member invalidData () : obj[][] = 
        [|
            [| [|39000.0; 30000.0; 21000.0; 37000.0; 46000.0|]; 0.10; 0.12 |]
            [| [|-39000.0; -30000.0; -21000.0; -37000.0; -46000.0|]; 0.10; 0.12 |]
        |]

    [<TestCaseSource("data")>]
    member _this.``mirr``(values:double seq, financeRate:double, reinvestRate:double,  expectedResult:double) = 
        match Financial.Mirr(values, financeRate, reinvestRate) with
        | Some mirr -> Assert.AreEqual(expectedResult, mirr)
        | None -> Assert.Fail("mirr should be Some!")
    
    [<TestCaseSource("invalidData")>]
    member _this.``mirr returns None when it cannot be calculated``(values:double seq, financeRate:double, reinvestRate:double) = 
        match Financial.Mirr(values, financeRate, reinvestRate) with
        | Some _ -> Assert.Fail("mirr should be None!")
        | None -> Assert.Pass()