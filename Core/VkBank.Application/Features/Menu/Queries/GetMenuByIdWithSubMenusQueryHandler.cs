﻿using MediatR;
using VkBank.Application.Interfaces.Repositories;
using VkBank.Application.Validations.Menu;
using VkBank.Domain.Contstants;
using VkBank.Domain.Entities;
using VkBank.Domain.Results.Data;

namespace VkBank.Application.Features.Menu.Queries
{
    public class GetMenuByIdWithSubMenusQueryRequest : IRequest<IDataResult<List<EntityMenu>>>
    {
        public long Id { get; set; }
    }

    public class GetMenuByIdWithSubMenusQueryHandler : IRequestHandler<GetMenuByIdWithSubMenusQueryRequest, IDataResult<List<EntityMenu>>>
    {
        private readonly GetMenuByIdWithSubMenusQueryRequestValidator _validator;
        private readonly IMenuQueryRepository _menuQueryRepository;

        public GetMenuByIdWithSubMenusQueryHandler(GetMenuByIdWithSubMenusQueryRequestValidator validator, IMenuQueryRepository menuQueryRepository)
        {
            _validator = validator;
            _menuQueryRepository = menuQueryRepository;
        }

        public async Task<IDataResult<List<EntityMenu>>> Handle(GetMenuByIdWithSubMenusQueryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                string errorMessages = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
                return new ErrorDataResult<List<EntityMenu>>(errorMessages);
            }

            var result = await _menuQueryRepository.GetMenuByIdWithSubMenusAsync(request.Id, cancellationToken);
            if (result == null)
            {
                return new ErrorDataResult<List<EntityMenu>>(ResultMessages.MenuNoData);
            }
            return new SuccessDataResult<List<EntityMenu>>(result.ToList());
        }
    }
}
