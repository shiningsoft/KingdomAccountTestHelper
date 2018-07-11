using System;
using System.Data;
using System.Reflection;

/// <summary>
/// 系统数据字典
/// 纯大写字母为统一账户标准数据字典
/// 驼峰命名法为自定义数据字典
/// </summary>
namespace Yushen.WebService.KessClient.Dict
{

    /// <summary>
    /// 认证类型
    /// </summary>
    class AUTH_TYPE : Dict
    {
        public const string 密码 = "0";
        public const string 证书 = "1";
        public const string 指纹 = "2";
    }
    
    /// <summary>
    /// 风险测评结果
    /// </summary>
    class RiskTestLevel : Dict
    {
        public const string 保守型 = "A";
        public const string 谨慎型 = "B";
        public const string 稳健型 = "C";
        public const string 积极型 = "D";
        public const string 激进型 = "E";
    }
    /// <summary>
    /// 银证开户操作类型
    /// </summary>
    class CubsbScOpenAcctOpType : Dict
    {
        public const string 一步式 = "0";
        public const string 预指定 = "1";
    }

    /// <summary>
    /// 银行代码
    /// </summary>
    class BankCode : Dict
    {
        public const string 农业银行 = "5100";
        public const string 招商银行 = "5500";
        public const string 建设银行 = "5000";
        public const string 中国银行 = "5300";
        public const string 工商银行 = "5200";
        public const string 兴业银行 = "5700";
        public const string 光大银行 = "5800";
        public const string 交通银行 = "5400";
        public const string 中信银行 = "5600";
        public const string 民生银行 = "6000";
        public const string 平安银行 = "6100";
        public const string 上海银行 = "7100";
        public const string 华夏银行 = "5900";
        public const string 广发银行 = "6200";
        public const string 邮储银行 = "8500";
        public const string 浦发银行 = "6300";
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    class YMT_STATUS : Dict
    {
        public const string 正常 = "0";
        public const string 注销 = "1";
    }

    /// <summary>
    /// 业务类型（非必传） 
    /// 创业板开通或查询时必输，01-开通 03-查询； 
    /// 使用信息维护时必输，01-新增，02-撤销，03-查询；
    /// 中登身份校验时必输，01-简项查询，无照片返回，02-简项查询，有照片返回；
    /// </summary>
    class AcctBiz_CLS : Dict
    {
        public const string 创业板_开通 = "01";
        public const string 创业板_查询 = "03";
        public const string 使用信息维护_新增 = "01";
        public const string 使用信息维护_撤销 = "02";
        public const string 使用信息维护_查询 = "03";
        public const string 中登身份校验_简项查询_无照片返回 = "01";
        public const string 中登身份校验_简项查询_有照片返回 = "02";
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    class OPERATION_TYPE : Dict
    {
        public const string 增加密码 = "0";
        public const string 修改密码 = "1";
        public const string 重置密码 = "3";
    }

    /// <summary>
    /// 资产账户状态
    /// </summary>
    class ACCT_STATUS : Dict
    {
        public const string 正常 = "00";
        public const string 挂失 = "01";
        public const string 冻结 = "02";
        public const string 休眠 = "03";
        public const string 注销 = "04";
        public const string 禁买 = "05";
        public const string 禁卖 = "06";
    }

    /// <summary>
    /// 手输职业
    /// </summary>
    class OCCUPATION : Dict
    {
        public const string 专业技术人员 = "专业技术人员";
        public const string 一般工商业服务业人员 = "一般工商业、服务业人员";
        public const string 农林牧渔水利业生产人员 = "农、林、牧、渔、水利业生产人员";
        public const string 生产运输设备操作人员及有关人员 = "生产、运输设备操作人员及有关人员";
        public const string 自由职业者 = "自由职业者";
        public const string 艺术品收藏拍卖等从业人员 = "艺术品收藏、拍卖等从业人员";
        public const string 娱乐场所博彩影视等从业人员 = "娱乐场所、博彩、影视等从业人员";
    }

    /// <summary>
    /// 操作类型
    /// 用于中登业务,一般情况都送0（增加）
    /// </summary>
    class OPERATOR_TYPE : Dict
    {
        public const string 增加 = "0";
        public const string 修改 = "1";
        public const string 删除 = "2";
    }

    /// <summary>
    /// 是否
    /// </summary>
    class YES_NO : Dict
    {
        public const string 否 = "0";
        public const string 是 = "1";
    }

    /// <summary>
    /// 复核状态
    /// 默认为0-未复核，若操作不需要进行操作员复核，则CHK_STATUS必须传2-已通过
    /// </summary>
    class CHK_STATUS : Dict
    {
        public const string 未复核 = "0";
        public const string 复核中 = "1";
        public const string 已通过 = "2";
        public const string 已撤销 = "3";
        public const string 已过期 = "4";
        public const string 待审核 = "5";
        public const string 通过 = "6";
        public const string 未通过 = "7";
        public const string 审核中 = "8";
    }

    /// <summary>
    /// 网络服务
    /// </summary>
    class NET_SERVICE : Dict
    {
        public const string 否 = "0";
        public const string 是 = "1";
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    class USER_TYPE : Dict
    {
        public const string 个人 = "0";
        public const string 机构 = "1";
        public const string 产品 = "2";
    }

    /// <summary>
    /// 网上业务类型
    /// </summary>
    class WEB_BIZ : Dict
    {
        public const string 网上自助 = "0";
        public const string 视频见证 = "1";
        public const string 双人见证 = "2";
        public const string 手机自助 = "3";
    }

    /// <summary>
    /// 签约类别
    /// </summary>
    class SIGN_CLS : Dict
    {
        public const string T加0开通 = "0";
        public const string T加2开通交易 = "2";
        public const string T加5开通交易 = "5";
        public const string 无需在本公司签署风险揭示书 = "A";
    }

    /// <summary>
    /// 开通类型
    /// 0-T+0 2-T+2 5-T+5 A-无需在本公司签署风险揭示书
    /// </summary>
    class OPEN_TYPE : Dict
    {
        public const string T加0 = "0";
        public const string T加2 = "2";
        public const string T加5 = "5";
        public const string 无需在本公司签署风险揭示书 = "A";
    }

