using MonsterCardTradingGame.data.repository;
using MonsterCardTradingGame.data_layer.entity;
using MonsterCardTradingGame.data_layer.repository;
using MTCG.repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Test
{
    public class TestTradeRepository
    {
        [SetUp]
        public void Setup()
        {
            Database.GetInstance().truncateTables();
            new UsersRepository().addUser("Test1", "test1");
            new UsersRepository().addUser("Test2", "test2");
            new CardsRepository().addCard("1", "Dragons", 25.0, 1, null, "Dragons");
            new CardsRepository().addCard("2", "WaterSpell", 20.0, 2, "Water", "Spell");
            new TradesRepository().addTrade("Test1", "1234", "1", "Spell", 15.0);
        }

        [Test]
        public void TestGetTradeById()
        {
            Trade trade = new TradesRepository().getTradeById("1234");

            Assert.IsNotNull(trade);
            Assert.AreEqual("Test1", trade.username);
        }

        [Test]
        public void TestGetTrade()
        {
            Assert.AreEqual(1, new TradesRepository().getTrade().Count);
            Assert.IsTrue(new TradesRepository().addTrade("Test2", "1235", "2", "monster", 15.0));

            List<Trade> tradeList = new TradesRepository().getTrade();
            Assert.IsNotNull(tradeList);
            Assert.AreEqual(2, tradeList.Count);
        }

        [Test]
        public void TestCheckTradeByIdAndUsername()
        {
            Assert.IsTrue(new TradesRepository().checkTradeByIdAndUsername("1234", "Test1"));
        }

        [Test]
        public void TestCheckTradeById()
        {
            Assert.IsTrue(new TradesRepository().checkTradeById("1234"));
            Assert.IsFalse(new TradesRepository().checkTradeById("1237"));
        }

        [Test]
        public void TestDeleteTradeById()
        {

            Assert.IsTrue(new TradesRepository().checkTradeById("1234"));

            Assert.IsTrue(new TradesRepository().deleteTradeById("1234"));

            Assert.IsFalse(new TradesRepository().checkTradeById("1234"));
        }

        [Test]
        public void TestUpdateIsSoldById()
        {
            Trade trade = new TradesRepository().getTradeById("1234");
            Assert.IsFalse(trade.is_sold);

            Assert.IsTrue(new TradesRepository().updateIsSoldById("1234"));

            Trade newTrade = new TradesRepository().getTradeById("1234");
            Assert.IsTrue(newTrade.is_sold);
        }
    }
}
