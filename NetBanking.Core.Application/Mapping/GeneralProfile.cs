﻿using AutoMapper;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModels.User;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Users

            #region SavingAccount

            CreateMap<SavingAccount, SaveSavingAccountVM>()
                .ReverseMap();

            #endregion

            #endregion
        }
    }
}
