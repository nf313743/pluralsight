
using System;

public class Metadata
{
    public string Practitioner { get; set; }
    public string Patient { get; set; }
    public DateTime DateRecorder { get; set; }
    public System.Collections.Generic.List<string> Tags { get; set; }
    public AudioFile File { get; set; }
}
