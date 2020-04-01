using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginMVC.Models;

namespace LoginMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut() 
        {
            int userId = (int)Session["UserID"];
            Session.Abandon();
            Session["UserName"] = "";
            return RedirectToAction("Index","Login");
        }

        [HttpPost]
        public ActionResult Autorizar(LoginMVC.Models.Login login)
        {
            BaseDedadosEntities db = new BaseDedadosEntities();
            //var dataBaseLogin = db.Logins.Where
            //    (
            //        x => x.Usuario == login.Usuario
            //        && x.Senha == login.Senha
            //        && x.StatusUsuario == true
            //        && x.TipoUsuario.Count() != 0
            //        && x.Id_Login > 0
            //    ).ToList();


            //Seleciona os dados dentro do banco de dados
            //var dataBaseLogin = db.Logins.OrderBy(
            //    x => x.Usuario == login.Usuario
            //    && x.Senha == login.Usuario).ToList().Select(
            //    x => new LoginMVC.Models.Login 
            //    { 
            //        Id_Login = x.Id_Login,
            //        Usuario = x.Usuario,
            //        Senha = x.Senha,
            //        TipoUsuario = x.TipoUsuario,
            //        StatusUsuario = x.StatusUsuario
            //    } ).ToList();


            //Retorna os itens com as mesmas autenticações do banco
            var dataBaseLogin = db.Logins.Where(
                x => x.Usuario == login.Usuario
                && x.Senha == login.Senha).ToList().FirstOrDefault();

            if (dataBaseLogin == null)
            {
                login.ErroNoLogin = "Qual foi rapa, errou na senha ou no usuario!";
                return View("Index", login);
            }
            else
            {
                login.Usuario = "";
                login.Senha = "";
                Session["UserID"] = dataBaseLogin.Id_Login;
                Session["UserName"] = dataBaseLogin.Usuario;
                return RedirectToAction("Index", "Home");            

            }                        
        }



    }
}