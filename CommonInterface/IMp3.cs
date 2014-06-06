using System;
namespace Id3LibTagAdapter
{
    public interface IMp3
    {
        string Album { get; set; }
        string AlbumArtist { get; set; }
        string Artist { get; set; }
        void Save();
        string Title { get; set; }
        string Track { get; set; }
        string Year { get; set; }
        string FullFilePath { get; }
        string FileName { get; }
        int BitRate { get; }
    }



}
