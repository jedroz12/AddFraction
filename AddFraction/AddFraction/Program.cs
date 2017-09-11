using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddFraction
{
    class Program
    {
        //these values could be in arrays but I don't care enough to do that

        //first fraction
        public static int numerator;
        public static int denominator;
        public static int mixed;

        //second fraction
        public static int numerator2;
        public static int denominator2;
        public static int mixed2;

        //third fraction
        public static int finalNumerator;
        public static int finalDenominator;
        public static int finalMixed;

        static void Main(string[] args)
        {
            //run main function
            GetInput();
        }

        static void GetInput()
        {
            //attempt to get user input
            try
            {
                Console.WriteLine("Enter Numerator: ");
                numerator = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Denominator: ");
                denominator = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Numerator For Second Fraction: ");
                numerator2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Denominator For Second Fraction: ");
                denominator2 = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Check Inputs.");
                GetInput();
            }

            //set greatestCommonFactor
            int greatestCommonFactor = 0;
            int greatestCommonFactor2 = 0;
            int greatestCommonFactor3 = 0;

            //find GCF of fractions 1 and 2
            GCF(ref numerator, ref denominator, ref greatestCommonFactor);
            GCF(ref numerator2, ref denominator2, ref greatestCommonFactor2);

            //simplify both fractions
            Simplify(ref numerator, ref denominator, ref greatestCommonFactor, ref mixed);
            Simplify(ref numerator2, ref denominator2, ref greatestCommonFactor2, ref mixed2);

            //add both fractions to create 3rd fraction
            Add(ref numerator, ref denominator, ref mixed, ref numerator2, ref denominator2, ref mixed2);
            
            //find GCF of that fraction
            GCF(ref finalNumerator, ref finalDenominator, ref greatestCommonFactor3);
            
            //simplify third fraction
            Simplify(ref finalNumerator, ref finalDenominator, ref greatestCommonFactor3, ref finalMixed);

            //uncomment to show results of other fractions, not just the final one
            Output(mixed, numerator, denominator, greatestCommonFactor);
            Output(mixed2, numerator2, denominator2, greatestCommonFactor2);
            Output(finalMixed, finalNumerator, finalDenominator, greatestCommonFactor3);

            //when finished, restart
            mixed = 0;
            mixed2 = 0;
            finalMixed = 0;
            GetInput();
        }

        static void Output(int outputMixed, int outputNumerator, int outputDenominator, int outputGreatestCommonFactor)
        {
            if (outputNumerator == 0)
            {
                //if the answer is a whole number, simply output mixed
                Console.WriteLine("Answer: {0}", outputMixed);
            }
            else if (outputNumerator == outputDenominator)
            {
                //above statement will not catch if num and den are equal, therefore there must be a seperate handler. Cannot both be in one if statement due to incrementation of outputMixed
                outputMixed++;
                Console.WriteLine("Answer: {0}", outputMixed);
            }
            else if (outputMixed == 0)
            {
                //if it is not a mixed fraction
                Console.WriteLine("Answer: {0}/{1} with a GCF of {2}", outputNumerator, outputDenominator, outputGreatestCommonFactor);
            }
            else
            {
                //otherwise, output mixed fraction
                Console.WriteLine("Answer: {0} {1}/{2} with a GCF of {3}", outputMixed, outputNumerator, outputDenominator, outputGreatestCommonFactor);
            }
        }

        static void GCF(ref int numerator, ref int denominator, ref int greatestCommonFactor)
        {
            //loop through denominator
            for (int x = 1; x <= denominator; x++)
            {
                //if both divide by x = 0
                if ((numerator % x == 0) && (denominator % x == 0))
                {
                    //set gcf to x
                    greatestCommonFactor = x;
                }
            }
            if (greatestCommonFactor == 0)
            {
                //on zero input
                return;
            }
        }

        static void Simplify(ref int num, ref int den, ref int greatest, ref int mix)
        {
            //divide numerator and denominator by gcf
            //must be wrapped in try catch due to zero inputs
            try
            {
                num = num / greatest;
                den = den / greatest;
                //set mixed to numerator divided by denominator if necessary
                if (num > den)
                {
                    //set mix for current fraction instance
                    int currentMix = num / den;
                    num = num - den * currentMix;
                    //set total mix
                    mix += currentMix;
                    //reset current for next fraction instance
                    currentMix = 0;
                }
            }
            catch
            {
                Console.WriteLine("Both Fractions are null!");
                //restart on zero
                mixed = 0;
                mixed2 = 0;
                finalMixed = 0;
                GetInput();
            }
        }

        static void Add(ref int num1, ref int den1, ref int mix1, ref int num2, ref int den2, ref int mix2)
        {
            //instead of finding GCF, just multiply them both. Brute-force method.
            finalDenominator = den1 * den2;
            //as above, but for numerator
            finalNumerator = (num1 * den2) + (num2 * den1);
            //add both mixes
            finalMixed = mix1 + mix2;
        }
    }
}
