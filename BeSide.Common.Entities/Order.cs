using System;

namespace BeSide.Common.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public int Price { get; set; }
        public DateTime Deadline { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public int IdService { get; set; }
        //Предоставляет услугу
        public int IdProvider { get; set; }
        //Выполняет услугу
        public int IdClient { get; set; }
    }
}
