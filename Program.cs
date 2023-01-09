//  do not shorten names of the variables
namespace Calculator;
using LL = System.Numerics.BigInteger;

using Do = System.Double;
using System.Runtime.CompilerServices;

internal class Program
{
    static void Main()
    {

        //Int Ix = new Int(1);
        //Decimal Dx = new Decimal(Ix, 0);
        //Int Iy = new Int(1);
        //Decimal Dy = new Decimal(Ix, 0);
        //Complex z = new Complex(Dx, (Dy * Dx));
        ////Complex zz = z * z;
        //z *= z;
        //z *= z;
        var temp = new Complex(new Decimal(2), new Decimal(3));
        var temp2 = new Complex(new Decimal(20), new Decimal(30));
        Decimal x = new(new Int(1), 0);
        Decimal y = new(new Int(12), 0);
        Decimal z = x / y;
        Console.WriteLine(
            new Decimal(new Int(12), 1) +
            new Decimal(new Int(13), 0)

            );
        //Console.WriteLine((x * x) / (y + y * y));

    }
}