namespace WanikaniApi.Models
{
    public interface IReadable : ISubject
    {
        Reading[] Readings { get; set; }
        
        int[] ComponentSubjectIds { get; set; }

        string ReadingMnemonic { get; set; }
    }
}
