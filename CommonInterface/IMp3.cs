using System;
namespace CommonInterface
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

    public interface IUpgradeMp3 :IMp3
    {
        void SetNewFilename(string newFilename);
    }

}
