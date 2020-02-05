﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Remotely.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Remotely.Server.Pages
{
    [Authorize]
    public class InviteModel : BaseModel
    {
        public InviteModel(DataService dataService)
        {
            this.DataService = dataService;
        }
        private DataService DataService { get; }
        public bool Success { get; set; }

        public class InputModel
        {
            public string InviteID { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public void OnGet(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ModelState.AddModelError("MissingID", "No invititation ID is specified.");
            }

            Input.InviteID = id;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Input?.InviteID))
            {
                Success = false;
                ModelState.AddModelError("MissingID", "No invititation ID is specified.");
                return Page();
            }

            var result = DataService.JoinViaInvitation(User.Identity.Name, Input.InviteID);
            if (result == false)
            {
                Success = false;
                ModelState.AddModelError("InviteIDNotFound", "The invitation ID wasn't found or is for another account.");
            }

            Success = true;
            return Page();
        }
    }
}