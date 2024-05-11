namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        public const int ERR_LoginRequestRepeated = 200001;
        public const int ERR_LoginInfoEmpty = 200002;
        public const int ERR_LoginPasswordError = 200003;
        public const int ERR_LoginAccountInBlackList = 200004;
        public const int ERR_LoginTokenError = 200005;
        public const int ERR_LoginCreateRoleNameSame = 200006;
    }
}