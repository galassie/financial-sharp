# FinancialSharp

[![Build status](https://ci.appveyor.com/api/projects/status/9lyvbtoqjcjg448v?svg=true)](https://ci.appveyor.com/project/galassie/financial-sharp)
![.NET Core](https://github.com/galassie/financial-sharp/workflows/.NET%20Core/badge.svg)

Simple and zero-dependency library, written entirely in F#, inspired by [numpy-financial](https://github.com/numpy/numpy-financial/) and  [financial](https://github.com/lmammino/financial/).

The library contains a collection of elementary financial functions.

## Functions

- fv : compute the future value
- pmt : compute the payment against loan principal plus interest
- nper : compute the number of periodic payments
- ipmt : compute the interest portion of a payment
- ppmt : compute the payment against loan principal
- pv : compute the present value 
- rate : compute the rate of interest per period
- irr : compute the Internal Rate of Return (IRR)
- npv : compute the NPV (Net Present Value) of a cash flow series
- mirr : compute the modified internal rate of return
