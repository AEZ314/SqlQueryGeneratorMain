using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SqlQueryGenerator;
using SqlQueryGenerator.Helpers;
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

            //var wrapper = new BenchmarkWrapper();
            //wrapper.Setup();

            //var result = wrapper.Automatic();

            //Console.WriteLine(result.QueryString);

            var model = new Model()
            {
                A = "AVal",
                B = 1,
                SubModel = new SubModel()
                {
                    a = "a",
                    b = 2,
                },
            };

            string sql = $@"SELECT COUNT(*) FROM {model.TBL(0)} 
                           WHERE {model.COL("A", 0)} = @Credentials_EmailAddress 
                           AND   {model.COL("B", 0)} = @Credentials_Password;";

        }
    }

    [SqlTableName("UserTable")]
    class Model
    {
        [SqlProperty("ACol")]
        public string A { get; set; }
        
        [SqlProperty("BCol")]
        public int B { get; set; }

        public SubModel SubModel { get; set; }
    }

    class SubModel
    {
        [SqlProperty("aCol")]
        public string a { get; set; }
        
        [SqlProperty("bCol")]
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
