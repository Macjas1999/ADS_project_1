using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration; 


namespace InitMain
{
    class Program
    {
        static long countOperations;
        static int numIter = 256;
        static int setSize = 131072;// 131072 2^17 //268435456 int 67108864
        // static int ;
        static void Main(string[] args)
        {
            
            // TestObject testArrayOne = new TestObject(268435456);// Task required 2^28 = 268435456

            CsvHandler handler = new CsvHandler("output.csv");
            handler.appendNext("LMaxI,LMaxT,LAvgI,LAvgT,BMaxI,BMaxT,BAvgI");
            handler.appendLastInRow("BAvgT");

            Console.WriteLine("  LMaxI |    LMaxT  |  LAvgI |   LAvgT   | BMaxI|   BMaxT   | BAvgI|   BAvgT   |");

            for (int arraySize = setSize; arraySize <= setSize*10; arraySize += setSize) 
            {
                using(TestObject testArray = new TestObject((uint)arraySize))
                {

                    linearMaxInstr(testArray, handler); 
                    linearMaxTime(testArray, numIter, handler);
                    linearAvgInstr(testArray, handler);
                    linearAvgTime(testArray, numIter, handler);
    
                    binaryMaxInstr(testArray, handler);
                    binaryMaxTime(testArray, numIter, handler);
                    binaryAvgInstr(testArray, handler);
                    binaryAvgTime(testArray, numIter, handler);

                    Console.WriteLine();
                }      
            }
        }
        

