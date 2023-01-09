
namespace Calculator;
using LL = System.Numerics.BigInteger;
using Do = System.Double;


public class Int : INumber
{
    private LL _Value;

    public Int(LL _Value)
    {
        this._Value = _Value;
    }
    override public string ToString()
    {

        string _Value_ToString = _Value.ToString();
        if (_Value_ToString.Length <= 2) return _Value_ToString;
        string _Value_ToString_Trimmed = _Value_ToString.TrimEnd('0');
        _Value_ToString_Trimmed = (_Value_ToString_Trimmed == "") ? "0" : _Value_ToString_Trimmed;
        var flag = _Value_ToString[0] == '-';
        var a = (flag ? "-" : "+");
        var b = flag ? _Value_ToString[1] : _Value_ToString[0];

        var k = ((flag) ? 2 : 1);
        var c = (_Value_ToString_Trimmed.Length > k);
        var d = (c ? "." : "");
        var e = _Value_ToString_Trimmed.Substring(k, _Value_ToString_Trimmed.Length - k);
        var f = "x10^(";
        var g = _Value_ToString.Length - k;
        var h = g.ToString();
        var i = ")";
        var l = (g == 0) ? "" :
            f + h + i;
        var j = a + b + d + e + l;
        return j;
        //return "";
    }
    public static Int operator +(in Int lhs, in Int rhs)
    {


        return new Int(lhs._Value + rhs._Value);
    }
    public static Int operator -(in Int lhs, in Int rhs)
    {
        return new Int(lhs._Value - rhs._Value);
    }
    public static bool operator ==(in Int lhs, in Int rhs)
    {
        return (lhs._Value == rhs._Value);
    }
    public static bool operator !=(in Int lhs, in Int rhs)
    {
        return !(lhs == rhs);
    }
    public static bool operator <(in Int lhs, in Int rhs)
    {
        return (lhs._Value < rhs._Value);
    }
    public static bool operator >(in Int lhs, in Int rhs)
    {
        return rhs < lhs;
    }
    public static bool operator <=(in Int lhs, in Int rhs)
    {
        return (lhs < rhs) || (lhs == rhs);
    }
    public static bool operator >=(in Int lhs, in Int rhs)
    {
        return rhs <= lhs;
    }
    public static Int operator *(in Int lhs, in Int rhs)
    {

        return new Int(lhs._Value * rhs._Value);
    }

    public static Int operator /(in Int lhs, in Int rhs)
    {

        return new Int(lhs._Value / rhs._Value);
    }

    public LL ToBigInteger()
    {
        return this._Value;
    }

    public Do ToDouble()
    {
        string _Value_String = _Value.ToString();
        int _Value_String_Length = _Value_String.Length;
        return (_Value_String_Length > 15)
            ? (Do.Parse(_Value_String.Substring(0, 15)) * (Math.Pow(10, (_Value_String_Length - 15))))
            : Convert.ToDouble(Do.Parse(_Value_String));
    }
    public static Int zero = new(0);

    public Do Length
    {
        set
        {
            _Value = (LL)value;
        }
        get
        {
            Do _Value_Decimal = this.ToDouble();
            return ((_Value_Decimal < 0)) ? -1 * _Value_Decimal : _Value_Decimal;
        }
    }

    public Do Degree
    {
        set
        {
            bool isNegative = (_Value < 0);
            if (value == 0 || value == 180)
            {
                if ((value == 0 && isNegative) || (value == 180 && !isNegative))
                    _Value = -_Value;
            }
            else throw new ApplicationException("the Degree of an Integer must be either 0 or 180 \n");
        }
        get
        {
            return (_Value < 0) ? 180 : 0;
        }
    }
}
