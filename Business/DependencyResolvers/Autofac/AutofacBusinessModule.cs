using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostManager>().As<IPostService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<FollowerManager>().As<IFollowerService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<ProfileManager>().As<IProfileService>();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<EfPostDal>().As<IPostDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<EfFollowerDal>().As<IFollowerDal>();
            builder.RegisterType<EfProfileDal>().As<IProfileDal>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();
          

        }


    }
}
