using System;
using System.Numerics;
using System.Diagnostics;

class Program
{
    static BigInteger instCounter;
    static long stopwatchStartTimestamp;
    static long stopwatchEndTimestamp;
    static double stopwatchResult;
    static bool enableInstrumentation;
    static bool enableTimer;


    static void Main(string[] args)
    {
        TargetContainer inputContainer = new TargetContainer();
        enableInstrumentation = true;
        enableTimer = true;
        instCounter = 0;

        CsvHandler handler = new CsvHandler("out.csv");
        handler.appendLastInRow("int, v1Ins, v1Tim, v2Ins, v2Tim, v3Ins, v3Tim");

        foreach (BigInteger elem in inputContainer.list)
        {
            Console.WriteLine("===================");
            Console.WriteLine("Element {0}", elem);
            try
            {
                //add first column 
                handler.appendNext(elem.ToString("F0"));
                if (IsPrimeV1(elem))
                {
                    Console.WriteLine("V1 true");
                }
                else
                {
                    Console.WriteLine("V1 false");
                }
                //save counter
                Console.Write("{0} | ", instCounter);
                handler.appendNext(instCounter.ToString("F0"));
                instrumentation('r', enableInstrumentation);
                //save timestamp
                Console.WriteLine("{0}", stopwatchResult.ToString("F8"));
                handler.appendNext(stopwatchResult.ToString("F8"));
                stopwatch('r', enableTimer);
                if (IsPrimeV2(elem))
                {
                    Console.WriteLine("V2 true");
                }
                else
                {
                    Console.WriteLine("V2 false");
                }
                //save counter
                Console.Write("{0} | ", instCounter);
                handler.appendNext(instCounter.ToString("F0"));
                instrumentation('r', enableInstrumentation);
                //save timestamp
                Console.WriteLine("{0}", stopwatchResult.ToString("F8"));
                handler.appendNext(stopwatchResult.ToString("F8"));
                stopwatch('r', enableTimer);
                if (IsPrimeV3(elem))
                {
                    Console.WriteLine("V3 true");
                }
                else
                {
                    Console.WriteLine("V3 false");
                }
                //save counter
                Console.Write("{0} | ", instCounter);
                handler.appendNext(instCounter.ToString("F0"));                
                instrumentation('r', enableInstrumentation);   
                //save timestamp
                Console.WriteLine("{0}", stopwatchResult.ToString("F8"));
                handler.appendLastInRow(stopwatchResult.ToString("F8"));
                stopwatch('r', enableTimer);
            }
            catch (System.Exception)
            {          
                Console.WriteLine("Evaluation failed");
            }
        }
    }


    static bool IsPrimeV1(BigInteger n)
    {
                stopwatch('e', enableTimer);
        if (n  < 2)
        {
            stopwatch('d', enableTimer);
            return false;
        }
        else if (n  < 4)
        {
            stopwatch('d', enableTimer);
            return true;
        }
        else if (n  % 2 == 0)
        {
            instrumentation('n', enableInstrumentation);
            stopwatch('d', enableTimer);
            return false;
        }
        else
        {
            for (BigInteger u = 3; u < n  / 2; u += 2)
            {
                instrumentation('n', enableInstrumentation);   
                if (n  % u == 0)
                {
                    stopwatch('d', enableTimer);
                    return false;
                }
            }
        }
        stopwatch('d', enableTimer);
        return true;
    }

    static bool IsPrimeV2(BigInteger n)
    {
        stopwatch('e', enableTimer);
        if (n < 2)
        {
            stopwatch('d', enableTimer);
            return false;
        }
        else if (n == 2)
        {
            stopwatch('d', enableTimer);
            return true;
        }
        int i = 2;
        do
        {
            instrumentation('n', enableInstrumentation);
            if (n % i == 0)
            {
                stopwatch('d', enableTimer);
                return false;
            }
            i++;
        } while (i <= Math.Sqrt((double)n));
        stopwatch('d', enableTimer);
        return true;
    }

    static bool IsPrimeV3(BigInteger n)
    {
        stopwatch('e', enableTimer);
        if (n < 2)
        {
            stopwatch('d', enableTimer);
            return false;
        }
        if (n == 2 || n == 3)
        {
            stopwatch('d', enableTimer);
            return true;
        }
        if (n % 2 == 0 || n % 3 == 0)
        {
            instrumentation('n', enableInstrumentation);
            stopwatch('d', enableTimer);
            return false;
        }
        int i = 5;
        while (i * i <= n)
        {
            instrumentation('n', enableInstrumentation);
            if (n % i == 0 || n % (i + 2) == 0)
            {
                stopwatch('d', enableTimer);
                return false;
            }
            i += 6;
        }
        stopwatch('d', enableTimer);
        return true;
    }

    //Testers

    static void instrumentation(char i, bool toggle)
    {
        if (toggle)
        {
            switch (i)
            {
                case 'n':
                    instCounter++;
                    break;

                case 'r':
                    instCounter = 0;
                    break;

                default:
                    break;
            }
        }
    }
    static void stopwatch(char i, bool toggle) // , long timer
    {
        if (toggle)
        {
            switch (i)
            {
                case 'e': // enable
                    stopwatchStartTimestamp = Stopwatch.GetTimestamp();
                    break;

                case 'd': //disable
                    stopwatchEndTimestamp = Stopwatch.GetTimestamp();
                    stopwatchResult = stopwatchEndTimestamp - stopwatchStartTimestamp;
                    stopwatchResult = stopwatchResult * (1.0 / Stopwatch.Frequency);
                    break;

                case 'r': //reset
                    stopwatchStartTimestamp = 0;
                    stopwatchEndTimestamp = 0;
                    stopwatchResult = 0;
                    break;

                default:
                    break;
            }
        }
    }
}

class TargetContainer
{
    public List<BigInteger> list;


    public TargetContainer(List<BigInteger> input)
    {
        this.list = input;
    }
    public TargetContainer()
    {
        this.list = new List<BigInteger>();
        string? input;
        Console.WriteLine("Input elements to the list of numbers o be evaluated one by one.");
        Console.WriteLine("If all elements have been added type \"-end\".");
        while (true)
        {
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) != true)
            {  
                if (input == "-end")
                {
                    break;
                }
                else
                {
                    try
                    {
                        this.list.Add(BigInteger.Parse(input));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Unable to parse {0}", input);
                    }
                }
            }
            else
            {
                Console.WriteLine("Input given cannot be null");
            }   
        }
    }
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