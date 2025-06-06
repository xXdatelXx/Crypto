﻿namespace Crypto.Application.Model;

public class CurrencyDTO {
   public Guid Id { get; set; }
   public string Name { get; set; }
   public IEnumerable<string> Users { get; set; }
}