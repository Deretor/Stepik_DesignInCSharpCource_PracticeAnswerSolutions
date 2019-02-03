using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public struct Rational {

        private double value { get { return (double)((double)Numerator / (double)Denominator); } }

        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        public bool IsNan { get { return Denominator == 0; } }

        public Rational(int numerator, int denominator) {
           
            if (denominator < 0) {
                denominator = Math.Abs(denominator);
                numerator *= -1;
            }
            this.Numerator = this.Denominator = 0;
            var mod = getMOD(numerator, denominator);
            this.Numerator = numerator/mod;
            this.Denominator = denominator/mod;
        }

        public Rational(int integer) {
            this.Numerator = integer;
            this.Denominator = 1;
        }




        // operators
        public static Rational operator +(Rational a, Rational b) {
            if (a.IsNan || b.IsNan) { return new Rational(0, 0); }
            if (a.Denominator == b.Denominator)
            {
                return new Rational(a.Numerator + b.Numerator, a.Denominator);
            }
            else
            {
                return new Rational(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator*b.Denominator);
            }
        }
        public static Rational operator +(Rational a, int b) {
            if (a.IsNan) { return new Rational(0,0);}
            return new Rational(a.Numerator + b * a.Denominator, a.Denominator);
        }
        public static Rational operator +(int a, Rational b) {
            if (b.IsNan) { return new Rational(0, 0); }
            return new Rational(a * b.Denominator + b.Numerator, b.Denominator);
        }


        //

        public static Rational operator *(Rational a, Rational b) {
            if (a.IsNan || b.IsNan) { return new Rational(0, 0); }
            return new Rational(a.Numerator * b.Numerator, a.Denominator*b.Denominator);
        }
        public static Rational operator *(Rational a, int b) {
            if (a.IsNan) { return new Rational(0, 0); }
            return new Rational(a.Numerator * b, a.Denominator);
        }
        public static Rational operator *(int a, Rational b) {
            if (b.IsNan) { return new Rational(0, 0); }
            return new Rational(a * b.Numerator, b.Denominator);
        }


        //

        public static Rational operator /(Rational a, Rational b)
        {
            if (a.IsNan || b.IsNan) { return new Rational(0, 0); }
            return new Rational(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }
        public static Rational operator /(Rational a, int b)
        {
            if (a.IsNan) { return new Rational(0, 0); }
            return new Rational(a.Numerator, a.Denominator*b);
        }
        public static Rational operator /(int a, Rational b)
        {
            if (b.IsNan) { return new Rational(0, 0); }
            return new Rational(b.Numerator, b.Denominator*a);
        }

        public static Rational operator - (Rational a, Rational b) {
            if (a.IsNan || b.IsNan) { return new Rational(0, 0); }
            if (a.Denominator == b.Denominator)
            {
                return new Rational(a.Numerator - b.Numerator, a.Denominator);
            }
            else {
                return new Rational(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);                
            }
        }
        public static Rational operator -(Rational a, int b) {
            if (a.IsNan) { return new Rational(0, 0); }
            return new Rational(a.Numerator - b*a.Denominator, a.Denominator);
        }
        public static Rational operator -(int a, Rational b) {
            if (b.IsNan) { return new Rational(0, 0); }
            return new Rational(a*b.Denominator - b.Numerator, b.Denominator);
        }

        // convertions
        public static implicit operator int(Rational instance)
        {
            if (instance.IsNan) { return default(int); }

            if (instance.Numerator % instance.Denominator == 0) {
                return Convert.ToInt32(instance.value);
            }
            throw new ArgumentException();
        }

        public static implicit operator Rational(int val)
        {
            return new Rational(val);
        }
        public static implicit operator short(Rational instance)
        {
            if (instance.IsNan) { return default(short); }
            if (instance.Numerator % instance.Denominator == 0)
            {
                return Convert.ToInt16(instance.value);
            }
            throw new ArgumentException();
        }
        public static implicit operator Rational(short val)
        {
            return new Rational(val);
        }
        public static implicit operator double(Rational instance)
        {
            if (instance.IsNan) { return default(double); }
            return instance.value;
        }
        public static implicit operator Rational(double val)
        {
            return new Rational(Convert.ToInt32(val));
        }


        private int getMOD(int a, int b) {

            int i;
            int ob = 0;
            for (i = 2; i < System.Math.Max(a, b); i++)
                if ((a % i == 0) && (b % i == 0))
                {
                    ob = i;
                    break;
                }
            if (ob != 0)
                return ob;
            else
                return 1;
            

        }
    }
    
}
