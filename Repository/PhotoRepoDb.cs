
using System;
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using Microsoft.EntityFrameworkCore;

namespace LHDTV.Repo
{
    public class PhotoRepoDb : IPhotoRepo
    {

        public PhotoRepoDb()
        {
        }

        public PhotoDb Create(PhotoDb entity, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.Photo.Add(entity);
                ctx.SaveChanges();
                return entity;
            }


        }
        public PhotoDb Read(int id, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                var photo = ctx.Photo.Include(p => p).ThenInclude(t => t).Include(p => p.Tag).FirstOrDefault(p => p.Id == id);
                return photo;
            }
        }
        public PhotoDb Update(PhotoDb entity, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.Photo.Update(entity);
                ctx.SaveChanges();

                return entity;
            }
        }

        public void RemoveTag(TagDb tag, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.TagDb.Remove(tag);
                ctx.SaveChanges();
            }
        }

        //Si borra correctamente devuelve un null
        public PhotoDb Delete(int id, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                var photo = ctx.Remove(id);
                ctx.SaveChanges();

                return null;
            }
        }

        public List<PhotoDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId)
        {
            using (var ctx = new LHDTVContext())
            {

                var query = ctx.Photo.Include(p => p.Tag).Where(p => p.UserId == userId);
                if (pagination.Filter != null)
                    foreach (var pair in pagination.Filter)
                    {
                        var filterKey = pair.Key;
                        var filterVal = pair.Value;
                        switch (filterKey)
                        {
                            case "title":
                                query = query.Where(p => p.Title.Contains(filterKey));
                                break;
                            case "dateIni":
                                query = query.Where(p => p.RealDate > DateTime.Parse(filterVal));
                                break;
                            case "dateEnd":
                                query = query.Where(p => p.RealDate < DateTime.Parse(filterVal));
                                break;
                            case "uploadDateStart":
                                query = query.Where(p => p.UploadDate > DateTime.Parse(filterVal));
                                break;
                            case "uploadDateEnd":
                                query = query.Where(p => p.UploadDate < DateTime.Parse(filterVal));
                                break;
                            default:
                                break;
                        }
                    }

                var queryRes = query.Skip((pagination.NumPag - 1) * pagination.TamPag)
                        .Take(pagination.TamPag).ToList();
                // var f = query.Select(p => p.GetType().GetProperty(pagination.FilterField[0]).GetValue(p, null).ToString()).ToList();



                return queryRes;
            }
        }

        public List<PhotoDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId, int folderId)
        {
            using (var ctx = new LHDTVContext())
            {

                var query = ctx.Photo.Include(p => p.PhotosFolder).Include(p => p.Tag)
                        .Where(p => p.UserId == userId &&
                            !p.PhotosFolder
                                .Where(pf => pf.FolderId == folderId)
                                .Select(pf => pf.PhotoId)
                                .Contains(p.Id))
                        .Skip((pagination.NumPag - 1) * pagination.TamPag)
                        .Take(pagination.TamPag).ToList();
                // var f = query.Select(p => p.GetType().GetProperty(pagination.FilterField[0]).GetValue(p, null).ToString()).ToList();
                return query;
            }
        }


        public ICollection<PhotoTagsTypes> getTagTypes()
        {
            using (var ctx = new LHDTVContext())
            {
                return ctx.TagTypeMaster.Include(t => t.Extra1).Include(t => t.Extra2).Include(t => t.Extra3).ToList();
            }
        }
        public void UpdateTag(TagDb tag, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.TagDb.Update(tag);
                ctx.SaveChanges();
            }
        }
    }
}