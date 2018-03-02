using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading;
using NAudio.Wave;

namespace iFlyDotNet
{
    enum ErrorCode
    {
        MspSuccess = 0,
        MspErrorFail = -1,
        MspErrorException = -2,

        /* General errors 10100(0x2774) */
        MspErrorGeneral = 10100, 	/* 0x2774 */
        MspErrorOutOfMemory = 10101, 	/* 0x2775 */
        MspErrorFileNotFound = 10102, 	/* 0x2776 */
        MspErrorNotSupport = 10103, 	/* 0x2777 */
        MspErrorNotImplement = 10104, 	/* 0x2778 */
        MspErrorAccess = 10105, 	/* 0x2779 */
        MspErrorInvalidPara = 10106, 	/* 0x277A */
        MspErrorInvalidParaValue = 10107, 	/* 0x277B */
        MspErrorInvalidHandle = 10108, 	/* 0x277C */
        MspErrorInvalidData = 10109, 	/* 0x277D */
        MspErrorNoLicense = 10110, 	/* 0x277E */
        MspErrorNotInit = 10111, 	/* 0x277F */
        MspErrorNullHandle = 10112, 	/* 0x2780 */
        MspErrorOverflow = 10113, 	/* 0x2781 */
        MspErrorTimeOut = 10114, 	/* 0x2782 */
        MspErrorOpenFile = 10115, 	/* 0x2783 */
        MspErrorNotFound = 10116, 	/* 0x2784 */
        MspErrorNoEnoughBuffer = 10117, 	/* 0x2785 */
        MspErrorNoData = 10118, 	/* 0x2786 */
        MspErrorNoMoreData = 10119, 	/* 0x2787 */
        MspErrorSkipped = 10120, 	/* 0x2788 */
        MspErrorAlreadyExist = 10121, 	/* 0x2789 */
        MspErrorLoadModule = 10122, 	/* 0x278A */
        MspErrorBusy = 10123, 	/* 0x278B */
        MspErrorInvalidConfig = 10124, 	/* 0x278C */
        MspErrorVersionCheck = 10125, 	/* 0x278D */
        MspErrorCanceled = 10126, 	/* 0x278E */
        MspErrorInvalidMediaType = 10127, 	/* 0x278F */
        MspErrorConfigInitialize = 10128, 	/* 0x2790 */
        MspErrorCreateHandle = 10129, 	/* 0x2791 */
        MspErrorCodingLibNotLoad = 10130, 	/* 0x2792 */

        /* Error codes of network 10200(0x27D8)*/
        MspErrorNetGeneral = 10200, 	/* 0x27D8 */
        MspErrorNetOpensock = 10201, 	/* 0x27D9 */   /* Open socket */
        MspErrorNetConnectsock = 10202, 	/* 0x27DA */   /* Connect socket */
        MspErrorNetAcceptsock = 10203, 	/* 0x27DB */   /* Accept socket */
        MspErrorNetSendsock = 10204, 	/* 0x27DC */   /* Send socket data */
        MspErrorNetRecvsock = 10205, 	/* 0x27DD */   /* Recv socket data */
        MspErrorNetInvalidsock = 10206, 	/* 0x27DE */   /* Invalid socket handle */
        MspErrorNetBadaddress = 10207, 	/* 0x27EF */   /* Bad network address */
        MspErrorNetBindsequence = 10208, 	/* 0x27E0 */   /* Bind after listen/connect */
        MspErrorNetNotopensock = 10209, 	/* 0x27E1 */   /* Socket is not opened */
        MspErrorNetNotbind = 10210, 	/* 0x27E2 */   /* Socket is not bind to an address */
        MspErrorNetNotlisten = 10211, 	/* 0x27E3 */   /* Socket is not listenning */
        MspErrorNetConnectclose = 10212, 	/* 0x27E4 */   /* The other side of connection is closed */
        MspErrorNetNotdgramsock = 10213, 	/* 0x27E5 */   /* The socket is not datagram type */

        /* Error codes of mssp message 10300(0x283C) */
        MspErrorMsgGeneral = 10300, 	/* 0x283C */
        MspErrorMsgParseError = 10301, 	/* 0x283D */
        MspErrorMsgBuildError = 10302, 	/* 0x283E */
        MspErrorMsgParamError = 10303, 	/* 0x283F */
        MspErrorMsgContentEmpty = 10304, 	/* 0x2840 */
        MspErrorMsgInvalidContentType = 10305, 	/* 0x2841 */
        MspErrorMsgInvalidContentLength = 10306, 	/* 0x2842 */
        MspErrorMsgInvalidContentEncode = 10307, 	/* 0x2843 */
        MspErrorMsgInvalidKey = 10308, 	/* 0x2844 */
        MspErrorMsgKeyEmpty = 10309, 	/* 0x2845 */
        MspErrorMsgSessionIDEmpty = 10310, 	/* 0x2846 */
        MspErrorMsgLoginIDEmpty = 10311, 	/* 0x2847 */
        MspErrorMsgSyncIDEmpty = 10312, 	/* 0x2848 */
        MspErrorMsgAppIDEmpty = 10313, 	/* 0x2849 */
        MspErrorMsgExternIDEmpty = 10314, 	/* 0x284A */
        MspErrorMsgInvalidCmd = 10315, 	/* 0x284B */
        MspErrorMsgInvalidSubject = 10316, 	/* 0x284C */
        MspErrorMsgInvalidVersion = 10317, 	/* 0x284D */
        MspErrorMsgNoCmd = 10318, 	/* 0x284E */
        MspErrorMsgNoSubject = 10319, 	/* 0x284F */
        MspErrorMsgNoVersion = 10320, 	/* 0x2850 */
        MspErrorMsgMsspEmpty = 10321, 	/* 0x2851 */
        MspErrorMsgNewResponse = 10322, 	/* 0x2852 */
        MspErrorMsgNewContent = 10323, 	/* 0x2853 */
        MspErrorMsgInvalidSessionID = 10324, 	/* 0x2854 */

