//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Neumaticos_del_Cibao.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShoppingBillsArticle1
    {
        public long ShoppingBillId { get; set; }
        public long ArticleId { get; set; }
        public double ArticlePrice { get; set; }
        public double ArticleCount { get; set; }
        public long Id { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual ShoppingBill1 ShoppingBill { get; set; }
    }
}
