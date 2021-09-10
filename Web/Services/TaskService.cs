﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyIssue.Core.Commands;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Infrastructure.API;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Services
{
    public class TaskService : ITaskService
    {
        private readonly IServerConnector _server;

        private readonly IConfiguration _config;
       // private readonly ILogger<TaskService> _logger;

        private readonly string _remoteServiceBaseUrl;

        public TaskService(IServerConnector server, IConfiguration configuration)
        {
            _server = server;
            _config = configuration;
            //_logger = logger;
            //_remoteServiceBaseUrl = $"{_settings.Value.Url}/c/api/v1/task/";
        }

        public async Task<IEnumerable<Task>> GetTasks()
        {
            List<byte[]> cmds = new List<byte[]>().Concat(User.Login(_config.GetValue<string>("ServerConnection:Login"), _config.GetValue<string>("ServerConnection:Pass")))
                .Concat(Core.Commands.Task.GetTask().Concat(User.Logout())).ToList();

            string response = _server.SendData(cmds);

            var task = JsonSerializer.Deserialize<List<Task>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return task;

        }

        public async Task<IEnumerable<Task>> GetLastTasks(int howMany)
        {
            List<byte[]> cmds = new List<byte[]>().Concat(User.Login(_config.GetValue<string>("ServerConnection:Login"), _config.GetValue<string>("ServerConnection:Pass")))
                .Concat(Core.Commands.Task.GetLastTask(howMany).Concat(User.Logout())).ToList();
            string response = _server.SendData(cmds);

            var task = JsonSerializer.Deserialize<List<Task>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return task;
        }
    }
}