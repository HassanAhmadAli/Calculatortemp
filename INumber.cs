using System.Runtime.CompilerServices;
namespace Calculator;
using LL = System.Numerics.BigInteger;
using Do = System.Double;
using Decimal = Calculator.Decimal;
public interface INumber
{

    public Do Length
    { get; set; }
    public Do Degree
    { get; set; }
}