﻿using System;

namespace MyIssue.API.Model.Return
{
    public class TaskReturn
    {
        public decimal TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string TaskClient { get; set; }
        public string TaskAssignment { get; set; }
        public string TaskOwner { get; set; }
        public string TaskType { get; set; }
        public DateTime? TaskStart { get; set; }
        public DateTime? TaskEnd { get; set; }
        public DateTime TaskCreationDate { get; set; }
        public string CreatedByMail { get; set; }
    }
}