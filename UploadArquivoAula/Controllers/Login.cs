using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UploadArquivoAula.Models;

namespace UploadArquivoAula.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string Email, string Senha)
        {

            if (ModelState.IsValid)
            {
                SqlConnection minhaConexao = new SqlConnection(@"data source=EVERTON\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=11T");
                minhaConexao.Open();

                string strQuerySelect = string.Format($"SELECT EMAIL,SENHA FROM Login WHERE EMAIL='{Email}' AND SENHA='{Senha}'");
                SqlCommand cmdComandoSelect = new SqlCommand(strQuerySelect, minhaConexao);
                SqlDataReader dados = cmdComandoSelect.ExecuteReader();

                if (dados.Read())
                {

                    string email = dados[0].ToString();
                    string senha = dados[1].ToString();

                    if (email == Email && senha == Senha)
                    {
                        return Redirect("Home/Index");
                    }
                }
                return Redirect("Login/Index");
            }
            else
            {
                return Redirect("../Login/Index");
            }
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        //Cadastrando no banco de dados
        [HttpPost]
        public IActionResult Cadastro([FromForm] LoginModel info)
        {
            if (ModelState.IsValid)
            {
                SqlConnection minhaConexao = new SqlConnection(@"data source=EVERTON\SQLEXPRESS; Integrated Security=SSPI;Initial Catalog=11T");
                minhaConexao.Open();

                string strQueryInsert = string.Format($"INSERT INTO Login (Email, Senha) VALUES ('{info.Email}','{info.Senha}')");
                SqlCommand cmdComandoInsert = new SqlCommand(strQueryInsert, minhaConexao);
                cmdComandoInsert.ExecuteNonQuery();

                return Redirect("../Login/Index");
            }
            else
            {
                return Redirect("../Login/Cadastro");
            }
        }

        public IActionResult Alterar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(string Email, string Senha)
        {
            if (ModelState.IsValid)
            {
                SqlConnection minhaConexao = new SqlConnection(@"data source=EVERTON\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=11T");
                minhaConexao.Open();

                string strQueryUpdate = $"UPDATE Login SET Senha='{Senha}' WHERE Email='{Email}'";
                SqlCommand cmdComandoUpdate = new SqlCommand(strQueryUpdate, minhaConexao);
                cmdComandoUpdate.ExecuteNonQuery();

                return Redirect("../Login/Index");

            }
            return Redirect("../Login/Index");

        }

    }
}
