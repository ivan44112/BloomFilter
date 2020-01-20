using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            AddItem("test");
            AddItem("test2");
            AddItem("test3");
            AddItem("test4");
            AddItem("test5");

            Console.WriteLine(PossiblyExists("whatever"));
            Console.WriteLine(PossiblyExists("test"));
            Console.WriteLine(PossiblyExists("test2"));
            Console.WriteLine(PossiblyExists("test3"));
            Console.WriteLine(PossiblyExists("test4"));
            Console.WriteLine(PossiblyExists("test5"));
            Console.WriteLine(PossiblyExists("test6"));
            Console.ReadKey();
        }

        private static byte[] _bloomFilter = new byte[1000];

        static void AddItem(string item)
        {
            int hash = Hash(item) & 0x7FFFFFFF; 
            byte bit = (byte)(1 << (hash & 7)); 
            _bloomFilter[hash % _bloomFilter.Length] |= bit;
        }

        static bool PossiblyExists(string item)
        {
            int hash = Hash(item) & 0x7FFFFFFF;
            byte bit = (byte)(1 << (hash & 7)); 
            return (_bloomFilter[hash % _bloomFilter.Length] & bit) != 0;
        }

        private static int Hash(string item)
        {
            int result = 17;
            for (int i = 0; i < item.Length; i++)
            {
                unchecked
                {
                    result *= item[i];
                }
            }
            return result;
        }
    }
}