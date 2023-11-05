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
        public void Remove_Should_Remove_Item_From_Cache()
        {
            // given
            var config = new FinCacheConfig { Capacity = 1 };
            var cache = new FinCache(config);
            cache.AddCache("test-key-one", "test-value");

            // when
            cache.Remove("key");

            // then
            cache.GetCache("key").Should().BeNull();
        }

        [Fact]
        public void RemoveFromCache_Should_Evict_Oldest_Item_From_Cache()
        {
            // given
            var config = new FinCacheConfig { Capacity = 2 };
            var cache = new FinCache(config);
            cache.AddCache("test-key-one", "test-value-one");
            cache.AddCache("test-key-two", "test-value-two");

            // when
            cache.RemoveFromCache();

            // then
            cache.GetCache("test-key-one").Should().BeNull();
            cache.GetCache("test-key-two").Should().Be("test-value-two");
        }
    }
}
