using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Concrete
{
    public class Artist : IArtist
    {
        private string _name;
        private Dictionary<string, IAlbum> _albums;

        public Artist(string artistName)
        {
            _name = artistName;
            _albums = new Dictionary<string, IAlbum>();
        }

        public string Name
        {
            get { return _name; }
        }

        public List<IAlbum> Albums
        {
            get { return _albums.Values.ToList(); }
        }

        public void AddAlbum(IAlbum album)
        {
            if (!_albums.ContainsKey(album.Title.ToLower().Trim()))
            {
                _albums.Add(album.Title.ToLower().Trim(), album);
            }
        }

        public void AddAlbums(IAlbum[] albums)
        {
            foreach (var album in albums)
            {
                AddAlbum(album);
            }
        }

    }
}
