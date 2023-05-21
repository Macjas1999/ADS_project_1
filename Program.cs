using System;
using System.Numerics;
using System.Diagnostics;


class Program
{
    static void Main(string[] args)
    {
        TargetContainer boxOfLists = new TargetContainer(10, 1);
        // Console.WriteLine(container.listVShape.ToString());

        boxOfLists.listVShape.ForEach(Print);
        Console.WriteLine("============");
        for(int i = 0; i < 10; i++)
        {
            Console.Write("{0}, ", boxOfLists.listVShape.ElementAt(i).ToString("F0"));
        }
        Console.WriteLine("");
    }

    static void Print(int i)
    {
        Console.WriteLine(i.ToString());
    }

}

class TargetContainer
{
    public List<int> listStatic;
    public List<int> listRand;
    public List<int> listAsc;
    public List<int> listDesc;
    public List<int> listVShape;

    public TargetContainer(int length, int staticVal)
    {
        this.listStatic = new List<int>();
        for (int i = 0; i < length; i++)
        {
            this.listStatic.Add(staticVal);
        }

        this.listAsc = new List<int>();
        for (int i = 0; i < length; i++)
        {
            this.listStatic.Add(i);
        }

        this.listDesc = new List<int>();
        for (int i = 0; i < length; i++)
        {
            this.listStatic.Add(i);
        }

        this.listRand = new List<int>();
        var random = new Random();
        for (int i = 0; i < length; i++)
        {
            this.listStatic.Add(random.Next(sizeof(int)-1));
        }

        this.listVShape = new List<int>();

        for(int i = 0; i < length; i++)
        {
            this.listVShape.Add(0);
        }
        // int iUp = 1;
        // int iDown = 0;
        // for(int i = 0; i < length; i++)
        // {
        //     if(i == 0 || i%2 == 0)
        //     {
        //         listVShape.Insert((length/2)-iDown, i);
        //         iDown++;
        //     }
        //     else if(i%2 != 0)
        //     {
        //         listVShape.Insert((length/2)+iUp, i);
        //         iUp++;
        //     }
        // }
        int delta = 0;
        for(int i = length/2; i > 0; i--)
        {
            this.listVShape.Insert(delta++, i);
        }
        for (int i = 0; i < length/2; i++)
        {
            this.listVShape.Insert(delta++, i);
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

// switch (mode)
//         {
//             case 's':
//                 for (int i = 0; i < length; i++)
//                 {
//                     this.list.Add(staticVal);
//                 }
//             break;

//             case 'd':
//                 for (int i = 0; i < length; i++)
//                 {
//                     this.list.Add(random.Next());
//                 }
//             break;

            
//             default:
//                 this.list.Add(0);
//                 break;
//         }