using Kay.Framework.ObjectMapping.Abstractions;
using Kay.Framework.ObjectMapping.TinyMapper.Tests.Data;
using System;
using System.Diagnostics;
using Xunit;

namespace Kay.Framework.ObjectMapping.TinyMapper.Tests
{
    public class TinyMapperTest
    {
        [Fact]
        public void Test1()
        {
            IMapper mapper = new TinyMapperMapper();
            mapper.Bind<Person, PersonInputDto>();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100; i++)
            {
                var source = new PersonInputDto
                {
                    Name = i.ToString(),
                };
                var target = Nelibur.ObjectMapper.TinyMapper.Map<Person>(source);
            }
            stopwatch.Stop();
            Assert.Equal(100, stopwatch.ElapsedMilliseconds);
        }
    }
}