    /// <summary>
    /// 操作渠道
    /// </summary>
    class CHANNEL : Dict
    {
        public const string 柜台系统 = "0";
        public const string 漫游委托 = "1";
        public const string 电话委托 = "2";
        public const string 刷卡委托 = "3";
        public const string 热键委托 = "4";
        public const string 远程委托 = "5";
        public const string 分支委托 = "6";
        public const string 银行委托 = "7";
        public const string 网上委托 = "8";
        public const string 呼叫中心 = "9";
        public const string 手机炒股 = "a";
        public const string PB委托 = "p";
    }

    /// <summary>
    /// 首选联系地址
    /// </summary>
    class LINKADDR_ORDER : Dict
    {
        public const string 家庭地址 = "1";
        public const string 单位地址 = "2";
    }

    /// <summary>
    /// 首选联系地址
    /// </summary>
    class EDUCATION : Dict
    {
        public const string 博士 = "0";
        public const string 硕士 = "1";
        public const string 本科 = "2";
        public const string 大专 = "3";
        public const string 中专 = "4";
        public const string 高中 = "5";
        public const string 初中以下 = "6";
        public const string 其他 = "Z";
    }

    /// <summary>
    /// 反洗钱等级
    /// </summary>
    class AML_LVL : Dict
    {
        public const string 未定义 = "*";
        public const string 低 = "0";
        public const string 中 = "1";
        public const string 高 = "2";
        public const string 黑名单 = "3";
    }

    /// <summary>
    /// 预约开户方式
    /// </summary>
    class APPT_CHNL : Dict
    {
        public const string 柜台预约 = "0";
        public const string WEB方式预约 = "1";
        public const string 银行预约无确认 = "2";
        public const string 银行预约需确认 = "3";
        public const string 离柜开户 = "4";
        public const string 非现场开户新开户 = "5";
        public const string 非现场开户转户 = "6";
    }

    /// <summary>
    /// 预约来源
    /// </summary>
    class APPT_SOURCE : Dict
    {
        public const string 未定义 = "*";
        public const string 现场客户 = "0";
        public const string 工行预约 = "1";
        public const string 国元金网预约 = "2";
        public const string 社区 = "3";
        public const string 转介绍 = "4";
        public const string 国元CRM预约 = "5";
        public const string 展业平台开户 = "6";
        public const string 微信预约 = "7";
        public const string 网站自助开户 = "8";
    }

    /// <summary>
    /// 电子协议类型
    /// </summary>
    class CONTRACT_TYPE : Dict
    {
        public const string 投顾产品 = "00";
        public const string 网上开户业务约定书 = "01";
        public const string 风险揭示书 = "02";
        public const string 客户须知 = "03";
        public const string 上海证券交易所个人投资者行为指引 = "04";
        public const string 证券交易委托代理协议 = "05";
        public const string 个人数字证书申请责任书 = "06";
        public const string 数字证书业务风险提示书 = "07";
        public const string 三方存管协议 = "10";
        public const string OTC产品电子协议签署 = "20";
        public const string 网上开户电子协议 = "30";
        public const string 金易贷电子协议 = "40";
    }


    /// <summary>
    /// 电子协议类别
    /// </summary>
    class CONTRACT_CLS : Dict
    {
        public const string 投顾产品0 = "000";
        public const string 投顾产品1 = "001";
        public const string 投顾产品2 = "002";
        public const string 投顾产品3 = "003";
        public const string 投顾产品4 = "004";
        public const string 投顾产品5 = "005";
        public const string 网上开户业务约定书 = "010";
        public const string 风险揭示书 = "020";
        public const string 客户须知 = "030";
        public const string 上海证券交易所个人投资者行为指引 = "040";
        public const string 证券交易委托代理协议 = "050";
        public const string 个人数字证书申请责任书 = "060";
        public const string 数字证书业务风险提示书 = "070";
        public const string 三方存管协议 = "100";
        public const string 兴业银行三方存管协议 = "101";
        public const string 农业银行三方存管协议 = "102";
        public const string 招商银行三方存管协议 = "103";
        public const string 交通银行三方存管协议 = "104";
        public const string 建设银行三方存管协议 = "105";
        public const string 工商银行三方存管协议 = "106";
        public const string 中国银行三方存管协议 = "107";
        public const string 中信银行三方存管协议 = "108";
        public const string 华夏银行三方存管协议 = "109";
        public const string 民生银行三方存管协议 = "10a";
        public const string 光大银行三方存管协议 = "10b";
        public const string 平安银行三方存管协议 = "10c";
        public const string 广发银行三方存管协议 = "10d";
        public const string 浦发银行三方存管协议 = "10f";
        public const string 上海银行三方存管协议 = "10g";
        public const string OTC电子约定书 = "201";
        public const string OTC电子合同 = "202";
        public const string 理财协议 = "203";
        public const string 客户账户开户协议 = "300";
        public const string 客户须知302 = "302";
        public const string 指定交易协议 = "303";
        public const string 沪深权证风险揭示书 = "304";
        public const string 上海证券交易所风险警示股票交易风险揭示书 = "305";
        public const string 沪深交易所退市整理期股票交易风险揭示书 = "306";
        public const string 上海交易所个人投资者行为指引 = "307";
        public const string 证券交易委托代理协议308 = "308";
        public const string 证券投资基金投资人权益须知 = "309";
        public const string 证券交易委托风险揭示书 = "30a";
        public const string 开放式基金电话委托及网上交易委托协议书 = "30b";
        public const string 金易贷贷款协议 = "401";
    }

    /// <summary>
    /// 报送状态
    /// 文档描述：0-未报,1-已报,9-报送失败，与统一设置不一致
    /// </summary>
    class DEAL_STATUS : Dict
    {
        public const string 获取成功_全部 = "0";
        public const string 获取成功_部分 = "1";
        public const string 未获取 = "2";
    }

    /// <summary>
    /// 签署地
    /// 0-本证券公司 1-其它证券公司
    /// </summary>
    class SIGN_PLACE : Dict
    {
        public const string 本证券公司 = "0";
        public const string 其它证券公司 = "1";
    }

    /// <summary>
    /// 资产账户属性
    /// </summary>
    class CUACCT_ATTR : Dict
    {
        public const string 普通账户 = "0";
        public const string 信用账户 = "1";
        public const string OTC支付账户 = "2";
        public const string 股票期权 = "3";
        public const string OTC独立存管账户 = "4";
    }

