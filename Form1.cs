using FolderTracker.Util;

namespace FolderTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Folders folder = new Folders(textBox1.Text);




        }
    }
}