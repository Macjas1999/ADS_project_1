using System;
using System.Numerics;
using System.Diagnostics;
using System.ComponentModel;


class Program
{
    // static int length;
    static void Main(string[] args)
    {
        testsDataOutput();

        testQuicksortF();

    }
//Run functions
//Run task I II
    static void testsDataOutput()
    {
        CsvHandler handler = new CsvHandler("output.csv");

        string[] algorythms = {"Insertion S", "Selection S", "Heap S", "Cocktail S"};
        Console.WriteLine("Length ; Alg ; Random ; Ascending ; Descending ; Static ; VShape");
        handler.appendLastInRow("Length ; Alg ; Random ; Ascending ; Descending ; Static ; VShape");
        
        for (int length = 50000; length <= 150000; length += 5000)
        {
            double[,] fourTestsResults = fourTests(length);
            for (int i = 0; i < 4; i++)
            {
                Console.Write("{0}|", length);
                handler.appendNext(length.ToString("F0"));
                Console.Write("{0}|", algorythms[i]);
                handler.appendNext(algorythms[i]);
                for (int j = 0; j < 5; j++)
                {
                    if (j == 4)
                    {
                        Console.WriteLine("{0}", fourTestsResults[i,j].ToString("F8"));
                        handler.appendLastInRow(fourTestsResults[i,j].ToString("F8"));

                    }
                    else
                    {
                        Console.Write("{0} |", fourTestsResults[i,j].ToString("F8"));
                        handler.appendNext(fourTestsResults[i,j].ToString("F8"));
                    }
                }
            }
        }
    }
    static double[,] fourTests(int length)
    {
        TargetContainer boxOfLists = new TargetContainer(length, 1);
        Timer timer = new Timer();
        double[,] results = new double [4, 5];

        int[] bufferRandom = new int[boxOfLists.listRand.Length];
        int[] bufferAscending = new int[boxOfLists.listAsc.Length];
        int[] bufferDescending = new int[boxOfLists.listDesc.Length];
        int[] bufferStatic = new int[boxOfLists.listStatic.Length];
        int[] bufferVShape = new int[boxOfLists.listVShape.Length];

        Array.Copy(boxOfLists.listRand, bufferRandom, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listAsc, bufferAscending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listDesc, bufferDescending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listStatic, bufferStatic, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listVShape, bufferVShape, boxOfLists.listVShape.Length);

        timer.start();
        InsertionSort(bufferRandom);
        timer.stop();
        results[0, 0] = timer.result;
        timer.set();
        timer.start();
        InsertionSort(bufferAscending);
        timer.stop();
        results[0, 1] = timer.result;
        timer.set();
        timer.start();
        InsertionSort(bufferDescending);
        timer.stop();
        results[0, 2] = timer.result;
        timer.set();
        timer.start();
        InsertionSort(bufferStatic);
        timer.stop();
        results[0, 3] = timer.result;
        timer.set();
        timer.start();
        InsertionSort(bufferVShape);
        timer.stop();
        results[0, 4] = timer.result;
        timer.set();                

        Array.Copy(boxOfLists.listRand, bufferRandom, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listAsc, bufferAscending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listDesc, bufferDescending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listStatic, bufferStatic, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listVShape, bufferVShape, boxOfLists.listVShape.Length);

        timer.start();
        SelectionSort(bufferRandom);
        timer.stop();
        results[1, 0] = timer.result;
        timer.set();
        timer.start();
        SelectionSort(bufferAscending);
        timer.stop();
        results[1, 1] = timer.result;
        timer.set();
        timer.start();
        SelectionSort(bufferDescending);
        timer.stop();
        results[1, 2] = timer.result;
        timer.set();
        timer.start();
        SelectionSort(bufferStatic);
        timer.stop();
        results[1, 3] = timer.result;
        timer.set();
        timer.start();
        SelectionSort(bufferVShape);
        timer.stop();
        results[1, 4] = timer.result;
        timer.set(); 

        Array.Copy(boxOfLists.listRand, bufferRandom, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listAsc, bufferAscending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listDesc, bufferDescending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listStatic, bufferStatic, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listVShape, bufferVShape, boxOfLists.listVShape.Length);

        timer.start();
        HeapSort(bufferRandom);
        timer.stop();
        results[2, 0] = timer.result;
        timer.set();
        timer.start();
        HeapSort(bufferAscending);
        timer.stop();
        results[2, 1] = timer.result;
        timer.set();
        timer.start();
        HeapSort(bufferDescending);
        timer.stop();
        results[2, 2] = timer.result;
        timer.set();
        timer.start();
        HeapSort(bufferStatic);
        timer.stop();
        results[2, 3] = timer.result;
        timer.set();
        timer.start();
        HeapSort(bufferVShape);
        timer.stop();
        results[3, 4] = timer.result;
        timer.set();       

        Array.Copy(boxOfLists.listRand, bufferRandom, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listAsc, bufferAscending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listDesc, bufferDescending, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listStatic, bufferStatic, boxOfLists.listVShape.Length);
        Array.Copy(boxOfLists.listVShape, bufferVShape, boxOfLists.listVShape.Length);

        timer.start();
        CocktailSort(bufferRandom);
        timer.stop();
        results[3, 0] = timer.result;
        timer.set();
        timer.start();
        CocktailSort(bufferAscending);
        timer.stop();
        results[3, 1] = timer.result;
        timer.set();
        timer.start();
        CocktailSort(bufferDescending);
        timer.stop();
        results[3, 2] = timer.result;
        timer.set();
        timer.start();
        CocktailSort(bufferStatic);
        timer.stop();
        results[3, 3] = timer.result;
        timer.set();
        timer.start();
        CocktailSort(bufferVShape);
        timer.stop();
        results[3, 4] = timer.result;
        timer.set();       

        return results;
    }
    
//Run task III
    static void testQuicksortF()
    {
        CsvHandler handler = new CsvHandler("outputQ.csv");
        handler.appendLastInRow("Length ; Quicksort Recurrent ; Quicksort Iterative");

        Timer timer = new Timer();
        //double[] result = new double[];

        for (int length = 10000000; length <= 75000000; length += 5000000)
        {
            TargetContainer boxOfLists = new TargetContainer(length, 1);

            int[] bufferRandom = new int[boxOfLists.listRand.Length];
            Array.Copy(boxOfLists.listRand, bufferRandom, boxOfLists.listVShape.Length);

            handler.appendNext(length.ToString("F0"));

            timer.start();
            // QuickSortRe(bufferRandom, bufferRandom[0], bufferRandom[bufferRandom.Length-1]);
            QuickSortRe(bufferRandom, 0, bufferRandom.Length-1);
            timer.stop();
            handler.appendNext(timer.result.ToString("F8"));
            timer.set();

            Array.Copy(boxOfLists.listRand, bufferRandom, boxOfLists.listVShape.Length);

            timer.start();
            QuickSortIt(bufferRandom);
            timer.stop();
            handler.appendLastInRow(timer.result.ToString("F8"));
            timer.set();  
        }
    }


//Sorting Algorythms
//
//Task I II
//
    //InsertionSort()
    static void InsertionSort (int[] t)
    {
        for (int i = 1; i < t.Length; i++)
        {
            int j = i;                     // >i are sorted
            int Buf = t[j];           

            while ((j > 0) && (t[(j - 1)] > Buf))
            {
                t[j] = t[(j - 1)];  // Moving the element
                j--;
            }
        t[j] = Buf; // destination
        }
    }

