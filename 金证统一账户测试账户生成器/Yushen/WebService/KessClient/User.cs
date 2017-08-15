using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yushen.Util;

namespace Yushen.WebService.KessClient
{
    public class User
    {
        /// <summary>
        ///（私有）客户名称（必传）
        /// </summary>
        private string _user_name = "";
        /// <summary>
        ///客户名称（必传）
        /// </summary>
        public string user_name
        {
            get
            {
                return _user_name;
            }

            set
            {
                _user_name = value;
            }
        }
        /// <summary>
        /// 用户设置的密码
        /// </summary>
        private string _password = "";
        /// <summary>
        /// 用户设置的密码
        /// </summary>
        public string password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }
        /// <summary>
        ///（私有）用户类型（必传） dd[user_type]
        /// </summary>
        private string _user_type = "00";
        /// <summary>
        ///用户类型（必传） dd[user_type]
        /// </summary>
        public string user_type
        {
            get
            {
                return _user_type;
            }

            set
            {
                _user_type = value;
            }
        }
        /// <summary>
        ///（私有）证件类型（必传） dd[id_type]
        /// </summary>
        private string _id_type = "00";

        /// <summary>
        ///证件类型（必传） dd[id_type]
        /// </summary>
        public string id_type
        {
            get
            {
                return _id_type;
            }

            set
            {
                _id_type = value;
            }
        }

        /// <summary>
        ///（私有）银行卡号
        /// </summary>
        private string _bank_acct_code = "";

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string bank_acct_code
        {
            get
            {
                return _bank_acct_code;
            }

            set
            {
                _bank_acct_code = value;
            }
        }
        /// <summary>
        ///（私有）证件号码（必传）
        /// </summary>
        private string _id_code = "";

