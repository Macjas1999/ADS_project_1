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

        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", boxOfLists.listVShape[i].ToString("F0"));
        }
        Console.WriteLine("");

        //InsertionSort(boxOfLists.listVShape);
        Console.WriteLine("============");
        int[] resInserSort = InsertionSort(boxOfLists.listVShape);
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", resInserSort[i].ToString("F0"));
        }
        Console.WriteLine("");
        Console.WriteLine("============");
        int[] resQuickSortRec = new int[boxOfLists.listVShape.Length];
        Array.Copy(boxOfLists.listVShape, resQuickSortRec, boxOfLists.listVShape.Length);

        resQuickSortRec = QuickSRec(resQuickSortRec, resQuickSortRec[0], resQuickSortRec[length-1]);
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", resQuickSortRec[i].ToString("F0"));
        }
        Console.WriteLine("");
        Console.WriteLine("============");
        // int[] temp = new int[boxOfLists.listVShape.Length];
        // for (int iter = 0; iter < length; iter++)
        // {
        //     temp[iter] = (int)boxOfLists.listVShape[iter];
        // }
        int[] resQuickSortIte = QuickSIte(boxOfLists.listVShape);
        for(int i = 0; i < length; i++)
        {
            Console.Write("{0}, ", resQuickSortIte[i].ToString("F0"));
        }
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

    //InsertionSort()
    static int[] InsertionSort (int[] source)
    {
        int[] t = new int[source.Length];
        for (int inx = 0; inx < source.Length; inx++)
        {
            t[inx] = source[inx];
        }
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
        return t;
    }

    //Quicksort recurent
    // static void QuickSRec(uint[] t, int l, int p)
    // {
    //     // if (l >= p)
    //     //     return;

    //     uint x;
    //     int i = l;
    //     int j = p;

    //     x = t[(l+p)/2];
    //     // Body
    //     do
    //     {
    //         while (t[i] < x) i++; 
    //         while (x < t[j]) j--;
    //         if (i <= j) 
    //         {
    //             uint buf = t[i];
    //             t[i] = t[j];
    //             t[j] = buf;
    //             i++;
    //             j--;
    //         }
    //     }while (i <= j);


    //     if (l < j) QuickSRec(t, l, j);
    //     if (i < p) QuickSRec(t, i, p);
    // }
    // static void QuickSRec(int[] t, int l, int r)
    // {   
    //     if (l >= r)
    //         return;

    //     int i = l - 1;
    //     int j = r + 1;
    //     int pivot = t[(l + r) / 2];

    //     while (true)
    //     {
    //         do
    //         {
    //             i++;
    //         } while (t[i] < pivot);

    //         do
    //         {
    //             j--;
    //         } while (t[j] > pivot);

    //         if (i >= j)
    //             break;

    //         int temp = t[i];
    //         t[i] = t[j];
    //         t[j] = temp;
    //     }

    //     QuickSRec(t, l, j);
    //     QuickSRec(t, j + 1, r);
    // }
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


        // uint[] t = new uint[source.Count];
        // for (int iter = 0; iter < source.Count; iter++)
        // {
        //     t[iter] = source[iter];
        // }