    // Heapsort
    static void Heapify(int[] t, uint left, uint right)
    { // procedura budowania/naprawiania kopca
        uint i = left,
        j = 2 * i + 1;
        int buf = t[i]; // ojciec
        while(j <= right) // przesiewamy do dna stogu
        {
            if(j < right) // wybieramy większego syna
            if(t[j] < t[j+1])j++;
            if(buf >= t[j])break;
            t[i] = t[j];
            i = j;
            j = 2 * i + 1; // przechodzimy do dzieci syna
        }
        t[i] = buf;
    }

    static void HeapSort(int[] t)
    {
        uint left = ((uint)t.Length / 2),
        right = (uint)t.Length - 1;
        while (left > 0) // budujemy kopiec idąc od połowy tablicy
        {
            left--;
            Heapify(t, left, right);
        }
        while (right > 0) // rozbieramy kopiec
        {
            int buf = t[left];
            t[left] = t[right];
            t[right] = buf; // największy element
            right--; // kopiec jest mniejszy
            Heapify(t, left, right); // ale trzeba go naprawić
        }
    }

    //Selection Sort
    static void SelectionSort(int[] t)
    {
        uint k;
        for (uint i = 0; i < (t.Length - 1); i++)
        {
            int Buf = t[i]; // bierzemy i-ty element
            k = i; // i jego indeks
            for (uint j = i + 1; j < t.Length; j++)
            if (t[j] < Buf) // szukamy najmniejszego z prawej
            {
                k = j;
                Buf = t[j];
            }
            t[k] = t[i]; // zamieniamy i-ty z k-tym
            t[i] = Buf;
        }
    }

