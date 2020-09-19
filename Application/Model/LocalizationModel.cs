using Domain;
using Infra;
using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class LocalizationModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = Messages.RequiredField)]
        public string Address { get; set; }
        [Required(ErrorMessage = Messages.RequiredField)]
        public string Neighborhood { get; set; }
        [Required(ErrorMessage = Messages.RequiredField)]
        public string City { get; set; }
        [Required(ErrorMessage = Messages.RequiredField)]
        public State? State { get; set;}
        public string StateName => State?.GetDescription();

        public LocalizationModel() { }

        public LocalizationModel(Localization localization)
        {
            Id = localization.Id;
            Address = localization.Address;
            City = localization.City;
            Neighborhood = localization.Neighborhood;
            State = localization.State;
        }
    
        
    }
}
