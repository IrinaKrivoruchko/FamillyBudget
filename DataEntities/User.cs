﻿namespace DataEntities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Wallet Wallet { get; set; }
    }
}
