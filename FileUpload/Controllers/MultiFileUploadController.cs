using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;                    // 檔案處理必需引用 System.IO

namespace FileUpload.Controllers
{
    public class MultiFileUploadController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        #region 多檔案上傳模組
        /// <summary>
        /// 多檔案上傳模組
        /// </summary>
        /// <param name="photos"> 相片上傳陣列 </param>
        /// <returns> 重新導向顯示頁 </returns>
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase[] photos)
        {
            string fName = "";

            for(int i = 0; i < photos.Length; i++)
            {
                // 取得目前檔案上傳的 HttpPostedFileBase 物件
                HttpPostedFileBase f =
                    (HttpPostedFileBase)photos[i];

                if(f != null)
                {
                    // 取得上傳的檔案名稱
                    fName = f.FileName.Substring(
                        f.FileName.LastIndexOf("\\") + 1);
                    // 儲存到網站的 Photos 資料夾下
                    f.SaveAs(Server.MapPath("~/Photos") + "\\" + fName);
                }
            }

            return RedirectToAction("ShowPhotos");
        }
        #endregion

        public string ShowPhotos()
        {
            string show = "";

            // 建立可操作資料夾的物件
            // 用以讀取相片檔案路徑
            DirectoryInfo dir = new DirectoryInfo(
                Server.MapPath("~/Photos"));

            // 取得相片檔路徑
            FileInfo[] fInfo = dir.GetFiles();

            int n = 0;

            // 逐一讀出路徑
            foreach(FileInfo result in fInfo)
            {
                n++;

                show += "<a href='../Photos/" + result.Name +
                    "' target='_blank'><img src='../Photos/" + result.Name
                    + "' width='90' height='60' border='0'></a>";

                if (n % 4 == 0)
                    show += "<p>";
            }

            show += "<p><a href='Create'>返回</a></p>";
            return show;
        }
    }
}