        /* Error codes of DataBase 10400(0x28A0)*/
        MspErrorDbGeneral = 10400, 	/* 0x28A0 */
        MspErrorDbException = 10401, 	/* 0x28A1 */
        MspErrorDbNoResult = 10402, 	/* 0x28A2 */
        MspErrorDbInvalidUser = 10403, 	/* 0x28A3 */
        MspErrorDbInvalidPwd = 10404, 	/* 0x28A4 */
        MspErrorDbConnect = 10405, 	/* 0x28A5 */
        MspErrorDbInvalidSql = 10406, 	/* 0x28A6 */
        MspErrorDbInvalidAppid = 10407,	/* 0x28A7 */

        /* Error codes of Resource 10500(0x2904)*/
        MspErrorResGeneral = 10500, 	/* 0x2904 */
        MspErrorResLoad = 10501, 	/* 0x2905 */   /* Load resource */
        MspErrorResFree = 10502, 	/* 0x2906 */   /* Free resource */
        MspErrorResMissing = 10503, 	/* 0x2907 */   /* Resource File Missing */
        MspErrorResInvalidName = 10504, 	/* 0x2908 */   /* Invalid resource file name */
        MspErrorResInvalidID = 10505, 	/* 0x2909 */   /* Invalid resource ID */
        MspErrorResInvalidImg = 10506, 	/* 0x290A */   /* Invalid resource image pointer */
        MspErrorResWrite = 10507, 	/* 0x290B */   /* Write read-only resource */
        MspErrorResLeak = 10508, 	/* 0x290C */   /* Resource leak out */
        MspErrorResHead = 10509, 	/* 0x290D */   /* Resource head currupt */
        MspErrorResData = 10510, 	/* 0x290E */   /* Resource data currupt */
        MspErrorResSkip = 10511, 	/* 0x290F */   /* Resource file skipped */

        /* Error codes of TTS 10600(0x2968)*/
        MspErrorTtsGeneral = 10600, 	/* 0x2968 */
        MspErrorTtsTextend = 10601, 	/* 0x2969 */  /* Meet text end */
        MspErrorTtsTextEmpty = 10602, 	/* 0x296A */  /* no synth text */

        /* Error codes of Recognizer 10700(0x29CC) */
        MspErrorRecGeneral = 10700, 	/* 0x29CC */
        MspErrorRecInactive = 10701, 	/* 0x29CD */
        MspErrorRecGrammarError = 10702, 	/* 0x29CE */
        MspErrorRecNoActiveGrammars = 10703, 	/* 0x29CF */
        MspErrorRecDuplicateGrammar = 10704, 	/* 0x29D0 */
        MspErrorRecInvalidMediaType = 10705, 	/* 0x29D1 */
        MspErrorRecInvalidLanguage = 10706, 	/* 0x29D2 */
        MspErrorRecUriNotFound = 10707, 	/* 0x29D3 */
        MspErrorRecUriTimeout = 10708, 	/* 0x29D4 */
        MspErrorRecUriFetchError = 10709, 	/* 0x29D5 */

        /* Error codes of Speech Detector 10800(0x2A30) */
        MspErrorEpGeneral = 10800, 	/* 0x2A30 */
        MspErrorEpNoSessionName = 10801, 	/* 0x2A31 */
        MspErrorEpInactive = 10802, 	/* 0x2A32 */
        MspErrorEpInitialized = 10803, 	/* 0x2A33 */

        /* Error codes of TUV */
        MspErrorTuvGeneral = 10900, 	/* 0x2A94 */
        MspErrorTuvGethidparam = 10901, 	/* 0x2A95 */   /* Get Busin Param huanid*/
        MspErrorTuvToken = 10902, 	/* 0x2A96 */   /* Get Token */
        MspErrorTuvCfgfile = 10903, 	/* 0x2A97 */   /* Open cfg file */
        MspErrorTuvRecvContent = 10904, 	/* 0x2A98 */   /* received content is error */
        MspErrorTuvVerfail = 10905, 	/* 0x2A99 */   /* Verify failure */

        /* Error codes of IMTV */
        MspErrorImtvSuccess = 11000, 	/* 0x2AF8 */   /* 成功 */
        MspErrorImtvNoLicense = 11001, 	/* 0x2AF9 */   /* 试用次数结束，用户需要付费 */
        MspErrorImtvSessionidInvalid = 11002, 	/* 0x2AFA */   /* SessionId失效，需要重新登录通行证 */
        MspErrorImtvSessionidError = 11003, 	/* 0x2AFB */   /* SessionId为空，或者非法 */
        MspErrorImtvUnlogin = 11004, 	/* 0x2AFC */   /* 未登录通行证 */
        MspErrorImtvSystemError = 11005, 	/* 0x2AFD */   /* 系统错误 */

        /* Error codes of HCR */
        MspErrorHcrGeneral = 11100,
        MspErrorHcrResourceNotExist = 11101,

        /* Error codes of http 12000(0x2EE0) */
        MspErrorHttpBase = 12000,	/* 0x2EE0 */

        /*Error codes of ISV */
        MspErrorIsvNoUser = 13000,	/* 32C8 */    /* the user doesn't exist */
    }

