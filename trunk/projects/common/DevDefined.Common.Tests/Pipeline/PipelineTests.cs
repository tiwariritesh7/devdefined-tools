﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevDefined.Common.Pipeline;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Pipeline
{
    [TestFixture]
    public class PipelineTests
    {
        [Test]
        public void AyendePipelineExample()
        {
            TrivialProcessPipline pipeline = new TrivialProcessPipline();
            pipeline.Execute();
        }
    }

    public class TrivialProcessPipline : Pipeline<IEnumerable<Process>, object>
    {
        public TrivialProcessPipline()
        {
            Register(new GetAllProcesses(),
                     new LimitByWorkingSetSize(),
                     new PrintProcessName());
        }
    }

    public class GetAllProcesses : IOperation<IEnumerable<Process>, object>
    {
        public IEnumerable<Process> Execute(IEnumerable<Process> input, object context)
        {
            return Process.GetProcesses();
        }
    }

    public class LimitByWorkingSetSize : IOperation<IEnumerable<Process>, object>
    {
        public IEnumerable<Process> Execute(IEnumerable<Process> input, object context)
        {
            int maxSizeBytes = 50 * 1024 * 1024;
            foreach (Process process in input)
            {
                if (process.WorkingSet64 > maxSizeBytes)
                    yield return process;
            }
        }
    }

    public class PrintProcessName : IOperation<IEnumerable<Process>, object>
    {
        public IEnumerable<Process> Execute(IEnumerable<Process> input, object context)
        {
            foreach (Process process in input)
            {
                System.Console.WriteLine(process.ProcessName);
            }

            return null;
        }
    }
}
