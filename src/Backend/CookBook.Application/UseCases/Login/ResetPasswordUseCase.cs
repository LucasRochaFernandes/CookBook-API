using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Communication.Requests;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Security.Cryptography;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Login;
public class ResetPasswordUseCase : IResetPasswordUseCase
{
    private readonly ICodeToPerformActionRepository _codeToPerformActionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncripter _passwordEncripter;

    public ResetPasswordUseCase(ICodeToPerformActionRepository codeToPerformActionRepository, IUserRepository userRepository, IPasswordEncripter passwordEncripter)
    {
        _codeToPerformActionRepository = codeToPerformActionRepository;
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(ResetYourPasswordRequest request)
    {
        var code = await _codeToPerformActionRepository.GetByCode(request.Code);
        if (code is null)
            throw new ValidationException(["Invalid Code"]);
        var user = await _userRepository.FindBy(usr => usr.Id.Equals(code.UserId), true);
        if (user is null || user.Email.Equals(request.Email) is false)
            throw new ValidationException(["Invalid Code"]);
        if (DateTime.Compare(code.CreatedAt.AddHours(1), DateTime.UtcNow) <= 0)
            throw new ValidationException(["Invalid Code"]);
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);
        _codeToPerformActionRepository.DeleteAllUserCodes(user.Id);
        await _codeToPerformActionRepository.Commit();
        _userRepository.Update(user);
        await _userRepository.Commit();
    }
}
