using Domain;
using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application.Model
{
    public class CreateOrderModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public DateTime? InitialDate { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public string Name { get; set; }
        public int LightPoleId { get; set; }
        public string Localization { get; set; }

        public string RequestDescription { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public Problems? Problem { get; set; }

        public string ProblemName => Problem?.GetDescription();

        public StatusOrder? Status { get; set; }

        public string StatusName => Status?.GetDescription();

        public CreateOrderModel()
        {

        }

        public CreateOrderModel(
            int id, 
            DateTime? date,
            string localization,
            int lightPoleId,
            string name, 
            Problems? problem, 
            StatusOrder? status, 
            string requestDescription
            )
        {
            Id = id;
            InitialDate = date;
            Localization = localization;
            LightPoleId = lightPoleId;
            Name = name;
            Problem = problem;
            Status = status;
            RequestDescription = requestDescription;
        }
    }
}
