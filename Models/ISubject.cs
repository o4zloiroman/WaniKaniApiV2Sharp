using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanikaniApi.Models
{
    public interface ISubject
    {
        AuxiliaryMeanings[] AuxiliaryMeanings { get; set; }
                
        string Characters { get; set; }
                
        DateTimeOffset CreatedAt { get; set; }
                
        Uri DocumentUrl { get; set; }
                
        object HiddenAt { get; set; }
                
        long LessonPosition { get; set; }
                
        int Level { get; set; }
                
        string MeaningMnemonic { get; set; }
                
        Meanings[] Meanings { get; set; }
                
        string Slug { get; set; }
    }
}
