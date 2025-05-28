using MediatR;
using Site.Application._shared;
using Site.Domain.Agents;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Create;

public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, OperationResult>
{
    private readonly IAgentRepository _repository;
    public CreateAgentCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        // اعتبارسنجی اولیه
        if (string.IsNullOrWhiteSpace(request.FullName))
            return OperationResult.Error("نام نمی‌تواند خالی باشد.");

        if (string.IsNullOrWhiteSpace(request.Email))
            return OperationResult.Error("ایمیل نمی‌تواند خالی باشد.");

        if (string.IsNullOrWhiteSpace(request.Description))
            return OperationResult.Error("توضیحات نمی‌تواند خالی باشد.");

        if (string.IsNullOrWhiteSpace(request.ImageName))
            return OperationResult.Error("عکس نمی‌تواند خالی باشد.");

        if (string.IsNullOrWhiteSpace(request.GithubLink))
            return OperationResult.Error("لینک گیتهاب نمی‌تواند خالی باشد.");

        if (string.IsNullOrWhiteSpace(request.Password))
            return OperationResult.Error("کلمه عبور نمی‌تواند خالی باشد.");


        var agent = Agent.Create(
            fullName: request.FullName,
            githubLink: request.GithubLink,
            imageName: request.ImageName,
            description: request.Description,
            email: request.Email,
            phoneNumber: request.PhoneNumber,
            resumeFileName: request.ResumeFileName,
            password: request.Password 
        );

        await _repository.AddAsync(agent);
        await _repository.SaveChangesAsync();
        return OperationResult.Success("کاربر با موفقیت ایجاد شد");
    }
}