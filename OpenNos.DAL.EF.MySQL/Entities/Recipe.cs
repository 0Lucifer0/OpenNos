namespace OpenNos.DAL.EF.MySQL
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Recipe")]
    public partial class Recipe
    {
        #region Instantiation

        public Recipe()
        {
            RecipeItem = new HashSet<RecipeItem>();
        }

        #endregion

        #region Properties

        public byte Amount { get; set; }
        public virtual Item Item { get; set; }
        public short ItemVNum { get; set; }
        public virtual MapNpc MapNpc { get; set; }
        public int MapNpcId { get; set; }
        public short RecipeId { get; set; }
        public virtual ICollection<RecipeItem> RecipeItem { get; set; }

        #endregion
    }
}