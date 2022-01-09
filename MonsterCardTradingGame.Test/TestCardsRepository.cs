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
    public class TestCardsRepository
    {
        
        [SetUp]
        public void Setup()
        {
            Database.GetInstance().truncateTables();
            new CardsRepository().addCard("1","Dragons",25.0,1,null,"Dragons");
            new CardsRepository().addCard("2", "WaterSpell", 20.0, 2, "Water", "Spell");
        }
       
        [Test]
        public void TestGetCardsById()
        {
            Card card = new CardsRepository().getCardByid("1");
            Assert.AreEqual(25, card.damage);
            Assert.IsEmpty(card.element_type);
        }
        
        [Test]
        public void TestGetLastPckageId()
        {
            Assert.AreEqual(2, new CardsRepository().getLastPackageId());
        }
        

        [Test]
        public void TestAddCard()
        {
            Assert.IsTrue(new CardsRepository().addCard("3","FireSpell",40,3,"Fire","Spell"));
            Assert.IsFalse(new CardsRepository().addCard("3", "FireSpell", 40, 3, "Fire", "Spell"));
        }

        [Test]
        public void TestFirstFreePakageId()
        {
            Assert.AreEqual(1,new CardsRepository().getFirstFreePackageId());

            new CardsRepository().updateUsernameAfterGetPackage("Test1", 1);

            Assert.AreNotEqual(1, new CardsRepository().getFirstFreePackageId());
            Assert.AreEqual(2, new CardsRepository().getFirstFreePackageId());
        }
        
        [Test]
        public void TestGetListOfCardByUsername()
        {
            new CardsRepository().updateUsernameAfterGetPackage("Test1", 1);
            List<Card> cardList = new CardsRepository().getListOfCardsByUsername("Test1");

            Assert.AreEqual(1, cardList.Count);
            Assert.AreEqual("1", cardList[0].id);
        }
        
        [Test]
        public void TestGetDeckByUsername()
        {
            new CardsRepository().updateUsernameAfterGetPackage("Test1", 1);
            List<Card> cardList = new CardsRepository().getDeckByUsername("Test1");

            Assert.AreEqual(0, cardList.Count);
            Assert.IsTrue(new CardsRepository().updateDeckTrue("1"));

            List<Card> newCardList = new CardsRepository().getDeckByUsername("Test1");
            Assert.AreEqual(1, newCardList.Count);
            Assert.AreEqual("1", newCardList[0].id);
        }

        [Test]
        public void TestControlCardBelongsToUsername()
        {
            new CardsRepository().updateUsernameAfterGetPackage("Test1", 1);

            Assert.IsTrue(new CardsRepository().controlCardBelongsToUsername("Test1","1"));
        }


        [Test]
        public void TestUpdateUsernameOfCardById()
        {
            new CardsRepository().updateUsernameAfterGetPackage("Test1", 1);
            Assert.AreEqual("Test1",new CardsRepository().getCardByid("1").username);

            Assert.IsTrue(new CardsRepository().updateUsernameOfCardById("Test2","1"));

            Assert.AreNotEqual("Test1", new CardsRepository().getCardByid("1").username);
            Assert.AreEqual("Test2", new CardsRepository().getCardByid("1").username);
        }


        [Test]
        public void TestUpdateDeckFalse()
        {
            new CardsRepository().updateUsernameAfterGetPackage("Test1", 1);
            Assert.IsTrue(new CardsRepository().updateDeckTrue("1"));
            Assert.IsTrue(new CardsRepository().getCardByid("1").is_deck);

            Assert.IsTrue(new CardsRepository().updateDeckFalse("1"));
            Assert.IsFalse(new CardsRepository().getCardByid("1").is_deck);
        }
        
    }
}