    #region TTS枚举常量
    /// <summary>
    /// vol参数的枚举常量
    /// </summary>
    public enum EnuVol
    {
        XSoft,
        Soft,
        Medium,
        Loud,
        XLoud
    }

    /// <summary>
    /// speed语速参数的枚举常量
    /// </summary>
    public enum EnuSpeed
    {
        XSlow,
        Slow,
        Medium,
        Fast,
        XFast
    }
    /// <summary>
    /// speeker朗读者枚举常量
    /// </summary>
    public enum EnuSpeeker
    {
        小燕青年女声中英文普通话 = 0,
        小宇青年男声中英文普通话,
        凯瑟琳青年女声英语,
        亨利青年男声英语,
        玛丽青年女声英语,
        小研青年女声中英文普通话,
        小琪青年女声中英文普通话,
        小峰青年男声中英文普通话,
        小梅青年女声中英文粤语,
        小莉青年女声中英文台普,
        小蓉青年女声汉语四川话,
        小芸青年女声汉语东北话,
        小坤青年男声汉语河南话,
        小强青年男声汉语湖南话,
        小莹青年女声汉语陕西话,
        小新童年男声汉语普通话,
        楠楠童年女声汉语普通话,
        老孙老年男声汉语普通话
    }

    public enum SynthStatus
    {
        TtsFlagStillHaveData = 1,
        TtsFlagDataEnd,
        TtsFlagCmdCanceled
    }
    #endregion

    #region ISR枚举常量
    public enum AudioStatus
    {
        IsrAudioSampleInit = 0x00,
        IsrAudioSampleFirst = 0x01,
        IsrAudioSampleContinue = 0x02,
        IsrAudioSampleLast = 0x04,
        IsrAudioSampleSuppressed = 0x08,
        IsrAudioSampleLost = 0x10,
        IsrAudioSampleNewChunk = 0x20,
        IsrAudioSampleEndChunk = 0x40,

        IsrAudioSampleValidbits = 0x7f /* to validate the value of sample->status */
    }

    public enum EpStatus
    {
        IsrEpNull = -1,
        IsrEpLookingForSpeech = 0,          ///还没有检测到音频的前端点
        IsrEpInSpeech = 1,                   ///已经检测到了音频前端点，正在进行正常的音频处理。
        IsrEpAfterSpeech = 3,                ///检测到音频的后端点，后继的音频会被MSC忽略。
        IsrEpTimeout = 4,                     ///超时
        IsrEpError = 5,                       ///出现错误
        IsrEpMaxSpeech = 6                   ///音频过大
    }

    public enum RecogStatus
    {
        IsrRecNull = -1,
        IsrRecStatusSuccess = 0,             ///识别成功，此时用户可以调用QISRGetResult来获取（部分）结果。
        IsrRecStatusNoMatch = 1,            ///识别结束，没有识别结果
        IsrRecStatusIncomplete = 2,          ///正在识别中
        IsrRecStatusNonSpeechDetected = 3, ///保留
        IsrRecStatusSpeechDetected = 4,     ///发现有效音频
        IsrRecStatusSpeechComplete = 5,     ///识别结束
        IsrRecStatusMaxCpuTime = 6,        ///保留
        IsrRecStatusMaxSpeech = 7,          ///保留
        IsrRecStatusStopped = 8,             ///保留
        IsrRecStatusRejected = 9,            ///保留
        IsrRecStatusNoSpeechFound = 10     ///没有发现音频
    }
    #endregion

    public class FlyTts
    {
        public class JinDuEventArgs : EventArgs
        {
            public readonly int AllLenth;
            public readonly int AllP;
            public readonly int ThisLenth;
            public readonly int ThisP;
            public JinDuEventArgs(int allLenth, int allp, int thisLenth, int thisp)
            {
                AllLenth = allLenth;
                AllP = allp;
                ThisLenth = thisLenth;
                ThisP = thisp;
            }
        }

        public event EventHandler<JinDuEventArgs> Finished;

        /// <summary>
        /// 引入TTSDll函数的类
        /// </summary>
        private class TtsDll
        {
            #region TTS dll import

