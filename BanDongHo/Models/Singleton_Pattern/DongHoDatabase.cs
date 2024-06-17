using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BanDongHo.Models
{
    public class DongHoDatabase
    {   
        private static DoAnPM_LTEntities instance;

        private DongHoDatabase() { }

        public static DoAnPM_LTEntities Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoAnPM_LTEntities();
                }
                return instance;
            }
        }
    }
}