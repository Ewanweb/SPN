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
    public class Agent :  IdentityUser
    {
        public string FullName { get; private set; }
        public string Slug { get; private set; }
        public string GithubLink { get; private set; }
        public string ImageName { get; private set; }
        public string Description { get; private set; }
        public string Email { get; private set; }
        public AgentPhoneNumber PhoneNumber { get; private set; }
        public string? ResumeFileName { get; private set; }
        public AgentStatus Status { get; private set; }
        public List<AgentFeature> AgentFeatures { get; private set; } = new();

        private Agent()
        {
        }

        public Agent(string fullName, string githubLink, string imageName, string description, string slug,
            string email, AgentPhoneNumber phoneNumber,
            AgentStatus status, string? resumeFileName)
        {
            FullName = fullName;
            GithubLink = githubLink;
            ImageName = imageName;
            Description = description;
            Email = email;
            PhoneNumber = phoneNumber;
            Status = status;
            ResumeFileName = resumeFileName;
        }

        public static Agent Create(string fullName, string githubLink, string imageName, string description,
            string email, AgentPhoneNumber phoneNumber, string? resumeFileName, string password)
        {
            var agent = new Agent();
            agent.Guard(fullName, githubLink, imageName, description, email, phoneNumber);

            var passwordHasher = new PasswordHasher<Agent>();


            agent.FullName = fullName;
            agent.GithubLink = githubLink;
            agent.ImageName = imageName;
            agent.Description = description;
            agent.Email = email;
            agent.PhoneNumber = phoneNumber;
            agent.Status = AgentStatus.Active;
            agent.Slug = GenerateSlug(fullName);
            agent.ResumeFileName = resumeFileName;
            agent.PasswordHash = passwordHasher.HashPassword(agent, password);


            return agent;
        }

        public static Agent Register(string fullName, string email, AgentPhoneNumber phone)
        {
            const string defaultGithub = "https://github.com/";
            const string defaultImage = "default.png";
            const string defaultDescription = "کاربر جدید";

            var slug = GenerateSlug(fullName);

            return new Agent(
                fullName, defaultGithub, defaultImage, defaultDescription, slug,
                email, phone, AgentStatus.Active, null);
        }

        public void Edit(string fullName, string githubLink, string imageName, string description,
            string email, AgentPhoneNumber phoneNumber)
        {
            Guard(fullName, githubLink, imageName, description, email, phoneNumber);

            FullName = fullName;
            GithubLink = githubLink;
            ImageName = imageName;
            Description = description;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void ChangeStatus(AgentStatus status) => Status = status;

        public void AddFeature(List<AgentFeature>? features)
        {
            if (features is null || !features.Any())
                throw new ArgumentException("هیچ تصویری ارسال نشده است", nameof(features));

            var agentFeature = features
                .Select(feature => new AgentFeature(feature.Key))
                .ToList();

            AgentFeatures.AddRange(agentFeature);
        }

        public void Guard(string fullName, string githubLink, string imageName, string description,
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

        private static string GenerateSlug(string text)
        {
            return text.Trim().ToLower().Replace(" ", "-");
        }
    }
}
