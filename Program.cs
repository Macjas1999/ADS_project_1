using System;
using System.Numerics;

class Program
{
    bool IsPrime(BigInteger Num)
    {
        if (Num < 2) return false;
        else if (Num < 4) return true;
        else if (Num % 2 == 0) return false;
        else for (BigInteger u = 3; u < Num / 2; u += 2)
        if (Num % u == 0) return false;
        return true;
    }
}