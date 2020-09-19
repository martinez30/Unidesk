using Domain;
using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace Application.Model
{
    public class LightPoleModel
    {
        public int Id { get; set; }
        public int? LocId { get; set; }
        public string Localization { get; set; }
        public int? NumberHouse { get; set; }
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = Messages.RequiredField)]
        public bool Active { get; set; }

        public LightPoleModel() { }

        public LightPoleModel(LightPole lightPole)
        {
            Id = lightPole.Id;
            LocId = lightPole.Localization.Id;
            Localization = $"{lightPole.Localization.Address} - {lightPole.Localization.Neighborhood} - {lightPole.Localization.City}/{lightPole.Localization.State}";
            NumberHouse = lightPole.Number;
            SerialNumber = lightPole.SerialNumber;
            Active = lightPole.Active;

        }
    }
}
