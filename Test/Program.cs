using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SqlQueryGenerator;
using SqlQueryGenerator.Attributes;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<BenchmarkWrapper>();
            //return;

            var wrapper = new BenchmarkWrapper();
            wrapper.Setup();

            var result = wrapper.Automatic();

            Console.WriteLine(result.QueryString);
        }
    }

    [SqlTableName(2, TableName = "Users")]
    class Model
    {
        [SqlProperty(2, ColumnName = "Col_A 2")]
        [SqlProperty(ColumnName = "Col_A")]
        public string A { get; set; }
        
        [SqlProperty(2, ColumnName = "Col_B 2")]
        [SqlProperty(ColumnName = "Col_B")]
        public int B { get; set; }

        [SqlProperty()]
        public SubModel SubModel { get; set; }
    }

    class SubModel
    {
        [SqlProperty(2, ColumnName = "Col_a 2")]
        [SqlProperty(ColumnName = "Col_a")]
        public string a { get; set; }
        
        [SqlProperty(2, ColumnName = "Col_b 2")]
        [SqlProperty(ColumnName = "Col_b")]
        public int b { get; set; }

    }

    [ThreadingDiagnoser]
    [MemoryDiagnoser]
    public class BenchmarkWrapper
    {
        Model model;
        SqlGen gen;

        [GlobalSetup]
        public void Setup()
        {
            model = new Model()
            {
                A = "A_Val",
                B = 1,

                SubModel = new SubModel()
                {
                    a = "a_Val",
                    b = 2,
                },
            };

            gen = new SqlGen(model);
        }

        //[Benchmark]
        public SqlParameter Manual()
        {
            var result = gen.ManualInsert<SqlProperty>("sample_table", ("A", "a", ""), ("B", "b", ""), ("C", "c", ""));
            return result;
        }

        [Benchmark]
        public SqlParameter Automatic()
        {
            var result = gen.AutoInsert(2);
            return result;
        }
    }
}
