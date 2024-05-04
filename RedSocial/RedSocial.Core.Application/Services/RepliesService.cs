using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comment;
using RedSocial.Core.Application.ViewModels.Replies;
using RedSocial.Core.Domain.Entities;
using StockApp.Core.Application.Services;
using System;


namespace RedSocial.Core.Application.Services
{
    public class RepliesService : GenericService<SaveRepliesViewModel, RepliesViewModel, Replies>, IRepliesServices
    {
        private readonly IRepliesRepository _repliesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;

        public override async Task<SaveRepliesViewModel> Add(SaveRepliesViewModel vm)
        {
            vm.Usuarioid = userViewModel.Id;
            return await base.Add(vm);
        }

        public override async Task Update(SaveRepliesViewModel vm, int id)
        {
            vm.Usuarioid = userViewModel.Id;
            await base.Update(vm, id);
        }
        public RepliesService(IRepliesRepository repliesRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repliesRepository, mapper)
        {
            _repliesRepository = repliesRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
    }
}