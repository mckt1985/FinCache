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
        [Fact]
        public void GetCache_Should_Get_Value_By_Key()
        {
            //given
            var config = new FinCacheConfig { Capacity = 10 };
            var cache = new FinCache(config);
            var key = "test-key";
            
            //when 
            cache.AddCache(key, 1);

            //then
            var item = cache.GetCache(key);
            item.Should().Be(1);

        }

        [Theory]
        [InlineData(null)]
        public void GetCache_Should_Throw_Exception_If_Key_IsNull(string key)
        {
            //given
            var config = new FinCacheConfig { Capacity = 1 };
            var cache = new FinCache(config);

            //when 
            Action result = () => cache.GetCache(key);

            //then
            result.Should().Throw<ArgumentNullException>()
                .WithMessage("Key cannot be null*");

        }

        [Fact]
        public void GetCache_Should_Get_Values_By_Different_Key_Types()
        {
            //given
            var config = new FinCacheConfig { Capacity = 10 };
            var cache = new FinCache(config);
            var key1 = "test-value";
            var key2 = 2;
            
            //when 
            cache.AddCache(key1, 12345);
            cache.AddCache(key2, "test-value");

            //then
            var item1 = cache.GetCache(key1);
            var item2 = cache.GetCache(key2);

            item1.Should().Be(12345);
            item2.Should().Be("test-value");

        }

        [Fact]
        public void GetCache_Should_Add_Accessed_Key_To_Top_Of_Cache_List()
        {
            //given
            var config = new FinCacheConfig { Capacity = 5 };
            var cache = new FinCache(config);

            //when 
            cache.AddCache("test-key1", 1);
            cache.AddCache("test-key2", 1);
            cache.AddCache("test-key3", 1);
            cache.AddCache("test-key4", 1);
            cache.AddCache("test-key5", 1);

            _ = cache.GetCache("test-key4");

            //then
            var result = cache.GetFirstKey();

            result.Should().Be("test-key4");


        }

    }
}
