using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Hosting;
using FlickrNet;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.Services.Implement;
using Umbraco.Web;


namespace Website.Sample.ImportData
{
    public class FlickrImportService : IImportService
    {
        private readonly MediaService _mediaService;
        private readonly ContentService _contentService;
        private readonly MediaFileSystem _mediaFileSystem;
        private readonly IUmbracoContextFactory _contextFactory;

        public FlickrImportService(MediaService mediaService, ContentService contentService, MediaFileSystem mediaFileSystem,
            IUmbracoContextFactory contextFactory)
        {
            _mediaService = mediaService;
            _contentService = contentService;
            _mediaFileSystem = mediaFileSystem;
            _contextFactory = contextFactory;
            // poke around in Umbraco.Core.Composing.Current.
        }

        public bool ProcessDataImportFromStream(MemoryStream stream, string fileFileName, StringBuilder sb)
        {
            throw new NotImplementedException();
        }

        public bool AddDummyContent(StringBuilder sb)
        {
            Flickr flickr = new Flickr("93d4b479487d79846186bf81486e1934");

            var options = new PhotoSearchOptions { PerPage = 1, Page = 1 };
            options.Tags = "colorful";
            var photo = flickr.PhotosGetRecent(1, 1).FirstOrDefault();

            // add media item
            var newmedia = _mediaService.CreateMediaWithIdentity(photo.Title, -1, "image");
            var destinationFolder = HostingEnvironment.MapPath("~/App_Data/FlickrTemp/");
            System.IO.Directory.CreateDirectory(destinationFolder);
            var newFileName = Path.Combine(destinationFolder, photo.PhotoId + ".jpg");
            new WebClient().DownloadFile(photo.LargeUrl, newFileName);
            var s = new FileStream(newFileName, FileMode.Open);
            // let us hope someone responds on : https://our.umbraco.com/forum/umbraco-8//96589-upload-a-new-media-item-from-code-in-v8
            newmedia.SetValue("umbracoFile", Path.GetFileName(newFileName), s); // First issue
            s.Close();
            _mediaService.Save(newmedia);

            // bad code, should use repository
            Umbraco.Core.Models.PublishedContent.IPublishedContent productsNode;
            using (var cref = _contextFactory.EnsureUmbracoContext())
            {
                var contentCache = cref.UmbracoContext.ContentCache;
                productsNode = contentCache.GetSingleByXPath($"//{PublishedContentModels.Products.ModelTypeAlias}");
            }

            var newNode = _contentService.Create(photo.PhotoId, productsNode.Id,
                PublishedContentModels.Product.ModelTypeAlias);

            newNode.SetValue("description", "flickr description: " + photo.Description ?? "none");
            newNode.SetValue("photos", $"{newmedia.GetUdi()}");
            Random rand = new Random();
            newNode.SetValue("price", rand.Next(1, 1000));
            newNode.SetValue("category", "flickr");
            newNode.SetValue("productName", photo.Title);
            _contentService.SaveAndPublish(newNode);

            sb.Append("imported most recent photo from flickr: " + photo.Title);
            return true;
        }
    }

    public interface IImportService
    {
        bool ProcessDataImportFromStream(MemoryStream stream, string fileFileName, StringBuilder sb);
        bool AddDummyContent(StringBuilder sb);
    }
}
