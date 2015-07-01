using System;
using AspNetFeatureToggle;

namespace ExampleWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FeatureToggle.Initialize();

            this.featureD.Visible = FeatureToggle.Check("featureD");
        }
    }
}