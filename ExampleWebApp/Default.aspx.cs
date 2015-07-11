using System;
using AspNetFeatureToggle;

namespace ExampleWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FeatureToggle.Initialize();

            string userName = this.featureCTextBox.Text;
            this.featureCLabel.Text = FeatureToggle.IsEnabled("featureC", userName) ? "On" : "Off";

            this.featureD.Visible = FeatureToggle.IsEnabled("featureD");
        }
    }
}