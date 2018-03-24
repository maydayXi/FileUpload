using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FileUpload.Controllers
{
    public class FileUploadController : Controller
    {

        public ActionResult Create()
        {
            return View();
        }

        #region 上傳檔案模組
        /// <summary>
        /// 上傳檔案模組
        /// </summary>
        /// <param name="photo"> 上傳的相片檔 </param>
        /// <returns> 導自相片顯示頁 </returns>
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase photo)
        {

            string fileName = "";

            if(photo != null)
            {
                if(photo.ContentLength > 0)
                {
                    // 取得檔名
                    fileName = Path.GetFileName(photo.FileName);
                    // 設定路徑至伺服器的 Photo 資料夾
                    var path = Path.Combine(
                        Server.MapPath("~/Photos"), fileName);
                    photo.SaveAs(path);
                }
            }

            return RedirectToAction("ShowPhotos");
        }
        #endregion

        /// <summary>
        /// 顯示相片頁面
        /// </summary>
        /// <returns> html tag 顯示相片 </returns>
        public string ShowPhotos()
        {
            string show = "";

            // 建立可操作 Photos 資料夾的 dir 物件
            DirectoryInfo dir = new DirectoryInfo(
                Server.MapPath("~/Photos"));

            // 取得 dir 物件下的所有檔案
            // 並存入 fInfo 檔案資訊陣列
            FileInfo[] fInfo = dir.GetFiles();

            int n = 0;
            // 逐一讀取 fInfo 檔案資訊陣列內的所有內容指定給 show 變數
            foreach(FileInfo result in fInfo)
            {
                n++;

                show += "<a href='../Photos/" + result.Name +
                    "' target='_blank'><img src='../Photos/" + result.Name
                    + "' width='90' height='60' border='0'></a>";

                // 每四張圖一個段落
                if (n % 4 == 0)
                    show += "<p>";
            }

            show += "<p><a href='Create'>返回</a></p>";

            return show;
        }
    }
}