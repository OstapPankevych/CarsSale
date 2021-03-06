﻿using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Identity.Entities;

namespace CarsSale.WebUi.Models.Advertisement
{
    public class AdvertisementViewModel
    {
        public CarsSaleUser User { get; set; }
        public Region Region { get; set; }
        public DataAccess.DTO.Advertisement Advertisement { get; set; }
    }
}