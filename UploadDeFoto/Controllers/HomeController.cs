using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UploadDeFoto.Controllers
{
    public class HomeController : Controller
    {
        public static string caminhoFoto = "";
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AlterFotoPefil()
        {
            string caminhodefault = "~/Uploads/Usuarios/default.jpg";
            return PartialView("AlterFotoPerfil", new AlterarFotoViewModel { Foto = caminhodefault });
        }

        [HttpPost]
        public JsonResult EnviarFoto()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (UploadDeFoto.verificaTipoDoArquivo(pic))
                {
                    if (!UploadDeFoto.verificaTamanhoDoArquivo(pic))
                    {
                        UploadDeFoto.uploadFotoPerfil(id, pic, Server);
                        return Json(new { success = true, responseText = "Foto alterada com suscesso!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { success = false, responseText = "A foto é muito grande, tamanho maximo é de 500Kb." }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { success = false, responseText = "O arquivo enviado não tem o formato aceito, envie .jpg ou png" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "Ocorreu um erro no envio da foto" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}