using Crypto.Application.Model;
using MediatR;

namespace Crypto.Application.Logic.Queries.Wallet;

public record GetWalletQuery(UserDTO user) : IRequest<WalletDTO>;