    /// <summary>
    /// 资产账户状态
    /// </summary>
    class CUACCT_STATUS : Dict
    {
        public const string 正常 = "0";
        public const string 锁定 = "1";
        public const string 冻结 = "2";
        public const string 挂失 = "3";
        public const string 休眠 = "4";
        public const string 不合格 = "5";
        public const string 销户 = "9";
    }

    /// <summary>
    /// 首选联系方式
    /// </summary>
    class LINGKINDS_ORDER : Dict
    {
        public const string 手机 = "1";
        public const string 固定电话 = "2";
        public const string 电子邮件 = "3";
        public const string 传真 = "4";
    }

    /// <summary>
    /// 首选联系电话
    /// </summary>
    class LINKTEL_ORDER : Dict
    {
        public const string 手机 = "1";
        public const string 家庭电话 = "2";
        public const string 工作电话 = "3";
        public const string 小灵通 = "4";
    }

    /// <summary>
    /// 性别
    /// </summary>
    class SEX : Dict
    {
        public const string 男 = "0";
        public const string 女 = "1";
        public const string 其他 = "2";
    }

    /// <summary>
    /// 影像状态
    /// </summary>
    class IMG_STATUS : Dict
    {
        public const string 正常 = "0";
        public const string 未上传 = "1";
        public const string 上传未审核 = "2";
        public const string 修改未审核 = "3";
        public const string 上传审核取消 = "4";
        public const string 修改审核取消 = "5";
        public const string 删除 = "6";
        public const string 作废 = "7";
        public const string 丢失 = "8";
        public const string 未采集 = "A";
        public const string 已采集 = "B";
        public const string 任务流水被异常 = "T";
    }

    /// <summary>
    /// 银证业务类型
    /// </summary>
    class CUBSB_TYPE : Dict
    {
        public const string 转帐 = "0";
        public const string 存管 = "1";
        public const string 银衍 = "2";
    }

    /// <summary>
    /// 客户类别
    /// </summary>
    class CUST_CLS : Dict
    {
        public const string 标准客户 = "0";
        public const string 核心客户 = "1";
        public const string 重要客户 = "2";
        public const string PB客户 = "8";
        public const string 透支帐户 = "m";
        public const string 关联方帐户 = "n";
        public const string 权属不清存疑 = "o";
        public const string 涉嫌违规存疑 = "p";
        public const string 调查帐户存疑 = "q";
        public const string 监控帐户 = "r";
        public const string 权证大赛 = "s";
    }

    /// <summary>
    /// 对接远程系统
    /// </summary>
    class REMOTE_SYS : Dict
    {
        public const string 普通交易系统 = "0";
        public const string 融资融券系统 = "1";
        public const string 股票期权系统 = "2";
        public const string 统一认证系统 = "3";
    }

    /// <summary>
    /// 客户类型
    /// </summary>
    class CUST_TYPE :Dict
    {
        public const string 普通 = "0";
        public const string 自营 = "1";
        public const string 资管 = "2";
        public const string QFII = "3";
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    class ID_TYPE : Dict
    {
        public const string 身份证 = "00";
        public const string 护照 = "01";
        public const string 军官证 = "02";
        public const string 士兵证 = "03";
        public const string 回乡证 = "04";
        public const string 户口本 = "05";
        public const string 外国护照 = "06";
        public const string 技术监督局号码 = "08";
        public const string 其它证件 = "09";
        public const string 香港居民通行证 = "0b";
        public const string 澳门居民通行证 = "0c";
        public const string 台湾居民通行证 = "0d";
        public const string 外国人永久居留证 = "0e";
        public const string 社会保障号 = "0f";
        public const string 文职证 = "0g";
        public const string 警官证 = "0h";
        public const string 香港居民身份证 = "0i";
        public const string 澳门居民身份证 = "0j";
        public const string 工商营业执照 = "10";
        public const string 社团法人注册登记证书 = "11";
        public const string 机关法人成立批文 = "12";
        public const string 事业法人成立批文 = "13";
        public const string 境外有效商业登记证明文件 = "14";
        public const string 武警 = "15";
        public const string 军队 = "16";
        public const string 基金会 = "17";
        // public const string 技术监督局号码 = "18";
        public const string 其它证书 = "19";
        public const string 组织机构代码证 = "1A";
        public const string 批文 = "1Z";
    }

    /// <summary>
    /// 开户类别
    /// </summary>
    class ACCT_OPENTYPE : Dict
    {
        public const string 临柜办理开户 = "0";
        public const string 客户网上自助 = "1";
        public const string 视频见证开户 = "2";
        public const string 双人见证开户 = "3";
    }

    /// <summary>
    /// 交易板块
    /// </summary>
    class STKBD : Dict
    {
        public const string 深圳A股 = "00";
        public const string 深圳B股 = "01";
        public const string 上海A股 = "10";
        public const string 上海B股 = "11";
        public const string 股转A = "20";
        public const string 股转B = "21";
        public const string 深三板A = "02";
        public const string 深三板B = "03";
        public const string 深圳个股期权 = "05";
        public const string 上海个股期权 = "15";
    }

    /// <summary>
    /// 证券账户类别
    /// </summary>
    class ACCT_TYPE : Dict
    {
        public const string 沪市A股账户 = "11";
        public const string 沪市B股账户 = "12";
        public const string 沪市封闭式基金账户 = "13";
        public const string 沪市A股信用证券账户 = "14";
        public const string 沪市衍生品合约账户 = "15";
        public const string 深市A股账户 = "21";
        public const string 深市B股账户 = "22";
        public const string 深市封闭式基金账户 = "23";
        public const string 深市A股信用证券账户 = "24";
        public const string 深市衍生品合约账户 = "25";
        public const string 全国中小企业股份转让系统 = "31";
        public const string 其他 = "99";
    }

