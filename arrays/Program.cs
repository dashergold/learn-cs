using System;

namespace arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = reverseStrings(new string[] { "a", "b", "c", "d" });
            var cop = copyStrings(new string[] { "a", "b", "c", "d" });
            var empty = new double[0];
            var one = new double[] { 0.5 };
            var two = new double[] { -12, 92378237923923982982.0 };
            var three = new double[] { 1, 2, 3 };
            var c = new int[5];
            printMedian(new double[]{3});
            printMedian(new double[]{3, 4 , 12, 6});
            printMedian(new double[]{3, 7, 2, 1, 9});

            sparseCopies();
            printAverage(empty);
            printAverage(two);
            printAverage(one);
            printProduct(three);
            Console.WriteLine("the sum is {0}", skipSum(new double[] { 8, -3, 7, -4, 1, -5.0 / 2, 9 }));
        }


static void sparseCopies() {
    var a1 = sparseCopy(new string []{"a"}); //  a
    var a2 = sparseCopy(new string []{"a", "b"}); //  a
    var a3 = sparseCopy(new string []{"a", "b", "c"}); //  a c
    var a4 = sparseCopy(new string []{"a", "b", "c", "d", "e", "f", "g", "h", "i", "j" }); //  a c e g i
}
        static void printAverage(double[] data)
        {
            var a = average(data);
            Console.WriteLine("average is {0}", a);
        }
        static double average(double[] data)
        {
            if (data.Length == 0)
            {
                return 0;
            }
            double sum = 0;
            for (int i = 0; i < data.Length; ++i)
            {
                sum = sum + data[i];
            }
            return sum / data.Length;


        }
        static void stringArrays()
        {
            var a = new string[]
                {
                "hej",
                "din",
                "tjockskalle"
                };

            var s = string.Join(" ", a);
            var a2 = s.Split(" ");
            Console.WriteLine(s);
            a2[2] = "dumbom";
            var s2 = string.Join(",", a2);
            Console.WriteLine(s2);

            Console.WriteLine(a2[1]);




        }
        static double product(double[] data)
        {
            if (data.Length == 0)
            {
                return 0;
            }
            double p = 1;
            for (int i = 0; i < data.Length; ++i)
            {

                // same as p = p * data[i];
                p *= data[i];


            }
            return p;
        }
        static void printProduct(double[] data)
        {
            var a = product(data);
            Console.WriteLine("product is {0}", a);
        }

        //compute the sum of every other number
        static double skipSum(double[] data)
        {
            if (data.Length == 0)
            {
                return 0;
            }
            double sum = 0;
            for (int i = 0; i < data.Length; i += 2)
            {
                sum = sum + data[i];
            }
            return sum;


        }
        static string[] reverseStrings(string[] array)
        {

            var r = new string[array.Length];
            int j = 0;
            for (int i = array.Length - 1; i >= 0; --i, ++j)
            {
                r[j] = array[i];
            }
            return r;

        }

        static string [] copyStrings(string[] array)
        {
            var cop = new string[array.Length];
            
            for(int i=0; i<array.Length; ++i)
            {
                cop[i] = array[i];
            }
            return cop;
        }

        static void printMedian(double[] array)
        {
            Console.WriteLine("median is {0}", medianDouble(array));
        }
        static double medianDouble(double [] array)
        {

            Array.Sort(array);
            if(array.Length%2==0)
            {
                var i= array.Length/2;
                var med=(array[i-1]+array[i])/2;
                return med;
            }else
            {
                var i = array.Length/2;
                return array[i];
            }
            
        }
    }
}
