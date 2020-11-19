# SlackApi

Slack Home Events
- ScheduledHomePageBuild
  - This will publish a default homepage for each slack users
  - Can be use to reset a change you've made from SlackAppInteraction
- SlackAppIntegration
  - Subscribed to interaction events to the bot
  - After running ScheduleHomePageBuild, select an answer from the drop down select on the App home tab. The function will update the Home page view depending on which answer you select.
- JoinTeamWebhook
  - Subscribed to events chosen from App Management
  - I've chose join.team which would be the one to watch for new people
  - I've also chosen the update profile events, as this is easier to trigger for a test.



Enviroment is set up on https://alexincco.slack.com/
alex4@purplecubed.com / idontcare

To debug Slack locally follow this guide:
https://api.slack.com/tutorials/tunneling-with-ngrok

`ngrok http yourAzureFunctionPort`

The generated ngrok URL should be set up in from https://api.slack.com/apps

For the App Interactivty function the "Request URL" needs to be set in the "Interactivity & Shortcuts" section

e.g. http://f80b46bdbe34.ngrok.io/api/InteractionHandler

For the JoinTeamWebhook it needs to be set rom "Event Subscriptions" 

e.g. https://f80b46bdbe34.ngrok.io/api/SyncNewMember

