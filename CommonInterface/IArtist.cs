using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IArtist
    {
        string Name { get; }
        List<IAlbum> Albums {get;}

        void AddAlbum(IAlbum album);

        void AddAlbums(IAlbum[] albums);
    }
}
