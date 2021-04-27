using System;

namespace arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            var empty=new double[0];
            var one = new double[]{0.5};
            var two = new double[]{-12, 92378237923923982982.0};
            var three = new double[]{1, 2, 3};
            printAverage(empty);
            printAverage(two);
            printAverage(one);
            printProduct(three);
            Console.WriteLine("the sum is {0}", skipSum(new double[]{8, -3, 7, -4, 1, -5.0/2, 9}));
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
            return sum/data.Length;


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
        for(int i = 0; i<data.Length; ++i)
        {
            
            p = p*data[i];
            

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
            for (int i = 0; i < data.Length; i+=2)
            {
                sum = sum + data[i];
            }
            return sum;


        }
        
    }
}
