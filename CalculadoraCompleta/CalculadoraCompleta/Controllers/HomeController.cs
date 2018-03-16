using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        [HttpGet] //facultativo, pq por defeito é sempre o verbo utilizado
        public ActionResult Index()
        {
            //inicializaçao dos primeiros valores da calculadora
            Session["primeiroOperando"] = true;
            Session["iniciaOperando"] = true;
            ViewBag.Display = "0";

            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt,string display)
        {
            //avaliar o valor atribuido à variavel bt
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":

                    if ((bool)Session["iniciaOperando"] || display.Equals("0"))
                    {
                        display = bt;
                    }
                    else display += bt;
                    Session["iniciaOperando"] = false;
                    break;

                case "+/-":
                    display = Convert.ToDouble(display)* -1 + "";
                    break;

                case ",":
                    if (!display.Contains(",")) display += ",";
                    break;

                case "+":
                case "-":
                case "X":
                case ":":
                case "=":
                    //se nao é a primeira vez que carrego num operador
                    if (!(bool)Session["primeiroOperando"])
                    {

                        //recuperar os valore dos operandos
                        double operando1 = Convert.ToDouble((string)Session["primeiroOperando"]);
                        double operando2 = Convert.ToDouble(display);
                        switch ((string)Session["operadorAnterior"])
                        {
                            case "+":
                                display = operando1 + operando2 + "";
                                break;
                            case "-":
                                display = operando1 - operando2 + "";
                                break;
                            case "X":
                                display = operando1 * operando2 + "";
                                break;
                            case ":":
                                display = operando1 / operando2 + "";
                                break;
                        } 
                        //guardar os dados para utilização futura
                        Session["primeiroOperando"] = display;
                        Session["operadorAnterior"] = bt;
                        Session["iniciaOperando"] = true;

                    }

                    //guardar o valor do 1º operando
                    Session["primeiroOperando"] = display;
                    //limpar display
                    Session["iniciaOperando"] = true;

                    if (bt.Equals("="))
                    {
                        //marcar o operador como primeiro operando
                        Session["primeiroOperando"] = true;
                    }
                    else
                    {
                        //guardar o valor do operador
                        Session["operadorAnterior"] = bt;
                        Session["primeiroOperando"] = false;
                    }

                    //marcar o display para reinicio
                    Session["iniciaOperando"] = true;

                    break;

                case "C":
                    //reiniciar a calculadora
                    Session["iniciaOperando"] = true;
                    Session["primeiroOperando"] = true;
                    display = "0";
                    break;
            }
            //preparar os dados para serem enviados para a View
            ViewBag.Display = display;

            return View();
        }
    }
}