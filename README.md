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

## Build on your machine

If you want to build this library on your machine, execute the following commands:

``` shell
git clone https://github.com/galassie/financial-sharp.git
cd financial-sharp
dotnet build
```

If you want to run the tests, execute the following command:

``` shell
dotnet test
```

## Build in Docker

Required:
- Install [Docker](https://hub.docker.com/search/?type=edition&offering=community) for your system

Build a Docker image called `financial-sharp`. This will work without any local .NET Core installation.

```shell
docker build -t financial-sharp .
```

Use the following to instantiate a Docker container from the `financial-sharp` image and run the tests inside:

```shell
docker run --rm financial-sharp dotnet test
```
