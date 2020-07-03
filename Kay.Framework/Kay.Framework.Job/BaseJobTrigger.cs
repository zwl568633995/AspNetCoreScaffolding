
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace Kay.Framework.Job
{
    public abstract class BaseJobTrigger
        : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly TimeSpan _dueTime;
        private readonly TimeSpan _periodTime;

        private readonly IJobExecutor _jobExcutor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dueTime">到期执行时间</param>
        /// <param name="periodTime">间隔时间</param>
        /// <param name="jobExcutor">任务执行者</param>
        protected BaseJobTrigger(TimeSpan dueTime,
             TimeSpan periodTime,
             IJobExecutor jobExcutor)
        {
            _dueTime = dueTime;
            _periodTime = periodTime;
            _jobExcutor = jobExcutor;
        }

        #region 注入配置获取Logger
        public IServiceProvider ServiceProvider { get; set; }

        protected BaseJobTrigger(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        #endregion

        #region Logger

        protected ILogger Logger => LazyLogger.Value;

        public ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();

        private Lazy<ILogger> LazyLogger =>
            new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        #endregion

        #region  计时器相关方法

        private void StartTimerTrigger()
        {
            if (_timer == null)
                _timer = new Timer(ExcuteJob, _jobExcutor, _dueTime, _periodTime);
            else
                _timer.Change(_dueTime, _periodTime);
        }

        private void StopTimerTrigger()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void ExcuteJob(object obj)
        {
            try
            {
                var excutor = obj as IJobExecutor;
                excutor?.StartJob();
            }
            catch (Exception e)
            {
                Logger.LogError($"执行任务({nameof(GetType)})时出错，信息：{e}");
            }
        }
        #endregion

        #region Host方法
        /// <summary>
        ///  执行启动
        /// </summary>
        /// <returns></returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                StartTimerTrigger();
            }
            catch (Exception e)
            {
                Logger.LogError($"启动定时任务({nameof(GetType)})时出错，信息：{e}");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        ///  执行关闭
        /// </summary>
        /// <returns></returns>
        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _jobExcutor.StopJob();
                StopTimerTrigger();
            }
            catch (Exception e)
            {
                Logger.LogError($"停止定时任务({nameof(GetType)})时出错，信息：{e}");
            }
            return Task.CompletedTask;
        }
        #endregion

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
