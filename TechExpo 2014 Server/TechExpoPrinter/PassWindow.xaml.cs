#region

using System;
using TechExpo.Data.Models;

#endregion

namespace TechExpoPrinter
{
    /// <summary>
    ///     Interaction logic for PassWindow.xaml
    /// </summary>
    public partial class PassWindow
    {
        public PassWindow(Registrant registrant)
        {
            InitializeComponent();

            this.Left = -75000;

            if (registrant == null)
            {
                return;
            }
            _textBlockName.Text = String.Format("{0} {1}", registrant.FirstName.Trim(), registrant.LastName.Trim());
            _textBlockCompany.Text = registrant.Company;
            _textBlockJobTitle.Text = registrant.JobTitle;
        }
    }
}