namespace Calculator;
using LL = System.Numerics.BigInteger;
using Do = System.Double;
using System.Data.Common;

class Complex : INumber
{
    public Decimal _Real, _Imaginary;
    public Complex(Decimal Real, Decimal Img)
    {
        _Real = Real;
        _Imaginary = Img;
    }
    public override string ToString()
    {
        bool r0 = (_Real == Decimal.zero);
        bool i0 = (_Imaginary == Decimal.zero);
        return (r0 && i0 ? "+0" : "") + (r0 ? "" : _Real.ToString()) + ((i0) ? "" : _Imaginary.ToString() + "i");
    }
    private readonly static int precision = 16;
    public Do Length
    {
        get
        {
            Do x = _Real.ToDouble();
            x *= x;
            Do y = _Imaginary.ToDouble();
            y *= y;
            x += y;
            return Math.Sqrt(x);

        }
        set
        {
            Do deg = this.Degree;
            Do len = value;
            Do x = len * Math.Cos(deg);
            Do y = len * Math.Sin(deg);
            string xs = x.ToString(string.Format("E{0}", precision));
            int x_index = xs.IndexOf("E");
            this._Real = new(new Int(int.Parse(xs.Substring(0, x_index).Replace(".", ""))), (int.Parse(xs.Substring(x_index + 1)) + 1 - precision));
            string s = y.ToString(string.Format("E{0}", precision));
            int y_index = s.IndexOf("E");
            int xx = int.Parse(s.Substring(0, y_index).Replace(".", ""));
            int yy = int.Parse(s.Substring(y_index + 1)) + 1 - precision;
            this._Imaginary = new(new Int(xx), yy);
        }
    }

    public Do Degree
    {
        get
        {

            return Math.Atan2(_Real.ToDouble(), _Imaginary.ToDouble());

        }
        set
        {
            Do deg = value;
            Do len = this.Length;
            Do x = len * Math.Cos(deg);
            Do y = len * Math.Sin(deg);
            string xs = x.ToString(string.Format("E{0}", precision));
            int x_index = xs.IndexOf("E");
            this._Real = new(new Int(int.Parse(xs.Substring(0, x_index).Replace(".", ""))), (int.Parse(xs.Substring(x_index + 1)) + 1 - precision));
            string s = y.ToString(string.Format("E{0}", precision));
            int y_index = s.IndexOf("E");
            int xx = int.Parse(s.Substring(0, y_index).Replace(".", ""));
            int yy = int.Parse(s.Substring(y_index + 1)) + 1 - precision;
            this._Imaginary = new(new Int(xx), yy);
        }

    }
    public static bool operator ==(in Complex lhs, Complex rhs)
    {

        return (lhs._Real == rhs._Real) && (lhs._Imaginary == rhs._Imaginary);
    }
    public static bool operator !=(in Complex lhs, Complex rhs)
    {

        return !(lhs == rhs);
    }
    public static Complex operator *(in Complex lhs, Complex rhs)
    {
        //return new Complex(((lhs._Real * rhs._Real) - (lhs._Imaginary * rhs._Imaginary)), ((lhs._Real * rhs._Imaginary) + (lhs._Imaginary * rhs._Real)));

        var lx = lhs._Real.ToDouble();
        var rx = rhs._Real.ToDouble();
        var ly = lhs._Imaginary.ToDouble();
        var ry = rhs._Imaginary.ToDouble();

        var Real_ = (lx * rx) - (ly * ry);
        var Imaginary_ = (lx * ry) + (ly * rx);
        var Real = new Decimal(Real_);
        var Imaginary = new Decimal(Imaginary_);
        return new Complex(Real, Imaginary);
    }
    public Complex Conjugate()
    {
        return new Complex(_Real, new Decimal(new Int(-1), 0) * _Imaginary);
    }
    public static Complex operator /(in Complex lhs, Complex rhs)
    {
        if (lhs == rhs) return new Complex(new Decimal(1), Decimal.zero);

        Complex res = lhs * rhs.Conjugate();
        var c = (rhs._Real * rhs._Real) + (lhs._Imaginary * rhs._Imaginary);
        res._Real /= c;
        res._Imaginary /= c;
        return res;
    }
    public static Complex operator +(Complex lhs, Complex rhs)
    {
        lhs._Real += rhs._Real;
        lhs._Imaginary += rhs._Imaginary;
        return lhs;
    }
    public static Complex operator -(Complex lhs, Complex rhs)
    {
        lhs._Real -= rhs._Real;
        lhs._Imaginary -= rhs._Imaginary;
        return lhs;
    }

}