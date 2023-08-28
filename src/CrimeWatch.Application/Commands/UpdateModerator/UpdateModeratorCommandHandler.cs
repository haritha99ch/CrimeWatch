using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.UpdateModerator;
internal class UpdateModeratorCommandHandler : IRequestHandler<UpdateModeratorCommand, Moderator>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public UpdateModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<Moderator> Handle(UpdateModeratorCommand request, CancellationToken cancellationToken)
    {
        GetModeratorByIdIncludingAll specification = new(request.Id);

        Moderator? moderator =
            await _moderatorRepository.GetByAsync(specification, cancellationToken)
            ?? throw new Exception("Moderator not found");

        moderator.User!.FirstName = request.FirstName;
        moderator.User!.LastName = request.LastName;
        moderator.User!.DateOfBirth = request.DateOfBirth;
        moderator.User!.PhoneNumber = request.PhoneNumber;
        moderator.User!.Gender = request.Gender;
        moderator.Account!.Email = request.Email;
        moderator.Account!.Password = request.Password;
        moderator.PoliceId = request.PoliceId;
        moderator.Province = request.Province;

        return await _moderatorRepository.UpdateAsync(moderator, cancellationToken);
    }
}
