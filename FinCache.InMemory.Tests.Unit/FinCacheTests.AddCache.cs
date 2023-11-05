using FinCache.InMemory.Config;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.InMemory.Tests.Unit
{
    public partial class FinCacheTests
    {

        [Theory]
        [InlineData("test-key", "test-value")]
        public void AddCache_Should_Add_Item_To_Cache(string key, string value)
        {
            //given
            var config = new FinCacheConfig { Capacity = 1 };
            var cache = new FinCache(config);

            //when 
            cache.AddCache(key, value);

            //then
            var item = cache.GetCache(key);
            item.Should().Be(value);

        }

        [Fact]
        public void AddCache_Should_Add_Values_Of_Different_Types_To_Cache()
        {
            //given
            var config = new FinCacheConfig { Capacity = 10 };
            var cache = new FinCache(config);
            var value1 = "test-value";
            var value2 = 2;
            var value3 = new string[] { "one", "two", "three", "four" };
            var value4 = new int[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };
            var value5 = 1.345;

            //when 
            cache.AddCache(1, value1);
            cache.AddCache(2, value2);
            cache.AddCache(3, value3);
            cache.AddCache(4, value4);
            cache.AddCache(5, value5);

            //then
            var itemCount = cache.ItemCount;
            itemCount.Should().Be(5);

        }

        [Fact]
        public void AddCache_Should_Add_Keys_Of_Different_Types_To_Cache()
        {
            //given
            var config = new FinCacheConfig { Capacity = 10 };
            var cache = new FinCache(config);
            var key1 = "test-value";
            var key2 = 2;
            var key3 = new string[] { "one", "two", "three", "four" };
            var key4 = new int[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };
            var key5 = 1.345;

            //when 
            cache.AddCache(key1, 1);
            cache.AddCache(key2, 2);
            cache.AddCache(key3, 3);
            cache.AddCache(key4, 4);
            cache.AddCache(key5, 5);

            //then
            var itemCount = cache.ItemCount;
            itemCount.Should().Be(5);
        }

        [Theory]
        [InlineData(null, "test-value")]
        public void AddCache_Should_Throw_Exception_If_Key_IsNull(string key, string value)
        {
            //given
            var config = new FinCacheConfig { Capacity = 1 };
            var cache = new FinCache(config);

            //when 
            Action result = () => cache.AddCache(key, value);

            //then
            result.Should().Throw<ArgumentNullException>()
                .WithMessage("Key cannot be null*");

        }

        [Theory]
        [InlineData("test-key", null)]
        public void AddCache_Should_Throw_Exception_If_Value_IsNull(string key, string value)
        {
            //given
            var config = new FinCacheConfig { Capacity = 1 };
            var cache = new FinCache(config);

            //when 
            Action result = () => cache.AddCache(key, value);

            //then
            result.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null*");

        }

        [Fact]
        public void AddCache_Should_Not_Exceed_Cache_Capacity()
        {
            //given
            var config = new FinCacheConfig { Capacity = 4 };
            var cache = new FinCache(config);           

            //when 
            cache.AddCache(1, 1);
            cache.AddCache(2, 2);
            cache.AddCache(3, 3);
            cache.AddCache(4, 4);
            cache.AddCache(5, 5);

            //then
            var itemCount = cache.ItemCount;
            itemCount.Should().Be(config.Capacity);
        }


        [Fact]
        public void AddCache_Should_Evict_Least_Recently_Used_When_Capacity_Reached()
        {
            //given
            var config = new FinCacheConfig { Capacity = 1 };
            var cache = new FinCache(config);

            //when
            cache.AddCache("test-key-one", 1);
            cache.AddCache("test-key-two", 2);

            //then
            cache.GetCache("test-key-one").Should().BeNull();
            cache.GetCache("test-key-two").Should().Be(2);

        }

        [Fact]
        public void AddCache_Should_Add_All_Items_To_Cache_On_Different_Threads() 
        {
            //given
            var config = new FinCacheConfig { Capacity = 1000 };
            var cache = new FinCache(config);

            //when
            Parallel.ForEach(Enumerable.Range(1, 1000), i =>
            {
                cache.AddCache(i, i);
            });

            //then
            var itemCount = cache.ItemCount;
            itemCount.Should().Be(config.Capacity);
        }

        [Fact]
        public void AddCache_Should_Add_Large_Amount_Of_Items_To_Cache()
        {
            //given
            var config = new FinCacheConfig { Capacity = 10000 };
            var cache = new FinCache(config);

            //when
            for (int i = 0; i < 10000; i++)
            {
                cache.AddCache(i, i);
            }
            
            //then
            var itemCount = cache.ItemCount;
            itemCount.Should().Be(config.Capacity);
        }
    }
}