    /// <summary>
    /// 证券账户代理中登业务
    /// </summary>
    class ACCTBIZ_EXCODE : Dict
    {
        public const string 一码通账户开立 = "01";
        public const string 证券账户开立 = "02";
        public const string 账户注册资料修改 = "03";
        public const string 一码通账户注销 = "04";
        public const string 证券账户注销 = "05";
        public const string 账户注册资料查询 = "06";
        public const string 证券账户查询 = "07";
        public const string 一码通账户查询 = "08";
        public const string 首次交易日期查询 = "09";
        public const string 账户资料核对 = "10";
        public const string 证券账户使用信息维护 = "11";
        public const string 合伙人信息维护 = "12";
        public const string 适当性管理信息维护 = "13";
        public const string 信息批量查询申请 = "14";
        public const string 客户关键信息修改历史查询 = "15";
        public const string 休眠账户激活 = "16";
        public const string 不合格账户解除限制 = "17";
        public const string 证券账户关联关系确认 = "18";
        public const string 证券账户解除挂失 = "19";
        public const string 存量账户关联关系报送信息查询 = "20";
        public const string 产品客户一码通账户开立 = "22";
        public const string 身份信息验证 = "23";

    }

    /// <summary>
    /// 账户业务处理状态
    /// </summary>
    class ACCTBIZ_STATUS : Dict
    {
        public const string 未发送 = "0";
        public const string 已发送 = "1";
        public const string 处理成功 = "2";
        public const string 处理失败 = "3";
    }

    /// <summary>
    /// 适当性类别
    /// </summary>
    class PROPER_CLS : Dict
    {
        public const string 创业板 = "1";
        public const string 其他 = "9";
    }

    /// <summary>
    /// 协议类型 DD[CUST_AGMT_TYPE]
    /// </summary>
    class CUST_AGMT_TYPE : Dict
    {
        public const string 代理新股申购 = "00";
        public const string 代理新股配售 = "01";
        public const string 代理配股缴款 = "02";
        public const string 证券投资基金风险揭示书 = "03";
        public const string 客户委托理财电子签名约定书 = "07";
        public const string 代理无效 = "08";
        public const string 权证交易协议 = "09";
        public const string 创业板协议 = "0A";
        public const string 开通债券回购协议 = "0B";
        public const string 股票期权自动行权协议 = "0G";
        public const string 开通报价回购 = "0L";
        public const string 开通跨境跨市场ETF = "0M";
        public const string 开通协议大宗交易 = "0N";
        public const string 债券专业投资者协议 = "0O";
        public const string 开通优先股转让交易权限 = "0P";
        public const string 中小企业私募债券协议 = "0S";
        public const string 新债券合格投资者协议 = "0W";
        public const string 开通约定购回 = "0Y";
        public const string 客户退市协议 = "0b";
        public const string 股转受限投资者交易协议 = "0c";
        public const string 风险警示协议 = "0e";
        public const string 自主行权协议 = "0f";
        public const string 开通上证LOF交易业务权限 = "0g";
        public const string 挂牌公司股票交易协议 = "0i";
        public const string 港股通协议 = "0l";
        public const string 股票质押回购融入交易协议 = "0m";
        public const string 股票质押回购融出交易协议 = "0n";
        public const string 风险警示债券买入协议 = "0o";
        public const string 暂停上市债券买入协议 = "0p";
        public const string 小贷通交易融入权限 = "0q";
        public const string 资管产品份额交易协议 = "0r";
        public const string 黄金ETF协议 = "0s";
        public const string 国债预发行交易协议 = "0u";
        public const string 股票期权协议 = "0w";
        public const string 两网退市股票交易协议 = "0y";
        public const string 第三方存管单客户多银行协议 = "0z";
        public const string 债券合格投资者协议 = "11";
        public const string 优先股转让适当性协议 = "12";
        public const string 证券电子签名约定书 = "14";
        public const string 场内基金盲拆协议 = "15";
        public const string 质押协议回购融入协议 = "17";
        public const string 质押协议回购融出协议 = "18";
        public const string 股转优先股交易协议 = "21";
        public const string 一键登录协议 = "22";
        public const string 分级基金合格投资者权限 = "26";
    }

    /// <summary>
    /// 交易账户状态
    /// </summary>
    class TRDACCT_STATUS : Dict
    {
        public const string 正常 = "0";
        public const string 挂失 = "1";
        public const string 冻结 = "2";
        public const string 休眠 = "4";
        public const string 禁买 = "5";
        public const string 禁卖 = "6";
        public const string 注销 = "9";
    }

    /// <summary>
    /// 交易账户类别
    /// </summary>
    class TRDACCT_EXCLS : Dict
    {
        public const string 个人帐户 = "0";
        public const string 机构帐户 = "1";
        public const string 个人信用帐户 = "2";
        public const string 机构信用帐户 = "3";
        public const string 个人基金帐户 = "4";
        public const string 机构基金帐户 = "5";
        public const string 产品普通账户 = "6";
        public const string 产品基金账户 = "7";
        public const string 产品信用账户 = "8";
        public const string 境外账户 = "9";
        public const string 全部 = "@";
    }

    /// <summary>
    /// 交易指定状态
    /// </summary>
    class TREG_STATUS : Dict
    {
        public const string 未指定 = "0";
        public const string 首日指定 = "1";
        public const string 已指定 = "2";
    }

    /// <summary>
    /// 交易指定状态
    /// </summary>
    class BREG_STATUS : Dict
    {
        public const string 未指定 = "0";
        public const string 首日指定 = "1";
        public const string 已指定 = "2";
    }

    /// <summary>
    /// 用户角色
    /// </summary>
    class USER_ROLE : Dict
    {
        public const string 结算法人 = "0";
        public const string 客户 = "1";
        public const string 操作员 = "2";
        public const string 经纪人 = "3";
        public const string 代理人 = "4";
    }

    /// <summary>
    /// 使用范围
    /// </summary>
    class USE_SCOPE : Dict
    {
        public const string 登录和交易 = "0";
        public const string 资金业务 = "1";
        public const string 信用交易和登录密码 = "2";
        public const string 信用资金密码 = "3";
        public const string 股票期权交易密码 = "4";
        public const string 股票期权资金密码 = "5";
        public const string OTC交易密码 = "6";
        public const string OTC资金密码 = "7";
        public const string 消费支付密码 = "8";
        public const string 网厅用户密码 = "W";
    }

