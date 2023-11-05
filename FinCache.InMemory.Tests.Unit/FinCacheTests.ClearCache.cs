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
        public void ClearCache_Should_Remove_All_Items_From_Cache()
        {
            //given
            var config = new FinCacheConfig { Capacity = 10 };
            var cache = new FinCache(config);

            //when 
            cache.AddCache("test-key-one", "test-value");
            cache.AddCache("test-key-two", "test-value");
            cache.AddCache("test-key-three", "test-value");
            cache.AddCache("test-key-four", "test-value");
            cache.ClearCache();

            //then
            var itemCount = cache.ItemCount;
            itemCount.Should().Be(0);
        }

    }
}
