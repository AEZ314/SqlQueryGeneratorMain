using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SqlQueryGenerator;
using SqlQueryGenerator.Helpers;
using SqlQueryGenerator.Attributes;
using System;
using SqlQueryGenerator.Repository;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new SqlGen().ManualUpdate("sample_table", "ID = @id",
                new ISqlProperty[]
                {
                    new SqlProperty(null, "ConditionParameter1", "condit 1"),
                    new SqlProperty(null, "ConditionParameter2", "condit 2"),
                },
                new SqlProperty("C_col", "C_prop", "C_val"),
                new SqlProperty("A_col", "A_prop", "A_val"),
                new SqlProperty("B_col", "B_prop", "B_val"));

            //BenchmarkRunner.Run<BenchmarkWrapper>();
            //return;

            //var wrapper = new BenchmarkWrapper();
            //wrapper.Setup();

            //var result = wrapper.Automatic();

            //Console.WriteLine(result.QueryString);

            //var model = new Model()
            //{
            //    A = "AVal",
            //    B = 1,
            //    SubModel = new SubModel()
            //    {
            //        a = "a",
            //        b = 2,
            //    },
            //};

            //string sql = $@"SELECT COUNT(*) FROM {model.TBL()} 
            //               WHERE {model.COL("A")} = @Credentials_EmailAddress 
            //               AND   {model.COL("B")} = @Credentials_Password;";

            //Console.WriteLine(sql);

            //new GenericRepository(null).AutoInsert(model);

            var job = new Job()
            { 
                Name = "sample name",
                Description = "sample description",
            };

            new GenericRepository(null).AutoInsert(job);
        }
    }

    class Model
    {
        [SqlPropertyIgnore]
        public string A { get; set; }
        
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
            var result = gen.ManualInsert("sample_table",
                new SqlProperty("A_col", "A_prop", "A_val"),
                new SqlProperty("B_col", "B_prop", "B_val"),
                new SqlProperty("C_col", "C_prop", "C_val"));

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
