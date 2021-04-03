using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.Presentation.Models
{
    public class GroupedTransaction
    {
        public string Key { get; set; }
        public int Sum { get; set; }

        public double SumDouble { get => (double)Sum / 100; }
    }
}
