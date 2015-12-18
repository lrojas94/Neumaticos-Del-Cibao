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
    using System.Xml.Serialization;

    [Serializable]
    public partial class ShoppingBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoppingBill()
        {
            this.Credit = "f";
            this.ITBIS = "f";
            this.ITBISPercent = 0D;
            this.ShoppingBillsArticles = new HashSet<ShoppingBillsArticle>();
        }
    
        public long Id { get; set; }
        public Nullable<long> Company { get; set; }
        public string Date { get; set; }
        public string Credit { get; set; }
        public string ITBIS { get; set; }
        public Nullable<double> ITBISPercent { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [XmlIgnoreAttribute]
        public virtual ICollection<ShoppingBillsArticle> ShoppingBillsArticles { get; set; }
        public virtual CreditShoppingBill CreditShoppingBill { get; set; }
    }
}
