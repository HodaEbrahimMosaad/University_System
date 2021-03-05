using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;
namespace University.Service
{
    public interface IPost
    {
        public List<Post>  GetAll();
        public Post GetDetails(int? id);
        public void Insert(Post post);

        public void UpdatePost(int PostID, string Body, string Title);

        public void DeletePost(int id);
    }
}
