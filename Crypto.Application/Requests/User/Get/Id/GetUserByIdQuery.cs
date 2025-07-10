using MediatR;

namespace Crypto.Queries.Model.Get.Id;

public record GetUserByIdQuery(Guid id) : IRequest<UserResponse>;