    /// <summary>
    /// 测评类别
    /// </summary>
    class SURVEY_CLS : Dict
    {
        public const string 经纪业务问卷测评 = "0";
        public const string 基金投资者问卷测评 = "1";
        public const string 元e贷征信评级 = "2";
        public const string 信用账户征信资料 = "3";
        public const string 股票期权适当性评估 = "5";
        public const string 股票期权投资人准入标准 = "6";
        public const string OTC风险测评 = "7";
        public const string 元e贷风险测评 = "8";
        public const string 港股通评估问卷 = "9";
        public const string 私募投资基金投资者风险问卷 = "S";
    }

    /// <summary>
    /// 职业类型
    /// </summary>
    class OCCU_EXTYPE : Dict
    {
        public const string 文教科卫专业人员 = "01";
        public const string 党政在职或离退休机关干部 = "02";
        public const string 企事业单位干部 = "03";
        public const string 行政企事业单位工人 = "04";
        public const string 农民 = "05";
        public const string 个体 = "06";
        public const string 无业 = "07";
        public const string 军人 = "08";
        public const string 学生 = "09";
        public const string 自由职业者 = "0A";
        public const string 证券从业人员 = "10";
        public const string 离退休 = "11";
        public const string 其他 = "99";
    }
    /// <summary>
    /// 远程同步系统（风险测评）
    /// </summary>
    class SURVEY_SYN : Dict
    {
        public const string 集中交易系统 = "0";
        public const string 融资融券系统 = "1";
        public const string OTC系统 = "2";
        public const string 股票期权系统 = "3";
        public const string 集中交易系统_存在客户号 = "4";
        public const string 不同步 = "9";
    }

    /// <summary>
    /// 货币
    /// </summary>
    class CURRENCY : Dict
    {
        public const string 人民币 = "0";
        public const string 港币 = "1";
        public const string 美元 = "2";
    }

    /// <summary>
    /// 子系统
    /// </summary>
    class SUBSYS : Dict
    {
        public const string 簿记子系统 = "1";
        public const string 融资融券子系统 = "10";
        public const string 消费支付子系统 = "11";
        public const string OTC子系统 = "13";
        public const string 股票期权子系统 = "16";
        public const string 资金管理系统 = "18";
        public const string 清算子系统 = "2";
        public const string 统一认证子系统 = "27";
        public const string 交易子系统 = "3";
        public const string 影像子系统 = "30";
        public const string 期货IB子系统 = "31";
        public const string CRM数据中心 = "32";
        public const string 数据服务中心 = "4";
        public const string 其他 = "9";
    }

    /// <summary>
    /// 证券业务
    /// </summary>
    class STK_BIZ : Dict
    {
        public const string 证券买入 = "100";
        public const string 证券卖出 = "101";
        public const string 证券可售冻结卖出 = "102";
        public const string 新股申购 = "103";
        public const string 新股增发 = "104";
        public const string 配股 = "106";
        public const string 要约收购 = "107";
        public const string 解除要约 = "108";
        public const string 质押入库 = "110";
        public const string 质押出库 = "111";
        public const string 市值配售新股申购 = "113";
        public const string 质押入库且冻结 = "115";
        public const string 质押出库且解冻 = "116";
        public const string 港股通买入 = "130";
        public const string 港股通卖出 = "131";
        public const string 国债分销 = "140";
        public const string 债券兑付 = "145";
        public const string 债券兑息 = "146";
        public const string 回购融资 = "150";
        public const string 回购融券 = "151";
        public const string 转债申购 = "152";
        public const string 转债配债 = "153";
        public const string 转债转股 = "160";
        public const string 转债回售 = "161";
        public const string 买断回购融资 = "162";
        public const string 买断回购融券 = "163";
        public const string 质押回购融资 = "164";
        public const string 质押回购融券 = "165";
        public const string 货币基金申购 = "170";
        public const string 货币基金赎回 = "171";
        public const string 货币基金收益结转 = "172";
        public const string 资管份额转入 = "175";
        public const string 资管份额转出 = "176";
        public const string ETF网下现金认购 = "180";
        public const string ETF申购 = "181";
        public const string ETF赎回 = "182";
        public const string ETF网上现金认购 = "183";
        public const string ETF网下股票认购 = "184";
        public const string 跨境ETF申购 = "187";
        public const string 跨境ETF赎回 = "188";
        public const string 开放式基金认购沪 = "190";
        public const string 开放式基金跨市场转托 = "191";
        public const string 开放式基金分红方式设定 = "192";
        public const string 开放式基金转换 = "193";
        public const string 开放式基金申购 = "194";
        public const string 开放式基金赎回 = "195";
        public const string 基金红利 = "196";
        public const string TA发起业务 = "197";
        public const string 开放式基金认购深 = "198";
        public const string 权证创设 = "200";
        public const string 权证注销 = "201";
        public const string 证券给付型认购权证行权 = "202";
        public const string 证券给付型认沽权证行权 = "203";
        public const string 现金结算型认购权证行权 = "204";
        public const string 现金结算型认沽权证行权 = "205";
        public const string 自主行权 = "206";
        public const string ETF申购赎回现金差额 = "208";
        public const string ETF申购赎回现金替代多退少补 = "209";
        public const string B转H股业务 = "220";
        public const string 国债预发行买入开仓 = "230";
        public const string 国债预发行卖出开仓 = "231";
        public const string 国债预发行买入平仓 = "232";
        public const string 国债预发行卖出平仓 = "233";
        public const string 挂牌公司股票定价买入 = "260";
        public const string 挂牌公司股票定价卖出 = "261";
        public const string 挂牌公司股票互报成交确认买入 = "262";
        public const string 挂牌公司股票互报成交确认卖出 = "263";
        public const string 挂牌公司股票成交确认买入 = "264";
        public const string 挂牌公司股票成交确认卖出 = "265";
        public const string 指定交易 = "300";
        public const string 撤销指定 = "301";
        public const string 回购指定 = "302";
        public const string 回购撤销 = "303";
        public const string 证券转托管出 = "330";
        public const string 密码服务 = "340";
        public const string 议案投票 = "345";
        public const string 网络投票 = "346";
        // public const string 资管份额转入 = "350";
        // public const string 资管份额转出 = "355";
        public const string 证券转换业务 = "499";
        public const string 新股登记 = "500";
        public const string 股份托管 = "501";
        public const string 合并证券账户 = "502";
        public const string 更换证券账户 = "503";
        public const string 红股到帐 = "504";
        public const string 股息派发 = "505";
        public const string 退款退息明细 = "506";
        public const string 证券转托管入 = "508";
        public const string ETF认购到账 = "510";
        public const string 司法冻结 = "600";
        public const string 司法冻结解冻 = "601";
        public const string 司法冻结续冻 = "602";
        public const string 司法冻结轮候 = "603";
        public const string 司法冻结轮候解除 = "604";
        public const string 证券冻结 = "610";
        public const string 证券解冻 = "611";
        public const string 证券过户 = "612";
        public const string 股份转入转出业务 = "613";
        public const string 债券存提券 = "614";
        public const string 证券转换 = "620";
        public const string 股份特殊调账业务 = "623";
        public const string 法人转配股转股 = "624";
        public const string 三板买卖回退 = "640";
        public const string 担保品买入 = "700";
        public const string 担保品卖出 = "701";
        public const string 融资买入 = "702";
        public const string 融券卖出 = "703";
        public const string 买券还券 = "704";
        public const string 卖券还款 = "705";
        public const string 融资平仓 = "706";
        public const string 融券平仓 = "707";
        public const string 担保品转入 = "708";
        public const string 担保品转出 = "709";
        public const string 现券还券 = "710";
        public const string 余券划转 = "711";
        public const string 国债分销买入 = "800";
        public const string 国债分销卖出 = "801";
        public const string 摘牌 = "802";
        public const string 盘后基金合并业务 = "820";
        public const string 盘后基金拆分业务 = "821";
        public const string 跨市场ETF现金申购 = "825";
        public const string 跨市场ETF现金赎回 = "826";
        public const string 跨市场ETF申购 = "827";
        public const string 跨市场ETF赎回 = "828";
        public const string 盘后跨市场ETF申赎冲销 = "829";
        public const string 上证LOF认购 = "830";
        public const string 上证LOF申购 = "831";
        public const string 上证LOF赎回 = "832";
        public const string 上证LOF转托管 = "833";
        public const string 上证LOF拆分 = "834";
        public const string 上证LOF合并 = "835";
        public const string 限售股 = "850";
        public const string 报价回购初始交易 = "855";
        public const string 报价回购购回交易 = "856";
        public const string 报价回购质押入库 = "865";
        public const string 报价回购质押出库 = "866";
        public const string 约定购回初始交易 = "870";
        public const string 约定购回购回交易 = "871";
        public const string 约定购回调账回补 = "872";
        public const string 约定购回调账购回 = "873";
        public const string 股票质押初始交易 = "880";
        public const string 股票质押购回交易 = "881";
        public const string 股票质押补充质押 = "882";
        public const string 股票质押解除质押 = "883";
        public const string 股票质押终止购回 = "884";
        public const string 股票质押违约处置 = "885";
        public const string 证券买入_大宗 = "950";
        public const string 证券卖出_大宗 = "951";
        public const string 柜台单费处理 = "981";
        public const string 柜台股息红利差别化个税补税处理 = "991";
        public const string 柜台股权激励自主行权延时扣税处理 = "992";
    }


