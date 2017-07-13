using NLog;

namespace Yushen.NLog
{
    /// <summary>
    /// NLog测试类
    /// 
    /// 调用前需要通过程序包管理器控制台先安装NLog：Install-Package NLog.Config
    /// 并在NLog.config文件中配置好target和rule
    /// </summary>
    class NLogTest
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public NLogTest()
        {
            logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");
        }
    }
}
