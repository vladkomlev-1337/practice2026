using System;
using System.Threading;
namespace task14;

public class DefiniteIntegral
{
    public double a {get;set;}
    public double b {get;set;}
    public Func<double, double> function {get;set;}
    public double step {get;set;}
    public int threadsnumber {get;set;}
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double totalsum = 0.0;
        var barrier = new Barrier(threadsnumber + 1);
        double sectionLength = (b-a)/threadsnumber;
        for (int i = 0; i < threadsnumber; i ++)
        {
            double localA = a + i * sectionLength;
            double localB = localA + sectionLength;
            if (localA > b)
            {
                localA = b;
            }
            if(localB > b)
            {
                localB = b;
            }
            new Thread(() =>
            {

                int n = (int)((localB-localA)/step);
                if (n < 1)
                {
                    n = 1;
                }
                double h = (localB - localA) / n;
                double sum = (function(localA) + function(localB)) / 2.0;
                for (int j  = 1; j < n; j++)
                {
                    double x = localA + j*h;
                    sum += function(x);
                }
                double localsum = sum * h;
                double oldValue;
                double newValue;
                while (true)
                {
                    oldValue = totalsum;
                    newValue = oldValue + localsum;
                    if (Interlocked.CompareExchange(ref totalsum, newValue, oldValue) == oldValue)
                    {
                        break;
                    }
                }
                barrier.SignalAndWait();
            }).Start();
            

        }
        barrier.SignalAndWait();
        return totalsum;
    }
}