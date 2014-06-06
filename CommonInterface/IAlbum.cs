using Id3LibTagAdapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAlbum
    {
        string Title { get; }
        string ArtistName { get; }
        string ReleaseYear { get; set; }
        IMp3[] Tracks();
        string Source { get; }
        FileInfo[] GetAdditionalFiles {get;}
        
        string GetAlbumYearFromTracks();
        int GetAverageBitRateFromTracks();

    }

    public interface IAlbumFileReader 
    {
        List<IMp3> GetAlbumTracks();
        FileInfo[] GetAdditionalFiles();
        string Source { get; }
    }
}
