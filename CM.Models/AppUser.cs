﻿using Microsoft.AspNetCore.Identity;
using System;

namespace CM.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {

        }
        //public DateTime? DateBanned { get; set; } May be another model
    }
}
