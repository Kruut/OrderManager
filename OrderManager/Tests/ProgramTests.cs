using NUnit.Framework;

namespace OrderManager.Tests
{
    [TestFixture]
    internal class ProgramTests
    {
        [Test]
        [TestCase("_inputDeliveryOrder=Tests\\SuccessfullRunData.txt", "_cityDistrict=2560493", "_firstDeliveryDateTime=29-10-2024 23:25:20", "Tests\\SuccessfullRunDataResult.txt")]
        public void TestProgramSuccessfulRun(string inputFile, string consoleString0, string consoleString1, string expectedOutputFile) 
        {
            var capturedStdOut = CapturedStdOut(() =>
            {
                RunApp(arguments: new string[] { inputFile, consoleString0, consoleString1 });
            });

            Assert.That(File.ReadAllText("sortedOrders.txt"), Is.EqualTo(File.ReadAllText(expectedOutputFile)));
        }

        void RunApp(string[]? arguments = default)
        {
            var entryPoint = typeof(Program).Assembly.EntryPoint!;
            entryPoint.Invoke(null, new object[] { arguments ?? Array.Empty<string>() });
        }

        string CapturedStdOut(Action callback)
        {
            TextWriter originalStdOut = Console.Out;

            using var newStdOut = new StringWriter();
            Console.SetOut(newStdOut);

            callback.Invoke();
            var capturedOutput = newStdOut.ToString();

            Console.SetOut(originalStdOut);

            return capturedOutput;
        }
    }
}
