using System;
using System.Collections.Generic;

namespace Domain
{
    public class Order : BaseEntity
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Name { get; set; }
        public LightPole LightPole { get; set; }
        public string RequestDescription { get; set; }
        public Problems? Problem { get; set; }
        public string ResponseDescription { get; set; }
        public List<Service> Proviced_Services { get; set; }
        public StatusOrder Status { get; set; }

        protected Order()
        {
        }

        public Order(DateTime? begin,string name, string requestDescription, Problems? problem, LightPole lightPole)
        {
            InitialDate = begin;
            LightPole = lightPole;
            Name = name;
            RequestDescription = requestDescription;
            Problem = problem;
            Status = StatusOrder.Open;
        }

        public Order(int id)
        {
            Id = id;
        }

    }
}
