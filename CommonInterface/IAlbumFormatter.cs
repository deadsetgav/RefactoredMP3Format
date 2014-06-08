using CommonInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces
{
    public interface IAlbumFormatter
    {
        DirectoryInfo GetFolderToWriteTo(string writeFolder, IArtist artist, IAlbum album);
        void FormatMp3Tags(IMp3 track, IAlbum album);

    }
}
