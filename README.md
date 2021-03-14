# FinancialSharp

[![Build status](https://ci.appveyor.com/api/projects/status/9lyvbtoqjcjg448v?svg=true)](https://ci.appveyor.com/project/galassie/financial-sharp)
[![Build Status](https://travis-ci.org/galassie/financial-sharp.svg?branch=master)](https://travis-ci.org/galassie/financial-sharp) [![NuGet](https://img.shields.io/nuget/v/FinancialSharp.svg)](https://nuget.org/packages/FinancialSharp) ![.NET Core](https://github.com/galassie/financial-sharp/workflows/.NET%20Core/badge.svg)

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
- irr : compute the internal rate of return
- npv : compute the net present value of a cash flow series
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

## Usage

You can see the some examples in the folder "samples" (both in F# and C#).

Here how it looks a simple F# program that uses this library:

```fsharp
open FinancialSharp

[<EntryPoint>]
let main argv  =
    printfn "This is a series of examples on how to use FinancialSharp!\n"

    Financial.Nper(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriod.Begin)
    |> printfn "Number of periodic payments: %f"

    Financial.Fv(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.End)
    |> printfn "Future value: %f"

    Financial.Npv(0.05, [|-15000.0; 1500.0; 2500.0; 3500.0; 4500.0; 6000.0|])
    |> printfn "Net present value of a cash flow series: %f"

    Financial.Pv(0.0, 20.0, 12000.0, 0.0)
    |> printfn "Present value: %f"
    0
```

This program will output the following text:

```shell
This is a series of examples on how to use FinancialSharp!

Number of periodic payments: 20.761564
Future value: 86609.362673
Net present value of a cash flow series: 122.894855
Present value: -240000.000000
```

With F# 5.0, it is possible to use [Open Type Declarations](https://devblogs.microsoft.com/dotnet/announcing-f-5/#open-type-declarations) for a more concise usage.
For example:

```fsharp
open type FinancialSharp.Financial
open type FinancialSharp.PaymentDuePeriod

[<EntryPoint>]
let main argv  =
    printfn "This is a series of examples on how to use FinancialSharp with Open Type declarations!\n"
    
    // PaymentDuePeriodBegin is the static property of Financial
    Nper(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriodBegin)
    |> printfn "Number of periodic payments: %f"

    // It is possible to open also Discriminated Unions like PaymentDuePeriod
    Fv(0.075, 20.0, -2000.0, 0.0, End)
    |> printfn "Future value: %f"

    Npv(0.05, [|-15000.0; 1500.0; 2500.0; 3500.0; 4500.0; 6000.0|])
    |> printfn "Net present value of a cash flow series: %f"

    Pv(0.0, 20.0, 12000.0, 0.0)
    |> printfn "Present value: %f"
    0
```

It is possible to use this library also in a C# project:

```csharp
using System;

namespace FinancialSharp.CSharp.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var nper = Financial.Nper(0.075, -2000.0, 0.0, 100000.0, PaymentDuePeriod.Begin);
            Console.WriteLine($"Number of periodic payments: {nper}");

            var fv = Financial.Fv(0.075, 20.0, -2000.0, 0.0, PaymentDuePeriod.End);
            Console.WriteLine($"Future value: {fv}");

            var npv = Financial.Npv(0.05, new[] { -15000.0, 1500.0, 2500.0, 3500.0, 4500.0, 6000.0 });
            Console.WriteLine($"Net present value of a cash flow series: {npv}");

            var pv = Financial.Pv(0.0, 20.0, 12000.0, 0.0, paymentDuePeriod: null);
            Console.WriteLine($"Present value: {pv}");
        }
    }
}

```

## Contributing

Code contributions are more than welcome! 😻

Please commit any pull requests against the `master` branch.  
If you find any issue, please [report it](https://github.com/galassie/financial-sharp/issues)!

## License

This project is licensed under [The MIT License (MIT)](https://raw.githubusercontent.com/galassie/financial-sharp/master/LICENSE.md).

Author: [Enrico Galassi](https://twitter.com/enricogalassi88)
