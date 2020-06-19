using Kay.Framework.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Utility.Utilities.Snowflake
{
    public class IdWorker
    {
        //基准时间
        public const long Twepoch = 1288834974657L;

        //机器标识位数
        private const int WorkerIdBits = 3;

        //数据标志位数
        private const int DatacenterIdBits = 3;

        //序列号识位数
        private const int SequenceBits = 8;

        //机器ID最大值
        private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);

        //数据标志ID最大值
        private const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        //序列号ID最大值
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        //机器ID偏左移12位
        private const int WorkerIdShift = SequenceBits;

        //数据ID偏左移17位
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;

        //时间毫秒左移22位
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        private static readonly IdWorker Instance; // 单例实例对象

        private static readonly object _lock = new object();
        private long _lastTimestamp = -1L;

        static IdWorker()
        {
            int initDatacenterId;
            int initWorkerId;
            try
            {
                initWorkerId = (Environment.MachineName.GetHashCode() % 7).Abs() + 1;
                initDatacenterId = (Environment.UserDomainName.GetHashCode() % 7).Abs() + 1;
            }
            catch
            {
                initWorkerId = new Random().Next(1, 7);
                initDatacenterId = new Random().Next(1, 7);
            }

            if (initWorkerId > initDatacenterId) initWorkerId = initDatacenterId;
            Instance = new IdWorker(initWorkerId, initDatacenterId);
        }

        public IdWorker(long workerId, long datacenterId, long sequence = 0L)
        {
            // 如果超出范围就抛出异常
            if (workerId > MaxWorkerId || workerId < 0)
                throw new ArgumentException(string.Format("worker Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxWorkerId));

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
                throw new ArgumentException(string.Format("region Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxDatacenterId));

            //先检验再赋值
            WorkerId = workerId;
            DatacenterId = datacenterId;
            Sequence = sequence;
        }

        public long WorkerId { get; protected set; }
        public long DatacenterId { get; protected set; }

        public long Sequence { get; internal set; }

        /// <summary>
        ///     默认的Id生成，每次调用生成的都不一样
        ///     【 没有配置workerId、datacenterId、long sequence）】
        /// </summary>
        public static long NewDefaultId => Instance.NextId();

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                if (timestamp < _lastTimestamp)
                    throw new Exception(string.Format("时间戳必须大于上一次生成ID的时间戳.  拒绝为{0}毫秒生成id", _lastTimestamp - timestamp));

                //如果上次生成时间和当前时间相同,在同一毫秒内
                if (_lastTimestamp == timestamp)
                {
                    //sequence自增，和sequenceMask相与一下，去掉高位
                    Sequence = (Sequence + 1) & SequenceMask;
                    //判断是否溢出,也就是每毫秒内超过1024，当为1024时，与sequenceMask相与，sequence就等于0
                    if (Sequence == 0)
                        //等待到下一毫秒
                        timestamp = TilNextMillis(_lastTimestamp);
                }
                else
                {
                    //如果和上次生成时间不同,重置sequence，就是下一毫秒开始，sequence计数重新从0开始累加,
                    //为了保证尾数随机性更大一些,最后一位可以设置一个随机数
                    //Sequence = 0; //new Random().Next(10);
                    Sequence = new Random(Guid.NewGuid().GetHashCode()).Next(255); // 2的8次方-1
                }

                _lastTimestamp = timestamp;
                var idWorker = ((timestamp - Twepoch) << TimestampLeftShift)
                              | (DatacenterId << DatacenterIdShift)
                              | (WorkerId << WorkerIdShift)
                              | Sequence;
                return idWorker;
            }
        }

        // 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp) timestamp = TimeGen();
            return timestamp;
        }

        // 获取当前的时间戳
        protected virtual long TimeGen()
        {
            return TimeExtension.CurrentTimeMillis();
        }
    }
}
