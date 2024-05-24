﻿using FluentValidation;
using VkBank.Application.Features.Menu.Commands;
using VkBank.Application.Validations.Common;

namespace VkBank.Application.Validations.Menu
{
    public class UpdateMenuValidator : AbstractValidator<UpdateMenuCommandRequest>
    {
        public UpdateMenuValidator()
        {
            RuleFor(r => r.Id).ValidateId();
            RuleFor(r => r.ParentId).ValidateMenuParentId();
            RuleFor(r => r.Name_TR).ValidateMenuName();
            RuleFor(r => r.Name_EN).ValidateMenuName();
            RuleFor(r => r.ScreenCode).ValidateMenuScreenCode();
            RuleFor(r => r.Type).ValidateMenuType();
            RuleFor(r => r.Priority).ValidateMenuPriority();
            RuleFor(r => r.Keyword).ValidateMenuKeyword();
            RuleFor(r => r.Icon).ValidateMenuIcon();
            RuleFor(r => r.IsGroup).ValidateMenuIsGroup();
            RuleFor(r => r.IsNew).ValidateMenuIsNew();
            RuleFor(r => r.NewStartDate).ValidateMenuNewStartDate();
            RuleFor(r => r.NewEndDate).ValidateMenuNewEndDate(r => r.NewStartDate);
            RuleFor(r => r.IsActive).ValidateIsActive();
        }
    }
}