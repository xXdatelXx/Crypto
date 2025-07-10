using MediatR;
using Crypto.Queries.Model.Get.Id;
using Crypto.Queries.Model;
using Crypto.Data.Interface;
using Crypto.Application.Requests.User.Extensions;

namespace Crypto.Application.Requests.User.Get.Id;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserResponse?> {
    public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.id, cancellationToken);
        return user?.MapToResponse();
    }
}

