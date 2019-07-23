using EZSpecification;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace EZSpecification.Test
{
    public class SpecificationExtensions_Test
    {
        private readonly IEnumerable<Item> _items;

        public SpecificationExtensions_Test()
        {
            _items = new[]
            {
                new Item("Specialized"),
                new Item("Cervelo"),
                new Item("Trek"),
                new Item("Giant")
            };
        }

        [Fact]
        [Trait("Category", "integration")]
        public void And_Has_Single_Item()
        {
            // Assemble
            var spec = AssembleAnd(out var item);

            // Act
            var result = _items.Where(spec);
            
            // Assert
            Assert.Single(result);
        }

        [Fact]
        [Trait("Category", "integration")]
        public void And_Has_Correct_Item()
        {
            // Assemble
            var spec = AssembleAnd(out var item);

            // Act
            var result = _items.Where(spec);
            
            // Assert
            Assert.Equal(item, result.First());
        }

        [Fact]
        [Trait("Category", "integration")]
        public void Or_Contains_First_Item()
        {
            // Assemble
            var testSpec = AssembleOr(out var item1, out var item2);

            // Act
            var results = _items.Where(testSpec.Expression.Compile());

            // Assert
            Assert.Contains(item1, results);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Or_Contains_Second_Item()
        {
            // Assemble
            var testSpec = AssembleOr(out var item1, out var item2);

            // Act
            var results = _items.Where(testSpec.Expression.Compile());

            // Assert
            Assert.Contains(item2, results);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Or_Contains_Correct_Item_Number()
        {
            // Assemble
            var testSpec = AssembleOr(out var item1, out var item2);

            // Act
            var results = _items.Where(testSpec);

            // Assert
            Assert.Equal(2, results.Count());
        }

        private Specification<Item> AssembleAnd(out Item item)
        {
            item = _items.ElementAt((int)(_items.Count() / 2));
         
            var itemName = item.Name;

            var spec1 = new Mock<Specification<Item>>();
            spec1.SetupGet(i => i.Expression)
                 .Returns(i => i.Name.StartsWith(itemName.Substring(0, 2)));

            var spec2 = new Mock<Specification<Item>>();
            spec2.SetupGet(i => i.Expression)
                 .Returns(i => i.Name.Contains(itemName.Substring(itemName.Length / 2, 2)));

            return spec1.Object.And(spec2.Object);
        }

        private Specification<Item> AssembleOr(out Item item1, out Item item2)
        {
            item1 = _items.First();
            item2 = _items.Last();

            var item1Name = item1.Name;
            var item2Name = item2.Name;

            var spec1 = new Mock<Specification<Item>>();
            spec1.SetupGet(i => i.Expression)
                 .Returns(i => i.Name == item1Name);


            var spec2 = new Mock<Specification<Item>>();
            spec2.SetupGet(i => i.Expression)
                 .Returns(i => i.Name == item2Name);

            return spec1.Object.Or(spec2.Object);
        }

        public class Item
        {
            public Guid Id { get; }
            public string Name { get; }

            public Item(string name)
            {
                Id = Guid.NewGuid();
                Name = name;
            }
        }
    }
}
