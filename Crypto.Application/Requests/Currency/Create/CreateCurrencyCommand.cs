﻿using MediatR;

namespace Crypto.Application.Logic.Commands.Currency.Create;

public record CreateCurrencyCommand(string Name) : IRequest<Guid>;