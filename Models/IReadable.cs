namespace WanikaniApi.Models
{
    public interface IReadable
    {
        Readings[] Readings { get; set; }
        
        int[] ComponentSubjectIds { get; set; }

        string ReadingMnemonic { get; set; }
    }
}
