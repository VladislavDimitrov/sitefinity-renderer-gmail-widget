# Sitefinity .NET Core Renderer - Gmail Widget
Demo project built using the new Sitefinity 15 .NET Core Renderer to showcase a Gmail Widget that can be used to send emails on behalf of your gmail account.

## Setup guide
1. Complete the setup of the Sitefinity CMS Server as described here: https://github.com/VladislavDimitrov/sitefinity-server-gmail-widget
2. Clone this repo locally.
3. Restore NuGet packages.
4. Build the solution.
5. Verify on which port does IIS Express run your Sitefinity CMS Server that you started in 1.
6. Edit the appsettings.json file located on the root folder of the Renderer project. You need to change the 'Url' poperty of: "Sitefinity": {"Url": "https://localhost:44325/",} and put the Sitefinity CMS Sever url that you verified in 5.
7. Make sure the Sitefinity CMS Server app is running.
8. Start the Renderer project from Visual Studio.

## Testing Instructions
1. Log in the Sitefinity backend from the .NET Core Renderer instance (for example: locahost:5001/sitefinity)
2. Go to Pages and Createa a new page.
3. Add any layout element.
4. Click on 'Add Widgets' in the layout element.
5. Locate the 'Gmail Widget' widget and select it.
6. Initially the widget should display 'Incomplete Configuration' message, this message appears only during page edit. To complete the configuration, open the widget designer and add the following values, ClientID: '882910652956-s0temek4rqecooimi06rnbds09fcc9b0.apps.googleusercontent.com', client secret: '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'
7. Save the changes in the designer and publish the page.
8. Browse the page on the frontend. The widget states that you should initiate an authorization with google in order to use its functionalities.
9. Click on the 'Log In' button.
10. A new browser tab appears for OAuth2 authentication. Complete the sigin process and grant the application the permissions it requests.
11. Once done, you will be redirected to a localhost:{randomPort} return Url with a message on the screen indicating you have obtained a token and you cn close this window.
12. Go back to the sitefinity page, now you should see the main interface of the Gmail Widget. Apart from the usual email fields, there is also a Logout button that would unauthenticate you from Google on click.
13. The widget supports multiselect of recipients implemented with Kendo Multiselect. These recipients are coming from your google contacts. **Important: **Make sure you have contacts that have email addresses added to them, otherwise the recipients multiselect might be empty. You can check that in your Google Contacts.
14. The Body of the email can be composed using the Kendo Editor.
15. Once you have composed the desired email you can Send It to your recipients.

If you have any questions, don't hesitate to reach out!