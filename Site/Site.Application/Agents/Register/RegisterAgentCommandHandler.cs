using MediatR;
using Microsoft.AspNetCore.Identity;
using Site.Application._shared;
using Site.Domain._shared;
using Site.Domain.Agents;
using Site.Domain.Agents.ValueObjects;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Register;

public class RegisterAgentCommandHandler : IRequestHandler<RegisterAgentCommand, OperationResult>
{
    private readonly IAgentRepository _repository;

    public RegisterAgentCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(RegisterAgentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FullName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return OperationResult.Error("همه فیلدها الزامی هستند.");
        }

        // بررسی موجود بودن ایمیل
        var existingEmail = await _repository.GetAgentByEmail(request.Email);

        if (existingEmail is not null)
            return OperationResult.Error("این ایمیل قبلاً ثبت ‌نام شده است.");

        // تبدیل شماره به ValueObject
        AgentPhoneNumber phoneNumber;
        try
        {
            phoneNumber = new AgentPhoneNumber(request.PhoneNumber);
        }
        catch (Exception ex)
        {
            return OperationResult.Error("شماره تماس نامعتبر است.");
        }

        // بررسی موجود بودن شماره تماس (بر اساس مقدار واقعی)
        var phoneExists = _repository.PhoneNumberExist(phoneNumber);

        if (phoneExists)
            return OperationResult.Error("شماره تماس قبلاً استفاده شده است.");

        try
        {
            // ساخت Agent
            var agent = Agent.Create(
                fullName: request.FullName,
                email: request.Email,
                password: request.Password,
                phoneNumber: phoneNumber
            );

            await _repository.AddAsync(agent);
            await _repository.SaveChangesAsync();

            return OperationResult.Success("ثبت ‌نام با موفقیت انجام شد.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error($"{e}");
        }

    }
}