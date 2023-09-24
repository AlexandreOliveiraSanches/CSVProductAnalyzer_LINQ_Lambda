using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using CSVProductAnalyzer_LINQ_Lambda.Entities;

namespace CSVProductAnalyzer_LINQ_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            Console.WriteLine("____________________________");
            Console.WriteLine();

            List<Product> list = new List<Product>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(','); 
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    list.Add(new Product(name, price));
                }
            }

            //Average of all products in list
            var avg = list.Select(p => p.Price).DefaultIfEmpty(0).Average();
            Console.WriteLine("Average price: $" + avg.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine();

            //Selection of product names below Average Price
            var names = list.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);
            Console.WriteLine("Selection of product names below Average Price:");
            foreach (string name in names) 
            {
                Console.WriteLine("- " + name);
            }
        }
    }
}