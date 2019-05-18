using System;
using System.Collections.Generic;

namespace WanikaniApi
{
    public class WaniKaniItem
    {   
        public int AssignmentId { get; set; }

        public int SubjectId { get; set; }

        public int IncorrectMeaningAnswers { get; set; } = 0;

        public int IncorrectReadingAnswers { get; set; } = 0;

        public bool ReadingDone { get; set; } = false;

        public bool MeaningDone { get; set; } = false;

        public string Object { get; set; }

        public string Characters { get; set; }

        public List<string> Meanings { get; set; }

        public List<string> Readings { get; set; }
    }
}
