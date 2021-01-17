using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class ProductRepository
    {
        public ProductRepository()
        {
        }
        public static async Task<string> AsyncInsertSingleItem(Product product)
        {
            product.UserCreated = 0;
            product.UserUpdated = 0;
            product.DateCreated = DateTime.Now;
            product.DateUpdated = DateTime.Now;
            SqlParameter[] parameters =
            {
new SqlParameter("@ID",product.ID),
new SqlParameter("@UserCreated",product.UserCreated),
new SqlParameter("@DateCreated",product.DateCreated),
new SqlParameter("@UserUpdated",product.UserUpdated),
new SqlParameter("@DateUpdated",product.DateUpdated),
new SqlParameter("@ParentID",product.ParentID),
new SqlParameter("@Note",product.Note),
new SqlParameter("@Active",product.Active),
new SqlParameter("@CategoryID",product.CategoryID),
new SqlParameter("@Title",product.Title),
new SqlParameter("@URLCode",product.URLCode),
new SqlParameter("@MetaTitle",product.MetaTitle),
new SqlParameter("@MetaKeyword",product.MetaKeyword),
new SqlParameter("@MetaDescription",product.MetaDescription),
new SqlParameter("@Tags",product.Tags),
new SqlParameter("@Author",product.Author),
new SqlParameter("@Image",product.Image),
new SqlParameter("@ImageThumbnail",product.ImageThumbnail),
new SqlParameter("@Description",product.Description),
new SqlParameter("@ContentMain",product.ContentMain),
new SqlParameter("@Price",product.Price),
new SqlParameter("@PriceUnitID",product.PriceUnitID),
new SqlParameter("@DatePublish",product.DatePublish),
new SqlParameter("@Page",product.Page),
new SqlParameter("@TitleEnglish",product.TitleEnglish),
new SqlParameter("@FileName",product.FileName),
new SqlParameter("@Liked",product.Liked),
new SqlParameter("@Comment",product.Comment),
new SqlParameter("@Share",product.Share),
new SqlParameter("@Reach",product.Reach),
new SqlParameter("@Duration",product.Duration),
new SqlParameter("@IsVideo",product.IsVideo),
new SqlParameter("@ArticleTypeID",product.ArticleTypeID),
new SqlParameter("@CompanyID",product.CompanyID),
new SqlParameter("@AssessID",product.AssessID),
new SqlParameter("@IndustryID",product.IndustryID),
new SqlParameter("@SegmentID",product.SegmentID),
new SqlParameter("@ProductID",product.ProductID),
new SqlParameter("@GUICode",product.GUICode),
new SqlParameter("@Source",product.Source),
new SqlParameter("@DescriptionEnglish",product.DescriptionEnglish),
new SqlParameter("@IsSummary",product.IsSummary),
new SqlParameter("@IsData",product.IsData),
new SqlParameter("@SourceID",product.SourceID),
new SqlParameter("@TargetID",product.TargetID),
};
            string result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductInsertSingleItem", parameters);
            return result;
        }
        public static string InsertSingleItem(Product product)
        {
            product.UserCreated = 0;
            product.UserUpdated = 0;
            product.DateCreated = DateTime.Now;
            product.DateUpdated = DateTime.Now;
            SqlParameter[] parameters =
            {
new SqlParameter("@ID",product.ID),
new SqlParameter("@UserCreated",product.UserCreated),
new SqlParameter("@DateCreated",product.DateCreated),
new SqlParameter("@UserUpdated",product.UserUpdated),
new SqlParameter("@DateUpdated",product.DateUpdated),
new SqlParameter("@ParentID",product.ParentID),
new SqlParameter("@Note",product.Note),
new SqlParameter("@Active",product.Active),
new SqlParameter("@CategoryID",product.CategoryID),
new SqlParameter("@Title",product.Title),
new SqlParameter("@URLCode",product.URLCode),
new SqlParameter("@MetaTitle",product.MetaTitle),
new SqlParameter("@MetaKeyword",product.MetaKeyword),
new SqlParameter("@MetaDescription",product.MetaDescription),
new SqlParameter("@Tags",product.Tags),
new SqlParameter("@Author",product.Author),
new SqlParameter("@Image",product.Image),
new SqlParameter("@ImageThumbnail",product.ImageThumbnail),
new SqlParameter("@Description",product.Description),
new SqlParameter("@ContentMain",product.ContentMain),
new SqlParameter("@Price",product.Price),
new SqlParameter("@PriceUnitID",product.PriceUnitID),
new SqlParameter("@DatePublish",product.DatePublish),
new SqlParameter("@Page",product.Page),
new SqlParameter("@TitleEnglish",product.TitleEnglish),
new SqlParameter("@FileName",product.FileName),
new SqlParameter("@Liked",product.Liked),
new SqlParameter("@Comment",product.Comment),
new SqlParameter("@Share",product.Share),
new SqlParameter("@Reach",product.Reach),
new SqlParameter("@Duration",product.Duration),
new SqlParameter("@IsVideo",product.IsVideo),
new SqlParameter("@ArticleTypeID",product.ArticleTypeID),
new SqlParameter("@CompanyID",product.CompanyID),
new SqlParameter("@AssessID",product.AssessID),
new SqlParameter("@IndustryID",product.IndustryID),
new SqlParameter("@SegmentID",product.SegmentID),
new SqlParameter("@ProductID",product.ProductID),
new SqlParameter("@GUICode",product.GUICode),
new SqlParameter("@Source",product.Source),
new SqlParameter("@DescriptionEnglish",product.DescriptionEnglish),
new SqlParameter("@IsSummary",product.IsSummary),
new SqlParameter("@IsData",product.IsData),
new SqlParameter("@SourceID",product.SourceID),
new SqlParameter("@TargetID",product.TargetID),
};
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductInsertSingleItem", parameters);
            return result;
        }
    }
}
