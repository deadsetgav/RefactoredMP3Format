using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class AlbumFactory
    {
        public static IAlbum GetAlbum(string title, string artistName, string path)
        {
            var albumReader = new AlbumFileReader(path);
            return new Album(title, artistName, albumReader);
        }
    }
}
