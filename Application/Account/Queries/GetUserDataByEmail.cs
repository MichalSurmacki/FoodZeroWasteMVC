using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Queries
{
    public class GetUserDataByEmail : IRequest<UserDataReadDto>
    {
        public string Email { get; set; }

        public GetUserDataByEmail(string email)
        {
            Email = email;
        }
    }

    public class GetUserDataByEmailHandler : IRequestHandler<GetUserDataByEmail, UserDataReadDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserDataByEmailHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<UserDataReadDto> Handle(GetUserDataByEmail request, CancellationToken cancellationToken)
        {
            var userData = _context.UserData.FirstOrDefault(u => u.Email.Equals(request.Email));

            return Task.FromResult(_mapper.Map<UserDataReadDto>(userData));
        }
    }
}
