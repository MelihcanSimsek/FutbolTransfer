using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPostDal : EfEntityFrameworkBase<Post, FutbolTransferContext>, IPostDal
    {
        public List<PostDetailDto> GetPostDetails(Expression<Func<PostDetailDto, bool>> filter = null)
        {
            using (var context = new FutbolTransferContext())
            {
                var result = from post in context.Posts
                             join user in context.Users
                             on post.UserId equals user.Id
                             join profile in context.Profiles
                             on user.Id equals profile.UserId
                             select new PostDetailDto
                             {
                                 CreationDate = post.CreationDate,
                                 UserId = user.Id,
                                 Comment=post.Comment,
                                 Content=post.Content,
                                 Fav= context.Favs.Where(f=>f.PostId == post.Id).Select(f=>f.UserId).ToArray(),
                                 ParentId = post.ParentId,
                                 PostId=post.Id,
                                 ProfileImage = profile.ProfileImage,
                                 Roles = context.OperationClaims.Where(oc => context.UserOperationClaims
                                 .Any(uoc => uoc.UserId == user.Id && uoc.OperationClaimId == oc.Id))
                                 .Select(oc => oc.Name)
                                 .ToArray(),
                                 Status =post.Status,
                                 UserName=user.Name,
                                 Verify= context.Verifies.Where(v => v.PostId == post.Id).Select(v => v.UserId).ToArray()

                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

   
        public List<TransferPostDto> GetTransferPosts(Expression<Func<TransferPostDto, bool>> filter = null)
        {
            using (var context = new FutbolTransferContext())
            {
                var result = from post in context.Posts
                             join transfer in context.Transfers
                             on post.Id equals transfer.PostId
                             join club in context.Clubs
                             on transfer.TeamId equals club.Id
                             join player in context.Players
                             on transfer.PlayerId equals player.Id
                             join user in context.Users
                             on post.UserId equals user.Id
                             join profile in context.Profiles
                             on post.UserId equals profile.UserId
                             select new TransferPostDto
                             {
                                 PostId = post.Id,
                                 TeamId = club.Id,
                                 UserId = user.Id,
                                 TransferId = transfer.Id,
                                 PlayerId = player.Id,
                                 ClubImage = club.Image,
                                 ClubName = club.Team,
                                 Comment = post.Comment,
                                 Content = post.Content,
                                 CreationDate = post.CreationDate,
                                 Fav = context.Favs.Where(f => f.PostId == post.Id).Select(f => f.UserId).ToArray(),
                                 PlayerAge = player.Age,
                                 PlayerClub = player.Club,
                                 PlayerImage = player.ProfileImage,
                                 PlayerName = player.Name,
                                 PlayerNationality = player.Nationality,
                                 PlayerPosition = player.Position,
                                 Roles = context.OperationClaims.Where(oc => context.UserOperationClaims
                                 .Any(uoc => uoc.UserId == user.Id && uoc.OperationClaimId == oc.Id))
                                 .Select(oc => oc.Name)
                                 .ToArray(),
                                 UserName = user.Name,
                                 UserProfileImage = profile.ProfileImage,
                                 Verify = context.Verifies.Where(v=>v.PostId == post.Id).Select(v=>v.UserId).ToArray(),
                                 Status = post.Status,
                                 PlayerClubImage = context.Clubs.FirstOrDefault(c => c.Team == player.Club).Image
                             };



                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
