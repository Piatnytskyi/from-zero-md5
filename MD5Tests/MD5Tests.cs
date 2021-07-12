using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using System.Linq;

namespace MD5.Tests
{
    [TestClass()]
    public class MD5Tests
    {
        [TestMethod()]
        [DeploymentItem(@"Oracle_VM_VirtualBox_Extension_Pack-6.1.22.vbox-extpack", "files")]
        public void ComputeHashTest()
        {
            var mD5 = new MD5();

            Assert.AreEqual("D41D8CD98F00B204E9800998ECF8427E", mD5.ComputeHash(Encoding.ASCII.GetBytes("")).ToString());
            Assert.AreEqual("0CC175B9C0F1B6A831C399E269772661", mD5.ComputeHash(Encoding.ASCII.GetBytes("a")).ToString());
            Assert.AreEqual("900150983CD24FB0D6963F7D28E17F72", mD5.ComputeHash(Encoding.ASCII.GetBytes("abc")).ToString());
            Assert.AreEqual("F96B697D7CB7938D525A2F31AAF161D0", mD5.ComputeHash(Encoding.ASCII.GetBytes("message digest")).ToString());
            Assert.AreEqual("C3FCD3D76192E4007DFB496CCA67E13B", mD5.ComputeHash(Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwxyz")).ToString());
            Assert.AreEqual("D174AB98D277D9F5A5611C2C9F419D9F", mD5.ComputeHash(Encoding.ASCII.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")).ToString());
            Assert.AreEqual("57EDF4A22BE3C955AC49DA2E2107B67A", mD5.ComputeHash(Encoding.ASCII.GetBytes("12345678901234567890123456789012345678901234567890123456789012345678901234567890")).ToString());
            Assert.AreEqual("6B75536477315C3B420132982C263B82", mD5.ComputeHash(File.OpenRead(@"files\Oracle_VM_VirtualBox_Extension_Pack-6.1.22.vbox-extpack")).Result.ToString());
        }
    }
}