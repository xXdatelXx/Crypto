using Crypto.Queries.Model;
using MediatR;

namespace Crypto.Queries.Queries.Wallet;

public record GetWalletQuery(string telegramId) : IRequest<WalletModel>;