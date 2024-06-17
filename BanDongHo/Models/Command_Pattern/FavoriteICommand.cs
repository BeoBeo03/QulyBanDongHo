using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Models;
namespace BanDongHo.Models.Command_Pattern
{
    public interface FavoriteICommand
    {
        void Execute();
    }
    public class AddtoFavorite : FavoriteICommand
    {
        
        private readonly int _userID;
        private readonly int _productID;

        public AddtoFavorite( int userID, int productID)
        {
           
            _userID = userID;
            _productID = productID;
            
        }

        public void Execute()
        {
            
            // Nếu sản phẩm chưa tồn tại trong danh sách yêu thích, thêm nó vào
            var yeuThich = DongHoDatabase.Instance.Favorite.FirstOrDefault
                (yt => yt.IDSanPham == _productID && yt.IDUser == _userID);
            if (yeuThich != null)
            {
                // Xóa sản phẩm khỏi danh sách yêu thích
                DongHoDatabase.Instance.Favorite.Remove(yeuThich);
            }
            else
            {
                // Tìm sản phẩm dựa trên IDSanPham
                var sanPham = DongHoDatabase.Instance.Product.FirstOrDefault(sp => sp.IDSanpham == _productID);
                if (sanPham != null)
                {
                    // Tạo ID mới cho yêu thích
                    var maxIDYeuThich = DongHoDatabase.Instance.Favorite.Max(yt => (int?)yt.IDYeuThich) ?? 0;
                    var newIDYeuThich = maxIDYeuThich + 1;

                    // Thêm sản phẩm vào danh sách yêu thích
                    var yeuThichMoi = new Favorite
                    {
                        IDSanPham = _productID,
                        IDUser = _userID,
                        NgayThem = DateTime.Now,
                        IDYeuThich = newIDYeuThich
                    };

                    DongHoDatabase.Instance.Favorite.Add(yeuThichMoi);
                }
            }

            DongHoDatabase.Instance.SaveChanges();



        }
    }
}