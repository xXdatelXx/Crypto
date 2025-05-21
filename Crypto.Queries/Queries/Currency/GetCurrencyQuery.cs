using Crypto.Queris.Model;
using MediatR;

namespace Crypto.Application.Logic.Queries;

public record GetCurrencyQuery(string currency) : IRequest<CurrencyModel>;
