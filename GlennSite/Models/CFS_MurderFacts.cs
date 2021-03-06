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
    
    public partial class CFS_MurderFacts
    {
        public string GameId { get; set; }
        public Nullable<int> VictimId { get; set; }
        public string VictimFName { get; set; }
        public string VictimLName { get; set; }
        public string VictimPlace { get; set; }
        public string MurdererSex { get; set; }
        public Nullable<int> MurdererLocationId { get; set; }
        public Nullable<int> MurdererAreaId { get; set; }
        public Nullable<int> MurdererWeapon38 { get; set; }
        public Nullable<int> MurdererWeapon45 { get; set; }
        public Nullable<int> MurdererFingerPrints38Odd { get; set; }
        public Nullable<int> MurdererFingerPrints38Even { get; set; }
        public Nullable<int> MurdererFingerPrints45Odd { get; set; }
        public Nullable<int> MurdererFingerPrints45Even { get; set; }
    
        public virtual CFS_Alibi CFS_Alibi { get; set; }
        public virtual CFS_WhoDidIt CFS_WhoDidIt { get; set; }
        public virtual CFS_WhoWasWhere CFS_WhoWasWhere { get; set; }
    }
}
