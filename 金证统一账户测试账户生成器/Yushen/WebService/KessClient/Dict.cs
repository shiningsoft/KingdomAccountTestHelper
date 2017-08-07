/// <summary>
/// 统一账户系统数据字典
/// </summary>
namespace Yushen.WebService.KessClient.Dict
{

    /// <summary>
    /// 开户类别
    /// </summary>
    static class ACCT_OPENTYPE
    {
        public const string 临柜办理开户 = "0";
        public const string 客户网上自助 = "1";
        public const string 视频见证开户 = "2";
        public const string 双人见证开户 = "3";
    }

    /// <summary>
    /// 交易板块
    /// </summary>
    static class STKBD
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
    static class ACCT_TYPE
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
    static class ACCTBIZ_EXCODE
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
    static class ACCTBIZ_STATUS
    {
        public const string 未发送 = "0";
        public const string 已发送 = "1";
        public const string 处理成功 = "2";
        public const string 处理失败 = "3";
    }

    /// <summary>
    /// 测评类别
    /// </summary>
    static class SURVEY_CLS
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
    static class OCCU_EXTYPE
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
    /// 资产账户状态
    /// </summary>
    static class CUACCT_STATUS
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
    /// 客户协议类型
    /// </summary>
    static class CUST_AMGT_TYPE
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
    /// 货币
    /// </summary>
    static class CURRENCY
    {
        public const string 人民币 = "0";
        public const string 港币 = "1";
        public const string 美元 = "2";
    }

    /// <summary>
    /// 国籍
    /// </summary>
    static class CITIZENSHIP
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
}
