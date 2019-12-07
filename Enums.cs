using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WanikaniApi
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
}
