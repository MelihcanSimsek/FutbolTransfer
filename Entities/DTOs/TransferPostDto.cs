using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class TransferPostDto:IDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public int TransferId { get; set; }
        public int PlayerId { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Comment { get; set; }
        public int[] Fav { get; set; }
        public int[] Verify { get; set; }
        public bool Status { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public int PlayerAge { get; set; }
        public string PlayerNationality { get; set; }
        public string PlayerImage { get; set; }
        public string PlayerClub { get; set; }
        public string PlayerClubImage { get; set; }
        public string ClubName { get; set; }
        public string ClubImage { get; set; }
        public string UserProfileImage { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
        public int TransferRate { get; set; }

    }
}
