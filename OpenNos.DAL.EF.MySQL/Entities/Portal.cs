namespace OpenNos.DAL.EF.MySQL
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Portal")]
    public partial class Portal
    {
        #region Properties

        public short DestinationMapId { get; set; }
        public short DestinationX { get; set; }
        public short DestinationY { get; set; }
        public bool IsDisabled { get; set; }
        public virtual Map Map { get; set; }
        public virtual Map Map1 { get; set; }
        public int PortalId { get; set; }

        public short SourceMapId { get; set; }
        public short SourceX { get; set; }

        public short SourceY { get; set; }
        public sbyte Type { get; set; }

        #endregion
    }
}