    /// <summary>
    /// 是否
    /// </summary>
    class NATIONALITY : Dict
    {
        public const string 汉族 = "00";
        public const string 蒙古族 = "01";
        public const string 回族 = "02";
        public const string 藏族 = "03";
        public const string 维吾尔族 = "04";
        public const string 苗族 = "05";
        public const string 彝族 = "06";
        public const string 壮族 = "07";
        public const string 布依族 = "08";
        public const string 朝鲜族 = "09";
        public const string 满族 = "10";
        public const string 侗族 = "11";
        public const string 瑶族 = "12";
        public const string 白族 = "13";
        public const string 土家族 = "14";
        public const string 哈尼族 = "15";
        public const string 哈萨克族 = "16";
        public const string 傣族 = "17";
        public const string 黎族 = "18";
        public const string 傈僳族 = "19";
        public const string 佤族 = "20";
        public const string 畲族 = "21";
        public const string 高山族 = "22";
        public const string 拉祜族 = "23";
        public const string 水族 = "24";
        public const string 东乡族 = "25";
        public const string 纳西族 = "26";
        public const string 景颇族 = "27";
        public const string 柯尔克孜族 = "28";
        public const string 土族 = "29";
        public const string 达斡尔族 = "30";
        public const string 仫佬族 = "31";
        public const string 羌族 = "32";
        public const string 布朗族 = "33";
        public const string 撒拉族 = "34";
        public const string 毛南族 = "35";
        public const string 仡佬族 = "36";
        public const string 锡伯族 = "37";
        public const string 阿昌族 = "38";
        public const string 普米族 = "39";
        public const string 塔吉克族 = "40";
        public const string 怒族 = "41";
        public const string 乌兹别克族 = "42";
        public const string 俄罗斯族 = "43";
        public const string 鄂温克族 = "44";
        public const string 德昂族 = "45";
        public const string 保安族 = "46";
        public const string 裕固族 = "47";
        public const string 京族 = "48";
        public const string 塔塔尔族 = "49";
        public const string 独龙族 = "50";
        public const string 鄂伦春族 = "51";
        public const string 赫哲族 = "52";
        public const string 门巴族 = "53";
        public const string 珞巴族 = "54";
        public const string 基诺族 = "55";
        public const string 其它 = "56";
    }

