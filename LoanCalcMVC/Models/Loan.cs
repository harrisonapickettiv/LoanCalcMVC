using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalcMVC.Models
{
  public class Loan
  {
    public double Amount { get; set; }
    public double Rate { get; set; }
    public int Term { get; set; }
    public double Payment { get; set; }
    public double TotalInterest { get; set; }
    public double TotalCost { get; set; }
    public List<LoanPayment> Payments { get; set; } = new List<LoanPayment>();
  }
}