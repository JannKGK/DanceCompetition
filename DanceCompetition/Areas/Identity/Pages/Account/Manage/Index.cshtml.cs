using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DanceCompetition.Data;
using DanceCompetition.Models;

namespace DanceCompetition.Areas.Identity.Pages.Account.Manage
{
	public partial class IndexModel : PageModel
	{
		private readonly UserManager<DanceCompetitionUser> _userManager;
		private readonly SignInManager<DanceCompetitionUser> _signInManager;
        private readonly DanceCompetitionContext _context;

        public IndexModel(
						UserManager<DanceCompetitionUser> userManager,
						SignInManager<DanceCompetitionUser> signInManager,
                        DanceCompetitionContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;	
		}

		public string Username { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Phone]
			[Display(Name = "Phone number")]
			public string PhoneNumber { get; set; }
			[Display(Name = "Favourite Restaurant")]
			public string FavouriteRestaurant { get; set; }
		}

		private async Task LoadAsync(DanceCompetitionUser user)
		{
			var userName = await _userManager.GetUserNameAsync(user);
			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

			Username = userName;

			Input = new InputModel
			{
				PhoneNumber = phoneNumber,
				FavouriteRestaurant = user.FavoriteRestaurant
			};
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			await LoadAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}

			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			if (Input.PhoneNumber != phoneNumber)
			{
				var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
				if (!setPhoneResult.Succeeded)
				{
					StatusMessage = "Unexpected error when trying to set phone number.";
					return RedirectToPage();
				}
			}

            if (Input.FavouriteRestaurant != user.FavoriteRestaurant)
            {
                user.FavoriteRestaurant = Input.FavouriteRestaurant;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "Your profile has been updated";
			return RedirectToPage();
		}
	}
}
