﻿using System;

namespace Spotifalso.Aplication.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get;  set; }
        public string ProfilePhotoId { get; set; }
        public string Role { get; set; }
        public string Nickname { get; set; }
        public string Bio { get; set; }
    }
}
