﻿using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using SUPBank.Application.Interfaces.Repositories;
using SUPBank.Application.Validations.Menu;
using SUPBank.Domain.Contstants;
using SUPBank.Domain.Results;
using SUPBank.Domain.Results.Data;
using SUPBank.Domain.Entities;

namespace SUPBank.Application.Features.Menu.Commands
{
    public class CreateMenuCommandRequest : IRequest<IResult>
    {
        [Range(0, long.MaxValue)]
        public required long ParentId { get; set; }

        [MinLength(LengthLimits.MenuNameMinLength)]
        [MaxLength(LengthLimits.MenuNameMaxLength)]
        public required string Name_TR { get; set; }

        [MinLength(LengthLimits.MenuNameMinLength)]
        [MaxLength(LengthLimits.MenuNameMaxLength)]
        public required string Name_EN { get; set; }

        [Range(10001, int.MaxValue)]
        public required int ScreenCode { get; set; }

        [Range(1, byte.MaxValue)]
        public required byte Type { get; set; }

        [Range(1, int.MaxValue)]
        public required int Priority { get; set; }

        [MinLength(LengthLimits.MenuKeywordMinLength)]
        [MaxLength(LengthLimits.MenuKeywordMaxLength)]
        public required string Keyword { get; set; }

        [MinLength(LengthLimits.MenuIconMinLength)]
        [MaxLength(LengthLimits.MenuIconMaxLength)]
        public string? Icon { get; set; }

        public required bool IsGroup { get; set; }

        public required bool IsNew { get; set; }

        public DateTime? NewStartDate { get; set; }

        public DateTime? NewEndDate { get; set; }

        public required bool IsActive { get; set; }
    }

    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommandRequest, IResult>
    {
        private readonly CreateMenuValidator _validator;
        private readonly IMapper _mapper;
        private readonly IMenuQueryRepository _menuQueryRepository;
        private readonly IMenuCommandRepository _menuCommandRepository;

        public CreateMenuCommandHandler(CreateMenuValidator validator, IMapper mapper, IMenuQueryRepository menuQueryRepository, IMenuCommandRepository menuCommandRepository)
        {
            _validator = validator;
            _mapper = mapper;
            _menuQueryRepository = menuQueryRepository;
            _menuCommandRepository = menuCommandRepository;
        }

        public async Task<IResult> Handle(CreateMenuCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ErrorResult(string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage)));
            }

            if (request.ParentId != 0 && await _menuQueryRepository.IsParentIdExistsInMenuAsync(request.ParentId, cancellationToken) == false)
            {
                return new ErrorResult(ResultMessages.MenuParentIdNotExist);
            }

            EntityMenu menu = _mapper.Map<EntityMenu>(request);

            long? menuId = await _menuCommandRepository.CreateMenuAndGetIdAsync(menu, cancellationToken);
            if (menuId == null)
            {
                return new ErrorResult(ResultMessages.MenuCreateError);
            }

            return new SuccessDataResult<long?>(ResultMessages.MenuCreateSuccess, menuId);
        }
    }
}
