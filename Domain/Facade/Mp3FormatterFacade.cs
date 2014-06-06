using Domain.Concrete;
using Domain.Factory;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Facade
{
    public class Mp3FormatterFacade : FacadeBase
    {
       
        private IFormatterSettings _settings;

        public Mp3FormatterFacade(IFormatterSettings settings) : base(settings)
        {
            _settings = settings;
        }

        public override void Process()
        {
            Log.WriteInfoToLog("Starting Process()...");
            var sourceMusicCollection = ReadSourceDirectory();
            var formatter = AlbumFormatFactory.GetAlbumFormatter(_settings.Format);
            var mp3Writer = new Mp3Writer(_settings, formatter);

            WriteAlbums(mp3Writer, sourceMusicCollection.Artists);
            Log.WriteInfoToLog("Finished Process()...");
        }

        public void WriteAlbums(IMp3Writer formatWriter, IArtist[] artists)
        {
            foreach (IArtist artist in artists)
            {          
                try
                {
                    Log.WriteInfoToLog("Writing albums for artist: {0}",artist.Name); 
                    formatWriter.WriteAlbums(artist);
                }
                catch (Exception ex)
                {
                    Log.WriteErrorToLog(string.Format("failed to write albums for {0}",artist.Name), ex);
                }
            }
        }
        

    }
}
