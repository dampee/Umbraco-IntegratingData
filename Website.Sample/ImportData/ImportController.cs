using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Website.Sample.ImportData
{
    public class ImportController : UmbracoAuthorizedController
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AcceptVerbs("POST")]
        public string ProcessImportData(string immediate, [Bind(Include = "File")] HttpPostedFileBase file)
        {
            var temp = Request;
            if (immediate != "on")
            {
                // send to hangfire
                throw new NotImplementedException();
                return "Import scheduled";
            }

            var sb = new StringBuilder();
            using (var stream = new MemoryStream())
            {
                file.InputStream.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin);
                if (_importService.ProcessDataImportFromStream(stream, file.FileName, sb))
                {
                    //todo Export SB result
                    return sb.ToString();
                };
            }

            return "Error: " + sb;
        }

        [System.Web.Mvc.HttpGet]
        public bool Ping()
        {
            return true;
        }

    }
}