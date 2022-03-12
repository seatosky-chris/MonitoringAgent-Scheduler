# MonitoringAgent-Scheduler

This app is meant to be ran as a Timer Trigger Azure Function. It is simply used like a Cron job to twice a day call the Monitoring Agent.

Setup: Simply create a `local.settings.json` file using the template and fill in `MONITORING_AGENT_URL` with the full url of the Monitoring Agent azure function. Ensure the `returnType=email` get param is set. Deploy to an Azure Function using VS Code.

Cron schedule: `0 17,22 * * * 1-5`
The Azure function runs under UTC time so the hours 17 & 22 correspond to 9AM & 2PM PST. It will only run on days 1 through 5, so weekdays.

**Warning!** Be careful not to let VS Code overwrite the default `AzureWebJobsStorage` environment variable. When running locally this will be the storage emulator but for the live function it must be a connection string to blob storage. When the function is created this will be set correctly, but if you push settings in the future, it will try to overwrite this and the function will break.