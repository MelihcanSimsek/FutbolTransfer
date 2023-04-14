using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPostDal : EfEntityFrameworkBase<Post, FutbolTransferContext>, IPostDal
    {
        public List<PostDetailDto> GetPostDetails(Expression<Func<PostDetailDto, bool>>? filter = null)
        {
            using (var context = new FutbolTransferContext())
            {
                var result = from post in context.Posts
                             join user in context.Users
                             on post.UserId equals user.Id
                             join profile in context.Profiles
                             on post.UserId equals profile.UserId
                             join useroperationclaim in context.UserOperationClaims
                             on user.Id equals useroperationclaim.OperationClaimId
                             select new PostDetailDto
                             {
                                 CreationDate = post.CreationDate,
                                 UserId = user.Id,
                                 Comment=post.Comment,
                                 Content=post.Content,
                                 Fav=post.Fav,
                                 ParentId = post.ParentId,
                                 PostId=post.Id,
                                 ProfileImage = profile.ProfileImage,
                                 Roles = context.OperationClaims.Where(oc => oc.Id == useroperationclaim.OperationClaimId).Select(oc => oc.Name).ToArray(),
                                 Status=post.Status,
                                 UserName=user.Name
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public List<TransferPostDto> GetTransferPosts(Expression<Func<TransferPostDto, bool>>? filter = null)
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
                             join useroperationclaim in context.UserOperationClaims
                             on post.UserId equals useroperationclaim.UserId
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
                                 Fav = post.Fav,
                                 PlayerAge = player.Age,
                                 PlayerClub = player.Club,
                                 PlayerImage = player.ProfileImage,
                                 PlayerName = player.Name,
                                 PlayerNationality = player.Nationality,
                                 PlayerPosition = player.Position,
                                 Roles = context.OperationClaims.Where(oc=>oc.Id == useroperationclaim.OperationClaimId).Select(oc=>oc.Name).ToArray(),
                                 UserName = user.Name,
                                 UserProfileImage = profile.ProfileImage,
                                 Verify = post.Verify,
                                 Status = post.Status
                             };



                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
