using Crypto.Application.Logic.Commands;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public record GetCurrencyQuery(string currency) : IRequest<CurrencyDTO>;
