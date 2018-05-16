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

        public ImportController()
        {
            _importService = new DummyImportService();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AcceptVerbs("POST")]
        public string ProcessImportData(string immediate, [Bind(Include = "File")] HttpPostedFileBase file)
        {
            var temp = Request;
            if (immediate == "on")
            {
                // send to hangfire
                throw new NotImplementedException();
                return "Import scheduled";
            }

            var sb = new StringBuilder();
            using (var stream = new MemoryStream())
            {
                if (file != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    file.InputStream.CopyTo(stream);
                    // _importService.WithSuppliedFile
                }

                if (_importService.AddDummyContent(sb))
                {
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