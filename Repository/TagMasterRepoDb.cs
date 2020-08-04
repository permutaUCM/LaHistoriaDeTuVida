using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using Microsoft.EntityFrameworkCore;



namespace LHDTV.Repo
{

    public class TagMasterRepoDb : ITagMasterRepoDb
    {

        public TagMasterRepoDb()
        {


        }

        public PhotoTagsTypes Read(string title , int userId){
            
            using(var ctx = new LHDTVContext()){

                var tag = ctx.TagTypeMaster.Include(p => p).ThenInclude(t => t).Include(p => p.Name).FirstOrDefault(p => p.Name == title);
                return tag;

            }
                      
        }
        public List<PhotoTagsTypes> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId){
            
    
            using (var ctx = new LHDTVContext())
            {
                try
                {
                    var res = ctx.TagTypeMaster.Include(f => f.Name)
                                        .Skip((pagination.NumPag - 1) * pagination.TamPag)
                                        .Take(pagination.TamPag)
                                        .ToList();
                    return res;
                }
                catch (System.Exception e)
                {
                    return new List<PhotoTagsTypes>();
                }


            }
            
        }

        public PhotoTagsTypes Create(PhotoTagsTypes tg,int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.TagTypeMaster.Add(tg);
                ctx.SaveChanges();
                return tg;
            }

        }

        public PhotoTagsTypes Delete(string tg,int userId)
        {

            using (var ctx = new LHDTVContext())
            {
                var Tag = ctx.Remove(tg);
                ctx.SaveChanges();

                return null;
            }

        }
        public PhotoTagsTypes Update(PhotoTagsTypes tg, int userId)
        {

            using (var ctx = new LHDTVContext())
            {
                ctx.TagTypeMaster.Update(tg);
                ctx.SaveChanges();

                return tg;
            }

        }
    }

}