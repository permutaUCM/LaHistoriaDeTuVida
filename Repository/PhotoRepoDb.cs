
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using Microsoft.EntityFrameworkCore;
using System;


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
                var photo = ctx.Photo.Include(p => p).ThenInclude(t => t).Include(p => p.Tag).FirstOrDefault(p => p.Id == id && !p.Deleted);
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

                var query = ctx.Photo.Include(p => p.Tag).Where(p => p.UserId == userId && !p.Deleted);
                // var f = query.Select(p => p.GetType().GetProperty(pagination.FilterField[0]).GetValue(p, null).ToString()).ToList();
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
                            case "dateStart":
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
                            case "tags":
                                if (filterVal.Length == 0)
                                    continue;
                                var tagList = filterVal.Split(";");
                                query = query.Where(p => p.Tag.Where(t => tagList.ToList().Contains(t.Title)).Any());
                                break;
                            default:
                                break;
                        }
                    }

                var content = query.Skip((pagination.NumPag - 1) * pagination.TamPag)
                        .Take(pagination.TamPag).ToList();

                return content;
            }
        }

        public List<PhotoDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId, int folderId)
        {
            using (var ctx = new LHDTVContext())
            {

                var query = ctx.Photo.Include(p => p.Tag).Include(p => p.PhotosFolder)
                        .Where(p => p.UserId == userId && !p.Deleted &&
                            !p.PhotosFolder
                                .Where(pf => pf.FolderId == folderId)
                                .Select(pf => pf.PhotoId)
                                .Contains(p.Id));

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
                            case "dateStart":
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
                            case "tags":
                                if (filterVal.Length == 0)
                                    continue;
                                var tagList = filterVal.Split(";");
                                query = query.Where(p => p.Tag.Where(t => tagList.ToList().Contains(t.Title)).Any());
                                break;
                            default:
                                break;
                        }
                    }

                var content = query.Skip((pagination.NumPag - 1) * pagination.TamPag)
                        .Take(pagination.TamPag).ToList();
                // var f = query.Select(p => p.GetType().GetProperty(pagination.FilterField[0]).GetValue(p, null).ToString()).ToList();

                return content;
            }
        }


        private class TagDbComparer : IEqualityComparer<TagDb>
        {
            public bool Equals(TagDb tag1, TagDb tag2)
            {
                //
                // See the full list of guidelines at
                //   http://go.microsoft.com/fwlink/?LinkID=85237
                // and also the guidance for operator== at
                //   http://go.microsoft.com/fwlink/?LinkId=85238
                //

                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(tag1, tag2)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(tag1, null) || Object.ReferenceEquals(tag2, null))
                    return false;

                return tag1 == tag2;
            }

            // override object.GetHashCode
            public int GetHashCode(TagDb tag)
            {
                // TODO: write your implementation of GetHashCode() here
                return tag.Title.GetHashCode();
            }
        }
        public List<TagDb> getAllTags(int userId, int folderId)
        {
            using (var ctx = new LHDTVContext())
            {
                // var retTemp = ctx.PhotoFolderMap.Include(pf => pf.Photo).ThenInclude(p => p.Tag).Where(pf => pf.Photo.UserId == userId && pf.FolderId != folderId).ToList();
                var ret = ctx.Photo.Include(p => p.Tag).Include(p => p.PhotosFolder)
                        .Where(p => p.UserId == userId &&
                            !p.PhotosFolder
                                .Where(pf => pf.FolderId == folderId)
                                .Select(pf => pf.PhotoId)
                                .Contains(p.Id)).Select(p => p.Tag.Select(t => t)).ToList();
                IEnumerable<TagDb> listTag = new List<TagDb>();

                foreach (var tags in ret)
                {
                    listTag = listTag.Concat(tags);
                }



                var r = listTag.GroupBy(x => x.Title).Select(x => x.First()).ToList();

                return r;


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

        public void RemovePhotoFromFolders(PhotoDb photo, int UserId){
            using (var ctx = new LHDTVContext())
            {
                List<PhotoFolderMap> temp = ctx.PhotoFolderMap.Where(pf => pf.PhotoId == photo.Id).ToList();
                ctx.PhotoFolderMap.RemoveRange(temp);
                ctx.SaveChanges();
            }
        }
    }
}