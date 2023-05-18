﻿using Infrastructure.Models;

namespace RestArchitecture.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CatalogId { get; set; }
    }
}
