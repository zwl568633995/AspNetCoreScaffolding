using Kay.Framework.Domain.UnitOfWork;
using Kay.Framework.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kay.Framework.AspNetCore.Mvc.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DatabaseController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 数据库迁移
        /// </summary>
        /// <returns></returns>
        [Route("/nalong-database/migration")]
        [HttpPost]

        public bool Migration()
        {
            var directory = new DirectoryInfo("SqlScripts");
            if (directory.Exists)
            {
                var latest = directory.GetFiles().OrderByDescending(a => a.CreationTime).FirstOrDefault();
                if (latest != null)
                {
                    var sql = System.IO.File.ReadAllText(latest.FullName, Encoding.UTF8);
                    _unitOfWork.ExecuteSqlNonQuery(sql);
                }
                else
                {
                    throw new NotFoundException("SqlScripts中数据库文件不存在");
                }
            }
            else
            {
                throw new NotFoundException("SqlScripts不存在");
            }

            return true;
        }

        /// <summary>
        /// 获取数据库版本,返回集合
        /// </summary>
        /// <returns></returns>
        [Route("/nalong-database/latest-version")]
        [HttpGet]

        public List<DatabaseVersionModel> GetDatabaseVersion()
        {
            var sql =
                "select vername as VersionName ,verdesc as VersionDesc,UpdateTime from version t where updatetime = (select max(updatetime) from version)";
            var result = _unitOfWork.FromSql<DatabaseVersionModel>(sql);
            return result.ToList();
        }
    }
}