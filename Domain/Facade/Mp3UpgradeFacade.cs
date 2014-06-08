using CommonInterface;
using DataRepository;
using Domain.HelperClasses;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Facade
{
    public class Mp3UpgradeFacade: FacadeBase
    {

        public Mp3UpgradeFacade(ISettings settings):base (settings)
        {
   
        }

        public override void Process()
        {
            var reader = new CollectionFileReader();
            var collection = reader.ReadCollection(settings.SourceDirectoryPath);
            var dataAccess = new DataAccess("Albums");



        }

        

    }


    public class Upgrader
    {

        public UpgradeResult UpgradeAlbum(IAlbum rippedAlbum, IAlbum dbAlbum)
        {
            var rippedTracks = rippedAlbum.Tracks().ToList();
            var dbTracks = dbAlbum.Tracks().ToList();
            var errors = 0;

            

            if (rippedTracks.Count == dbTracks.Count)
            {

            } 
            else
            {
                errors += 1;
            }

            return UpgradeResult.NoErrors;

        }

        public void CleanseTrack(IUpgradeMp3 rippedTrack, IMp3 dbTrack)
        {
            if (TracksMatch(rippedTrack, dbTrack))
            {
                rippedTrack.Track = dbTrack.Track;
                rippedTrack.Title = dbTrack.Title;
                rippedTrack.SetNewFilename(dbTrack.FileName);
                rippedTrack.Year = dbTrack.Year;
                rippedTrack.Artist = dbTrack.Artist;
                rippedTrack.AlbumArtist = dbTrack.AlbumArtist;
                rippedTrack.Album = dbTrack.Album;
            }
            else
            {
                throw new TrackMismatchException(string.Format("ripped: {0} - {1}, {2} - {3}, db: {4} - {5}, {6} - {7}",
                    rippedTrack.Artist, rippedTrack.Album, rippedTrack.Track, rippedTrack.Title,
                    dbTrack.Artist,dbTrack.Album,dbTrack.Track,dbTrack.Title));
            }

        }
       
        private bool TracksMatch(IMp3 rippedTrack, IMp3 dbTrack)
        {
            if (!TrackNumbersMatch(rippedTrack.Track , dbTrack.Track)) return false;
            if (!DoStringsMatch(rippedTrack.Title, dbTrack.Title)) return false;
            if (!DoStringsMatch(rippedTrack.Album , dbTrack.Album)) return false;
            if (!DoStringsMatch(rippedTrack.Artist, dbTrack.Artist)) return false;

            return true;
        }

        private bool TrackNumbersMatch(string ripped, string db)
        {
            if (ripped == db)
            {
                return true;
            }
            else
            {
                if (Convert.ToInt16(ripped) != Convert.ToInt16(db))
                    return false;
                else
                    return true;
            }
        }

        private bool DoStringsMatch(string ripped, string db)
        {
            if (ripped == db)
            {
                return true;
            }
            else
            {
                return StringMatcher.StringsCloselyMatch_IgnoreCaseAndNonAlphaNumeric(ripped,db);
            }
        }

    }

    public class TrackMismatchException : Exception
    {
        public TrackMismatchException(string message)
            : base(message)
        {

        }
    }

    public enum UpgradeResult{
        NoErrors,
        MinorErrors,
        Fail
    }
}
