using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace PALBBR.Reports
{
    public partial class TestXtraReport : XtraReport
    {
        public TestXtraReport()
        {
            InitializeComponent();
        }

        public void Initialize(TestReport report)
        {
            if (report == null) return;

            objectDataSource1.DataSource = report;
        }
    }

    public class TestReport
    {
        public string CustomerNumber { get; set; }
        public string BillNumber { get; set; }
        public DateTime Created { get; set; }
        public List<TestReportItem> Items { get; set; }

        public TestReport()
        {
            Items = new List<TestReportItem>();
        }
    }

    public class TestReportItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
