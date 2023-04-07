using System;
using System.Numerics;
using System.Diagnostics;

class Program
{
    static BigInteger testNum;

    static void Main(string[] args)
    {
        testNum = -1;

        try
        {
            if (IsPrimeV1(testNum))
            {
                Console.WriteLine("V1 true");
            }
            else
            {
                Console.WriteLine("V1 false");
            }
            if (IsPrimeV2(testNum))
            {
                Console.WriteLine("V2 true");
            }
            else
            {
                Console.WriteLine("V2 false");
            }
            if (IsPrimeV3(testNum))
            {
                Console.WriteLine("V3 true");
            }
            else
            {
                Console.WriteLine("V3 false");
            }   
        }
        catch (System.Exception)
        {          
            throw;
        }
    }

    static bool IsPrimeV1(BigInteger n)
    {
        if (n  < 2)
        {
            return false;
        }
        else if (n  < 4)
        {
            return true;
        }
        else if (n  % 2 == 0)
        {
            return false;
        }
        else
        {
            for (BigInteger u = 3; u < n  / 2; u += 2)
            {
                if (n  % u == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
    static bool IsPrimeV2(BigInteger n)
    {
        if (n < 2)
        {
            return false;
        }
        else if (n == 2)
        {
            return true;
        }
        int i = 2;
        do
        {
            if (n % i == 0)
            {
                return false;
            }
            i++;
        } while (i <= Math.Sqrt((double)n));
        return true;
    }
    static bool IsPrimeV3(BigInteger n)
    {
        if (n < 2)
        {
            return false;
        }
        if (n == 2 || n == 3)
        {
            return true;
        }
        if (n % 2 == 0 || n % 3 == 0)
        {
            return false;
        }
        int i = 5;
        while (i * i <= n)
        {
            if (n % i == 0 || n % (i + 2) == 0)
            {
                return false;
            }
            i += 6;
        }
        return true;
    }

    //Misc
    // static bool handleInput()
    // {
        
    //     string? input = Console.ReadLine();
    //     if (input != null)
    //     {
    //         testNum = BigInteger.Parse(input);
    //         return true;
    //     }
    //     else
    //     {
    //         Console.WriteLine("Invalid input");
    //         return false;
    //     }
    // }


}




//
//Misc
//
class CsvHandler
{
    public string fileName;
    public bool test;
    public CsvHandler(string fileName)
    {
        this.fileName = fileName;
        if(File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        else if(!File.Exists(fileName))
        {
            using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate , FileAccess.ReadWrite))
            {
                if(fs.CanWrite) this.test = true;
            }
        }
    }
    public void appendNext(string data)
    {
        using(StreamWriter writer = new StreamWriter(this.fileName, append: true))
        {
            writer.Flush();
            writer.Write(data);
            writer.Write(",");
            writer.Close();
        }
    }
    public void appendLastInRow(string data)
    {
        using(StreamWriter writer = new StreamWriter(this.fileName, append: true))
        {
            writer.Flush();
            writer.Write(data);
            writer.Write(Environment.NewLine);
            writer.Close();
        }
    }
}