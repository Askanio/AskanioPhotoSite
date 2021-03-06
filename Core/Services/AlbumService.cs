﻿using System;
using System.Collections.Generic;
using System.Linq;
using AskanioPhotoSite.Core.Models;
using AskanioPhotoSite.Data.Entities;
using AskanioPhotoSite.Data.Storage;

namespace AskanioPhotoSite.Core.Services
{
    public class AlbumService : BaseService<Album>
    {
        public AlbumService(IStorage storage) : base (storage) { }

        public override IEnumerable<Album> GetAll()
        {
            return _storage.GetRepository<Album>().GetAll().ToList();
        }

        public override Album GetOne(int id)
        {
            var album = GetAll().SingleOrDefault(x => x.Id == id);

            if (album == null) return null;
            return new Album()
            {
                Id = album.Id,
                ParentId = album.ParentId,
                DescriptionEng = album.DescriptionEng,
                DescriptionRu = album.DescriptionRu,
                TitleEng = album.TitleEng,
                TitleRu = album.TitleRu,
                CoverPath = album.CoverPath,
                ViewPattern = album.ViewPattern
            };
        }

        public override Album AddOne(object obj)
        {
            var model = (EditAlbumModel)obj;

            var album = new Album()
            {
                Id = 0,
                ParentId = model.ParentAlbum.Id,
                TitleRu = model.TitleRu,
                TitleEng = model.TitleEng,
                DescriptionEng = model.DescriptionEng,
                DescriptionRu = model.DescriptionRu,
                ViewPattern = model.ViewPattern
            };

           var updated =  _storage.GetRepository<Album>().AddOne(album);
            _storage.Commit();
            return updated;
        }

        public override Album[] AddMany(object[] obj)
        {
            throw new NotImplementedException();
        }

        public override Album UpdateOne(object obj)
        {
            if(obj.GetType() == typeof(Album))
            {
                var entity = (Album)obj;

                var updatedEntity = _storage.GetRepository<Album>().UpdateOne(entity);
                _storage.Commit();
                return updatedEntity;
            }

            var model = (EditAlbumModel)obj;

            var album = GetOne(model.Id);

            album.DescriptionEng = model.DescriptionEng;
            album.DescriptionRu = model.DescriptionRu;
            album.TitleEng = model.TitleEng;
            album.TitleRu = model.TitleRu;
            album.ParentId = model.ParentAlbum?.Id ?? 0;
            album.CoverPath = model.CoverPath;
            album.ViewPattern = model.ViewPattern;

            var updated = _storage.GetRepository<Album>().UpdateOne(album);
            _storage.Commit();
            return updated;
        }

        public override void DeleteOne(int id)
        {
            var photoRepository = _storage.GetRepository<Photo>();
            var photos = photoRepository.GetAll().Where(x => x.AlbumId == id);

            if (photos.Count() > 0)
            {
                photoRepository.DeleteMany(photos.Select(x => x.Id).ToArray());
            }

            _storage.GetRepository<Album>().DeleteOne(id);
            _storage.Commit();
        }
    }
}