        ////Search functions
        //Linear
        static bool searchLinearGeneric(int[] vector, int searchTarget)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] == searchTarget)
                    return true;
            }
            return false; 
        }
        //Linear with instrumentation
        static bool searchLinearInst(int[] vector, int searchTarget)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                countOperations++;
                if (vector[i] == searchTarget)
                {
                    return true;
                }
            }
            return false; 
        }
        // Binary
        static bool searchBinaryGeneric(int[] vector, int searchTarget)
        {
            int left = 0;
            int right = vector.Length - 1;
            int middle;

            while (left <= right)
            {
                middle = (left + right) / 2;
                if (vector[middle] == searchTarget)
                {
                    return true;
                }
                else
                {
                    if (vector[middle] > searchTarget)
                    {
                        right = middle - 1;
                    }
                    else left = middle + 1;
                }
            }
            return false;
        }
        // Binary with instrumentation
        static bool searchBinaryInst(int[] vector, int searchTarget)
        {
            int left = 0;
            int right = vector.Length - 1;
            int middle;

            while (left <= right)
            {
                middle = (left + right) / 2;
                countOperations++;
                if (vector[middle] == searchTarget)
                {
                    return true;
                }
                else
                {
                    countOperations++;
                    if (vector[middle] > searchTarget)
                    {
                        right = middle - 1; 
                    }
                    else left = middle + 1;
                }
            }
            return false;
        }
        //Time testing
        ///Max timespan
        ////Linear
        static void linearMaxInstr(TestObject targetObj, CsvHandler handler)
        {
            countOperations = 0;
            bool present = searchLinearInst(targetObj.array, targetObj.array.Length - 1);
            Console.Write(/*"\t" +*/ countOperations.ToString("F2") +" |");
            handler.appendNext(countOperations.ToString("F2"));
        }
        static void linearMaxTime(TestObject targetObj, int numIter, CsvHandler handler)
        {
            double elapSeconds;
            //int elapTime = 0;
            long mintime = long.MaxValue; //targetObj.array[0];
            long maxTime = long.MinValue; //targetObj.array[targetObj.array.Length - 1];
            long iterationElapTime;

            for (int n = 0; n < (numIter); ++n)
            {
                long startTime = Stopwatch.GetTimestamp();
                // Insert function 
                bool isPresent = searchLinearGeneric(targetObj.array, targetObj.array.Length - 1);
                long endTime = Stopwatch.GetTimestamp();
                iterationElapTime = endTime - startTime;
                //elapTime += iterationElapTime;
                if (iterationElapTime < mintime) mintime = iterationElapTime;
                if (iterationElapTime > maxTime) maxTime = iterationElapTime; 
            }   
            //Insert export to csv
            // changed ! elapTime to maxTime
            elapSeconds = maxTime * (1.0 / Stopwatch.Frequency);
            Console.Write(/*"\t" +*/ elapSeconds.ToString("F8") +" |");
            handler.appendNext(elapSeconds.ToString("F8"));
        }
        ////Binary
        static void binaryMaxInstr(TestObject targetObj, CsvHandler handler)
        {
            countOperations = 0;
            bool present = searchBinaryInst(targetObj.array, targetObj.array.Length - 1);
            Console.Write(/*"\t" +*/ countOperations.ToString("F2") +" |");
            handler.appendNext(countOperations.ToString("F2"));        
        }
        static void binaryMaxTime(TestObject targetObj, int numIter, CsvHandler handler)
        {
            double elapSeconds;
            //int elapTime = 0;
            long mintime = long.MaxValue; //targetObj.array[0];
            long maxTime = long.MinValue; //targetObj.array[targetObj.array.Length - 1];
            long iterationElapTime;

            for (int n = 0; n < (numIter); ++n)
            {
                long startTime = Stopwatch.GetTimestamp();
                // Insert function 
                bool isPresent = searchBinaryGeneric(targetObj.array, targetObj.array.Length - 1);
                long endTime = Stopwatch.GetTimestamp();
                iterationElapTime = endTime - startTime;
                //elapTime += iterationElapTime;
                if (iterationElapTime < mintime) mintime = iterationElapTime;
                if (iterationElapTime > maxTime) maxTime = iterationElapTime; 
            }   
            //Insert export to csv

            elapSeconds = maxTime * (1.0 / Stopwatch.Frequency);
            Console.Write(/*"\t" +*/ elapSeconds.ToString("F8") +" |");
            handler.appendNext(elapSeconds.ToString("F8"));        
        }

        ///Mean timespan
        ////Linear
        static void linearAvgInstr(TestObject targetObj, CsvHandler handler)
        {
            countOperations = 0;
            bool present;
            for (int i = 0; i < targetObj.array.Length; i++)
            {
                present = searchLinearInst(targetObj.array, i);
            }
            Console.Write(/*"\t" +*/ ((double)countOperations / (double)targetObj.array.Length).ToString("F2") +" |");
            handler.appendNext(((double)countOperations / (double)targetObj.array.Length).ToString("F2"));
        }

        static void linearAvgTime(TestObject targetObject, int numIter, CsvHandler handler)
        {
            double meanSeconds;
            double sumTime = 0;
            long iterationElapTime;

            for(int n = 0; n < (numIter); ++n)
            {
        
                long startTime = Stopwatch.GetTimestamp();
                bool isPresent = searchLinearGeneric(targetObject.array, numIter);
                long endTime = Stopwatch.GetTimestamp();
                iterationElapTime = endTime - startTime;
                sumTime += iterationElapTime;
            }

            meanSeconds = (sumTime / numIter) * (1.0 / Stopwatch.Frequency); //(numIter * Stopwatch.Frequency)
            Console.Write(/*"\t" +*/ meanSeconds.ToString("F8") +" |");   
            handler.appendNext(meanSeconds.ToString("F8"));                 
        }
        ////Binary
        static void binaryAvgInstr(TestObject targetObj, CsvHandler handler)
        {
            countOperations = 0;
            bool present;
            for (int i = 0; i < targetObj.array.Length +1; i++)
            {
                present = searchBinaryInst(targetObj.array, i);
            }
            Console.Write(/*"\t" +*/ ((double)countOperations / (double)targetObj.array.Length).ToString("F2") +" |");
            handler.appendNext(((double)countOperations / (double)targetObj.array.Length).ToString("F2"));        
        }
        static void binaryAvgTime(TestObject targetObject, int numIter, CsvHandler handler)
        {
            double meanSeconds;
            double sumTime = 0;
            long iterationElapTime;

            for(int n = 0; n < (numIter); ++n)
            {
        
                long startTime = Stopwatch.GetTimestamp();
                bool isPresent = searchBinaryGeneric(targetObject.array, numIter);
                long endTime = Stopwatch.GetTimestamp();
                iterationElapTime = endTime - startTime;
                sumTime += iterationElapTime;
            }

            meanSeconds = (sumTime / numIter) * (1.0 / Stopwatch.Frequency); //(numIter * Stopwatch.Frequency)
            Console.Write(/*"\t" +*/ meanSeconds.ToString("F8") +" |");    
            handler.appendLastInRow(meanSeconds.ToString("F8"));                
        }
    }
    class TestObject : IDisposable
    {
        public int[] array;
        //
        public TestObject(): this(2684) // Task required 2^28 = 268435456
        {}
        public TestObject(uint vectorSize)
        {
            this.array = new int[vectorSize];
            for(int i = 0; i < this.array.Length; ++i)
            {
                this.array[i] = i;
            }
        }
        // public TestObject(int min, int max, uint vectorSize)
        // {
        //     Random rnd = new Random();
        //     this.array = new int[vectorSize];
        //     for(int i = 0; i < this.array.Length; ++i)
        //     {
        //         this.array[i] = rnd.Next(min, max);
        //     }
        // }

        protected virtual void Dispose(bool disposing){}
        public void Dispose()
        {
          Dispose(true);
          GC.SuppressFinalize(this);
        }
        ~TestObject( )
        {
          Dispose(false);
        }

        public void getArrayPrint()
        {
            for(int i = 0; i < this.array.Length; ++i)
            {
                Console.Write(this.array[i] + ", ");
            }   
        }
    }
}

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