    /// <summary>
    /// 国籍
    /// </summary>
    class CITIZENSHIP : Dict
    {
        public const string 阿鲁巴 = "ABW";
        public const string 阿富汗 = "AFG";
        public const string 安哥拉 = "AGO";
        public const string 安圭拉 = "AIA";
        public const string 阿尔巴尼亚 = "ALB";
        public const string 安道尔 = "AND";
        public const string 荷属安的列斯 = "ANT";
        public const string 阿拉伯联合酋长国 = "ARE";
        public const string 阿根廷 = "ARG";
        public const string 亚美尼亚 = "ARM";
        public const string 美属萨摩亚 = "ASM";
        public const string 南极洲 = "ATA";
        public const string 法属南部领土 = "ATF";
        public const string 安提瓜和巴布达 = "ATG";
        public const string 澳大利亚 = "AUS";
        public const string 奥地利 = "AUT";
        public const string 阿塞拜疆 = "AZE";
        public const string 布隆迪 = "BDI";
        public const string 比利时 = "BEL";
        public const string 贝宁 = "BEN";
        public const string 布基纳法索 = "BFA";
        public const string 孟加拉国 = "BGD";
        public const string 保加利亚 = "BGR";
        public const string 巴林 = "BHR";
        public const string 巴哈马 = "BHS";
        public const string 波斯尼亚和黑塞哥维那 = "BIH";
        public const string 白俄罗斯 = "BLR";
        public const string 伯利兹 = "BLZ";
        public const string 百慕大 = "BMU";
        public const string 玻利维亚 = "BOL";
        public const string 巴西 = "BRA";
        public const string 巴巴多斯 = "BRB";
        public const string 文莱 = "BRN";
        public const string 不丹 = "BTN";
        public const string 布维岛 = "BVT";
        public const string 博茨瓦纳 = "BWA";
        public const string 中非 = "CAF";
        public const string 加拿大 = "CAN";
        public const string 科科斯群岛 = "CCK";
        public const string 瑞士 = "CHE";
        public const string 智利 = "CHL";
        public const string 中国 = "CHN";
        public const string 科特迪瓦 = "CIV";
        public const string 喀麦隆 = "CMR";
        public const string 海峡群岛 = "CNI";
        public const string 刚果金 = "COD";
        public const string 刚果 = "COG";
        public const string 库克群岛 = "COK";
        public const string 哥伦比亚 = "COL";
        public const string 科摩罗 = "COM";
        public const string 佛得角 = "CPV";
        public const string 哥斯达黎加 = "CRI";
        public const string 圣诞岛 = "CSR";
        public const string 中国台湾 = "CTN";
        public const string 古巴 = "CUB";
        public const string 开曼群岛 = "CYM";
        public const string 塞浦路斯 = "CYP";
        public const string 捷克 = "CZE";
        public const string 德国 = "DEU";
        public const string 吉布提 = "DJI";
        public const string 多米尼克 = "DMA";
        public const string 丹麦 = "DNK";
        public const string 多米尼加共和国 = "DOM";
        public const string 阿尔及利亚 = "DZA";
        public const string 厄瓜多尔 = "ECU";
        public const string 埃及 = "EGY";
        public const string 厄立特里亚 = "ERI";
        public const string 西撒哈拉 = "ESH";
        public const string 西班牙 = "ESP";
        public const string 爱沙尼亚 = "EST";
        public const string 埃塞俄比亚 = "ETH";
        public const string 芬兰 = "FIN";
        public const string 斐济 = "FJI";
        public const string 马尔维纳斯群岛 = "FLK";
        public const string 法国 = "FRA";
        public const string 法罗群岛 = "FRO";
        public const string 密克罗尼西亚 = "FSM";
        public const string 加蓬 = "GAB";
        public const string 英国 = "GBR";
        public const string 格鲁吉亚 = "GEO";
        public const string 加纳 = "GHA";
        public const string 直布罗陀 = "GIB";
        public const string 几内亚 = "GIN";
        public const string 瓜德罗普 = "GLP";
        public const string 冈比亚 = "GMB";
        public const string 几内亚比绍 = "GNB";
        public const string 赤道几内亚 = "GNQ";
        public const string 希腊 = "GRC";
        public const string 格林纳达 = "GRD";
        public const string 格陵兰 = "GRL";
        public const string 危地马拉 = "GTM";
        public const string 法属圭亚那 = "GUF";
        public const string 关岛 = "GUM";
        public const string 格恩西 = "GUS";
        public const string 圭亚那 = "GUY";
        public const string 香港 = "HKG";
        public const string 赫德岛和麦克唐纳岛 = "HMD";
        public const string 洪都拉斯 = "HND";
        public const string 克罗地亚 = "HRV";
        public const string 海地 = "HTI";
        public const string 匈牙利 = "HUN";
        public const string 印度尼西亚 = "IDN";
        public const string 印度 = "IND";
        public const string 尼维斯岛 = "INE";
        public const string 马恩岛 = "IOM";
        public const string 英属印度洋领土 = "IOT";
        public const string 爱尔兰 = "IRL";
        public const string 伊朗 = "IRN";
        public const string 伊拉克 = "IRQ";
        public const string 冰岛 = "ISL";
        public const string 以色列 = "ISR";
        public const string 意大利 = "ITA";
        public const string 牙买加 = "JAM";
        public const string 约旦 = "JOR";
        public const string 日本 = "JPN";
        public const string 泽西 = "JSY";
        public const string 哈萨克斯坦 = "KAZ";
        public const string 肯尼亚 = "KEN";
        public const string 吉尔吉斯斯坦 = "KGZ";
        public const string 柬埔寨 = "KHM";
        public const string 基里巴斯 = "KIR";
        public const string 圣基茨和尼维斯 = "KNA";
        public const string 韩国 = "KOR";
        public const string 科威特 = "KWT";
        public const string 老挝 = "LAO";
        public const string 黎巴嫩 = "LBN";
        public const string 利比里亚 = "LBR";
        public const string 利比亚 = "LBY";
        public const string 圣卢西亚 = "LCA";
        public const string 列支敦士登 = "LIE";
        public const string 斯里兰卡 = "LKA";
        public const string 莱索托 = "LSO";
        public const string 立陶宛 = "LTU";
        public const string 卢森堡 = "LUX";
        public const string 拉脱维亚 = "LVA";
        public const string 澳门 = "MAC";
        public const string 曼岛 = "MAN";
        public const string 摩洛哥 = "MAR";
        public const string 摩纳哥 = "MCO";
        public const string 摩尔多瓦 = "MDA";
        public const string 马达加斯加 = "MDG";
        public const string 马尔代夫 = "MDV";
        public const string 墨西哥 = "MEX";
        public const string 马绍尔群岛 = "MHL";
        public const string 马斯顿 = "MKD";
        public const string 马里 = "MLI";
        public const string 马耳他 = "MLT";
        public const string 缅甸 = "MMR";
        public const string 蒙古 = "MNG";
        public const string 北马里亚纳 = "MNP";
        public const string 莫桑比克 = "MOZ";
        public const string 毛里塔尼亚 = "MRT";
        public const string 蒙特塞拉特 = "MSR";
        public const string 马提尼克 = "MTQ";
        public const string 毛里求斯 = "MUS";
        public const string 马拉维 = "MWI";
        public const string 马来西亚 = "MYS";
        public const string 马约特 = "MYT";
        public const string 纳米比亚 = "NAM";
        public const string 新喀里多尼亚 = "NCL";
        public const string 尼日尔 = "NER";
        public const string 诺福克岛 = "NFK";
        public const string 尼日利亚 = "NGA";
        public const string 尼加拉瓜 = "NIC";
        public const string 纽埃 = "NIU";
        public const string 荷兰属地 = "NLA";
        public const string 荷兰 = "NLD";
        public const string 挪威 = "NOR";
        public const string 尼泊尔 = "NPL";
        public const string 瑙鲁 = "NRU";
        public const string 新西兰 = "NZL";
        public const string 其他 = "OTH";
        public const string 阿曼 = "OWN";
        public const string 巴基斯坦 = "PAK";
        public const string 巴拿马 = "PAN";
        public const string 皮特凯恩群岛 = "PCN";
        public const string 秘鲁 = "PER";
        public const string 菲律宾 = "PHL";
        public const string 贝劳 = "PLW";
        public const string 巴布亚新几内亚 = "PNG";
        public const string 波兰 = "POL";
        public const string 波多黎各 = "PRI";
        public const string 朝鲜 = "PRK";
        public const string 葡萄牙 = "PRT";
        public const string 巴拉圭 = "PRY";
        public const string 巴勒斯坦 = "PST";
        public const string 法属波利尼西亚 = "PYF";
        public const string 卡塔尔 = "QAT";
        public const string 留尼汪 = "REU";
        public const string 罗马尼亚 = "ROM";
        public const string 俄罗斯 = "RUS";
        public const string 卢旺达 = "RWA";
        public const string 沙竺阿拉伯 = "SAU";
        public const string 苏丹 = "SDN";
        public const string 塞内加尔 = "SEN";
        public const string 新加坡 = "SGP";
        public const string 南乔治亚岛和南桑德韦奇岛 = "SGS";
        public const string 圣赫勒拿 = "SHN";
        public const string 斯瓦尔巴群岛 = "SJM";
        public const string 所罗门群岛 = "SLB";
        public const string 塞拉利昂 = "SLE";
        public const string 萨尔瓦多 = "SLV";
        public const string 圣马力诺 = "SMR";
        public const string 索马里 = "SOM";
        public const string 圣皮埃尔和密克隆 = "SPM";
        public const string 圣多美和普林西比 = "STp";
        public const string 苏里南 = "SUR";
        public const string 斯洛伐克 = "SVK";
        public const string 斯洛文尼亚 = "SVN";
        public const string 瑞典 = "SWE";
        public const string 斯威士兰 = "SWZ";
        public const string 塞舌尔 = "SYC";
        public const string 叙利亚 = "SYR";
        public const string 特克斯科斯群岛 = "TCA";
        public const string 乍得 = "TCD";
        public const string 多哥 = "TGO";
        public const string 泰国 = "THA";
        public const string 塔吉克斯坦 = "TJK";
        public const string 托克劳 = "TKL";
        public const string 土库曼斯坦 = "TKM";
        public const string 东帝汶 = "TMP";
        public const string 汤加 = "TON";
        public const string 特立尼达和多巴哥 = "TTO";
        public const string 突尼斯 = "TUN";
        public const string 土耳其 = "TUR";
        public const string 图瓦卢 = "TUV";
        public const string 坦桑尼亚 = "TZA";
        public const string 乌干达 = "UGA";
        public const string 乌克兰 = "UKR";
        public const string 美属太平洋各群岛 = "UMI";
        public const string 乌拉圭 = "URY";
        public const string 美国 = "USA";
        public const string 乌兹别克斯坦 = "UZB";
        public const string 梵蒂冈 = "VAT";
        public const string 圣文森特和格林纳丁斯 = "VCT";
        public const string 委内瑞拉 = "VEN";
        public const string 英属维尔京群岛 = "VGB";
        public const string 美属维尔京群岛 = "VIR";
        public const string 越南 = "VNM";
        public const string 瓦努阿图 = "VUT";
        public const string 瓦利斯和富图纳群岛 = "WLF";
        public const string 西萨摩亚 = "WSM";
        public const string 也门 = "YEM";
        public const string 南斯拉夫 = "YUG";
        public const string 南非 = "ZAF";
        public const string 扎伊尔 = "ZAR";
        public const string 赞比亚 = "ZMB";
        public const string 津巴布韦 = "ZWE";
    }

