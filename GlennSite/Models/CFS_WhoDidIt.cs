//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlennSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CFS_WhoDidIt
    {
        public string GameId { get; set; }
        public Nullable<int> WhoDidIt { get; set; }
    
        public virtual CFS_MurderFacts CFS_MurderFacts { get; set; }
    }
}
