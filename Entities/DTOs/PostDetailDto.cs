using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PostDetailDto:IDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Comment { get; set; }
        public int[] Fav { get; set; }
        public int[] Verify { get; set; }
        public bool  Status { get; set; }
        public string[] Roles { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }


    }
}
