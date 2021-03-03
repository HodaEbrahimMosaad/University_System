using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Models;
using MVCCore03Osama.Data;
namespace MVCCore03Osama.Service
{
    public class PostRepoService:IPost
    {
        private readonly ApplicationDbContext context;
        public PostRepoService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Post> GetAll()
        {
            return context.posts.ToList();
        }

        public Post GetDetails(int? id)
        {
            return context.posts.Find(id);
        }
        public void Insert(Post p)
        {   
            context.posts.Add(p);
            context.SaveChanges();
        }


        public void UpdatePost(int PostID,string Body , string Title)
        {
            Post P = context.posts.Find(PostID);
            P.Body = Body;
            P.Title =Title;

            context.SaveChanges();
        }
        public void DeletePost(int id)
        {
            context.Remove(context.posts.Find(id));
            context.SaveChanges();
        }
    }
}
