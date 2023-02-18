using CustomForms;

namespace VaporExperiment
{
    public partial class Form1 : CustomForm
    {
        public Form1()
        {
            InitializeComponent();

            titlebar.Owner = this;
        }
    }
}
