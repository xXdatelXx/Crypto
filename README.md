# Crypto:
KPI course work

# Stack
- .NetCore WebApi
- EFCore
- EFCore functions
- Fluent validation
- NUnit
- CQRS
- PGSql
- C# telegram bot library 

# Project purpose:
- Crypto.Application: CQRS commands.
- Crypto.Data: DB context, models, repositories.
- Crypto.Function: Azure function.
- Crypto.Queries: CQRS queris.
- Crypto.Telegram: Telegram bot.
- Crypto.Tests: UnitTests

# Prerequisites
- .NET SDK 8.0 or higher
- PostgreSQL database
- Azure account for deploying Azure Functions

# User Secrets template:
```{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "TelegramBotToken": "",
  "CoinMarketCapApiKey": "",
  "ApiBaseAddress": ""
}
```

## Contributing
Feel free to submit issues or pull requests to improve the project.

## License
This project is licensed under the MIT License.

