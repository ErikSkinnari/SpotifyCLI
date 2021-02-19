using System;

namespace SpotifyCLI.Utilities {
    public class ConsoleOutputHandler : IOutputHandler {
        public void Output(string output) {
            Console.WriteLine(output);
        }
    }
}