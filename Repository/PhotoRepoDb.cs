
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

                var query = ctx.Photo.Where(p => p.UserId == userId).Skip((pagination.NumPag - 1) * pagination.TamPag)
                        .Take(pagination.TamPag).ToList();
                // var f = query.Select(p => p.GetType().GetProperty(pagination.FilterField[0]).GetValue(p, null).ToString()).ToList();
                if (pagination.FilterField != null)
                {
                    // for (int i = 0; i < pagination.FilterField.Count; i++)
                    // {

                    //     query = query.Where(f => f.GetType().GetProperty(pagination.FilterField[i])
                    //         .GetValue(f, null).ToString() == pagination.FilterValue[i]);
                    // }

                }


                return query;
            }
        }

        public List<PhotoDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId, int folderId)
        {
            using (var ctx = new LHDTVContext())
            {

                var query = ctx.Photo.Include(p => p.PhotosFolder)
                        .Where(p => p.UserId == userId && 
                            !p.PhotosFolder
                                .Where(pf => pf.FolderId == folderId)
                                .Select(pf => pf.PhotoId)
                                .Contains(p.Id))
                        .Skip((pagination.NumPag - 1) * pagination.TamPag)
                        .Take(pagination.TamPag).ToList();
                // var f = query.Select(p => p.GetType().GetProperty(pagination.FilterField[0]).GetValue(p, null).ToString()).ToList();
                if (pagination.FilterField != null)
                {
                    // for (int i = 0; i < pagination.FilterField.Count; i++)
                    // {

                    //     query = query.Where(f => f.GetType().GetProperty(pagination.FilterField[i])
                    //         .GetValue(f, null).ToString() == pagination.FilterValue[i]);
                    // }

                }


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