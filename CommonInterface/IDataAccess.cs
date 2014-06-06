using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInterface
{
    public interface IDataAccess
    {
        void SaveAlbum(IAlbum album);
        IAlbum GetAlbum(string artist, string albumTitle);
        bool AlbumExists(string artist, string albumTitle);
    }
}
