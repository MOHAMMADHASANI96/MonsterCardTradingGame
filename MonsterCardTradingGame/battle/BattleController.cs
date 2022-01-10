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
        private int win = 0;
        private int lose = 0;
        bool is_played = false;
        private String result = null;



        private BattleController(){}
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
                    this.is_played = true;

                    this.playerA = list_player.ElementAt(0);
                    this.playerB = list_player.ElementAt(1);

                    cardsA = new CardsRepository().getDeckByUsername(this.playerA);
                    cardsB = new CardsRepository().getDeckByUsername(this.playerB);
     
                    while (cardsA.Count !=0 && cardsB.Count !=0 && this.num_draw<100)
                    {
                        int rand_cardsA = rand.Next(cardsA.Count);
                        int rand_cardsB = rand.Next(cardsB.Count);
                        // take card randomly from list of cards 
                        Card cardA = cardsA.ElementAt(rand_cardsA);
                        Card cardB = cardsB.ElementAt(rand_cardsB);

                        int result = 0;
                        try
                        {
                            result = Program.basicPlay.getResult(cardA, cardB);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Error:" + exception.Message);
                        }

                        // 1 -> playerA win
                        // -1 -> playerB win
                        // 0 -> draw

                        if(result == 1)
                        {
                            // update state table  elo playerA ->+3 & elo playerB -> -5 
                            // update the username of playerB's card & deck -> false
                            // update deck of card playerA -> false
                            new StatsRepository().updateStatWinnerByUsername(cardA.username);
                            new StatsRepository().updateStatLoserByUsername(cardB.username);
                            new CardsRepository().updateUsernameOfCardById(cardA.username, cardB.id);
                            new CardsRepository().updateDeckFalse(cardB.id);
                            new CardsRepository().updateDeckFalse(cardA.id);

                            this.win++;
                            cardsA.RemoveAt(rand_cardsA);
                            cardsB.RemoveAt(rand_cardsB);

                        }
                        else if( result == -1)
                        {
                            // update state table  elo playerB ->+3 & elo playerA -> -5 
                            // update the username of playerA's card & deck -> false
                            // update deck of card playerB -> false
                            new StatsRepository().updateStatWinnerByUsername(cardB.username);
                            new StatsRepository().updateStatLoserByUsername(cardA.username);
                            new CardsRepository().updateUsernameOfCardById(cardB.username, cardA.id);
                            new CardsRepository().updateDeckFalse(cardA.id);
                            new CardsRepository().updateDeckFalse(cardB.id);

                            this.lose++;
                            cardsA.RemoveAt(rand_cardsA);
                            cardsB.RemoveAt(rand_cardsB);
                        }
                        else
                        {
                            // num of darw +1 
                            // update draw in stat table -> draw +1 PlayerA & PlayerB
                            this.num_draw++;
                            new StatsRepository().updateStatdrawByUsername(cardB.username);
                            new StatsRepository().updateStatdrawByUsername(cardA.username);
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
            this.result = this.playerA + ": win = " + this.win + " , lose = " + this.lose + " , draw = " + this.num_draw;
            this.result += this.playerB + ": win = " + this.lose + " , lose = " + this.win + " , draw = " + this.num_draw;
            return this.result;
        }
        public bool isPlayed()
        {
            if (this.is_played)
                return true;
            return false;
        }
    }
}
