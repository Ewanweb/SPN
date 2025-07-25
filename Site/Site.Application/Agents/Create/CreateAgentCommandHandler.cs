﻿using MediatR;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Domain.Agents;
using Site.Domain.Agents.ValueObjects;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Create;

public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, OperationResult>
{
    private readonly IAgentRepository _repository;
    private readonly IFileService _fileService;
    public CreateAgentCommandHandler(IAgentRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        // اعتبارسنجی اولیه
        if (string.IsNullOrWhiteSpace(request.FullName))
            return OperationResult.Error("نام نمی‌تواند خالی باشد.");

        if (string.IsNullOrWhiteSpace(request.Email))
            return OperationResult.Error("ایمیل نمی‌تواند خالی باشد.");


        if (string.IsNullOrWhiteSpace(request.Password))
            return OperationResult.Error("کلمه عبور نمی‌تواند خالی باشد.");

        try
        {
            AgentPhoneNumber phoneNumber = new AgentPhoneNumber(request.PhoneNumber);

            var agent = Agent.Create(
                fullName: request.FullName,
                email: request.Email,
                password: request.Password,
                phoneNumber: phoneNumber
            );

            await _repository.AddAsync(agent);
            await _repository.SaveChangesAsync();
            return OperationResult.Success("کاربر با موفقیت ایجاد شد");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error($"عملیات شکست خورد  {e}");
        }
    }
}