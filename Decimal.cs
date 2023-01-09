namespace Calculator;
using LL = System.Numerics.BigInteger;
using Do = System.Double;
class Decimal : INumber
{
    Int _Mantissa;
    int _Power;
    public Decimal(double x)
    {

        string s = x.ToString(string.Format("E{0}", 15));

        int index = s.IndexOf("E");
        if (index < 0)
        {
            _Mantissa = new Int(LL.Parse(s));
            _Power = 0;
            return;
        }
        else _Power = int.Parse(s[(index + 1)..]) - 15;
        _Mantissa = new Int(LL.Parse(s.Substring(0, index).Replace(".", "")));

    }
    public Decimal(Int Mantissa, int Power)
    {
        this._Mantissa = Mantissa;
        this._Power = Power;
        this.TrimEndZeros();
    }



    public void TrimEndZeros()
    {

        string s = _Mantissa.ToString();
        if (s == "0")
        {
            _Power = 0;
            return;
        }
        int sl = s.Length;
        string v = s.TrimEnd('0');
        if (v == "") v = "0";
        int vl = v.Length;
        if (sl > vl)
        {
            _Mantissa = new Int(LL.Parse(v));
            _Power += sl - vl;
        }
    }
    public void StretchZeros(int i)
    {
        this.TrimEndZeros();
        string s = _Mantissa.ToString();
        if (i > s.Length)
        {
            int diff = i - s.Length;
            s += new string('0', diff);

            _Mantissa = new Int(LL.Parse(s));
            _Power -= diff;
        }
    }
    void IncreaseMantissaLength(int x)
    {
        StretchZeros(_Mantissa.ToString().Length + x);
    }
    private static void EqualizePower(ref Decimal x, ref Decimal y)
    {
        var xx = x._Power;
        var yy = y._Power;
        if (yy < xx)
        {
            var diff = xx - yy;
            y.IncreaseMantissaLength(diff);
        }
        else
        {
            var diff = yy - xx;
            y.IncreaseMantissaLength(diff);
        }
    }


    private static void EqualizeLengthOfMantissa(ref Decimal x, ref Decimal y)
    {
        var xx = x._Mantissa.ToBigInteger();
        var yy = y._Mantissa.ToBigInteger();
        var xxx = xx.ToString();
        var yyy = yy.ToString();
        if (xxx.Length < yyy.Length)
        {
            var diff = yyy.Length - xxx.Length;
            xxx += new string('0', diff);
            x._Power -= diff;
        }
        else
        {
            var diff = xxx.Length - yyy.Length;
            yyy += new string('0', diff);
            x._Power -= diff;
        }
        xx = LL.Parse(xxx);
        yy = LL.Parse(yyy);
        x._Mantissa = new Int(xx);
        y._Mantissa = new Int(yy);
    }
    public Decimal PowerOfTen(int x)
    {
        return new Decimal(new Int(1), x);
    }
    public override string ToString()
    {

        this.TrimEndZeros();

        string input1 = this._Mantissa.ToString();

        bool flag = input1.Contains("x");
        if (flag)
        {
            string pattern = "x10\\^\\((\\d+)\\)";
        System.Text.RegularExpressions.Match match1 = System.Text.RegularExpressions.Regex.Match(input1, pattern);
        int exponent1 = int.Parse(match1.Groups[1].Value);
        exponent1 += _Power;
        string replacement = exponent1 != 0 ? "x10^(" + exponent1 + ")" : "";
        string output = System.Text.RegularExpressions.Regex.Replace(input1, pattern, replacement);
        return output;
        }


        return input1;

    }
    public static bool operator ==(in Decimal lhs, in Decimal rhs)
    {

        return (lhs._Mantissa == rhs._Mantissa) && ((lhs._Power == rhs._Power) || ((lhs._Mantissa == Int.zero)));
    }
    public static bool operator !=(in Decimal lhs, in Decimal rhs)
    {
        return !(lhs == rhs);
    }
    public static bool operator <(in Decimal lhs, in Decimal rhs)
    {
        bool is_rhs_Negative = (rhs._Mantissa < Int.zero);
        bool is_lhs_Negative = (lhs._Mantissa < Int.zero);
        if ((is_lhs_Negative) && !(is_rhs_Negative)) return true;
        else if (!(is_lhs_Negative) && (is_rhs_Negative)) return false;
        var lhs_temp = lhs;
        var rhs_temp = rhs;
        EqualizeLengthOfMantissa(ref lhs_temp, ref rhs_temp);
        if (is_lhs_Negative)
        {
            //the two numbers is negative
            if (lhs_temp._Power < rhs_temp._Power)
                return false;
            else
                if (lhs_temp._Power > rhs_temp._Power)
                return true;
            else
            {
                if (lhs_temp._Mantissa < rhs_temp._Mantissa) return false;
                else return true;
            }
        }
        else
        {
            //the two numbers is positive
            if (lhs_temp._Power < rhs_temp._Power) return true;
            else if (lhs_temp._Power > rhs_temp._Power) return false;
            else
            {
                if (lhs_temp._Mantissa < rhs_temp._Mantissa) return true;
                else return false;
            }
        }
    }
    public static bool operator >(in Decimal lhs, in Decimal rhs)
    {
        return rhs < lhs;
    }
    public static bool operator <=(in Decimal lhs, in Decimal rhs)
    {
        return (rhs < lhs) || (rhs == lhs);
    }
    public static bool operator >=(in Decimal lhs, in Decimal rhs)
    {
        return (rhs < lhs) || (rhs == lhs);
    }