            [DllImport("msc.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int QTTSInit(string configs);

            ///<summary>
            ///初始化msc，用户登录。使用其他接口前必须先调用MSPLogin，可以在应用程序启动时调用。
            ///</summary>
            ///<param name="usr">此参数保留，传入NULL即可。</param>
            ///<param name="pwd">此参数保留，传入NULL即可。</param>
            ///<param name="_params">appid = 5964bde1</param>
            ///<returns></returns>
            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int MSPLogin(string usr, string pwd, string _params);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr QTTSSessionBegin(string _params, ref int errorCode);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QTTSTextPut(string sessionID, string textString, uint textLen, string _params);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr QTTSAudioGet(string sessionID, ref uint audioLen, ref SynthStatus synthStatus, ref int errorCode);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr QTTSAudioInfo(string sessionID);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QTTSSessionEnd(string sessionID, string hints);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QTTSGetParam(string sessionID, string paramName, string paramValue, ref uint valueLen);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QTTSFini();

            /// <summary>
            /// 退出
            /// </summary>
            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int MSPLogout();
            #endregion
        }
        private string _sessionID;
        /// <summary>
        /// 合成音频的采样频率，8000、16000、44100等
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// 讯飞的AppID
        /// </summary>
        /// public string appid { get; set; }

        private string _speed;
        /// <summary>
        /// 语速
        /// </summary>
        public EnuSpeed Speed
        {
            get { return (EnuSpeed)Enum.Parse(typeof(EnuVol), _speed); }
            set { _speed = value.ToString("G").Replace('_', '-'); }
        }

        private string _vol;
        /// <summary>
        /// 音量
        /// </summary>
        public EnuVol Vol
        {
            get { return (EnuVol)Enum.Parse(typeof(EnuVol), _vol); }
            set { _vol = value.ToString("G").Replace('_', '-'); }
        }
        /// <summary>
        /// 最大音频长度
        /// </summary>
        public long Max { get; set; }

        public EnuSpeeker Speeker
        {
            get { return Speeker; }
            set { _dSpeeker.TryGetValue(value, out _speeker); }
        }

        private string _speeker;

        private Dictionary<EnuSpeeker, string> _dSpeeker = new Dictionary<EnuSpeeker, string>();

        private Dictionary<string, string> _dSpeekerName = new Dictionary<string, string>();

        private byte[] _buffting;
        /// <summary>
        /// 构造函数，初始化引擎
        /// </summary>
        /// <param name="configs">初始化引擎参数</param>
        /// <param name="szParams">开始会话用参数</param>
        public FlyTts(string configs)
        {
            _dSpeeker.Add(EnuSpeeker.小燕青年女声中英文普通话, "ent=intp65,vcn=xiaoyan");
            _dSpeeker.Add(EnuSpeeker.小宇青年男声中英文普通话, "ent=intp65,vcn=xiaoyu");
            _dSpeeker.Add(EnuSpeeker.凯瑟琳青年女声英语, "ent=intp65_en,vcn=Catherine");
            _dSpeeker.Add(EnuSpeeker.亨利青年男声英语, "ent=intp65_en,vcn=henry");
            _dSpeeker.Add(EnuSpeeker.玛丽青年女声英语, "ent=vivi21,vcn=vimary");
            _dSpeeker.Add(EnuSpeeker.小研青年女声中英文普通话, "ent=vivi21,vcn=vixy");
            _dSpeeker.Add(EnuSpeeker.小琪青年女声中英文普通话, "ent=vivi21,vcn=vixq");
            _dSpeeker.Add(EnuSpeeker.小峰青年男声中英文普通话, "ent=vivi21,vcn=vixf");
            _dSpeeker.Add(EnuSpeeker.小梅青年女声中英文粤语, "ent=vivi21,vcn=vixm");
            _dSpeeker.Add(EnuSpeeker.小莉青年女声中英文台普, "ent=vivi21,vcn=vixl");
            _dSpeeker.Add(EnuSpeeker.小蓉青年女声汉语四川话, "ent=vivi21,vcn=vixr");
            _dSpeeker.Add(EnuSpeeker.小芸青年女声汉语东北话, "ent=vivi21,vcn=vixyun");
            _dSpeeker.Add(EnuSpeeker.小坤青年男声汉语河南话, "ent=vivi21,vcn=vixk");
            _dSpeeker.Add(EnuSpeeker.小强青年男声汉语湖南话, "ent=vivi21,vcn=vixqa");
            _dSpeeker.Add(EnuSpeeker.小莹青年女声汉语陕西话, "ent=vivi21,vcn=vixying");
            _dSpeeker.Add(EnuSpeeker.小新童年男声汉语普通话, "ent=vivi21,vcn=vixx");
            _dSpeeker.Add(EnuSpeeker.楠楠童年女声汉语普通话, "ent=vivi21,vcn=vinn");
            _dSpeeker.Add(EnuSpeeker.老孙老年男声汉语普通话, "ent=vivi21,vcn=vils");

            _dSpeekerName.Add("xiaoyan", "ent=intp65,vcn=xiaoyan");
            _dSpeekerName.Add("xiaoyu", "ent=intp65,vcn=xiaoyu");
            _dSpeekerName.Add("catherine", "ent=intp65_en,vcn=Catherine");
            _dSpeekerName.Add("henry", "ent=intp65_en,vcn=henry");
            _dSpeekerName.Add("mary", "ent=vivi21,vcn=vimary");
            _dSpeekerName.Add("xy", "ent=vivi21,vcn=vixy");
            _dSpeekerName.Add("xq", "ent=vivi21,vcn=vixq");
            _dSpeekerName.Add("xf", "ent=vivi21,vcn=vixf");
            _dSpeekerName.Add("xm", "ent=vivi21,vcn=vixm");
            _dSpeekerName.Add("xl", "ent=vivi21,vcn=vixl");
            _dSpeekerName.Add("xr", "ent=vivi21,vcn=vixr");
            _dSpeekerName.Add("xyun", "ent=vivi21,vcn=vixyun");
            _dSpeekerName.Add("xk", "ent=vivi21,vcn=vixk");
            _dSpeekerName.Add("xqa", "ent=vivi21,vcn=vixqa");
            _dSpeekerName.Add("xying", "ent=vivi21,vcn=vixying");
            _dSpeekerName.Add("xx", "ent=vivi21,vcn=vixx");
            _dSpeekerName.Add("nn", "ent=vivi21,vcn=vinn");
            _dSpeekerName.Add("ls", "ent=vivi21,vcn=vils");

            _buffting = iFlyResource.ding;

            var ret = TtsDll.MSPLogin(null, null, configs);
            if (ret != 0) throw new Exception("初始化TTS引擎错误，错误代码：" + ret);
        }

        /// <summary>
        /// 把文字转化为声音,多路配置，多种语音
        /// </summary>
        /// <param name="speekText">要转化成语音的文字</param>
        /// <param name="outWaveFlie">把声音转为文件，默认为不生产wave文件</param>
        public void MultiSpeek(string speekText, string outWaveFlie = null)
        {
            Console.WriteLine("开始，now=" + DateTime.Now);
            var mStream = new MemoryStream();
            try
            {
                var speekTexts = System.Text.RegularExpressions.Regex.Split(speekText, "\r\n");
                mStream.Write(new byte[44], 0, 44);
                for (var i = 0; i < speekTexts.Length; i++)
                {

                    var thisStr = speekTexts[i];
                    thisStr = thisStr.Trim();               //去除前后的空白
                    if (thisStr == "") continue;            //空段的处理
                    var pos = thisStr.IndexOf('#');         //#在段中的位置
                    if (pos > 0)
                    {
                        //设定了讲话者时用指定的讲话者说
                        _dSpeekerName.TryGetValue(thisStr.Substring(0, pos).ToLower(), out _speeker);
                        Speek(thisStr.Substring(pos + 1, thisStr.Length - pos - 1), ref mStream);
                    }
                    else
                    {
                        if (thisStr.Length < 4)
                        {//没有指定讲话者文本长度小于4
                            Speek(thisStr, ref mStream);
                            continue;
                        }
                        if (thisStr.Substring(0, 4).ToLower() == "stop")
                        {//暂停一段时间
                            var s = Convert.ToInt32(thisStr.Substring(4, thisStr.Length - 4));
                            var bs = new byte[32000];
                            for (var j = 0; j < s; j++)
                            {
                                mStream.Write(new byte[32000], 0, 32000);
                            }
                        }
                        else if (thisStr.Substring(0, 4).ToLower() == "ting")
                        {//插入叮铃声
                            mStream.Write(_buffting, 0, _buffting.Length);
                        }
                        else
                        {//没有指定讲话者文本长度大于等于4
                            Speek(thisStr, ref mStream);
                        }
                    }
                    var e = new JinDuEventArgs(speekTexts.Length, i, thisStr.Length, 0);
                    Finished?.Invoke(this, e);
                }
                var ret = TtsDll.MSPLogout();
                if (ret != 0) throw new Exception("逆初始化TTS引擎错误，错误代码：" + ((ErrorCode)ret).ToString("G"));

                var header = getWave_Header((int)mStream.Length - 44);     //创建wav文件头
                var headerByte = StructToBytes(header);                         //把文件头结构转化为字节数组                      //写入文件头
                mStream.Position = 0;                                                        //定位到文件头
                mStream.Write(headerByte, 0, headerByte.Length);                             //写入文件头
                //mStream.Position = 0;
                //System.Media.SoundPlayer pl = new System.Media.SoundPlayer(mStream);
                //pl.Stop();
                //pl.Play();
                if (outWaveFlie != null)
                {
                    var ofs = new FileStream(outWaveFlie, FileMode.Create);
                    mStream.WriteTo(ofs);
                    ofs.Close();
                    ofs = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                var ret = TtsDll.MSPLogout();
                mStream.Close();
                mStream = null;
            }
            Console.WriteLine("完成，now=" + DateTime.Now);
        }

        /// <summary>
        /// 把文本转换成声音，写入指定的内存流
        /// </summary>
        /// <param name="speekText">要转化成语音的文字</param>
        /// <param name="mStream">合成结果输出的音频流</param>
        private void Speek(string speekText, ref MemoryStream mStream)
        {
            if (speekText == "" || _speed == "" || _vol == "" || _speeker == "") return;
            var szParams = "ssm=1," + _speeker + ",spd=" + _speed + ",aue=speex-wb;7,vol=" + _vol + ",auf=audio/L16;rate=16000";
            var ret = 0;
            try
            {
                _sessionID = Ptr2Str(TtsDll.QTTSSessionBegin(szParams, ref ret));
                if (ret != 0) throw new Exception("初始化TTS引会话错误，错误代码：" + ret);

                ret = TtsDll.QTTSTextPut(_sessionID, speekText, (uint)Encoding.Default.GetByteCount(speekText), string.Empty);
                if (ret != 0) throw new Exception("向服务器发送数据，错误代码：" + ret);

                uint audioLen = 0;
                var synthStatus = SynthStatus.TtsFlagStillHaveData;

                mStream = new MemoryStream();
                mStream.Write(new byte[44], 0, 44);
                while (true)
                {
                    IntPtr source = TtsDll.QTTSAudioGet(_sessionID, ref audioLen, ref synthStatus, ref ret);
                    byte[] array = new byte[(int)audioLen];
                    if (audioLen > 0)
                    {
                        Marshal.Copy(source, array, 0, (int)audioLen);
                    }
                    mStream.Write(array, 0, array.Length);
                    Thread.Sleep(1000);
                    if (synthStatus == SynthStatus.TtsFlagDataEnd || ret != 0)
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                ret = TtsDll.QTTSSessionEnd(_sessionID, "");
                if (ret != 0) throw new Exception("结束TTS会话错误，错误代码：" + ret);
            }
        }

        /// <summary>
        /// 把文字转化为声音,单路配置，一种语音
        /// </summary>
        /// <param name="speekText">要转化成语音的文字</param>
        /// <param name="outWaveFlie">把声音转为文件，默认为不生产wave文件</param>
        private void Speek(string speekText, string outWaveFlie = null)
        {
            if (speekText == "" || _speed == "" || _vol == "" || _speeker == "") return;
            _dSpeeker.TryGetValue(Speeker, out _speeker);
            var szParams = "ssm=1," + _speeker + ",spd=" + _speed + ",aue=speex-wb;7,vol=" + _vol + ",auf=audio/L16;rate=16000";
            var ret = 0;
            try
            {
                _sessionID = Ptr2Str(TtsDll.QTTSSessionBegin(szParams, ref ret));
                if (ret != 0) throw new Exception("初始化TTS引会话错误，错误代码：" + ret);

                ret = TtsDll.QTTSTextPut(_sessionID, speekText, (uint)Encoding.Default.GetByteCount(speekText), string.Empty);
                if (ret != 0) throw new Exception("向服务器发送数据，错误代码：" + ret);
                IntPtr audioData;
                uint audioLen = 0;
                var synthStatus = SynthStatus.TtsFlagStillHaveData;

                var fs = new MemoryStream();
                fs.Write(new byte[44], 0, 44);                              //写44字节的空文件头

                while (synthStatus == SynthStatus.TtsFlagStillHaveData)
                {
                    audioData = TtsDll.QTTSAudioGet(_sessionID, ref audioLen, ref synthStatus, ref ret);
                    if (ret != 0) break;
                    var data = new byte[audioLen];
                    if (audioLen > 0) Marshal.Copy(audioData, data, 0, (int)audioLen);
                    fs.Write(data, 0, data.Length);
                }

                var header = getWave_Header((int)fs.Length - 44);     //创建wav文件头
                var headerByte = StructToBytes(header);                         //把文件头结构转化为字节数组                      //写入文件头
                fs.Position = 0;                                                        //定位到文件头
                fs.Write(headerByte, 0, headerByte.Length);                             //写入文件头

                fs.Position = 0;
                var pl = new System.Media.SoundPlayer(fs);
                pl.Stop();
                pl.Play();
                if (outWaveFlie != null)
                {
                    var ofs = new FileStream(outWaveFlie, FileMode.Create);
                    fs.WriteTo(ofs);
                    fs.Close();
                    ofs.Close();
                    fs = null;
                    ofs = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                ret = TtsDll.QTTSSessionEnd(_sessionID, "");
                if (ret != 0) throw new Exception("结束TTS会话错误，错误代码：" + ret);
            }
        }

        /// <summary>
        /// wave文件头
        /// </summary>
        private struct WaveHeader
        {
            public int RiffID;           //4 byte , 'RIFF'
            public int FileSize;         //4 byte , 文件长度
            public int RiffType;         //4 byte , 'WAVE'

            public int FmtID;            //4 byte , 'fmt'
            public int FmtSize;          //4 byte , 数值为16或18，18则最后又附加信息
            public short FmtTag;          //2 byte , 编码方式，一般为0x0001
            public ushort FmtChannel;     //2 byte , 声道数目，1--单声道；2--双声道
            public int FmtSamplesPerSec;//4 byte , 采样频率
            public int AvgBytesPerSec;   //4 byte , 每秒所需字节数,记录每秒的数据量
            public ushort BlockAlign;      //2 byte , 数据块对齐单位(每个采样需要的字节数)
            public ushort BitsPerSample;   //2 byte , 每个采样需要的bit数

            public int DataID;           //4 byte , 'data'
            public int DataSize;         //4 byte , 
        }

        /// <summary>
        /// 根据数据段的长度，生产文件头
        /// </summary>
        /// <param name="dataLen">音频数据长度</param>
        /// <returns>返回wav文件头结构体</returns>
        WaveHeader getWave_Header(int dataLen)
        {
            var wavHeader = new WaveHeader();
            wavHeader.RiffID = 0x46464952;        //字符RIFF
            wavHeader.FileSize = dataLen + 36;
            wavHeader.RiffType = 0x45564157;      //字符WAVE

            wavHeader.FmtID = 0x20746D66;         //字符fmt
            wavHeader.FmtSize = 16;
            wavHeader.FmtTag = 0x0001;
            wavHeader.FmtChannel = 1;             //单声道
            wavHeader.FmtSamplesPerSec = 16000;   //采样频率
            wavHeader.AvgBytesPerSec = 32000;      //每秒所需字节数
            wavHeader.BlockAlign = 2;              //每个采样1个字节
            wavHeader.BitsPerSample = 16;           //每个采样8bit

            wavHeader.DataID = 0x61746164;        //字符data
            wavHeader.DataSize = dataLen;

            return wavHeader;
        }

        /// <summary>
        /// 把结构体转化为字节序列
        /// </summary>
        /// <param name="structure">被转化的结构体</param>
        /// <returns>返回字节序列</returns>
        Byte[] StructToBytes(Object structure)
        {
            var size = Marshal.SizeOf(structure);
            var buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, buffer, false);
                var bytes = new Byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 指针转字符串
        /// </summary>
        /// <param name="p">指向非托管代码字符串的指针</param>
        /// <returns>返回指针指向的字符串</returns>
        public static string Ptr2Str(IntPtr p)
        {
            var lb = new List<byte>();
            while (Marshal.ReadByte(p) != 0)
            {
                lb.Add(Marshal.ReadByte(p));
                p = p + 1;
            }
            var bs = lb.ToArray();
            return Encoding.Default.GetString(lb.ToArray());
        }
    }

    public class FlyIsr
    {
        private class AsrDll
        {
            #region ISR dllimport

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QISRInit(string configs);
            ///<summary>
            ///初始化msc，用户登录。使用其他接口前必须先调用MSPLogin，可以在应用程序启动时调用。
            ///</summary>
            ///<param name="usr">此参数保留，传入NULL即可。</param>
            ///<param name="pwd">此参数保留，传入NULL即可。</param>
            ///<param name="_params">appid = 5964bde1</param>
            ///<returns></returns>
            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int MSPLogin(string usr, string pwd, string _params);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr QISRSessionBegin(string grammarList, string _params, ref int errorCode);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QISRGrammarActivate(string sessionID, string grammar, string type, int weight);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QISRAudioWrite(string sessionID, IntPtr waveData, uint waveLen, AudioStatus audioStatus, ref EpStatus epStatus, ref RecogStatus recogStatus);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr QISRGetResult(string sessionID, ref RecogStatus rsltStatus, int waitTime, ref int errorCode);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QISRSessionEnd(string sessionID, string hints);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QISRGetParam(string sessionID, string paramName, string paramValue, ref uint valueLen);

            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int QISRFini();

            /// <summary>
            /// 退出
            /// </summary>
            [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern int MSPLogout();
            #endregion
        }

        /// <summary>
        /// 有识别数据返回的事件参数，包含了识别的文本结果
        /// </summary>
        public class DataArrivedEventArgs : EventArgs
        {
            public string Result;
            public DataArrivedEventArgs(string rs)
            {
                Result = rs;
            }
        }

        /// <summary>
        /// 识别数据返回的事件
        /// </summary>
        public event EventHandler<DataArrivedEventArgs> DataArrived;

        /// <summary>
        /// 识别过程结束的事件
        /// </summary>
        public event EventHandler IsrEnd;

        //string c1 = "server_url=dev.voicecloud.cn,appid=4e7bf06d,timeout=10000";
        //string c2 = "sub=iat,ssm=1,auf=audio/L16;rate=16000,aue=speex,ent=sms16k,rst=plain";
        public string Appid;
        public int Rate;
        public string Auf;

        private delegate void DltSpeek(string inFile, string outfile = null);




        private WaveInEvent WaveMonitor;
        private WaveFileWriter Writer;
        private string _fileName;

        private const int BufferNum = 1024 * 20;
        private const int WaitTime = 1000;
        private readonly string _sessID;

        /// <summary>
        /// 构造函数，初始化引擎，开Session
        /// </summary>
        /// <param name="c1">初始化引擎的参数</param>
        /// <param name="c2">开session的参数</param>
        public FlyIsr(string c1, string c2)
        {

            var ret = 0;
            //引擎初始化，只需初始化一次
            ret = AsrDll.MSPLogin(null, null, c1);
            if (ret != 0) throw new Exception("QISP初始化失败，错误代码:" + ((ErrorCode)ret).ToString("G"));
            //第二个参数为传递的参数，使用会话模式，使用speex编解码，使用16k16bit的音频数据
            //第三个参数为返回码
            var param = c2;
            _sessID = FlyTts.Ptr2Str(AsrDll.QISRSessionBegin(string.Empty, param, ref ret));
            if (ret != 0) throw new Exception("QISRSessionBegin失败，错误代码:" + ((ErrorCode)ret).ToString("G"));
        }

        /// <summary>
        /// 执行语音识别的异步方法
        /// </summary>
        /// <param name="inFile">音频文件，pcm无文件头，采样率16k，数据16位，单声道</param>
        /// <param name="outFile">输出识别结果到文件</param>
        public void Audio2TxtAsync(string inFile, string outFile = null)
        {
            var dlt = new DltSpeek(Audio2Txt);
            dlt.BeginInvoke(inFile, outFile, null, null);
        }

        /// <summary>
        /// 进行声音识别
        /// </summary>
        /// <param name="inFile">音频文件，pcm无文件头，采样率16k，数据16位，单声道</param>
        /// <param name="outFile">输出识别结果到文件</param>
        public void Audio2Txt(string inFile, string outFile = null)
        {
            var ret = 0;
            var result = "";
            try
            {
                //模拟录音，输入音频
                if (!File.Exists(inFile)) throw new Exception("文件" + inFile + "不存在！");
                if (inFile.Substring(inFile.Length - 3, 3).ToUpper() != "WAV" && inFile.Substring(inFile.Length - 3, 3).ToUpper() != "PCM")
                    throw new Exception("音频文件格式不对！");
                var fp = new FileStream(inFile, FileMode.Open);
                if (inFile.Substring(inFile.Length - 3, 3).ToUpper() == "WAV") fp.Position = 44;
                var buff = new byte[BufferNum];
                var bp = Marshal.AllocHGlobal(BufferNum);
                int len;
                var status = AudioStatus.IsrAudioSampleContinue;
                var epStatus = EpStatus.IsrEpNull;
                var recStatus = RecogStatus.IsrRecNull;
                var rsltStatus = RecogStatus.IsrRecNull;
                //ep_status        端点检测（End-point detected）器所处的状态
                //rec_status       识别器所处的状态
                //rslt_status      识别器所处的状态
                while (fp.Position != fp.Length)
                {
                    len = fp.Read(buff, 0, BufferNum);
                    Marshal.Copy(buff, 0, bp, buff.Length);
                    //开始向服务器发送音频数据
                    ret = AsrDll.QISRAudioWrite(_sessID, bp, (uint)len, status, ref epStatus, ref recStatus);
                    if (ret != 0)
                    {
                        fp.Close();
                        throw new Exception("QISRAudioWrite err,errCode=" + ((ErrorCode)ret).ToString("G"));
                    }
                    //服务器返回部分结果
                    if (recStatus == RecogStatus.IsrRecStatusSuccess)
                    {
                        var p = AsrDll.QISRGetResult(_sessID, ref rsltStatus, WaitTime, ref ret);
                        if (p != IntPtr.Zero)
                        {
                            var tmp = FlyTts.Ptr2Str(p);
                            DataArrived?.Invoke(this, new DataArrivedEventArgs(tmp));
                            result += tmp;
                            Console.WriteLine(@"返回部分结果！:" + tmp);
                        }
                    }
                    Thread.Sleep(500);
                }
                fp.Close();

                //最后一块数据
                status = AudioStatus.IsrAudioSampleLast;

                ret = AsrDll.QISRAudioWrite(_sessID, bp, 1, status, ref epStatus, ref recStatus);
                if (ret != 0) throw new Exception("QISRAudioWrite write last audio err,errCode=" + ((ErrorCode)ret).ToString("G"));
                Marshal.FreeHGlobal(bp);
                var loopCount = 0;
                //最后一块数据发完之后，循环从服务器端获取结果
                //考虑到网络环境不好的情况下，需要对循环次数作限定
                do
                {
                    var p = AsrDll.QISRGetResult(_sessID, ref rsltStatus, WaitTime, ref ret);
                    if (p != IntPtr.Zero)
                    {
                        var tmp = FlyTts.Ptr2Str(p);
                        DataArrived?.Invoke(this, new DataArrivedEventArgs(tmp)); //激发识别数据到达事件
                        result += tmp;
                        Console.WriteLine(@"传完音频后返回结果！:" + tmp);
                    }
                    if (ret != 0) throw new Exception("QISRGetResult err,errCode=" + ((ErrorCode)ret).ToString("G"));
                    Thread.Sleep(200);
                } while (rsltStatus != RecogStatus.IsrRecStatusSpeechComplete && loopCount++ < 30);
                if (outFile != null)
                {
                    var fout = new FileStream(outFile, FileMode.OpenOrCreate);
                    fout.Write(Encoding.Default.GetBytes(result), 0, Encoding.Default.GetByteCount(result));
                    fout.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ret = AsrDll.QISRSessionEnd(_sessID, string.Empty);
                ret = AsrDll.MSPLogout();
                IsrEnd?.Invoke(this, new EventArgs()); //通知识别结束
            }
        }

        #region 想实现编录边识别的功能，但还不行
        public void StartRecoding()
        {
            WaveMonitor = new WaveInEvent { WaveFormat = new WaveFormat(16000, 16, 1) };

            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");
            _fileName = Path.Combine("temp", Guid.NewGuid() + ".wav");

            Writer = new WaveFileWriter(_fileName, WaveMonitor.WaveFormat);

            WaveMonitor.DataAvailable += (s, a) => Writer.Write(a.Buffer, 0, a.BytesRecorded);
            WaveMonitor.RecordingStopped += (s, a) => { Writer?.Dispose(); WaveMonitor?.Dispose(); };

            WaveMonitor.StartRecording();
        }

        public void StopRecoding()
        {
            WaveMonitor.StopRecording();
            Writer?.Close();

            //var audioStatus = AudioStatus.IsrAudioSampleLast;
            //var epStatus = EpStatus.IsrEpNull;
            //var recStatus = RecogStatus.IsrRecNull;
            //var rsltStatus = RecogStatus.IsrRecNull;

            //var ret = AsrDll.QISRAudioWrite(_sessID, IntPtr.Zero, 0, audioStatus, ref epStatus, ref recStatus);
            //if (ret != 0) throw new Exception("QISRAudioWrite write last audio err,errCode=" + ((ErrorCode)ret).ToString("G"));

            //do
            //{
            //    var p = AsrDll.QISRGetResult(_sessID, ref rsltStatus, 0, ref ret);
            //    if (ret != 0) throw new Exception("QISRGetResult err,errCode=" + ((ErrorCode)ret).ToString("G"));
            //    if (p != IntPtr.Zero)
            //    {
            //        var tmp = FlyTts.Ptr2Str(p);
            //        DataArrived?.Invoke(this, new DataArrivedEventArgs(tmp)); //激发识别数据到达事件
            //        Console.WriteLine(@"传完音频后返回结果！-->" + tmp);
            //    }
            //    Thread.Sleep(200);
            //} while (rsltStatus != RecogStatus.IsrRecStatusSpeechComplete);

            //ret = AsrDll.QISRSessionEnd(_sessID, string.Empty);
            //ret = AsrDll.MSPLogout();
            //IsrEnd(this, new EventArgs());//通知识别结束

            Audio2Txt(_fileName);
        }
        

        private void wis_DataAvailable(object sender, WaveInEventArgs e)
        {
            Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond);

            var ret = 0;
            var bp = Marshal.AllocHGlobal(e.BytesRecorded);
            var status = AudioStatus.IsrAudioSampleContinue;
            var epStatus = EpStatus.IsrEpNull;
            var recStatus = RecogStatus.IsrRecNull;
            var rsltStatus = RecogStatus.IsrRecNull;
            //ep_status        端点检测（End-point detected）器所处的状态
            //rec_status       识别器所处的状态
            //rslt_status      识别器所处的状态
            Marshal.Copy(e.Buffer, 0, bp, e.BytesRecorded);
            //开始向服务器发送音频数据
            ret = AsrDll.QISRAudioWrite(_sessID, bp, (uint)e.BytesRecorded, status, ref epStatus, ref recStatus);
            if (ret != 0) throw new Exception("QISRAudioWrite err,errCode=" + ((ErrorCode)ret).ToString("G"));

            //服务器返回部分结果
            if (recStatus == RecogStatus.IsrRecStatusSuccess)
            {
                var p = AsrDll.QISRGetResult(_sessID, ref rsltStatus, 0, ref ret);
                if (p != IntPtr.Zero)
                {
                    var tmp = FlyTts.Ptr2Str(p);
                    DataArrived?.Invoke(this, new DataArrivedEventArgs(tmp)); //激发识别数据到达事件
                    Console.WriteLine(@"服务器返回部分结果！-->" + tmp);
                }
            }
            Thread.Sleep(500);
            Marshal.FreeHGlobal(bp);
            //System.Console.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond);
        }
        #endregion
    }
}
