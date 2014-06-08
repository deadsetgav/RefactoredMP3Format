using CommonInterface;
using Domain.Exceptions;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class DataAccess : IDataAccess
    {
        private MongoClient _client;
        private MongoServer _server;
        private MongoDatabase _db;
        private string _collectionName;

        public DataAccess(string collectionName)
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("Mp3Collection");
            _collectionName = collectionName;
        }
        
        public void SaveArtist(IArtist artist)
        {
            var collection = _db.GetCollection(_collectionName);
            var query = Query.EQ("Artist", artist.Name);

            var writeArtist = DataMapper.GetArtistAsBson(artist);
            collection.Update(query, Update.Replace(writeArtist), UpdateFlags.Upsert);
        }
     
        public void SaveAlbum(IAlbum album)
        {
            var collection = _db.GetCollection(_collectionName);
            var query = Query.And(
                    Query.EQ("Artist", album.ArtistName),
                    Query.EQ("Album", album.Title)
                );

            var writeAlbum = DataMapper.GetAlbumAsBson(album);
            collection.Update(query,Update.Replace(writeAlbum),UpdateFlags.Upsert);
        }

        public IArtist GetArtist(string artist)
        {
            var query = Query.EQ("Artist", artist); 
            var collection = _db.GetCollection(_collectionName);
            var cursor = collection.Find(query);
            var searchResult = cursor.FirstOrDefault();

            if (searchResult == null)
            {
                throw new ArtistNotFoundException(artist);
            }

            return DataMapper.GetArtistFromBson(searchResult);
        }

        public bool ArtistExists(string artist)
        {
            try
            {
                var readArtist = GetArtist(artist);
                return true;
            }
            catch (ArtistNotFoundException)
            {
                return false;
            }
        }

        public IAlbum GetAlbum(string artist, string albumTitle) 
        {
            var query = Query.And(
                   Query.EQ("Artist", artist),
                   Query.EQ("Album", albumTitle)
               );
            
            var collection = _db.GetCollection(_collectionName);
            var cursor = collection.Find(query);
            var searchResult = cursor.FirstOrDefault();

            if (searchResult == null)
            {
                throw new AlbumNotFoundException(albumTitle, artist);
            } 
            return DataMapper.GetAlbumFromBson(searchResult);
        }

        public bool AlbumExists(string artist, string albumTitle) 
        {
            try
            {
                var album = GetAlbum(artist, albumTitle);
                return true;
            }
            catch (AlbumNotFoundException)
            {
                return false;
            }
        } 
    
        public void DropCollection()
        {
            _db.DropCollection(_collectionName);
        }
    }
}
