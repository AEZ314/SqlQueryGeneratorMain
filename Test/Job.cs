using SqlQueryGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [SqlTableName("[dbo].[Jobs]")]
    public class Job
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        private int StateId { get; set; }
        [SqlPropertyIgnore]
        public JobState State
        {
            get
            {
                return (JobState)StateId;
            }
            set
            {
                StateId = (int)value;
            }
        }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string ResultPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public enum JobState
    {
        Pending,
        Processing,
        Successful,
        Failed,
    }
}
