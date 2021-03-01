using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Models;
using MVCCore03Osama.Data;

namespace MVCCore03Osama.Service
{
    public class CommentRepoService:IComment
    {
        private readonly ApplicationDbContext context;
        public CommentRepoService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Comment> GetAll()
        {
            return context.comments.ToList();
        }

        public Comment GetDetails(int id)
        {
            return context.comments.Find(id);
        }
        public void Insert(Comment com)
        {
            context.comments.Add(com);
            context.SaveChanges();
        }

        public void UpdateComment(int ComID, Comment Com)
        {
            Comment C = context.comments.Find(ComID);
            C.Body = Com.Body;
            C.Date = Com.Date;
            C.ID = Com.ID;
            C.PostID = Com.PostID;
            C.Post = Com.Post;
           
            
            context.SaveChanges();
        }
        public void DeleteComment(int id)
        {
            context.Remove(context.comments.Find(id));
            context.SaveChanges();
        }
    }
}
