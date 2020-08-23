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

                var tag = ctx.TagTypeMaster.Include(t => t.Extra1).Include(t => t.Extra2).Include(t => t.Extra3).FirstOrDefault(p => p.Name == title);
                return tag;

            }
                      
        }
        public List<PhotoTagsTypes> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId){
            
    
            using (var ctx = new LHDTVContext())
            {
                try
                {
                    var res = ctx.TagTypeMaster.Include(tag => tag.Extra1).Include(tag => tag.Extra2).Include(tag => tag.Extra3).ToList();
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
                var result = ctx.TagTypeMaster.Add(tg);
                ctx.SaveChanges();
                return result.Entity;
            }

        }

        public PhotoTagsTypes Delete(string tg,int userId)
        {

            using (var ctx = new LHDTVContext())
            {
                
                var tag = ctx.TagTypeMaster.Remove(ctx.TagTypeMaster.FirstOrDefault(t => t.Name == tg));
                ctx.SaveChanges();

                return null;
            }

        }
        public PhotoTagsTypes Update(PhotoTagsTypes tg, int userId)
        {

            using (var ctx = new LHDTVContext())
            {
                if(tg.Extra1 == null){
                    tg.Extra1Name = null;
                }
                if(tg.Extra2 == null){
                    tg.Extra2Name = null;
                }
                if(tg.Extra3 == null){
                    tg.Extra3Name = null;
                }
                ctx.TagTypeMaster.Update(tg);

                ctx.SaveChanges();

                return tg;
            }

        }

        public List<Extra> GetAllExtras(){
            using (var ctx = new LHDTVContext())
            {
                var extras = ctx.Extra.ToList();

                return extras;
            }
        }

    }

}