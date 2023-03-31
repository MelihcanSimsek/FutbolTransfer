using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string PostAdded = "Post eklendi";
        public static string PostDeleted = "Post silindi";
        public static string PostUpdated = "Post güncellendi";
        public static string PostListed = "Postlar listelendi";
        public static string FollowerAdded = "Takipçi eklendi";
        public static string FollowerDeleted = "Takipçi silindi";
        public static string FollowerListed = "Takipçi listelendi";
        public static string FollowerUpdated = "Takipçi güncellendi";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string UserListed = "Kullanıcı listelendi";
        public static string UserUpdated = "Kullanıcı güncellendi";
        public static string CommentListed = "Yorumlar listelendi";
        public static string MainPostListed = "Ana gönderi listelendi";
        public static string TokenCreated = "Token oluşturuldu";
        public static string PasswordError = "Parola hatası";
        public static string SuccessLogin = "Giriş Başarılı";
        public static string UserAlreadyHave = "Kullanıcı halihazırda mevcut";
        public static string UserAlreadyExists = "Kullanıcı var";
        public static string UserRegistered = "Kullanıcı kaydedildi";
    }
}
