using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data.repository;
using MTCG.repository;
using NUnit.Framework;

namespace MonsterCardTradingGame.Test
{
    public class TestUsersRepository
    {

        [SetUp]
        public void Setup()
        {
            Database.GetInstance().truncateTables();
            new UsersRepository().addUser("Test1", "test1");
            new UsersRepository().addUser("Test2", "test2");
        }
        [Test]
        public void TestGetUserPassword()
        {
            Assert.AreEqual("test1", new UsersRepository().getUser("Test1").password);
        }

        [Test]
        public void TestAddUser()
        {
            Assert.IsTrue(new UsersRepository().addUser("Test3", "test3"));
            Assert.IsFalse(new UsersRepository().addUser("Test1", "test1"));
        }

        [Test]
        public void TestGetUserByToken()
        {
            User user = new UsersRepository().getUserbyToken("Test1-mtcgToken");
            Assert.AreEqual("Test1", user.username);
            Assert.AreNotEqual("test1", user.username);
        }

        [Test]
        public void TestUpdateUser()
        {
            User user = new UsersRepository().getUser("Test1");
            Assert.IsEmpty(user.image);

            Assert.IsTrue(new UsersRepository().updateUser("Name Test", "Bio Test", "Image Test", user.username));

            User newUser = new UsersRepository().getUser("Test1");
            Assert.IsNotNull(newUser.image);
            Assert.AreEqual("Image Test", newUser.image);

        }


        [Test]
        public void TestUpdateCoin()
        {
            User user = new UsersRepository().getUser("Test1");
            Assert.AreEqual(20, user.coin);

            Assert.IsTrue(new UsersRepository().updateCoin(15, user.username));

            User newUser = new UsersRepository().getUser("Test1");
            Assert.AreEqual(15, newUser.coin);

        }

    }
}