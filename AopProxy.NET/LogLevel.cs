using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 关闭
        /// </summary>
        Off,
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        Warn,
        /// <summary>
        /// 信息
        /// </summary>
        Info,
        /// <summary>
        /// 调试
        /// </summary>
        Debug,
        /// <summary>
        /// 全部
        /// </summary>
        All
    }
}
