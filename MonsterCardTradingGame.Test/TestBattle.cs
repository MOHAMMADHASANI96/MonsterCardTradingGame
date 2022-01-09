using MonsterCardTradingGame.battle.play;
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
    public class TestBattle
    {
        [SetUp]
        public void Setup()
        {
            Database.GetInstance().truncateTables();
            new UsersRepository().addUser("Test1", "test1");
            new UsersRepository().addUser("Test2", "test2");

            new StatsRepository().addStat("Test1");
            new StatsRepository().addStat("Test2");

            new CardsRepository().addCard("1", "Dragon", 20.0, 1, null, "Dragon");
            new CardsRepository().addCard("2", "WaterSpell", 40.0, 2, "Water", "Spell");
            new CardsRepository().addCard("3", "FireSpell", 30.0, 1, "Fire", "Spell");
            new CardsRepository().addCard("4", "RegularSpell", 15.0, 2, "Regular", "Spell");
            new CardsRepository().addCard("5", "Dragon", 50.0, 1, null, "Dragon");
            new CardsRepository().addCard("6", "Knight", 55.0, 2, null, "Knight");
            new CardsRepository().addCard("7", "FireElf", 45.0, 1, "Fire", "Elf");
            new CardsRepository().addCard("8", "Dragon", 32.0, 2, null, "Dragon");
            new CardsRepository().addCard("9", "WaterSpell", 45.0, 1, "Water", "Spell");
            new CardsRepository().addCard("10", "WaterSpell", 45.0, 2, "Water", "Spell");
            new CardsRepository().addCard("11", "Goblin", 20.0, 1, null, "Goblin");
            new CardsRepository().addCard("12", "Dragon", 50.0, 1, null, "Dragon");

            new CardsRepository().updateUsernameAfterGetPackage("Test1", 1);
            new CardsRepository().updateUsernameAfterGetPackage("Test2", 2);

            for (int i = 1; i < 13; i++)
            {
                new CardsRepository().updateDeckTrue(Convert.ToString(i));
            }
        }

        [Test]
        public void TestOneSpellOneMonster()
        {
            Card cardA = new CardsRepository().getCardByid("1");
            Card cardB = new CardsRepository().getCardByid("2");

            Assert.AreEqual(-1,new OneSpellOneMonster().processRequest(cardA, cardB));
        }

        [Test]
        public void TestTwoSpells()
        {
            Card cardA = new CardsRepository().getCardByid("3");
            Card cardB = new CardsRepository().getCardByid("4");

            Assert.AreEqual(1, new TwoSpells().processRequest(cardA, cardB));
        }

        [Test]
        public void TestTwoMonster()
        {
            Card cardA = new CardsRepository().getCardByid("5");
            Card cardB = new CardsRepository().getCardByid("6");

            Assert.AreEqual(-1, new TwoMonsters().processRequest(cardA, cardB));
        }

        [Test]
        public void TestSpecialties()
        {
            Card cardA = new CardsRepository().getCardByid("7");
            Card cardB = new CardsRepository().getCardByid("8");

            Assert.AreEqual(1, new TwoMonsters().processRequest(cardA, cardB));
        }

        [Test]
        public void TestDraw()
        {
            Card cardA = new CardsRepository().getCardByid("9");
            Card cardB = new CardsRepository().getCardByid("10");

            Assert.AreEqual(0, new TwoMonsters().processRequest(cardA, cardB));
        }
        [Test]
        public void TestSpecialties2()
        {
            Card cardA = new CardsRepository().getCardByid("11");
            Card cardB = new CardsRepository().getCardByid("12");

            Assert.AreEqual(-1, new TwoMonsters().processRequest(cardA, cardB));
        }
    }
}
