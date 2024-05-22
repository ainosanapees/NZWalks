﻿namespace NZWalks.Api.Models.DTO
{
    public class AddRegionRequest
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string Code { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Population { get; set; }
    }
}
