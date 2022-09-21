//using AutoMapper;
//using GoBarber.Application.Contracts;
//using GoBarber.Application.Dtos;
//using GoBarber.Application.Helpers;
//using GoBarber.Application.Helpers.Interfaces;
//using GoBarber.Data.Interfaces;
//using GoBarber.Domain;

//namespace GoBarber.Application
//{
//  public class SessionService : ISessionService
//  {
//    private readonly IToken _token;
//    private readonly IGenericRepository<User> _genericRepository;
//    private readonly IMapper _mapper;

//    public SessionService(IMapper mapper, IGenericRepository<User> genericRepository, IToken token)
//    {
//      _mapper = mapper;
//      _genericRepository = genericRepository;
//      _token = token;
//    }

//    public async Task<UserDtoOut> Auth(AuthDtoIn userAuth)
//    {
//      var userForVerification = (await _genericRepository.Find(b=>b.Email == userAuth.Email)).FirstOrDefault();
//      if (userForVerification == null || !Password.VerifyPass(userAuth.Password, userForVerification.Password)) throw new Exception("Incorrect Email/Pass combination.");
//      var result = await _token.GenerateToken(_mapper.Map<UserDtoOut>(userForVerification));
//      return result;
//    }
//  }
//}
