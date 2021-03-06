using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace MyIssue.Core.DataTransferObjects.Return
{
    public class TaskReturnRoot
    {
        public List<TaskReturn> Tasks { get; set; }
    }
    public class TaskReturn
    {
        [JsonProperty("TaskId")]
        public decimal TaskId { get; set; }

        [JsonProperty("TaskTitle")]
        public string TaskTitle { get; set; }

        [JsonProperty("TaskDescription")]
        public string TaskDescription { get; set; }

        [JsonProperty("TaskClient")]
        public string TaskClient { get; set; }

        [JsonProperty("TaskAssignment")]
        public string TaskAssignment { get; set; }

        [JsonProperty("TaskOwner")]
        public string TaskOwner { get; set; }

        [JsonProperty("TaskType")]
        public string TaskType { get; set; }

        [JsonProperty("TaskStart")]
        [DefaultValue(true)]
        public DateTime? TaskStart { get; set; }

        [JsonProperty("TaskEnd")]
        [DefaultValue(true)]
        public DateTime? TaskEnd { get; set; }

        [JsonProperty("TaskCreationDate")]
        public DateTime TaskCreationDate { get; set; }

        [JsonProperty("CreatedByMail")]
        public string CreatedByMail { get; set; }

        [JsonProperty("EmployeeDescription")]
        public string EmployeeDescription { get; set; }
    }
}