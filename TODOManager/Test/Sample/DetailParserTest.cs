using System.Diagnostics;
using TODOManager.Presentation.ViewModels.Contents;

namespace Test.Sample
{
    [TestFixture]
    internal class DetailParserTest
    {
        static object[][] MainDetailTestData()
        {
            return new object[][]
            {
                new object[]{ "test1\n>test2\n>test3\n>test4" , "test1\n"},
            };
        }

        [TestCaseSource(nameof(MainDetailTestData))]
        public void CheckMainDetail(string input, string mainDetail)
        {
            DetailParser detailParser = new DetailParser(input);
            ParseRootData output = detailParser.Execute();

            Assert.AreEqual(output.detail, mainDetail);
            //Assert.AreEqual(output, collect);
            //foreach (string data in output)
            //{
            //    TestContext.WriteLine(data);
            //    TestContext.WriteLine("\n");
            //    TestContext.WriteLine("--");
            //}
        }

        static object[][] ChildTitleTestData()
        {
            return new object[][]
            {
                new object[]{ "detailroot\n>title2\ndetail2\n>>title3\ndetail3\n>>>title4" , new string[] {">title2" } },
            };
        }

        [TestCaseSource(nameof(ChildTitleTestData))]
        public void CheckChildTitle(string input, string[] childTitles)
        {
            DetailParser detailParser = new DetailParser(input);
            ParseRootData output = detailParser.Execute();

            Assert.AreEqual(output.childs[0].titleData.data, childTitles[0]);
            TestContext.WriteLine(output.childs[0].detail);
            TestContext.WriteLine(output.childs[0].childs[0].titleData.data);
            TestContext.WriteLine(output.childs[0].childs[0].detail);
            TestContext.WriteLine(output.childs[0].childs[0].childs[0].titleData.data);
        }
        static object[][] ChildTitleTestData2()
        {
            return new object[][]
            {
                new object[]{
                    "detailroot\n>title2\ndetail2\n>>title2-1\ndetail2-1\n>title3\ndetail3\n>>title3-1\ndetail3-1" , new string[] {">title2" } },
            };
        }

        [TestCaseSource(nameof(ChildTitleTestData2))]
        public void CheckChildTitle2(string input, string[] childTitles)
        {
            DetailParser detailParser = new DetailParser(input);
            ParseRootData output = detailParser.Execute();

            //Assert.AreEqual(output.childs[0].titleData.data, childTitles[0]);
            TestContext.WriteLine(output.childs[0].titleData.data);
            TestContext.WriteLine(output.childs[0].detail);
            TestContext.WriteLine(output.childs[0].childs[0].titleData.data);
            TestContext.WriteLine(output.childs[0].childs[0].detail);;

            TestContext.WriteLine(output.childs[1].titleData.data);
            TestContext.WriteLine(output.childs[1].detail);
            TestContext.WriteLine(output.childs[1].childs[0].titleData.data);
            TestContext.WriteLine(output.childs[1].childs[0].detail); ;

        }


    }
}