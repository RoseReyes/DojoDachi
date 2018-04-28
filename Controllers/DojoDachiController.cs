using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
 
namespace dojodachi.Controllers
{
    public class DojoDachiController : Controller
    {
        [HttpGet]
        [Route("dojodachi")]
        public IActionResult dojodachi()
        {
            int fullness = 20;
            int happiness = 20;
            int energy = 50;
            int meals = 3;
    
            int countfullness = HttpContext.Session.GetInt32("countfull")?? fullness;
            HttpContext.Session.SetInt32("countfull", countfullness);

            int counthappiness = HttpContext.Session.GetInt32("counthappy")?? happiness;
            HttpContext.Session.SetInt32("counthappy", counthappiness);
            
            int countenergy = HttpContext.Session.GetInt32("countenerg")?? energy;
            HttpContext.Session.SetInt32("countenerg", countenergy);
            
            int countmeals = HttpContext.Session.GetInt32("countmeal")?? meals;
            HttpContext.Session.SetInt32("countmeal", countmeals);
            
            ViewBag.ReturnFullness = countfullness;
            ViewBag.ReturnHappiness = counthappiness;
            ViewBag.ReturnEnergy = countenergy;
            ViewBag.ReturnMeals = countmeals;

            if(countenergy == 100 && counthappiness == 100 && countfullness == 100){
                TempData["activity"] = "Congratulations! You won!";
            }
            if(counthappiness == 0 || countfullness == 0) {
                TempData["activity"] = "Your tamagochi just died";
            }

            return View("Index");
        }

        [HttpPost]
        [Route("FormSubmitReset")]

        public IActionResult FormSubmitReset(){
            HttpContext.Session.Clear();
            return RedirectToAction("dojodachi");
        }

        [HttpPost]
        [Route("FormSubmitFeed")]
        public IActionResult FormSubmitFeed()
        {
            Random num = new Random();
            int newnum = num.Next(5,10);
        
            //Add Fullness count with random number
            int? countmeals = HttpContext.Session.GetInt32("countmeal");
            if(newnum == 8){
                TempData["activity"] = "Your tamagochi does not like to eat";
                if(countmeals == 0) {
                     TempData["activity"] = "You don't have enough meals to feed your tamagochi!";
                     return RedirectToAction("dojodachi");
                }
                countmeals -= 1;
                HttpContext.Session.SetInt32("countmeal", (int)countmeals);
                int? meals = HttpContext.Session.GetInt32("countmeal");
                return RedirectToAction("dojodachi");
            }
            else {
                if(countmeals == 0) {
                     TempData["activity"] = "You don't have enough meals to feed your tamagochi!";
                     return RedirectToAction("dojodachi");
                }
                countmeals -= 1;
                HttpContext.Session.SetInt32("countmeal", (int)countmeals);
                int? meals = HttpContext.Session.GetInt32("countmeal");

                int? countfullness = HttpContext.Session.GetInt32("countfull"); 
                countfullness += newnum;
                HttpContext.Session.SetInt32("countfull", (int)countfullness);
                int? full = HttpContext.Session.GetInt32("countfull");
            }
            TempData["activity"] = ("You feed your tamagochi");
           return RedirectToAction("dojodachi"); //since this is redirectToAction then declare your variable in the method that you called -- int this case it's the index
        }

        [HttpPost]
        [Route("FormSubmitPlay")]
        public IActionResult FormSubmitPlay()
        {
            //Feed
            Random num = new Random();
            int newnum = num.Next(1,10);
        
            //Happiness
            int? countenergy = HttpContext.Session.GetInt32("countenerg");
            if(newnum == 8) {
                TempData["activity"] ="Your tamagochi does not like to Play";
                
                if(countenergy == 0) {
                    TempData["activity"] = "Tamagochi's energy is low. Put him to sleep";
                    return RedirectToAction("dojodachi");
                 }
                //Energy will still decrease
                countenergy -= 5;
                HttpContext.Session.SetInt32("countenerg", (int)countenergy);
                int? energy = HttpContext.Session.GetInt32("countenerg");
                return RedirectToAction("dojodachi");
            }
            else {
                //Energy
                if(countenergy == 0) {
                    TempData["activity"] = "Tamagochi's energy is low. Put him to sleep";
                    return RedirectToAction("dojodachi");
                }
                countenergy -= 5;
                HttpContext.Session.SetInt32("countenerg", (int)countenergy);
                int? energy = HttpContext.Session.GetInt32("countenerg");

                //Happiness
                int? counthappiness = HttpContext.Session.GetInt32("counthappy");
                counthappiness += newnum;
                HttpContext.Session.SetInt32("counthappy", (int)counthappiness);
                int? happiness = HttpContext.Session.GetInt32("counthappy");
            }
            TempData["activity"] ="You played with your tamagochi";
           return RedirectToAction("dojodachi"); //since this is redirectToAction then declare your variable in the method that you called -- int this case it's the index
        }

        [HttpPost]
        [Route("FormSubmitWork")]
        public IActionResult FormSubmitWork()
        {
            //Feed
            Random num = new Random();
            int newnum = num.Next(1,3);
            
            //Energy
            int? countenergy = HttpContext.Session.GetInt32("countenerg");
            if(countenergy == 0) {
                TempData["activity"] = "Tamagochi's energy is low. Put him to sleep";
                return RedirectToAction("dojodachi");
            }
            countenergy -= 5;
            HttpContext.Session.SetInt32("countenerg", (int)countenergy);
            int? energy = HttpContext.Session.GetInt32("countenerg");

            //Meals
            int? countmeals = HttpContext.Session.GetInt32("countmeal");
            countmeals += newnum;
            HttpContext.Session.SetInt32("countmeal", (int)countmeals);
            int? meals = HttpContext.Session.GetInt32("countmeal");

           TempData["activity"] ="Your tamagochi is working";
           return RedirectToAction("dojodachi"); //since this is redirectToAction then declare your variable in the method that you called -- int this case it's the index
        }

        [HttpPost]
        [Route("FormSubmitSleep")]
        public IActionResult FormSubmitSleep()
        {
            //Energy
            int? countenergy = HttpContext.Session.GetInt32("countenerg");
            int? counthappiness = HttpContext.Session.GetInt32("counthappy");
            int? countfullness = HttpContext.Session.GetInt32("countfull"); 

            countenergy += 15;
            HttpContext.Session.SetInt32("countenerg", (int)countenergy);
            int? energy = HttpContext.Session.GetInt32("countenerg");

             //Hapiness
            counthappiness -= 5;
            HttpContext.Session.SetInt32("counthappy", (int)counthappiness);
            int? happiness = HttpContext.Session.GetInt32("counthappy");

            //Fullness
            countfullness -= 5;
            HttpContext.Session.SetInt32("countfull", (int)countfullness);
            int? full = HttpContext.Session.GetInt32("countfull");

           TempData["activity"] = "Your tamagochi is sleeping"; 
           return RedirectToAction("dojodachi"); //since this is redirectToAction then declare your variable in the method that you called -- int this case it's the index
        }

    }
}