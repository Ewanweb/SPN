using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Site.Domain._shared;
using Site.Domain.Agents.Enums;
using Site.Domain.Agents.ValueObjects;
using Site.Domain.Projects;
using static System.Net.Mime.MediaTypeNames;

namespace Site.Domain.Agents
{
    public class Agent : BaseEntity
    {
        public string FullName { get; private set; }
        public string Password { get; private set; }
        public string Slug { get; private set; }
        public string? GithubLink { get; private set; }
        public string ImageName { get; private set; }
        public string? Description { get; private set; }
        public string Email { get; private set; }
        public string? Profienece { get; private set; }
        public string? MyProfienece { get; private set; }
        public string? Experience { get; private set; }
        public AgentPhoneNumber PhoneNumber { get; private set; }
        public string? ResumeFileName { get; private set; }
        public AgentStatus Status { get; private set; }
        private readonly List<AgentFeature> _agentFeatures = new();
        public IReadOnlyCollection<AgentFeature> AgentFeatures => _agentFeatures;
        private Agent()
        {
        }

        public Agent(string fullName, string? githubLink, string imageName, string description, string slug,
            string email, AgentPhoneNumber phoneNumber,
            AgentStatus status, string? resumeFileName, string? profienece, string? experience, string? myProfienece)
        {
            FullName = fullName;
            GithubLink = githubLink;
            ImageName = imageName;
            Description = description;
            Email = email;
            PhoneNumber = phoneNumber;
            Status = status;
            ResumeFileName = resumeFileName;
            Profienece = profienece;
            Experience = experience;
            MyProfienece = myProfienece;
        }

        public static Agent Create(string fullName, string email, string password, AgentPhoneNumber phoneNumber)
        {
            var agent = new Agent();

            var passwordHasher = PasswordHasher.Hash(password);


            agent.FullName = fullName;
            agent.Profienece = "خالی";
            agent.Experience = "خالی";
            agent.MyProfienece = "خالی";
            agent.Description = "توضیحاتی نوشته نشده است";
            agent.GithubLink = "لینک گیتهابی نوشته نشده است";
            agent.ImageName = "noImage.png";
            agent.Email = email;
            agent.PhoneNumber = phoneNumber;
            agent.Status = AgentStatus.Active;
            agent.Slug = SlugHelper.GenerateSlug(fullName);
            agent.Password = passwordHasher;


            return agent;
        }

        public void Edit(string fullName, string? githubLink, string imageName, string? description,
            string email, AgentPhoneNumber phoneNumber, string? profienece, string? experience, string? myProfienece, string? resume)
        {
            Profienece = profienece;
            FullName = fullName;
            GithubLink = githubLink;
            ImageName = imageName;
            Description = description;
            Email = email;
            PhoneNumber = phoneNumber;
            Experience = experience;
            MyProfienece = myProfienece;
            ResumeFileName = resume;
        }

        public void ChangeStatus(AgentStatus status) => Status = status;

        public void AddFeatureGroup(Guid agentId, string title, int percentage)
        {
            if (string.IsNullOrWhiteSpace(title) || percentage < 0 || percentage > 100)
                throw new ArgumentException(" ویژگی نمی‌تواند خالی باشد");


            var feature = new AgentFeature(agentId, title, percentage);
            _agentFeatures.Add(feature);

        }

        public void Guard(string fullName, string? githubLink, string imageName, string description,
                string email, AgentPhoneNumber phoneNumber)
            {
                if (string.IsNullOrWhiteSpace(fullName))
                    throw new ArgumentException("نام نمی‌تواند خالی باشد", nameof(fullName));

                if (string.IsNullOrWhiteSpace(githubLink))
                    throw new ArgumentException("لینک گیت‌هاب نمی‌تواند خالی باشد", nameof(githubLink));

                if (!Uri.IsWellFormedUriString(githubLink, UriKind.Absolute))
                    throw new ArgumentException("لینک گیت‌هاب معتبر نیست", nameof(githubLink));

                if (string.IsNullOrWhiteSpace(imageName))
                    throw new ArgumentException("نام تصویر نمی‌تواند خالی باشد", nameof(imageName));

                if (string.IsNullOrWhiteSpace(description))
                    throw new ArgumentException("توضیحات نمی‌تواند خالی باشد", nameof(description));

                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("ایمیل نمی‌تواند خالی باشد", nameof(email));

                if (!email.Contains("@") || !email.Contains("."))
                    throw new ArgumentException("فرمت ایمیل نامعتبر است", nameof(email));

                if (phoneNumber == null)
                    throw new ArgumentNullException(nameof(phoneNumber), "شماره موبایل الزامی است");
            }

    }
}
