using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace LanceUmDesafio.Models
{
    public class UploadDeFoto
    {
        private static LanceUmDesafioDBEntities db;
        public static string uploadFotoPerfil(string IDUsuario, HttpPostedFile imagem, HttpServerUtilityBase Server)
        {
            db = new LanceUmDesafioDBEntities();
            var usuario = db.AspNetUsers.Find(IDUsuario);
            string extension = Path.GetExtension(imagem.FileName);

            string nomeFoto = IDUsuario.Substring(0, IDUsuario.IndexOf("-"));
            string renamedImage = Server.MapPath("/Uploads/Usuarios/" + nomeFoto + extension);
            string caminhoParcial = "/Uploads/Usuarios/" + nomeFoto + extension;
            if (File.Exists(renamedImage))
            {
                File.Delete(renamedImage);
                imagem.SaveAs(renamedImage);
            }
            else
                imagem.SaveAs(renamedImage);
            usuario.Foto = caminhoParcial;
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            return caminhoParcial;
        }

        public static bool verificaTamanhoDoArquivo(HttpPostedFile imagem)
        {
            const int maxFileLength = 512000; // 500KB = 500 * 1024 //1 Kilobyte (kB) = 2ˆ10 Byte = 1024 Bytes
            if (imagem.ContentLength > maxFileLength)
                return true;
            return false;
        }

        public static bool verificaTipoDoArquivo(HttpPostedFile imagem)
        {
            List<string> extensoes = new List<string>() { ".jpg", ".png"};
            string extension = Path.GetExtension(imagem.FileName);
            if (extensoes.Contains(extension))
                return true;
            return false;
        }
    }
}