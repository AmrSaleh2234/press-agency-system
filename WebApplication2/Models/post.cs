//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public post()
        {
            this.comment = new HashSet<comment>();
            this.like = new HashSet<like>();
            this.favourite = new HashSet<favourite>();
        }
    
        public int Tid { get; set; }
        public int cid { get; set; }
        public string artucle_title { get; set; }
        public string article_body { get; set; }
        public string post_creatin { get; set; }
        public string article_type { get; set; }
        public Nullable<int> numbers_of_views { get; set; }
        public string image { get; set; }
        public Nullable<int> accept { get; set; }
        public Nullable<int> likes { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> isNew { get; set; }
    
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like> like { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<favourite> favourite { get; set; }
    }
}
