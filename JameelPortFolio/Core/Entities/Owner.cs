﻿namespace Core.Entities
{
    public class Owner : EntityBase
    {
        public string FullName { get; set; }
        public string Portfil { get; set; }
        public string Avatar { get; set; }
        public Address Address { get; set; }
    }
}
