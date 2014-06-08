using Domain.Interfaces;
using CommonInterface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class DataMapper
    {
        public static UpdateDocument GetArtistAsBson(IArtist artist)
        {
            var writeArtist = new UpdateDocument()
            {
                {"Artist", artist.Name}
            };
            return writeArtist;
        }

        public static UpdateDocument GetAlbumAsBson(IAlbum album)
        {
            var writeAlbum = new UpdateDocument()
               {
                   { "Artist", album.ArtistName },
                   { "Album", album.Title },
                   { "Year", album.ReleaseYear },
                   { "BitRate", album.GetAverageBitRateFromTracks() },
                   { "Source", album.Source },
                   { "Tracks", GetAlbumTracksAsBson(album) }
               };
            return writeAlbum;
        }

        public static BsonArray GetAlbumTracksAsBson(IAlbum album)
        {
            var tracks = new BsonArray();
            foreach (IMp3 mp3 in album.Tracks())
            {
                var dbTrack = GetMp3AsBson(mp3);
                tracks.Add(dbTrack);
            }

            return tracks;
        }

        public static BsonDocument GetMp3AsBson(IMp3 mp3)
        {
            return new BsonDocument()
                {
                   { "Track", mp3.Track },
                   { "Title", mp3.Title },
                   { "BitRate", mp3.BitRate },
                   { "File" , mp3.FullFilePath }
                };
        }
       
        public static IArtist GetArtistFromBson(BsonDocument artist)
        {
            var artistname = artist.GetValue("Artist", string.Empty).ToString();
            return new DBArtist(artistname);
        }

        public static IAlbum GetAlbumFromBson(BsonDocument album)
        {
            var dbAlbum = new DBAlbum() 
            { 
                Title = album.GetValue("Album",string.Empty).ToString(),
                ArtistName = album.GetValue("Artist", string.Empty).ToString(),
                ReleaseYear = album.GetValue("Year",string.Empty).ToString(),
                BitRate = album.GetValue("BitRate",0).ToInt32(),
                Source = album.GetValue("Source", string.Empty).ToString()
            };

            var dbTracks = (BsonArray)album.GetValue("Tracks");
            var trackArray = GetMp3ArrayFromBson(dbTracks, dbAlbum);
            dbAlbum.SetTracks(trackArray);
            
            return dbAlbum;
        }

        public static IMp3[] GetMp3ArrayFromBson(BsonArray mp3s, IAlbum album)
        {
            var mp3list = new List<IMp3>();
            foreach (BsonDocument mp3 in mp3s)
            {
                mp3list.Add(GetMp3FromBson(mp3, album));
            }
            return mp3list.ToArray();
        }

        public static IMp3 GetMp3FromBson(BsonDocument mp3, IAlbum album)
        {
            var dbMp3 = new DBTrack()
            {
                Track = mp3.GetValue("Track",string.Empty).ToString(),
                Title = mp3.GetValue("Title",string.Empty).ToString(),
                BitRate = mp3.GetValue("BitRate",0).ToInt32(),  
                Album = album.Title,
                AlbumArtist = album.ArtistName,
                Artist = album.ArtistName,
                FullFilePath = mp3.GetValue("File", string.Empty).ToString(),
                Year = album.ReleaseYear
            };
                                    
            dbMp3.FileName = dbMp3.FullFilePath.Substring(dbMp3.FullFilePath.LastIndexOf("\\") + 1);
            return dbMp3;  
        }
          
    }
}
