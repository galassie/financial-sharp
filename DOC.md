## Financial.Fv(rate:double, nper:double, pmt:double, pv:double, ?paymentDuePeriod:PaymentDuePeriod)

Compute the future future value.

**Parameters:**
- rate: rate of interest
- nper: number of compounding periods
- pmt: payment
- pv: present value
- paymentDuePeriod: When payment are due (default is End)

## Financial.Pmt(rate:double, nper:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod)

Compute the payment against loan principal plus interest.

**Parameters:**
- rate: rate of interest
- nper: number of compounding periods
- pv: present value
- fv: future value (default is 0.0)
- paymentDuePeriod: when payment are due (default is End)

## Financial.Nper(rate:double, pmt:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod)

Compute the number of periodic payments.

**Parameters:**
- rate: rate of interest
- pmt: payment
- pv: present value
- fv: future value (default is 0.0)
- paymentDuePeriod: When payment are due (default is End)

## Financial.Ipmt(rate:double, per:double, nper:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod)

Compute the interest portion of a payment.

**Parameters:**
- rate: rate of interest
- per: interest paid against the loan changes during the life or the loan
- nper: number of compounding periods
- pv: present value
- fv: future value (default is 0.0)
- paymentDuePeriod: when payment are due (default is End)

## Financial.Ppmt(rate:double, per:double, nper:double, pv:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod)

Compute the payment against loan principal.

**Parameters:**
- rate: rate of interest
- per: Interest paid against the loan changes during the life or the loan
- nper: number of compounding periods
- pv: present value
- fv: future value (default is 0.0)
- paymentDuePeriod: when payment are due (default is End)

## Financial.Pv(rate:double, nper:double, pmt:double, ?fv:double, ?paymentDuePeriod:PaymentDuePeriod)

Compute the present value.

**Parameters:**
- rate: rate of interest
- nper: number of compounding periods
- pmt: payment
- fv: future value (default is 0.0)
- paymentDuePeriod: when payment are due (default is End)

## Financial.Rate(nper:double, pmt:double, pv:double, fv:double, ?paymentDuePeriod:PaymentDuePeriod, ?guess:double, ?tol:double, ?maxiter:int)

Compute the rate of interest per period.

**Parameters:**
- nper: number of compounding periods
- pmt: payment
- pv: present value
- fv: future value
- paymentDuePeriod: when payment are due (default is End)
- guess: starting guess for solving the rate of interest (default is 0.1)
- tol: required tolerance for the solution (default is 1e-6)
- maxiter: maximum iterations in finding the solution (default is 100)

## Financial.Irr(values:double seq, ?guess:double, ?tol:double, ?maxiter:int)

Compute the internal rate of return.

**Parameters:**
- values: input cash flows per time period</param>
- guess: starting guess for solving the rate of interest (default is 0.1)
- tol: required tolerance for the solution (default is 1e-6)
- maxiter: maximum iterations in finding the solution (default is 100)

## Financial.Npv(rate:double, values:double seq)

Compute the net present value of a cash flow series.

**Parameters:**
- rate: the discount rate
- values: the values of the time series of cash flows

## Financial.Mirr(values:double seq, financeRate:double, reinvestRate:double)

Compute the modified internal rate of return.

**Parameters:**
- values: cash flows (must contain at least one positive and one negative value)
- financeRate: interest rate paid on the cash flows
- reinvestRate: interest rate received on the cash flows upon reinvestment
