using NUnit.Framework;
using System.Collections.Generic;
using WinInsolGenerator;
using System.Linq;

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
            
            flat.SetWindowCombinations();
            var res = string.Join('_', flat.WindowCombinations);
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

            flat.SetWindowCombinations();
            var res = string.Join('_', flat.WindowCombinations);
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

            flat.SetWindowCombinations();
            var res = string.Join('_', flat.WindowCombinations);
            Assert.AreEqual(res, "0,1,2_0,1,3_0,1,4_0,1,5_0,2,3_0,2,4_0,2,5_0,3,4_0,3,5_0,4,5_1,2,3_1,2,4_1,2,5_1,3,4_1,3,5_1,4,5_2,3,4_2,3,5_2,4,5_3,4,5");
        }

        [TestCase(5, "CL_1_1,MD_0_1,MD_0_1,MD_0_1,CR_0_1,llu,MU_1_0,CR_1_0")]
        [TestCase(13, "CL_1_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,MD_0_1,CR_1_1,MU_2_0,MU_1_0,MU_1_0,llu,MU_1_0,MU_2_0,MU_1_0")]
        public void GetStepsFromCode(int res, string code)
        {
            Assert.AreEqual(res, Program.GetStepsFromCode(code));
        }

        [Test]
        public void SetCombinationsBool_3Rooms()
        {
            var flat = new Flat("CR_3_1");
            flat.SetWindowCombinationsBits();
            //var test = string.Join("",flat.WindowCombinationsBool[0].Select();
            Assert.IsTrue(flat.WindowCombinationsBits[0][0]);
        }

        [Test]
        public void SetCombinationsBool_4Rooms()
        {
            var flat = new Flat("CR_4_1");
            flat.SetWindowCombinationsBits();
            //var test = string.Join("",flat.WindowCombinationsBool[0].Select();
            Assert.AreEqual(flat.WindowCombinationsBits[0].ToBitString(), "11000");
        }
    }
}