using FluentValidation;

namespace Api.Apps.ClientApi.Dtos.UserDtos
{
	public class UserRegisterDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string FullName { get; set; }

	}

	public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDto> 
	{
		public UserRegisterDtoValidator()
		{
			RuleFor(x => x.UserName).MaximumLength(25).MinimumLength(4).NotEmpty();
			RuleFor(x => x.Email).MaximumLength(70).MinimumLength(6).NotEmpty();
			RuleFor(x => x.Password).MaximumLength(25).MinimumLength(8).NotEmpty();
			RuleFor(x => x.ConfirmPassword).MaximumLength(25).MinimumLength(8).NotEmpty();
			RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
			RuleFor(x => x.FullName).MaximumLength(25).MinimumLength(5).NotEmpty();
			RuleFor(x => x.Email).Matches("^\\S+@\\S+\\.\\S+$");
		}
	}
}
