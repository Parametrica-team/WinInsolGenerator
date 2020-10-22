using NUnit.Framework;
using System.Collections.Generic;
using WinInsolGenerator;

namespace WinInsolNUnitTests
{
    public class Tests
    {
        Program prog;

        [SetUp]
        public void Setup()
        {
            prog = new WinInsolGenerator.Program();
        }

        [Test]
        public void GetWindowCombinations_4Rooms()
        {
            var flat = new Flat()
            {
                TopWindows = 3,
                BottomWindows = 2,
                FType = "CL",
                Indexes = new List<int>() { 0, 1, 2, 3, 4 }
            };
            
            var result = flat.GetWindowCombinations();
            var res = string.Join('_', result);
            Assert.AreEqual(res, "0,1_0,2_0,3_0,4_1,2_1,3_1,4_2,3_2,4_3,4");
        }

        [Test]
        public void GetWindowCombinations_3Rooms()
        {
            var flat = new Flat()
            {
                TopWindows = 2,
                BottomWindows = 2,
                FType = "CL",
                Indexes = new List<int>() { 0, 1, 2, 3}
            };

            var result = flat.GetWindowCombinations();
            var res = string.Join('_', result);
            Assert.AreEqual(res, "0_1_2_3");
        }

        [Test]
        public void GetWindowCombinations_5Rooms()
        {
            var flat = new Flat()
            {
                TopWindows = 3,
                BottomWindows = 3,
                FType = "CL",
                Indexes = new List<int>() { 0, 1, 2, 3, 4, 5}
            };

            var result = flat.GetWindowCombinations();
            var res = string.Join('_', result);
            Assert.AreEqual(res, "0,1,2_0,1,3_0,1,4_0,1,5_0,2,3_0,2,4_0,2,5_0,3,4_0,3,5_0,4,5_1,2,3_1,2,4_1,2,5_1,3,4_1,3,5_1,4,5_2,3,4_2,3,5_2,4,5_3,4,5");
        }
    }
}