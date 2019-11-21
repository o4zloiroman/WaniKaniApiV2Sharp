using System;

namespace WanikaniApi.Models
{
    public interface ISubject
    {
        AuxiliaryMeaning[] AuxiliaryMeanings { get; set; }

        string Characters { get; set; }

        DateTimeOffset CreatedAt { get; set; }

        Uri DocumentUrl { get; set; }

        object HiddenAt { get; set; }

        long LessonPosition { get; set; }

        int Level { get; set; }

        string MeaningMnemonic { get; set; }

        Meaning[] Meanings { get; set; }

        string Slug { get; set; }
    }
}