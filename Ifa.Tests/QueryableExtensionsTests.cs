using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Ifa.Helpers;
using Moq;
using NUnit.Framework;

namespace Ifa.Tests
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class EntityViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [TestFixture]
    public class QueryableExtensionsTests
    {
        [Test]
        public void Can_Use_Page_On_IQueryable_To_Return_PagedResultViewModel_Of_Entity()
        {
            var entities = new List<Entity>{new Entity{Id = 1, Name = "Entity1", Value = 267.78M}};
            var queryable = entities.AsQueryable();
            var pagedResult = queryable.Page();

            Assert.That(pagedResult, Is.Not.Null); 
            Assert.That(pagedResult.Result, Is.InstanceOf<IList<Entity>>());
        }

        [Test]
        public void Can_Use_Page_On_IQueryable_To_Return_PagedResultViewModel_Of_EntityViewModel()
        {
            Mapper.CreateMap<Entity, EntityViewModel>();

            var entities = new List<Entity> { new Entity { Id = 1, Name = "Entity1", Value = 267.78M } };
            var queryable = entities.AsQueryable();
            var pagedResult = queryable.Page<Entity,EntityViewModel>();

            Assert.That(pagedResult, Is.Not.Null);
            Assert.That(pagedResult.Result, Is.InstanceOf<IList<EntityViewModel>>());
        }
    }
}
