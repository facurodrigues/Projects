using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ClashRoyale;
using ClashRoyaleExample;
using System.Collections.Generic;
using ClashRoyale.Models;


namespace ClashRoyaleExample
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<Card> cards = FillCardsList();
            List<Arena> arenas = FillArenasList();
            List<Chest> chests = FillChestList();


            log.LogInformation("C# HTTP trigger function processed a request.");

            string optionChose = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            optionChose = optionChose ?? data?.name;

            Card cardData =  Royale.GetCard(optionChose);

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(cardData);

            return optionChose != null
                ? (ActionResult)new OkObjectResult(jsonString)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        #region Fills
        private static List<Chest> FillChestList()
        {
            List<Chest> chest = Royale.GetChests();
            return chest;
        }

        private static List<Arena> FillArenasList()
        {
            List<Arena> arena = Royale.GetArenas();
            return arena;
        }

        private static List<Card> FillCardsList()
        {
            List<Card> cards = Royale.GetCards();
            return cards;
        }
        #endregion

    }
}
