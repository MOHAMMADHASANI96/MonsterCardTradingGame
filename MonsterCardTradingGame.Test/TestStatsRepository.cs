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
    public class TestStatsRepository
    {
        [SetUp]
        public void Setup()
        {
            Database.GetInstance().truncateTables();
            new UsersRepository().addUser("Test1", "test1");
            new UsersRepository().addUser("Test2", "test2");
            new StatsRepository().addStat("Test1");
            new StatsRepository().addStat("Test2");
        }

        [Test]
        public void TestGetCardsById()
        {
            new UsersRepository().addUser("Test3", "test3");
            Assert.IsTrue(new StatsRepository().addStat("Test3"));
            Assert.IsFalse(new StatsRepository().addStat("Test1"));
        }

        [Test]
        public void TestGetStats()
        {
            Stat stat = new StatsRepository().getStat("Test1");

            Assert.IsNotNull(stat);
            Assert.AreEqual(100, stat.elo);
        }

        [Test]
        public void TestGetListStat()
        {
            List<Stat> statList = new StatsRepository().getListStat();

            Assert.AreEqual(2, statList.Count);
        }

        [Test]
        public void TestUpdateStatWinnerByUsername()
        {
            Stat stat = new StatsRepository().getStat("Test1");
            Assert.AreEqual(100, stat.elo);
            Assert.AreEqual(0, stat.win);

            Assert.IsTrue(new StatsRepository().updateStatWinnerByUsername("Test1"));


            Stat newStat = new StatsRepository().getStat("Test1");
            Assert.AreEqual(103, newStat.elo);
            Assert.AreEqual(1, newStat.win);

        }

        [Test]
        public void TestUpdateStatLoserByUsername()
        {
            Stat stat = new StatsRepository().getStat("Test1");
            Assert.AreEqual(100, stat.elo);
            Assert.AreEqual(0, stat.lose);

            Assert.IsTrue(new StatsRepository().updateStatLoserByUsername("Test1"));


            Stat newStat = new StatsRepository().getStat("Test1");
            Assert.AreEqual(95, newStat.elo);
            Assert.AreEqual(1, newStat.lose);
        }

        [Test]
        public void TestUpdateStatdrawByUsername()
        {
            Stat stat = new StatsRepository().getStat("Test1");
            Assert.AreEqual(100, stat.elo);
            Assert.AreEqual(0, stat.draw);

            Assert.IsTrue(new StatsRepository().updateStatdrawByUsername("Test1"));


            Stat newStat = new StatsRepository().getStat("Test1");
            Assert.AreEqual(100, newStat.elo);
            Assert.AreEqual(1, newStat.draw);
        }
    }
}
