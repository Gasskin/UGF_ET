using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    [MemoryPackable]
    [Message(OuterMessage2.C2R_LoginAccount)]
    [ResponseType(nameof(R2C_LoginAccount))]
    public partial class C2R_LoginAccount : MessageObject, ISessionRequest
    {
        public static C2R_LoginAccount Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2R_LoginAccount), isFromPool) as C2R_LoginAccount;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        [MemoryPackOrder(2)]
        public string Password { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;
            this.Password = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.R2C_LoginAccount)]
    public partial class R2C_LoginAccount : MessageObject, ISessionResponse
    {
        public static R2C_LoginAccount Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(R2C_LoginAccount), isFromPool) as R2C_LoginAccount;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public string Token { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.Token = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.A2C_Disconnect)]
    public partial class A2C_Disconnect : MessageObject, ISessionMessage
    {
        public static A2C_Disconnect Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(A2C_Disconnect), isFromPool) as A2C_Disconnect;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.ServerInfoProto)]
    public partial class ServerInfoProto : MessageObject
    {
        public static ServerInfoProto Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(ServerInfoProto), isFromPool) as ServerInfoProto;
        }

        [MemoryPackOrder(0)]
        public int Id { get; set; }

        [MemoryPackOrder(1)]
        public int Status { get; set; }

        [MemoryPackOrder(2)]
        public string ServerName { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.Id = default;
            this.Status = default;
            this.ServerName = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.C2R_GetServerInfos)]
    [ResponseType(nameof(R2C_GetServerInfos))]
    public partial class C2R_GetServerInfos : MessageObject, ISessionRequest
    {
        public static C2R_GetServerInfos Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2R_GetServerInfos), isFromPool) as C2R_GetServerInfos;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Token { get; set; }

        [MemoryPackOrder(2)]
        public string Account { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Token = default;
            this.Account = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.R2C_GetServerInfos)]
    public partial class R2C_GetServerInfos : MessageObject, ISessionResponse
    {
        public static R2C_GetServerInfos Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(R2C_GetServerInfos), isFromPool) as R2C_GetServerInfos;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public List<ServerInfoProto> ServerInfoList { get; set; } = new();

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.ServerInfoList.Clear();

            ObjectPool.Instance.Recycle(this);
        }
    }

    public static class OuterMessage2
    {
        public const ushort C2R_LoginAccount = 10052;
        public const ushort R2C_LoginAccount = 10053;
        public const ushort A2C_Disconnect = 10054;
        public const ushort ServerInfoProto = 10055;
        public const ushort C2R_GetServerInfos = 10056;
        public const ushort R2C_GetServerInfos = 10057;
    }
}