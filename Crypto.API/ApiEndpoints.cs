﻿namespace Crypto;

public static class ApiEndpoints {
   private const string ApiBase = "api";
    
   public static class Currencies
   {
      private const string Base = $"{ApiBase}/currencies";

      public const string Create = $"{Base}/{{name}}";
      public const string Update = $"{Base}";
      public const string Delete = $"{Base}/{{id}}";
   }
   
   public static class Users
   {
      private const string Base = $"{ApiBase}/users";

      public const string Create = Base;
      public const string Get = $"{Base}/{{id}}";
      public const string GetByTelegram = $"{Base}/{{telegramId}}";
      public const string Update = $"{Base}";
      public const string Delete = $"{Base}/{{id}}";
   }
   
   public static class GreedFear
   {
      private const string Base = $"{ApiBase}/greed-fear";
      public const string Get = Base;
   }
   
   public static class Prices {
      private const string Base = $"{ApiBase}/prices";

      public const string Get = $"{Base}/{{currency}}";
      public const string GetDifference = $"{Get}/difference{{time}}";
   }
   
   public static class Wallet {
      private const string Base = $"{ApiBase}/wallet{{telegramId}}";
      public const string Get = Base;
   }
}