        /// <summary>
        ///证件号码（必传）
        /// </summary>
        public string id_code
        {
            get
            {
                return _id_code;
            }

            set
            {
                if (id_type == null)
                {
                    throwException("设置证件号码之前必须先设置证件类别");
                }

                if (id_type == "00" && !Validator.IsIDCard(value))
                {
                    throwException(value.ToString() + "身份证格式不正确");
                }

                if (id_type == "00" && value.Length == 18)
                {
                    this.birthday = value.Substring(6, 8);
                }
                else if (id_type == "00" && value.Length == 15)
                {
                    this.birthday = "19" + value.Substring(6, 6);
                }

                _id_code = value;
            }
        }
        /// <summary>
        ///（私有）用户全称（必传）
        /// </summary>
        private string _user_fname = "";
        /// <summary>
        ///用户全称（必传）
        /// </summary>
        public string user_fname
        {
            get
            {
                return _user_fname;
            }

            set
            {
                _user_fname = value;
            }
        }
        /// <summary>
        ///（私有）发证机关（必传）
        /// </summary>
        private string _id_iss_agcy = "";
        /// <summary>
        ///发证机关（必传）
        /// </summary>
        public string id_iss_agcy
        {
            get
            {
                return _id_iss_agcy;
            }

            set
            {
                _id_iss_agcy = value;
            }
        }
        /// <summary>
        ///（私有）证件有效日期（必传）
        /// </summary>
        private string _id_exp_date = "";
        /// <summary>
        ///证件有效日期（必传）
        /// </summary>
        public string id_exp_date
        {
            get
            {
                return _id_exp_date;
            }

            set
            {
                if (!Validator.IsInteger(value))
                {
                    throwException(value.ToString() + "证件有效日期格式不正确");
                }
                _id_exp_date = value;
            }
        }
        /// <summary>
        ///（私有）国籍（必传） dd[citizenship]
        /// </summary>
        private string _citizenship = "";
        /// <summary>
        ///国籍（必传） dd[citizenship]
        /// </summary>
        public string citizenship
        {
            get
            {
                return _citizenship;
            }

            set
            {
                _citizenship = value;
            }
        }
        /// <summary>
        ///（私有）民族（必传） dd[nationality]
        /// </summary>
        private string _nationality = "";
        /// <summary>
        ///民族（必传） dd[nationality]
        /// </summary>
        public string nationality
        {
            get
            {
                return _nationality;
            }

            set
            {
                _nationality = value;
            }
        }
        /// <summary>
        ///（私有）客户代码（非必传）
        /// </summary>
        private string _cust_code = "";
        /// <summary>
        ///客户代码（非必传）
        /// </summary>
        public string cust_code
        {
            get
            {
                return _cust_code;
            }

            set
            {
                _cust_code = value;
            }
        }
        /// <summary>
        ///（私有）机构代码（非必传）
        /// </summary>
        private string _int_org = "";
        /// <summary>
        ///机构代码（非必传）
        /// </summary>
        public string int_org
        {
            get
            {
                return _int_org;
            }

            set
            {
                _int_org = value;
            }
        }
        /// <summary>
        ///（私有）证件开始日期（非必传）
        /// </summary>
        private string _id_beg_date = "";
        /// <summary>
        ///证件开始日期（非必传）
        /// </summary>
        public string id_beg_date
        {
            get
            {
                return _id_beg_date;
            }

            set
            {
                if (!Validator.IsInteger(value))
                {
                    throwException(value.ToString() + "证件开始日期格式不正确");
                }
                _id_beg_date = value;
            }
        }
        /// <summary>
        ///（私有）证件邮编（非必传）
        /// </summary>
        private string _id_zip_code = "";
        /// <summary>
        ///证件邮编（非必传）
        /// </summary>
        public string id_zip_code
        {
            get
            {
                return _id_zip_code;
            }

            set
            {
                if (!Validator.IsPostalcode(value))
                {
                    throwException(value.ToString() + "证件邮编格式不正确");
                }
                _id_zip_code = value;
            }
        }
        /// <summary>
        ///（私有）证件地址（非必传）
        /// </summary>
        private string _id_addr = "";
        /// <summary>
        ///证件地址（非必传）
        /// </summary>
        public string id_addr
        {
            get
            {
                return _id_addr;
            }

            set
            {
                _id_addr = value;
            }
        }
        /// <summary>
        ///（私有）邮政编码（非必传）
        /// </summary>
        private string _zip_code = "";
        /// <summary>
        ///邮政编码（非必传）
        /// </summary>
        public string zip_code
        {
            get
            {
                return _zip_code;
            }

            set
            {
                if (!Validator.IsPostalcode(value))
                {
                    throwException(value.ToString() + "邮政编码格式不正确");
                }
                _zip_code = value;
            }
        }
        /// <summary>
        ///（私有）联系地址（非必传）
        /// </summary>
        private string _address = "";
        /// <summary>
        ///联系地址（非必传）
        /// </summary>
        public string address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }
        /// <summary>
        ///（私有）联系电话（非必传）
        /// </summary>
        private string _tel = "";
        /// <summary>
        ///联系电话（非必传）
        /// </summary>
        public string tel
        {
            get
            {
                return _tel;
            }

            set
            {
                if (!Validator.IsTelephone(value))
                {
                    throwException(value.ToString() + "联系电话格式不正确");
                }
                _tel = value;
            }
        }
        /// <summary>
        ///（私有）传真电话（非必传）
        /// </summary>
        private string _fax = "";
        /// <summary>
        ///传真电话（非必传）
        /// </summary>
        public string fax
        {
            get
            {
                return _fax;
            }

            set
            {
                if (!Validator.IsFax(value))
                {
                    throwException(value.ToString() + "传真电话格式不正确");
                }
                _fax = value;
            }
        }
        /// <summary>
        ///（私有）电子邮箱（非必传）
        /// </summary>
        private string _email = "";
        /// <summary>
        ///电子邮箱（非必传）
        /// </summary>
        public string email
        {
            get
            {
                return _email;
            }

            set
            {
                if (!Validator.IsEmail(value))
                {
                    throwException(value.ToString() + "电子邮箱格式不正确");
                }
                _email = value;
            }
        }
        /// <summary>
        ///（私有）移动电话（非必传）
        /// </summary>
        private string _mobile_tel = "";
        /// <summary>
        ///移动电话（非必传）
        /// </summary>
        public string mobile_tel
        {
            get
            {
                return _mobile_tel;
            }

            set
            {
                if (!Validator.IsMobile(value))
                {
                    throwException(value.ToString() + "手机号格式不正确");
                }
                _mobile_tel = value;
            }
        }
        /// <summary>
        ///（私有）学历（非必传） dd[education]
        /// </summary>
        private string _education = "";
        /// <summary>
        ///学历（非必传） dd[education]
        /// </summary>
        public string education
        {
            get
            {
                return _education;
            }

            set
            {
                _education = value;
            }
        }
        /// <summary>
        ///（私有）籍贯/注册地（非必传）
        /// </summary>
        private string _native_place = "";
        /// <summary>
        ///籍贯/注册地（非必传）
        /// </summary>
        public string native_place
        {
            get
            {
                return _native_place;
            }

            set
            {
                _native_place = value;
            }
        }
        /// <summary>
        ///（私有）一码通账号
        /// </summary>
        private string _ymt_code = "";
        /// <summary>
        /// 一码通账号
        /// </summary>
        public string ymt_code
        {
            get
            {
                return _ymt_code;
            }

            set
            {
                _ymt_code = value;
            }
        }

        /// <summary>
        ///（私有）性别（非必传） dd[sex]
        /// </summary>
        private string _sex = "";
        /// <summary>
        ///性别（非必传） dd[sex]
        /// </summary>
        public string sex
        {
            get
            {
                return _sex;
            }

            set
            {
                _sex = value;
            }
        }
        /// <summary>
        ///（私有）出生日期（非必传）
        /// </summary>
        private string _birthday = "";
        /// <summary>
        ///出生日期（非必传）
        /// </summary>
        public string birthday
        {
            get
            {
                return _birthday;
            }

            set
            {
                if (!Validator.IsInteger(value))
                {
                    throwException(value.ToString() + "出生日期格式不正确");
                }
                _birthday = value;
            }
        }
        /// <summary>
        ///（私有）备注信息（非必传）
        /// </summary>
        private string _remark = "";
        /// <summary>
        ///备注信息（非必传）
        /// </summary>
        public string remark
        {
            get
            {
                return _remark;
            }

            set
            {
                _remark = value;
            }
        }
        /// <summary>
        ///（私有）婚姻状况（非必传） dd[marry]
        /// </summary>
        private string _marry = "";
        /// <summary>
        ///婚姻状况（非必传） dd[marry]
        /// </summary>
        public string marry
        {
            get
            {
                return _marry;
            }

            set
            {
                _marry = value;
            }
        }
        /// <summary>
        ///（私有）兴趣爱好（非必传）
        /// </summary>
        private string _interest = "";
        /// <summary>
        ///兴趣爱好（非必传）
        /// </summary>
        public string interest
        {
            get
            {
                return _interest;
            }

            set
            {
                _interest = value;
            }
        }
        /// <summary>
        ///（私有）交通工具（非必传） dd[vehicle]
        /// </summary>
        private string _vehicle = "";
        /// <summary>
        ///交通工具（非必传） dd[vehicle]
        /// </summary>
        public string vehicle
        {
            get
            {
                return _vehicle;
            }

            set
            {
                _vehicle = value;
            }
        }
        /// <summary>
        ///（私有）住宅所有权状况（非必传）
        /// </summary>
        private string _house_owner = "";
        /// <summary>
        ///住宅所有权状况（非必传）
        /// </summary>
        public string house_owner
        {
            get
            {
                return _house_owner;
            }

            set
            {
                _house_owner = value;
            }
        }
        /// <summary>
        ///（私有）办公电话（非必传）
        /// </summary>
        private string _office_tel = "";
        /// <summary>
        ///办公电话（非必传）
        /// </summary>
        public string office_tel
        {
            get
            {
                return _office_tel;
            }

            set
            {
                _office_tel = value;
            }
        }
        /// <summary>
        ///（私有）小灵通电话（非必传）
        /// </summary>
        private string _well_tel = "";
        /// <summary>
        ///小灵通电话（非必传）
        /// </summary>
        public string well_tel
        {
            get
            {
                return _well_tel;
            }

            set
            {
                _well_tel = value;
            }
        }
        /// <summary>
        ///（私有）首选联系电话（非必传）
        /// </summary>
        private string _linktel_order = "";
        /// <summary>
        ///首选联系电话（非必传）
        /// </summary>
        public string linktel_order
        {
            get
            {
                return _linktel_order;
            }

            set
            {
                _linktel_order = value;
            }
        }
        /// <summary>
        ///（私有）办公地址（非必传）
        /// </summary>
        private string _office_addr = "";
        /// <summary>
        ///办公地址（非必传）
        /// </summary>
        public string office_addr
        {
            get
            {
                return _office_addr;
            }

            set
            {
                _office_addr = value;
            }
        }
        /// <summary>
        ///（私有）公司地址（非必传）
        /// </summary>
        private string _corp_addr = "";
        /// <summary>
        ///公司地址（非必传）
        /// </summary>
        public string corp_addr
        {
            get
            {
                return _corp_addr;
            }

            set
            {
                _corp_addr = value;
            }
        }
        /// <summary>
        ///（私有）首选联系地址（非必传）
        /// </summary>
        private string _linkaddr_order = "";
        /// <summary>
        ///首选联系地址（非必传）
        /// </summary>
        public string linkaddr_order
        {
            get
            {
                return _linkaddr_order;
            }

            set
            {
                _linkaddr_order = value;
            }
        }
        /// <summary>
        ///（私有）客户类别（非必传） dd[cust_cls]
        /// </summary>
        private string _cust_cls = "";
        /// <summary>
        ///客户类别（非必传） dd[cust_cls]
        /// </summary>
        public string cust_cls
        {
            get
            {
                return _cust_cls;
            }

            set
            {
                _cust_cls = value;
            }
        }
        /// <summary>
        ///（私有）客户类型（非必传） dd[cust_type]
        /// </summary>
        private string _cust_type = "";
        /// <summary>
        ///客户类型（非必传） dd[cust_type]
        /// </summary>
        public string cust_type
        {
            get
            {
                return _cust_type;
            }

            set
            {
                _cust_type = value;
            }
        }
        /// <summary>
        ///（私有）操作渠道（非必传） dd[channel]
        /// </summary>
        private string _channels = "";
        /// <summary>
        ///操作渠道（非必传） dd[channel]
        /// </summary>
        public string channels
        {
            get
            {
                return _channels;
            }

            set
            {
                _channels = value;
            }
        }
        /// <summary>
        ///（私有）规范客户标志（非必传） dd[criterion]
        /// </summary>
        private string _criterion = "";
        /// <summary>
        ///规范客户标志（非必传） dd[criterion]
        /// </summary>
        public string criterion
        {
            get
            {
                return _criterion;
            }

            set
            {
                _criterion = value;
            }
        }
        /// <summary>
        ///（私有）风险因素（非必传） dd[risk_factor]
        /// </summary>
        private string _risk_factor = "";
        /// <summary>
        ///风险因素（非必传） dd[risk_factor]
        /// </summary>
        public string risk_factor
        {
            get
            {
                return _risk_factor;
            }

            set
            {
                _risk_factor = value;
            }
        }
        /// <summary>
        ///（私有）信用级别（非必传）
        /// </summary>
        private string _credit_lvl = "";
        /// <summary>
        ///信用级别（非必传）
        /// </summary>
        public string credit_lvl
        {
            get
            {
                return _credit_lvl;
            }

            set
            {
                _credit_lvl = value;
            }
        }
        /// <summary>
        ///（私有）远程签署协议（非必传）
        /// </summary>
        private string _remote_protocol = "";
        /// <summary>
        ///远程签署协议（非必传）
        /// </summary>
        public string remote_protocol
        {
            get
            {
                return _remote_protocol;
            }

            set
            {
                _remote_protocol = value;
            }
        }
        /// <summary>
        ///（私有）客户来源（非必传）
        /// </summary>
        private string _cust_source = "";
        /// <summary>
        ///客户来源（非必传）
        /// </summary>
        public string cust_source
        {
            get
            {
                return _cust_source;
            }

            set
            {
                _cust_source = value;
            }
        }
        /// <summary>
        ///（私有）服务级别（非必传）
        /// </summary>
        private string _service_lvl = "";
        /// <summary>
        ///服务级别（非必传）
        /// </summary>
        public string service_lvl
        {
            get
            {
                return _service_lvl;
            }

            set
            {
                _service_lvl = value;
            }
        }
        /// <summary>
        ///（私有）签约客户姓名（非必传）
        /// </summary>
        private string _bsb_user_fname = "";
        /// <summary>
        ///签约客户姓名（非必传）
        /// </summary>
        public string bsb_user_fname
        {
            get
            {
                return _bsb_user_fname;
            }

            set
            {
                _bsb_user_fname = value;
            }
        }
        /// <summary>
        ///（私有）签约证件类型（非必传） dd[id_type]
        /// </summary>
        private string _bsb_id_type = "";
        /// <summary>
        ///签约证件类型（非必传） dd[id_type]
        /// </summary>
        public string bsb_id_type
        {
            get
            {
                return _bsb_id_type;
            }

            set
            {
                _bsb_id_type = value;
            }
        }
        /// <summary>
        ///（私有）签约证件号码（非必传）
        /// </summary>
        private string _bsb_id_code = "";
        /// <summary>
        ///签约证件号码（非必传）
        /// </summary>
        public string bsb_id_code
        {
            get
            {
                return _bsb_id_code;
            }

            set
            {
                _bsb_id_code = value;
            }
        }
        /// <summary>
        ///（私有）签约证件有效日期（非必传）
        /// </summary>
        private string _bsb_id_exp_date = "";
        /// <summary>
        ///签约证件有效日期（非必传）
        /// </summary>
        public string bsb_id_exp_date
        {
            get
            {
                return _bsb_id_exp_date;
            }

            set
            {
                _bsb_id_exp_date = value;
            }
        }
        /// <summary>
        ///（私有）磁卡号码（非必传）
        /// </summary>
        private string _card_id = "";
        /// <summary>
        ///磁卡号码（非必传）
        /// </summary>
        public string card_id
        {
            get
            {
                return _card_id;
            }

            set
            {
                _card_id = value;
            }
        }
        /// <summary>
        ///（私有）校验标志（非必传）
        /// </summary>
        private string _chk_right_flag = "";
        /// <summary>
        ///校验标志（非必传）
        /// </summary>
        public string chk_right_flag
        {
            get
            {
                return _chk_right_flag;
            }

            set
            {
                _chk_right_flag = value;
            }
        }
        /// <summary>
        ///（私有）开户来源（非必传） dd[open_source]
        /// </summary>
        private string _open_source = "";
        /// <summary>
        ///开户来源（非必传） dd[open_source]
        /// </summary>
        public string open_source
        {
            get
            {
                return _open_source;
            }

            set
            {
                _open_source = value;
            }
        }
        /// <summary>
        ///（私有）其它备注（非必传）
        /// </summary>
        private string _other_remark = "";
        /// <summary>
        ///其它备注（非必传）
        /// </summary>
        public string other_remark
        {
            get
            {
                return _other_remark;
            }

            set
            {
                _other_remark = value;
            }
        }
        /// <summary>
        ///（私有）特殊备注（非必传）
        /// </summary>
        private string _spec_remark = "";
        /// <summary>
        ///特殊备注（非必传）
        /// </summary>
        public string spec_remark
        {
            get
            {
                return _spec_remark;
            }

            set
            {
                _spec_remark = value;
            }
        }
        /// <summary>
        ///（私有）联络频度（非必传）
        /// </summary>
        private string _lintel_pd = "";
        /// <summary>
        ///联络频度（非必传）
        /// </summary>
        public string lintel_pd
        {
            get
            {
                return _lintel_pd;
            }

            set
            {
                _lintel_pd = value;
            }
        }
        /// <summary>
        ///（私有）反洗钱等级（非必传） dd[aml_lvl]
        /// </summary>
        private string _aml_lvl = "";
        /// <summary>
        ///反洗钱等级（非必传） dd[aml_lvl]
        /// </summary>
        public string aml_lvl
        {
            get
            {
                return _aml_lvl;
            }

            set
            {
                _aml_lvl = value;
            }
        }
        /// <summary>
        ///（私有）文件柜编号（非必传）
        /// </summary>
        private string _filingcabinet_no = "";
        /// <summary>
        ///文件柜编号（非必传）
        /// </summary>
        public string filingcabinet_no
        {
            get
            {
                return _filingcabinet_no;
            }

            set
            {
                _filingcabinet_no = value;
            }
        }
        /// <summary>
        ///（私有）客户分组（非必传） dd[cust_grp]
        /// </summary>
        private string _cust_grp = "";
        /// <summary>
        ///客户分组（非必传） dd[cust_grp]
        /// </summary>
        public string cust_grp
        {
            get
            {
                return _cust_grp;
            }

            set
            {
                _cust_grp = value;
            }
        }
        /// <summary>
        ///（私有）证件卡类型（非必传）
        /// </summary>
        private string _idcard_type = "";
        /// <summary>
        ///证件卡类型（非必传）
        /// </summary>
        public string idcard_type
        {
            get
            {
                return _idcard_type;
            }

            set
            {
                _idcard_type = value;
            }
        }
        /// <summary>
        ///（私有）证件卡校验标志（非必传） dd[idcard_check_flag]
        /// </summary>
        private string _idcard_check_flag = "";
        /// <summary>
        ///证件卡校验标志（非必传） dd[idcard_check_flag]
        /// </summary>
        public string idcard_check_flag
        {
            get
            {
                return _idcard_check_flag;
            }

            set
            {
                _idcard_check_flag = value;
            }
        }
        /// <summary>
        ///（私有）开户代理人（非必传）
        /// </summary>
        private string _open_agent = "";
        /// <summary>
        ///开户代理人（非必传）
        /// </summary>
        public string open_agent
        {
            get
            {
                return _open_agent;
            }

            set
            {
                _open_agent = value;
            }
        }
        /// <summary>
        ///（私有）主体身份（非必传）
        /// </summary>
        private string _subject_identity = "";
        /// <summary>
        ///主体身份（非必传）
        /// </summary>
        public string subject_identity
        {
            get
            {
                return _subject_identity;
            }

            set
            {
                _subject_identity = value;
            }
        }
        /// <summary>
        ///（私有）年收入（非必传）
        /// </summary>
        private string _income = "";
        /// <summary>
        ///年收入（非必传）
        /// </summary>
        public string income
        {
            get
            {
                return _income;
            }

            set
            {
                _income = value;
            }
        }
        /// <summary>
        ///（私有）境内外身份（非必传）
        /// </summary>
        private string _inoutside_identity = "";
        /// <summary>
        ///境内外身份（非必传）
        /// </summary>
        public string inoutside_identity
        {
            get
            {
                return _inoutside_identity;
            }

            set
            {
                _inoutside_identity = value;
            }
        }
        /// <summary>
        ///（私有）特殊身份（非必传）
        /// </summary>
        private string _special_status = "";
        /// <summary>
        ///特殊身份（非必传）
        /// </summary>
        public string special_status
        {
            get
            {
                return _special_status;
            }

            set
            {
                _special_status = value;
            }
        }
        /// <summary>
        ///（私有）企业层级（非必传）
        /// </summary>
        private string _enterprise_level = "";
        /// <summary>
        ///企业层级（非必传）
        /// </summary>
        public string enterprise_level
        {
            get
            {
                return _enterprise_level;
            }

            set
            {
                _enterprise_level = value;
            }
        }
        /// <summary>
        ///（私有）机构产品类别（深）（非必传）
        /// </summary>
        private string _szorgtype = "";
        /// <summary>
        ///机构产品类别（深）（非必传）
        /// </summary>
        public string szorgtype
        {
            get
            {
                return _szorgtype;
            }

            set
            {
                _szorgtype = value;
            }
        }
        /// <summary>
        ///（私有）国有属性（非必传） dd[national_attr]
        /// </summary>
        private string _national_attr = "";
        /// <summary>
        ///国有属性（非必传） dd[national_attr]
        /// </summary>
        public string national_attr
        {
            get
            {
                return _national_attr;
            }

            set
            {
                _national_attr = value;
            }
        }
        /// <summary>
        ///（私有）上市属性（非必传） dd[listed_attr]
        /// </summary>
        private string _listed_attr = "";
        /// <summary>
        ///上市属性（非必传） dd[listed_attr]
        /// </summary>
        public string listed_attr
        {
            get
            {
                return _listed_attr;
            }

            set
            {
                _listed_attr = value;
            }
        }
        /// <summary>
        ///（私有）证件卡读卡标志（非必传）
        /// </summary>
        private string _idcard_read_flag = "";
        /// <summary>
        ///证件卡读卡标志（非必传）
        /// </summary>
        public string idcard_read_flag
        {
            get
            {
                return _idcard_read_flag;
            }

            set
            {
                _idcard_read_flag = value;
            }
        }
        /// <summary>
        ///（私有）主证件年检日期（非必传）
        /// </summary>
        private string _main_year_chk_date = "";
        /// <summary>
        ///主证件年检日期（非必传）
        /// </summary>
        public string main_year_chk_date
        {
            get
            {
                return _main_year_chk_date;
            }

            set
            {
                _main_year_chk_date = value;
            }
        }
        /// <summary>
        ///（私有）工作单位（非必传）
        /// </summary>
        private string _workplace = "";
        /// <summary>
        ///工作单位（非必传）
        /// </summary>
        public string workplace
        {
            get
            {
                return _workplace;
            }

            set
            {
                _workplace = value;
            }
        }
        /// <summary>
        ///（私有）行业类型（非必传）
        /// </summary>
        private string _trade = "";
        /// <summary>
        ///行业类型（非必传）
        /// </summary>
        public string trade
        {
            get
            {
                return _trade;
            }

            set
            {
                _trade = value;
            }
        }
        /// <summary>
        ///（私有）当前职业（非必传）
        /// </summary>
        private string _occu_type = "";
        /// <summary>
        ///当前职业（非必传）
        /// </summary>
        public string occu_type
        {
            get
            {
                return _occu_type;
            }

            set
            {
                _occu_type = value;
            }
        }
        /// <summary>
        ///（私有）手机校验标识（非必传）
        /// </summary>
        private string _tel_chk_flag = "";
        /// <summary>
        ///手机校验标识（非必传）
        /// </summary>
        public string tel_chk_flag
        {
            get
            {
                return _tel_chk_flag;
            }

            set
            {
                _tel_chk_flag = value;
            }
        }
        /// <summary>
        ///（私有）邮箱校验标识（非必传）
        /// </summary>
        private string _email_chk_flag = "";
        /// <summary>
        ///邮箱校验标识（非必传）
        /// </summary>
        public string email_chk_flag
        {
            get
            {
                return _email_chk_flag;
            }

            set
            {
                _email_chk_flag = value;
            }
        }
        /// <summary>
        ///（私有）金融相关专业学历校验标识（非必传）
        /// </summary>
        private string _fin_edu_flag = "";
        /// <summary>
        ///金融相关专业学历校验标识（非必传）
        /// </summary>
        public string fin_edu_flag
        {
            get
            {
                return _fin_edu_flag;
            }

            set
            {
                _fin_edu_flag = value;
            }
        }
        /// <summary>
        /// （私有）客户代码
        /// </summary>
        private string _user_code = "";
        /// <summary>
        /// 客户代码
        /// </summary>
        public string user_code
        {
            get
            {
                return _user_code;
            }

            set
            {
                _user_code = value;
            }
        }
        /// <summary>
        ///（私有）资金账号
        /// </summary>
        private string _cuacct_code = "";
        /// <summary>
        /// 资金账号
        /// </summary>
        public string cuacct_code
        {
            get
            {
                return _cuacct_code;
            }

            set
            {
                _cuacct_code = value;
            }
        }
        /// <summary>
        ///（私有）深圳A股账号
        /// </summary>
        private string _szacct = "";
        /// <summary>
        /// 深圳A股账号
        /// </summary>
        public string szacct
        {
            get
            {
                return _szacct;
            }

            set
            {
                _szacct = value;
            }
        }
        /// <summary>
        ///（私有）上海A股账号
        /// </summary>
        private string _shacct = "";
        /// <summary>
        ///上海A股账号
        /// </summary>
        public string shacct
        {
            get
            {
                return _shacct;
            }

            set
            {
                _shacct = value;
            }
        }

        /// <summary>
        ///（私有）银行代码
        /// </summary>
        private string _bank_code = "";

        /// <summary>
        /// 统一账户银行代码
        /// </summary>
        public string bank_code
        {
            get
            {
                return _bank_code;
            }

            set
            {
                _bank_code = value;
            }
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        protected static Logger logger = LogManager.GetCurrentClassLogger();

        protected void throwException(string message)
        {
            logger.Error(message);
            throw new Exception(message);
        }

        /// <summary>
        /// 开立客户号
        /// </summary>
        //public void createCustomerCode()
        //{
        //    Response response;
        //    response = new Response(kess.getUserInfoById(id_code));
        //    if (response.length == 0 || kess.getSingleCommonParamValue("OPEN_CUST_CHECK_ID_FLAG") == "1")
        //    {
        //        response = kess.openCustomer(user_name, id_code, id_iss_agcy, id_beg_date, id_exp_date, citizenship, nationality);
        //        this.user_code = response.getSingleNodeText("/response/record/row/USER_CODE");
        //    }
        //    else
        //    {
        //        throwException("系统不允许同一证件开多个客户代码");
        //    }
        //}

        /// <summary>
        /// 开资金账号
        /// </summary>
        //public void createCuacctCode()
        //{
        //    Response response;
        //    // response = new Response(kess.qu);
        //}
    }
}
