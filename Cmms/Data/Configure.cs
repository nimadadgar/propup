
using Cmms.Core;
using Cmms.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cmms
{
  public static class InitialDb
    {
    public static bool InitialMetal(ApplicationContext ctx)
        {
      var client = ctx.Database.GetCosmosClient();
      ctx.Database.EnsureDeleted();
     ctx.Database.EnsureCreated();

     string metaStrings= File.ReadAllText("data/meta.json");
     var docs= Newtonsoft.Json.JsonConvert.DeserializeObject<MetaParent>(metaStrings);

            foreach (var l in docs.AccessLevels)
            {
        ctx.SetPartitionKey(l);
        ctx.Add(l);
        ctx.SaveChanges();
      }

            foreach (var l in docs.Jobs)
            {
                ctx.SetPartitionKey(l);
                ctx.Add(l);
                ctx.SaveChanges();
            }

            foreach (var l in docs.Factories)
            {
                ctx.SetPartitionKey(l);
                ctx.Add(l);
                ctx.SaveChanges();
            }


            return true;
      
    }
    }

    public class MetaParent
    {
        public List<AccessLevel> AccessLevels { get; set; }
        public List<Job> Jobs { get; set; }
        public List<Factory> Factories { get; set; }

    }
}