    /// <summary>
    /// 数据字典基类。
    /// 所有数据字典类应当继承自本类。
    /// </summary>
    abstract class Dict
    {
        /// <summary>
        /// 创建DataTable以便绑定数据源
        /// </summary>
        /// <returns></returns>
        public DataTable DataTable
        {
            get
            {
                Type t = GetType();
                FieldInfo[] FiledList = t.GetFields();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("value");

                if (selectable)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = "请选择";
                    dr["value"] = "";
                    dt.Rows.Add(dr);
                }

                foreach (FieldInfo item in FiledList)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = item.Name;
                    dr["value"] = item.GetValue(this).ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        private bool _selectable = false;

        /// <summary>
        /// 设置是否可选
        /// 设置为true之后，DataTable的第一行将是“请选择”
        /// </summary>
        public bool selectable
        {
            set
            {
                this._selectable = value;
            }
            get
            {
                return this._selectable;
            }
        }

        /// <summary>
        /// 判断指定值是否在字典中存在
        /// 如果存在则返回索引值，否则返回-1。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(string value)
        {
            Type t = this.GetType();
            FieldInfo[] FiledList = t.GetFields();
            for (int i = 0; i < FiledList.Length; i++)
            {
                if (value == FiledList[i].GetValue(this).ToString())
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 根据指定的字典值取得对应的字典项名称。
        /// 找不到时返回空。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string getNameByValue(string value)
        {
            Type t = this.GetType();
            FieldInfo[] FiledList = t.GetFields();
            for (int i = 0; i < FiledList.Length; i++)
            {
                if (value == FiledList[i].GetValue(value).ToString())
                {
                    return FiledList[i].Name;
                }
            }
            //throw new Exception("找不到" + value + "对应的字典项");
            return value;
        }
    }
}
