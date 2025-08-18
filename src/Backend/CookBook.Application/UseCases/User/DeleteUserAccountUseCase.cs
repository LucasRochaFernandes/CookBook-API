using CookBook.Application.UseCases.User.Interfaces;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.Storage;

namespace CookBook.Application.UseCases.User;
public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IUserRepository _userRepository;

    public DeleteUserAccountUseCase(IBlobStorageService lobStorageService, IUserRepository userRepository)
    {
        _blobStorageService = lobStorageService;
        _userRepository = userRepository;
    }

    public async Task Execute(Guid userId)
    {
        await _blobStorageService.DeleteContainer(userId);
        await _userRepository.Delete(userId);
        await _userRepository.Commit();
    }
}
