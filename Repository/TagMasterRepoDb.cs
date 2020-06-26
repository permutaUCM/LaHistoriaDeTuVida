
using System.Collections.Generic;
using LHDTV.Models.DbEntity;




namespace LHDTV.Repo
{

    public class TagMasterRepoDb : ITagMasterRepoDb
    {

        public TagMasterRepoDb()
        {


        }

        public TagDb Read(int id , int userId){
            
            using(var ctx = new LHDTVContext()){

                var tag = ctx.TagDb.Include(p => p).ThenInclude(t => t).Include(p => p.Tag).FirstOrDefault(p => p.Id == id);
                return tag;

            }
                      
        }
        public List<TagDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId){
            
    
            using (var ctx = new LHDTVContext())
            {
                try
                {
                    var res = ctx.TagDb.Include(f => f.Tag)
                                        .Skip((pagination.NumPag - 1) * pagination.TamPag)
                                        .Take(pagination.TamPag)
                                        .ToList();
                    return res;
                }
                catch (System.Exception e)
                {
                    return new List<TagDb>();
                }


            }
            
        }

        public TagDb Create(TagDb entity,int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.TagDb.Add(entity);
                ctx.SaveChanges();
                return entity;
            }

        }

        public TagDb Delete(int id,int userId)
        {

            using (var ctx = new LHDTVContext())
            {
                var Tag = ctx.Remove(id);
                ctx.SaveChanges();

                return null;
            }

        }
        public TagDb Update(TagDb entity, int userId)
        {

            using (var ctx = new LHDTVContext())
            {
                ctx.TagDb.Update(entity);
                ctx.SaveChanges();

                return entity;
            }

        }
    }

}