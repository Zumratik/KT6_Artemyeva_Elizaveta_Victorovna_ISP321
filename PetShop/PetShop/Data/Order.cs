//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetShop.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public int IdProduct { get; set; }
        public string Article { get; set; }
        public int Count { get; set; }
        public System.DateTime DataOrder { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public int IdPickupPoint { get; set; }
        public Nullable<int> IdUser { get; set; }
        public int Code { get; set; }
        public int IdStatus { get; set; }
    
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PicPoints PicPoints { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}