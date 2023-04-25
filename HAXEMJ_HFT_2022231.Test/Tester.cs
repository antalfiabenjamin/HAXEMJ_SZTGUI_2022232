using System;
using System.Collections.Generic;
using System.Linq;
using HAXEMJ_HFT_2022231.Logic.Classes;
using HAXEMJ_HFT_2022231.Models;
using HAXEMJ_HFT_2022231.Repository;
using HAXEMJ_HFT_2022231.Repository.Interfaces;
using HAXEMJ_HFT_2022231.Repository.ModelRepos;
using Moq;
using NUnit.Framework;

namespace HAXEMJ_HFT_2022231.Test
{
    [TestFixture]
    public class Tester
    {
        PhoneLogic pl;
        UserLogic ul;

        Mock<IRepository<Phone>> mockPhoneRepo;
        Mock<IRepository<Manufacturer>> mockManuRepo;
        Mock<IRepository<User>> mockUserRepo;

        // FAKES (for lazyloading issue)

        static PhoneDbContext ctx = new PhoneDbContext();

        PhoneRepository p = new PhoneRepository(ctx);
        ManufacturerRepository m = new ManufacturerRepository(ctx);
        UserRepository u = new UserRepository(ctx);

        [SetUp]
        public void Init()
        {
            mockPhoneRepo = new Mock<IRepository<Phone>>();
            mockPhoneRepo.Setup(m => m.ReadAll()).Returns(new List<Phone>()
            {
                new Phone("1#iPhone 14#Purple#APL113#12#5.5"),
                new Phone("2#iPhone 12#Black#APL113#11#3.4"),
                new Phone("3#iPhone 12#Black#APL113#11#2.1"),
                new Phone("4#Galaxy S20#Black#SMG140#13#4")
            }.AsQueryable());

            mockManuRepo = new Mock<IRepository<Manufacturer>>();
            mockManuRepo.Setup(m => m.ReadAll()).Returns(new List<Manufacturer>()
            {
                new Manufacturer("APL113#Apple#USA"),
                new Manufacturer("SMG140#Samsung#Korea")
            }.AsQueryable());

            mockUserRepo = new Mock<IRepository<User>>();
            mockUserRepo.Setup(m => m.ReadAll()).Returns(new List<User>()
            {
                new User("11#John Smith#"),
                new User("12#Tom Whatts"),
                new User("13#Steve Johnson")
            }.AsQueryable());

        }

        [Test]
        public void GetAlliPhonesTest()
        {
            pl = new PhoneLogic(p, m, u);

            var query = pl.GetAlliPhones().ToList();
            var expectedResult = new List<Phone>()
            {
                new Phone()
                {
                    PhoneId = 1,
                    Name = "iPhone 14",
                },
                new Phone()
                {
                    PhoneId = 2,
                    Name = "iPhone 12",
                },
                new Phone()
                {
                    PhoneId = 3,
                    Name = "iPhone 12",
                }
            };

            Assert.AreEqual(expectedResult, query);
        }

        [TestCase("Black")]
        public void PreferredColorTest(string input)
        {
            pl = new PhoneLogic(p, m, u);

            var query = pl.GetPreferredColorPhones(input).ToList();
            var expectedResult = new List<Manufacturer>()
            {
                new Manufacturer()
                {
                    Name = "Apple",
                    ColorPhoneCount = 2
                },
                new Manufacturer()
                {
                    Name = "Samsung",
                    ColorPhoneCount = 1
                }
            };

            Assert.AreEqual(expectedResult, query);
        }

        [Test]
        public void UserPhoneCountTest()
        {
            pl = new PhoneLogic(p, m, u);

            var q = pl.GetPhoneCountByUser().ToList();
            var expected = new List<User>()
            {
                new User()
                {
                    Name = "Tom Whatts",
                    PhoneCount = 1
                },
                new User()
                {
                    Name = "John Smith",
                    PhoneCount = 2
                },
                new User()
                {
                    Name = "Steve Johnson",
                    PhoneCount = 1
                },
            };

            Assert.AreEqual(expected, q);
        }

        [TestCase("USA")]
        public void PhonesBasedOnLocationTest(string company)
        {
            pl = new PhoneLogic(p, m, u);

            var q = pl.PhonesBySpecificLocation(company).ToList();
            var expected = new List<Phone>()
            {
                new Phone()
                {
                    PhoneId = 1,
                    Name = "iPhone 14"
                },
                new Phone()
                {
                    PhoneId = 2,
                    Name = "iPhone 12"
                },
                new Phone()
                {
                    PhoneId = 3,
                    Name = "iPhone 12"
                }
            };

            Assert.AreEqual(expected, q);
        }

        [Test]
        public void AvgScreenTimeByUsersTest()
        {
            pl = new PhoneLogic(p, m, u);

            var query = pl.GetScreenTimeBd().ToList();
            var expected = new List<User>()
            {
                new User()
                {
                    Name = "Tom Whatts",
                    TotalAvgScrTime = 5.5
                },
                new User()
                {
                    Name = "John Smith",
                    TotalAvgScrTime = 2.75
                },new User()
                {
                    Name = "Steve Johnson",
                    TotalAvgScrTime = 4
                }
            };

            Assert.AreEqual(expected, query);
        }

        [Test]
        public void CreatePhoneTest()
        {
            pl = new PhoneLogic(mockPhoneRepo.Object, mockManuRepo.Object, mockUserRepo.Object);

            var p = new Phone() { Name = "iPhone 14" };
            pl.Create(p);

            mockPhoneRepo.Verify(r => r.Create(p), Times.Once);
        }

        [Test]
        public void CreatePhoneWithoutNameTest()
        {
            pl = new PhoneLogic(mockPhoneRepo.Object, mockManuRepo.Object, mockUserRepo.Object);

            var p = new Phone();
            try { pl.Create(p); } catch { }
            
            mockPhoneRepo.Verify(r => r.Create(p), Times.Never);
        }

        [Test]
        public void CreateUserWithTooShortName()
        {
            ul = new UserLogic(mockUserRepo.Object);

            var user = new User() { Name = "asd" };
            try { ul.Create(user); } catch { }

            mockUserRepo.Verify(r => r.Create(user), Times.Never);
        }

        [TestCase(1)]
        public void DeletePhoneTest(int id)
        {
            pl = new PhoneLogic(mockPhoneRepo.Object, mockManuRepo.Object, mockUserRepo.Object);

            var phone = mockPhoneRepo.Object.Read(id);
            pl.Delete(id);

            var all = mockPhoneRepo.Object.ReadAll();

            Assert.IsFalse(all.Contains(phone));
        }

        [TestCase(1)]
        public void ReadPhoneTest(int id)
        {
            pl = new PhoneLogic(mockPhoneRepo.Object, mockManuRepo.Object, mockUserRepo.Object);

            var phone = pl.Read(id);
            var expected = mockPhoneRepo.Object.Read(id);

            Assert.That(expected, Is.EqualTo(phone));
        }
    }
}

