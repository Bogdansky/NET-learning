using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var context = new ProfileSampleEntities();

            /*var sources = context.ImgSources.Take(20).Select(x => x.Id);

            var model = new List<ImageModel>();

            foreach (var id in sources)
            {
                var item = context.ImgSources.Find(id);

                var obj = new ImageModel()
                {
                    Name = item.Name,
                    Data = item.Data
                };

                model.Add(obj);
            }*/

            var sources = await context.ImgSources
                .Take(20)
                .Select(x => x.Data)
                .ToListAsync();

            var model = sources.Select(x =>
            {
                var converted = Convert.ToBase64String(x);
                return new ImageModel
                {
                    ImageSourceAttribute = string.Format("data:image/jpg;base64,{0}", converted)
                };
            }).ToList();

            return View(model);
        }

        //public ActionResult Convert()
        //{
        //    var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), "*.jpg");

        //    using (var context = new ProfileSampleEntities())
        //    {
        //        foreach (var file in files)
        //        {
        //            using (var stream = new FileStream(file, FileMode.Open))
        //            {
        //                byte[] buff = new byte[stream.Length];

        //                stream.Read(buff, 0, (int) stream.Length);

        //                var entity = new ImgSource()
        //                {
        //                    Name = Path.GetFileName(file),
        //                    Data = buff,
        //                };

        //                context.ImgSources.Add(entity);
        //                context.SaveChanges();
        //            }
        //        } 
        //    }

        //    return RedirectToAction("Index");
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}