    //Coctail Sort
    static void CocktailSort(int[] t)
    {
        int Left = 1, Right = t.Length - 1, k = t.Length-1;
        do
        {
            for(int j = Right; j >= Left; j--) // przesiewanie od dołu
            if(t[j - 1] > t[j])
            {
                int Buf=t[j-1]; t[j-1]=t[j]; t[j]=Buf;
                k = j; // zamiana elementów i zapamiętanie indeksu
            }
            Left = k + 1; // zacieśnienie lewej granicy
            for(int j = Left; j <= Right; j++) // przesiewanie od góry
            if(t[j - 1] > t[j])
            {
                int Buf=t[j-1]; t[j-1]=t[j]; t[j]=Buf;
                k = j; // zamiana elementów i zapamiętanie indeksu
            }
            Right = k - 1; // zacieśnienie prawej granicy
        } while(Left <= Right);
    }
//
//Task III
//
    static void QuickSortRe(int[] t, int l, int p)
    {
        int i, j, x;
        i = l;
        j = p;
        x = t[(l+p)/2]; // (pseudo)mediana
        do
        {
            while (t[i] < x) i++; // przesuwamy indeksy z lewej
            while (x < t[j]) j--; // przesuwamy indeksy z prawej
            if (i <= j) // jeśli nie minęliśmy się indeksami (koniec kroku)
            { // zamieniamy elementy
                int buf = t[i]; t[i] = t[j]; t[j] = buf;
                i++; j--;
            }
        }
        while (i <= j);
        if (l < j) QuickSortRe(t, l, j); // sortujemy lewą część (jeśli jest)
        if (i < p) QuickSortRe(t, i, p); // sortujemy prawą część (jeśli jest)
    }

//iteracyjna
    static void QuickSortIt(int[] t)
    {
        int i, j, l, p;
        // Two separate heaps
        Stack<int> heap_l = new Stack<int>();
        Stack<int> heap_p = new Stack<int>(); 

        heap_l.Push(0);
        heap_p.Push(t.Length - 1);

        while (heap_l.Count > 0)
        {
            l = heap_l.Pop();
            p = heap_p.Pop();

            do
            {
                int x;
                i = l;
                j = p;
                x = t[(l + p) / 2];

                do
                {
                    while (t[i] < x)
                        i++;
                    while (x < t[j])
                        j--;

                    if (i <= j)
                    {
                        int buf = t[i];
                        t[i] = t[j];
                        t[j] = buf;
                        i++;
                        j--;
                    }
                } while (i <= j);

                if (i < p)
                {
                    heap_l.Push(i);
                    heap_p.Push(p);
                }

                p = j;
            } while (l < p);
        }
    }
}
//
// Testers
//
class Timer
{
    public long startTimestamp;
    public long endTimestamp;
    public double result;

    public Timer()
    {
        this.startTimestamp = 0;
        this.endTimestamp = 0;
        this.result = 0;
    }
    public void start()
    {
        this.startTimestamp = Stopwatch.GetTimestamp();
    }
    public void stop()
    {
        this.endTimestamp = Stopwatch.GetTimestamp();
        this.result = this.endTimestamp - this.startTimestamp;
        this.result = this.result * (1.0 / Stopwatch.Frequency);
    }
    public void set()
    {
        this.startTimestamp = 0;
        this.endTimestamp = 0;
        this.result = 0;
    }
}
//
//Data creator
//
class TargetContainer
{
    public int[] listStatic;
    public int[] listRand;
    public int[] listAsc;
    public int[] listDesc;
    public int[] listVShape;

    public TargetContainer(int length, int staticVal)
    {
        this.listStatic = new int[length];
        for (int i = 0; i < length; i++)
        {
            this.listStatic[i] = staticVal;
        }

        this.listAsc = new int[length];
        for (int i = 0; i < length; i++)
        {
            this.listAsc[i] = i;
        }

        this.listDesc = new int[length];
        for (int i = 0; i < length; i++)
        {
            this.listDesc[i] = length - i;
        }

        this.listRand = new int[length];
        var random = new Random();
        for (int i = 0; i < length; i++)
        {
            this.listRand[i] = (int)random.Next(sizeof(int)-1);
        }

        this.listVShape = new int[length];

        int delta = 0;
        for(int i = length/2; i > 0; i--)
        {
            this.listVShape[delta++] = i;
        }
        for (int i = 0; i < length/2; i++)
        {
            this.listVShape[delta++] = i;
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
            using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
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
            writer.Write(";");
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