using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WaniKaniApi
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LessonsPresentationOrder
    {
        [EnumMember(Value = "ascending_level_then_subject")]
        AscendingThenSubject,
        [EnumMember(Value = "shuffled")]
        Shuffled,
        [EnumMember(Value = "ascending_level_then_shuffled")]
        AscendingThenShuffled
    }

    public enum SubjectType
    {
        Radical,
        Kanji,
        Vocabulary        
    }

    public enum SrsStageEnum
    {
        Initiate = 0,
        ApprenticeI = 1,
        ApprenticeII = 2,
        ApprenticeIII = 3,
        ApprenticeIV = 4,
        GuruI = 5,
        GuruII = 6,
        Master = 7,
        Enlightened = 8,
        Burned = 9
    }
}
