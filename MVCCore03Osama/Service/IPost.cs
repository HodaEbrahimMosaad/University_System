using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCCore03Osama.Models;
namespace MVCCore03Osama.Service
{
    public interface IPost
    {
        public List<Post>  GetAll();
        public Post GetDetails(int? id);
        public void Insert(Post post);
        public void UpdatePost(int id, Post post);
        public void DeletePost(int id);
    }
}
