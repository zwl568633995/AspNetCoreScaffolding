using Kay.Framework.ObjectMapping.Abstractions;
using Kay.Framework.ObjectMapping.TinyMapper;
using Kay.Framework.ObjectMapping.TinyMapper.Tests.Data;
using System;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IMapper mapper = new TinyMapperMapper();
            mapper.Bind<PersonInputDto, Person>();

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
            Console.WriteLine("耗时"+stopwatch.ElapsedMilliseconds);
            Console.Read();
        }
    }
}
