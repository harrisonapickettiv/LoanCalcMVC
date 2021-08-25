using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalcMVC.Models
{
  public class Loan
  {
    public double? Amount { get; set; }
    public double? Rate { get; set; }
    public int? Term { get; set; }

    public double Payment
    {
      get
      {
        if (Rate.HasValue && Term.HasValue && Amount.HasValue)
        {
          double monthlyRate = CalcMonthlyRate(Rate.Value);
          return (Amount.Value * monthlyRate) / (1 - Math.Pow(1 + monthlyRate, -1 * Term.Value));
        }
        return 0;
      }
    }

    public double TotalInterest { get; private set; }
    public double TotalCost { get; private set; }
    public List<LoanPayment> Payments { get; private set; } = new List<LoanPayment>();

    public Loan Update()
    {
      if (Rate.HasValue && Term.HasValue && Amount.HasValue)
      {
        double balance = Amount.Value;
        double totalInterest = 0;
        double monthlyInterest = 0;
        double monthlyPrincipal = 0;
        double monthlyRate = CalcMonthlyRate(Rate.Value);

        for (int month = 1; month <= Term.Value; month++)
        {
          monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
          totalInterest += monthlyInterest;
          monthlyPrincipal = Payment - monthlyInterest;
          balance -= monthlyPrincipal;

          LoanPayment loanPayment = new LoanPayment
          {
            Month = month,
            Payment = Payment,
            MonthlyPrincipal = monthlyPrincipal,
            MonthlyInterest = monthlyInterest,
            TotalInterest = totalInterest,
            Balance = balance,
          };

          Payments.Add(loanPayment);
        }

        TotalInterest = totalInterest;
        TotalCost = totalInterest + Amount.Value;
      }
      return this;
    }

    private double CalcMonthlyRate(double rate)
    {
      return rate / 1200;
    }
    private double CalcMonthlyInterest(double balance, double monthlyRate)
    {
      return balance * monthlyRate;
    }
  }
}