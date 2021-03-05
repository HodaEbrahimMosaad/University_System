using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;
namespace University.Service
{
    public interface IComment
    {
        public List<Comment> GetAll();
        public Comment GetDetails(int id);
        public void Insert(Comment Com);
        public void UpdateComment(int id, Comment Com);
        public void DeleteComment(int id);
    }
}
