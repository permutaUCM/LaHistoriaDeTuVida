

using System;
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using LHDTV.Models.Forms;
using Microsoft.EntityFrameworkCore;



namespace LHDTV.Repo
{


    public class FolderRepoDb : IFolderRepo
    {

        public FolderRepoDb()
        {


        }

        public FolderDb Create(FolderDb entity, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.Folder.Add(entity);
                ctx.SaveChanges();
                return entity;
            }

        }


        public FolderDb Read(int id, int userId)
        {

            //TODO No debe devolver todas las fotos de la carpeta, si no no se puede filtrar ni paginar.
            using (var ctx = new LHDTVContext())
            {
                var folder = ctx.Folder.Include(f => f.PhotosTags)
                .Include(f => f.PhotosFolder).ThenInclude(pf => pf.Photo).ThenInclude(p => p.Tag)
                .Where(f => f.Deleted == false).FirstOrDefault(f => f.Id == id);
                return folder;
            }

        }

        public FolderDb Read(int id, Pagination p, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                var folder = ctx.Folder.Include(f => f.PhotosTags).Where(f => f.Deleted == false)
                    .FirstOrDefault(f => f.Id == id && f.UserId == userId);

                var photoFolderListQuery = ctx.PhotoFolderMap.Include(pf => pf.Photo).ThenInclude(p => p.Tag)
                    .Where(pf => pf.FolderId == folder.Id);


                foreach (var filter in p.Filter)
                {
                    if (filter.Key == "dateIni")
                    {
                        DateTime dateIni;
                        if (!DateTime.TryParse(filter.Value, out dateIni))
                            continue;
                        photoFolderListQuery = photoFolderListQuery.Where(p => p.Photo.RealDate >= dateIni);
                    }
                    else if (filter.Key == "dateEnd")
                    {
                        DateTime dateEnd;
                        if (!DateTime.TryParse(filter.Value, out dateEnd))
                            continue;
                        photoFolderListQuery = photoFolderListQuery.Where(p => p.Photo.RealDate <= dateEnd);
                    }
                    else if (filter.Key == "tags")
                    {
                        if (filter.Value.Length == 0)
                            continue;
                        var tagList = filter.Value.Split(";");
                        photoFolderListQuery = photoFolderListQuery.Where(p => p.Photo.Tag.Where(t => tagList.ToList().Contains(t.Title)).Any());

                    }
                }
                
                var photoFolderList = photoFolderListQuery.ToList().Skip((p.NumPag - 1) * p.TamPag).Take(p.TamPag).ToList();
                folder.PhotosFolder = photoFolderList;
                return folder;
            }

        }
        // update (datos generales)
        public FolderDb Update(FolderDb entity, int userId)
        {

            using (var ctx = new LHDTVContext())
            {
                ctx.Folder.Update(entity);
                ctx.SaveChanges();

                return entity;
            }

        }


        public FolderDb Delete(int id, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                // control de errores??
                var folder = ctx.Remove(id);
                ctx.SaveChanges();

                return null;
            }


        }
        // repasar
        public List<FolderDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                try
                {
                    var query = ctx.Folder.Where(f => f.User.Id == userId).Where(f => f.Deleted == false);
                    var filt = pagination.Filter;
                    if(filt != null){
                        foreach(var pair in filt){
                            var key = pair.Key;
                            var val = pair.Value;

                            switch (key.ToLower())
                            {
                                case "title":
                                    query = query.Where(f => f.Title.Contains(val));
                                    break;
                                
                                default:
                                    break;
                            }
                        }
                    }

                    var res = query.Include(f => f.DefaultPhoto).Skip((pagination.NumPag - 1) * pagination.TamPag)
                                .Take(pagination.TamPag)
                                .ToList();
                    return res;
                }
                catch (System.Exception e)
                {
                    return new List<FolderDb>();
                }


            }

        }
        // recibe una lista de fotos la mete en la carpeta, funcion pendiente

        public FolderDb AddPhotoToFolder(FolderDb entFolder, PhotoDb photo, int userId)
        {


            using (var ctx = new LHDTVContext())
            {

                var newMap = ctx.PhotoFolderMap.Add(new PhotoFolderMap()
                {
                    PhotoId = photo.Id,
                    FolderId = entFolder.Id,
                });
                ctx.SaveChanges();

                var ret = ctx.Folder.FirstOrDefault(f => f.Id == entFolder.Id);

                return ret;
            }

        }


        //eliminar una foto de una carpeta (dado una id de carpeta y una id de photo)

        public FolderDb deletePhotosToFolder(int id, List<PhotoDb> photos, int userId)
        {


            using (var ctx = new LHDTVContext())
            {

                ctx.PhotoFolderMap.RemoveRange(photos.Select(p => new PhotoFolderMap()
                {
                    FolderId = id,
                    PhotoId = p.Id
                }).ToList());

                ctx.SaveChanges();

                var ret = ctx.Folder.Include(f => f.PhotosFolder).ThenInclude(pf => pf.Photo).FirstOrDefault(f => f.Id == id);

                return ret;
            }

        }





        //Mirar el actualizar photo por defecto

        public FolderDb updateDefaultPhotoToFolder(FolderDb folder, PhotoDb p, int userId)
        {

            using (var ctx = new LHDTVContext())
            {

                folder.DefaultPhoto = p;
                ctx.Folder.Update(folder);
                ctx.SaveChanges();

                return folder;
            }



        }

        public List<LHDTV.Models.DbEntity.PhotoTransition> GetTransitionMetadata()
        {
            using (var ctx = new LHDTVContext())
            {
                var transitions = ctx.PhotoTransition.ToList();

                return transitions;
            }
        }

        public bool ExistsPhoto(int folderId, int photoId)
        {

            using (var ctx = new LHDTVContext())
            {
                var exists = ctx.PhotoFolderMap.Where(pf => pf.PhotoId == photoId && pf.FolderId == folderId).SingleOrDefault() != null;
                return exists;
            }

        }


    }





}