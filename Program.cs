using System;
using System.Numerics;
using System.Diagnostics;
using System.ComponentModel;


class Program
{
    static int length;
    static void Main(string[] args)
    {
        length = 10;
        TargetContainer boxOfLists = new TargetContainer(length, 1);
        Timer timer = new Timer();


        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", boxOfLists.listVShape[i].ToString("F0"));
        }

        Console.WriteLine("");
        Console.WriteLine("============");

        int[] resInserSort = new int[boxOfLists.listVShape.Length];
        Array.Copy(boxOfLists.listVShape, resInserSort, boxOfLists.listVShape.Length);
        timer.start();
        InsertionSort(resInserSort);
        timer.stop();
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", resInserSort[i].ToString("F0"));
        }
        Console.Write(" {0}", timer.result.ToString("F8"));
        Console.WriteLine("");
        Console.WriteLine("============");

        int[] resSelecSort = new int[boxOfLists.listVShape.Length];
        Array.Copy(boxOfLists.listVShape, resSelecSort, boxOfLists.listVShape.Length);
        SelectionSort(resSelecSort);
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", resSelecSort[i].ToString("F0"));
        }

        Console.WriteLine("");
        Console.WriteLine("============");

        int[] resHeapSort = new int[boxOfLists.listVShape.Length];
        Array.Copy(boxOfLists.listVShape, resHeapSort, boxOfLists.listVShape.Length);
        HeapSort(resHeapSort);
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", resHeapSort[i].ToString("F0"));
        }

        Console.WriteLine("");
        Console.WriteLine("============");

        int[] resCoctailSort = new int[boxOfLists.listVShape.Length];
        Array.Copy(boxOfLists.listVShape, resCoctailSort, boxOfLists.listVShape.Length);
        CocktailSort(resCoctailSort);
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", resCoctailSort[i].ToString("F0"));
        }


        // Console.WriteLine("");
        // Console.WriteLine("============");
        // int[] resQuickSortRec = new int[boxOfLists.listVShape.Length];
        // Array.Copy(boxOfLists.listVShape, resQuickSortRec, boxOfLists.listVShape.Length);

        // resQuickSortRec = QuickSRec(resQuickSortRec, resQuickSortRec[0], resQuickSortRec[length-1]);
        // for(int i = 0; i < length; i++)
        // {
        //     Console.Write("{0}, ", resQuickSortRec[i].ToString("F0"));
        // }

        // Console.WriteLine("");
        // Console.WriteLine("============");
        // int[] resQuickSortIte = QuickSIte(boxOfLists.listVShape);
        // for(int i = 0; i < length; i++)
        // {
        //     Console.Write("{0}, ", resQuickSortIte[i].ToString("F0"));
        // }

        Console.WriteLine("");
        Console.WriteLine("============");
        //Check
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", boxOfLists.listVShape[i].ToString("F0"));
        }
        Console.WriteLine("");
    }

//Sorting Algorythms
//Insertion Sort, Selection Sort, Heap Sort, Cocktail Sort
    //InsertionSort()
    static void InsertionSort (int[] t)
    {
        for (int i = 1; i < length; i++)
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

    static int[] QuickSRec(int[] t, int l, int p)
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
        if (l < j) t = QuickSRec(t, l, j); // sortujemy lewą część (jeśli jest)
        if (i < p) t = QuickSRec(t, i, p); // sortujemy prawą część (jeśli jest)
        return t;
    } /* qsort() */


    static int[] QuickSIte(int[] source)
    { 
        int[] t = new int[source.Length];
        Array.Copy(source, t, source.Length);

        int i, j, l, p;
        int sp;
        int[] stos_l = new int[t.Length],
        stos_p = new int[t.Length]; // przechowywanie żądań podziału
        sp=0; stos_l[sp] = 0; stos_p[sp] = t.Length -1; // rozpoczynamy od całej tablicy
        do
        {
            l = stos_l[sp];
            p = stos_p[sp];
            sp--; // pobieramy żądanie podziału
            do
            { 
                int x;
                i = l;
                j = p; 
                x=(int)t[(l+p)/2];
                do
                {
                    while (t[i] < x) i++;
                    while (x < t[j]) j--;
                    if (i <= j)
                    {
                        int buf = t[i];
                        t[i] = t[j];
                        t[j] = buf;
                        i++;
                        j--;
                    }
                } while (i <= j);
                if(i<p) 
                {
                    sp++;
                    stos_l[sp] = i;
                    stos_p[sp] = p; 
                } 
                p = j;
            } while(l<p);
        } while(sp >= 0); 
        return t;
    }

}

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