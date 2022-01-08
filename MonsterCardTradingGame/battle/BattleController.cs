using MonsterCardTradingGame.data_layer.entity;
using MonsterCardTradingGame.data_layer.repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MonsterCardTradingGame.battle
{
    public class BattleController
    {
        private String playerA;
        private String playerB;
        private static BattleController instance = null;
        private static String Lock = "Lock";
        private List<String> list_player = new List<String>();
        DateTime startTime;
        private List<Card> cardsA;
        private List<Card> cardsB;
        static Random rand = new Random();
        private int num_draw = 0;

        public BattleController(){}
        public static BattleController getInstance()
        {
            lock(Lock)
            {
                if (instance == null)
                    instance = new BattleController();
            }
            return instance;
        }
        public bool addplayer(String username)
        {
            lock (list_player)
            {
                if (list_player.Contains(username))
                    return false;
                list_player.Add(username);
                if (startTime == DateTime.MinValue)
                    startTime = DateTime.Now;
                return true;
            }
        }
        public bool isProcess()
        {
            if((startTime.AddSeconds(15) > DateTime.Now) && startTime != DateTime.MinValue)
            {
                if(list_player.Count == 2)
                {
                    this.playerA = list_player.ElementAt(0);
                    this.playerB = list_player.ElementAt(1);

                    //To do

                    cardsA = new CardsRepository().getDeckByUsername(this.playerA);
                    cardsB = new CardsRepository().getDeckByUsername(this.playerB);
     
                    while (cardsA.Count !=0 && cardsB.Count !=0 && this.num_draw<100)
                    {
                        int rand_cardsA = rand.Next(cardsA.Count);
                        int rand_cardsB = rand.Next(cardsB.Count);
                        // take card randomly from list of cards 
                        Card cardA = cardsA.ElementAt(rand_cardsA);
                        Card cardB = cardsB.ElementAt(rand_cardsB);

                        // true -> one player win
                        // false --> draw
                        bool result = false;
                        try
                        {
                            result = Program.basicPlay.getResult(cardA, cardB);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Error:" + exception.Message);
                        }
                        if (result == false)
                            this.num_draw++;
                        else
                        {
                            cardsA.RemoveAt(rand_cardsA);
                            cardsB.RemoveAt(rand_cardsB);
                        }
                    }

                    startTime = DateTime.MinValue;
                    return false;
                }
                return true;
            }
            startTime = DateTime.MinValue;
            list_player.Clear();
            return false;
        }
        public String getResult()
        {
            return "Ok";
        }
    }
}
