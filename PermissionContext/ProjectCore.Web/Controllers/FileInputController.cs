using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using OfficeOpenXml;

namespace ProjectCore.Web.Controllers
{
    public class FileInputController : Controller
    {
        private readonly IHostingEnvironment _hostingEnv;

        public FileInputController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExportOne()
        {

          
                Thread.Sleep(5000);
                string sWebRootFolder = _hostingEnv.WebRootPath;
                string sFileName = "ExportOne.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // add a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Jeffcky");

                    //sheet header
                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Age";

                    //Add values
                    worksheet.Cells["A2"].Value = 1000;
                    worksheet.Cells["B2"].Value = "Jeffcky1";
                    worksheet.Cells["C2"].Value = 18;

                    worksheet.Cells["A3"].Value = 1001;
                    worksheet.Cells["B3"].Value = "Jeffcky2";
                    worksheet.Cells["C3"].Value = 19;
                    package.Save(); //Save the workbook.
                }
           
            return Json(URL);

        }

        public async Task<IActionResult> Export()
        {

          var url1= await Task.Run<string>(() =>
            {
                Thread.Sleep(5000);
                string sWebRootFolder = _hostingEnv.WebRootPath;
                string sFileName = "dddd.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // add a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Jeffcky");

                    //sheet header
                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Age";

                    //Add values
                    worksheet.Cells["A2"].Value = 1000;
                    worksheet.Cells["B2"].Value = "Jeffcky1";
                    worksheet.Cells["C2"].Value = 18;

                    worksheet.Cells["A3"].Value = 1001;
                    worksheet.Cells["B3"].Value = "Jeffcky2";
                    worksheet.Cells["C3"].Value = 19;
                    package.Save(); //Save the workbook.
                }

                return URL;
            });
            return  Json(url1);

        }

        [HttpPost]
        public JsonResult UploadFiles()
        {

            // ReSharper disable once TooWideLocalVariableScope
            var fileFullname = "";
            // ReSharper disable once TooWideLocalVariableScope
            var filename = "";
            var result = new List<Dictionary<string, string>>();
            try
            {
                var files = Request.Form.Files;
                if (files.Count <= 0)
                {
                    return Json(new { code = "1", msg = "上传的文件不存在或没有内容" });
                }
                foreach (var file in files)
                {
                    //获取名称
                    filename = ContentDispositionHeaderValue
                                     .Parse(file.ContentDisposition)
                                     .FileName
                                     .Trim('"');
                    //文件保存的路径
                    var filePath = _hostingEnv.WebRootPath + $@"\images\";

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    //保存的全路径
                    fileFullname = filePath + filename;

                    using (FileStream fs = System.IO.File.Create(fileFullname))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    result.Add(new Dictionary<string, string>()
                    {
                        { "Code","0" },
                        { "File_BusinessColumn", "dd" },
                        { "File_Name", filename },
                        { "caption" , filename },
                        { "File_Url", fileFullname },
                        { "url",  filePath}
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new { code = 1, msg = e.Message });
            }
            return Json(result);
        }

        public ActionResult DeleteFile(string key)
        {
            string webRootPath = _hostingEnv.WebRootPath + key;
            string result = "删除成功！";
            try
            {

                if (System.IO.File.Exists(webRootPath))
                {
                    System.IO.File.Delete(webRootPath);//删除文件
                }
            }
            catch (Exception ex)
            {
                result = "删除失败！" + ex.Message;
            }
            return Json(result);
        }

        public ActionResult DeleteFileId(string key)
        {
            string webRootPath = _hostingEnv.WebRootPath + key;
            string result = "删除成功！";
            try
            {

                if (System.IO.File.Exists(webRootPath))
                {
                    System.IO.File.Delete(webRootPath);//删除文件
                }
            }
            catch (Exception ex)
            {
                result = "删除失败！" + ex.Message;
            }
            return Json(result);
        }

    }
}