    public static Decimal operator +(in Decimal lhs, in Decimal rhs)
    {
        var lhs_temp = lhs;
        var rhs_temp = rhs;
        lhs_temp.TrimEndZeros();
        rhs_temp.TrimEndZeros();
        EqualizePower(ref lhs_temp, ref rhs_temp);
        lhs_temp._Mantissa += rhs_temp._Mantissa;
        lhs_temp.TrimEndZeros();
        return lhs_temp;
    }
    public static Decimal operator -(in Decimal lhs, in Decimal rhs)
    {
        var lhs_temp = lhs;
        var rhs_temp = rhs;
        lhs_temp.TrimEndZeros();
        rhs_temp.TrimEndZeros();
        EqualizePower(ref lhs_temp, ref rhs_temp);
        lhs_temp._Mantissa -= rhs_temp._Mantissa;
        lhs_temp.TrimEndZeros();
        return lhs_temp;
    }
    public static Decimal operator *(in Decimal lhs, in Decimal rhs)
    {
        var lhs_temp = lhs;
        var rhs_temp = rhs;
        lhs_temp.TrimEndZeros();
        rhs_temp.TrimEndZeros();
        lhs_temp._Mantissa *= rhs_temp._Mantissa;
        lhs_temp._Power += rhs_temp._Power;
        lhs_temp.TrimEndZeros();
        return lhs_temp;
    }
    public static readonly Decimal zero = new(Int.zero, 0);
    public static Decimal operator /(in Decimal lhs, in Decimal rhs)
    {
        if (lhs == rhs) return new Decimal(1);
        var lhs_temp = lhs;
        lhs_temp.TrimEndZeros();
        var rhs_temp = rhs;
        rhs_temp.TrimEndZeros();
        var x = rhs_temp.Length.ToString().Length * 2 + 16;
        if (x > 40) x = 40;
        var pw = Math.Pow(10, x);
        var po = new LL(pw);
        var pI = new Int(po);
        rhs_temp._Mantissa = pI / rhs_temp._Mantissa;

        rhs_temp._Power = -rhs_temp._Power - x;
        return lhs_temp * rhs_temp;

    }
    public Do ToDouble()
    {
        return ((this._Mantissa.ToDouble()) * (Math.Pow(10, (this._Power))));
    }
    public Do Degree
    {
        get
        {
            return (this._Mantissa < Int.zero) ? 180 : 0;
        }
        set
        {
            bool isNegative = (this._Mantissa < Int.zero);
            if ((value == 180 && !isNegative) || (value == 0 && isNegative))
            {
                this._Mantissa = new Int(-1) * this._Mantissa;
            }
            else throw new ApplicationException("The Degree Of A Decimal Number Must be Either 0 or 180");
        }
    }
    public Do Length
    {
        get
        {
            return (this._Mantissa > Int.zero)
                ? (this.ToDouble())
                : (-1 * this.ToDouble());

        }
        set
        {
            var x = value.ToString("E");
        }
    }
}