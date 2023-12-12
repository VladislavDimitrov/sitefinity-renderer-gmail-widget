using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Renderer.Entities;
using Renderer.Models.GmailWidget;

namespace Renderer.ViewComponents
{
    [SitefinityWidget(Title = "Gmail Widget", Category = "Google")]
    public class GmailWidgetViewComponent : ViewComponent
    {
        private readonly IGmailWidgetModel model;

        public GmailWidgetViewComponent(IGmailWidgetModel model)
        {
            this.model = model;
        }

        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<GmailWidgetEntity> context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(context.Entity.ClientID) || string.IsNullOrEmpty(context.Entity.ClientSecret))
                //TODO: reosurce label for the error message
                return this.View("IncompleteConfiguration",
                    "Google client ID and client secret are missing. This widget will not render on the page. Please enter the client ID and client secret in the widget designer ");

            if (!model.IsConfigured())
                this.model.SetClientIdAndSecret(context.Entity.ClientID, context.Entity.ClientSecret);

            if (!this.model.UserIsAuthenticated())
                return this.View("UnauthenticatedUser");

            return this.View("AuthenticatedUser");
        }
    }
}
