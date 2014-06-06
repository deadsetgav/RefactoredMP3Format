using DataRepository;
using Domain.Concrete;
using Domain.Exceptions;
using Domain.Interfaces;
using Id3LibTagAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Facade
{
    public class Mp3StandardiseFacade : FacadeBase
    {
        private IFormatterSettings _settings;

        public Mp3StandardiseFacade(IFormatterSettings settings)
            : base(settings)
        {
            _settings = settings;
        }

        public override void Process()
        {
            Log.WriteInfoToLog("Starting Process()...");
            var collection = ReadSourceDirectory();
            var dataAccess = new DataAccess("Artist");

            foreach (IArtist artist in collection.Artists) 
            {
                if (!dataAccess.ArtistExists(artist.Name))
                {
                    try
                    { 
                        StandardiseArtist(artist);
                        dataAccess.SaveArtist(artist);
                    }
                    catch (Exception ex) 
                    {
                        Log.WriteErrorToLog(string.Format(
                            "Couldn't standardise Artist {0}",artist.Name), ex);
                    }
                }            
            }
            
            Log.WriteInfoToLog("Finished Process()...");
        }

        public void StandardiseArtist(IArtist artist)
        {
            
                var mostCommonArtistName = ArtistReader.GetMostCommonArtistNameFromAlbums(artist);

                foreach (IAlbum album in artist.Albums) 
                {
                    var comparer = new AlbumCompare(album, mostCommonArtistName);
                    foreach(IMp3 mp3 in album.Tracks()) 
                    {
                        StandardiseTrack(comparer, mp3);
                    }
                }
           
        }
        
        private void StandardiseTrack(AlbumCompare comparer, IMp3 mp3) 
        {
            {
                if (!comparer.TrackMatchesCommonAlbumTitles(mp3))
                {
                    comparer.MakeTrackMatch(mp3);
                    mp3.Save();
                    Log.WriteDetailToLog(string.Format("Renamed {0} - {1} - {2}",
                            mp3.Artist, mp3.Album, mp3.Title));
                }
            }
        }

    }


    public class AlbumCompare
    {
        public string MostCommonAlbumYear { get; private set; }
        public string MostCommonAlbumTitle { get; private set; }
        public string MostCommonArtistName { get; private set; }
        
        public AlbumCompare(IAlbum album, string artistName)
        {
            this.MostCommonAlbumTitle = AlbumReader.GetMostCommonAlbumTitleFromTracks(album);
            this.MostCommonAlbumYear = AlbumReader.GetMostCommonYearFromTracks(album);
            this.MostCommonArtistName = artistName;
        }

        public bool TrackMatchesCommonAlbumTitles(IMp3 track) 
        {
            var match = false;
            if ((track.Year == MostCommonAlbumYear) &&
                (track.Artist == MostCommonArtistName) &&
                 (track.AlbumArtist == MostCommonArtistName) &&
                  (track.Album == MostCommonAlbumTitle))
            {
                match = true;
            }

            return match;
        }
        
        public void MakeTrackMatch(IMp3 track)
        {
            if (TrackCloselyMatchesCommonAlbumTitles(track))
            {
                track.Album = MostCommonAlbumTitle;
                track.AlbumArtist = MostCommonArtistName;
                track.Artist = MostCommonArtistName;
                track.Year = MostCommonAlbumYear;
            }
            else
            {
                throw new SignificantTagMismatchException(track);
            }
           
        }
        private bool TrackCloselyMatchesCommonAlbumTitles(IMp3 track) 
        {
            var closeMatch = false;
            if (AlbumReader.StringsCloselyMatch(track.Year, MostCommonAlbumYear) &&
                 AlbumReader.StringsCloselyMatch(track.Artist ,MostCommonArtistName) &&
                  AlbumReader.StringsCloselyMatch (track.AlbumArtist , MostCommonArtistName) &&
                   AlbumReader.StringsCloselyMatch(track.Album, MostCommonAlbumTitle))
            {
                closeMatch = true;
            }

            return closeMatch;
        }
    }
}
