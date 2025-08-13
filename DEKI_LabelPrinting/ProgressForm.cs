using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEKI_LabelPrinting
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 60;

            //Task.Run(() =>
            //{
            //    Thread.Sleep(5000); // Simulate long task
            //    progressBar1.Invoke((Action)(() =>
            //    {
            //        progressBar1.Style = ProgressBarStyle.Blocks; // reset to normal
            //        progressBar1.Value = 0;
            //    }));
            //});
        }
    }
}
