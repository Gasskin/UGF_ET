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

    [MemoryPackable]
    [Message(OuterMessage2.RoleInfoProto)]
    public partial class RoleInfoProto : MessageObject
    {
        public static RoleInfoProto Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(RoleInfoProto), isFromPool) as RoleInfoProto;
        }

        [MemoryPackOrder(0)]
        public long Id { get; set; }

        [MemoryPackOrder(1)]
        public string Name { get; set; }

        [MemoryPackOrder(2)]
        public int State { get; set; }

        [MemoryPackOrder(3)]
        public string Account { get; set; }

        [MemoryPackOrder(4)]
        public long LastLoginTime { get; set; }

        [MemoryPackOrder(5)]
        public int ServerId { get; set; }

        [MemoryPackOrder(6)]
        public long CreateTime { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.Id = default;
            this.Name = default;
            this.State = default;
            this.Account = default;
            this.LastLoginTime = default;
            this.ServerId = default;
            this.CreateTime = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.C2R_GetRoles)]
    [ResponseType(nameof(R2C_GetRoles))]
    public partial class C2R_GetRoles : MessageObject, ISessionRequest
    {
        public static C2R_GetRoles Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2R_GetRoles), isFromPool) as C2R_GetRoles;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Token { get; set; }

        [MemoryPackOrder(2)]
        public string Account { get; set; }

        [MemoryPackOrder(2)]
        public int ServerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Token = default;
            this.Account = default;
            this.ServerId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.R2C_GetRoles)]
    public partial class R2C_GetRoles : MessageObject, ISessionResponse
    {
        public static R2C_GetRoles Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(R2C_GetRoles), isFromPool) as R2C_GetRoles;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public List<RoleInfoProto> RoleInfoList { get; set; } = new();

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.RoleInfoList.Clear();

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.C2R_CreateRole)]
    [ResponseType(nameof(R2C_CreateRole))]
    public partial class C2R_CreateRole : MessageObject, ISessionRequest
    {
        public static C2R_CreateRole Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2R_CreateRole), isFromPool) as C2R_CreateRole;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Token { get; set; }

        [MemoryPackOrder(2)]
        public string Account { get; set; }

        [MemoryPackOrder(3)]
        public string Name { get; set; }

        [MemoryPackOrder(4)]
        public int ServerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Token = default;
            this.Account = default;
            this.Name = default;
            this.ServerId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.R2C_CreateRole)]
    public partial class R2C_CreateRole : MessageObject, ISessionResponse
    {
        public static R2C_CreateRole Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(R2C_CreateRole), isFromPool) as R2C_CreateRole;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public RoleInfoProto RoleInfoProto { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.RoleInfoProto = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.C2R_GetRealmKey)]
    [ResponseType(nameof(R2C_CreateRole))]
    public partial class C2R_GetRealmKey : MessageObject, ISessionRequest
    {
        public static C2R_GetRealmKey Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2R_GetRealmKey), isFromPool) as C2R_GetRealmKey;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Token { get; set; }

        [MemoryPackOrder(2)]
        public string Account { get; set; }

        [MemoryPackOrder(3)]
        public int ServerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Token = default;
            this.Account = default;
            this.ServerId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.R2C_GetRealmKey)]
    public partial class R2C_GetRealmKey : MessageObject, ISessionResponse
    {
        public static R2C_GetRealmKey Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(R2C_GetRealmKey), isFromPool) as R2C_GetRealmKey;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public string Address { get; set; }

        [MemoryPackOrder(4)]
        public long key { get; set; }

        [MemoryPackOrder(5)]
        public long GateId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.Address = default;
            this.key = default;
            this.GateId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.C2G_LoginGameGate)]
    [ResponseType(nameof(G2C_LoginGameGate))]
    public partial class C2G_LoginGameGate : MessageObject, ISessionRequest
    {
        public static C2G_LoginGameGate Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2G_LoginGameGate), isFromPool) as C2G_LoginGameGate;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        [MemoryPackOrder(2)]
        public long Key { get; set; }

        [MemoryPackOrder(3)]
        public long RoleId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;
            this.Key = default;
            this.RoleId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.G2C_LoginGameGate)]
    public partial class G2C_LoginGameGate : MessageObject, ISessionResponse
    {
        public static G2C_LoginGameGate Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(G2C_LoginGameGate), isFromPool) as G2C_LoginGameGate;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public long PlayerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.PlayerId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.C2G_EnterGame)]
    [ResponseType(nameof(G2C_EnterGame))]
    public partial class C2G_EnterGame : MessageObject, ISessionRequest
    {
        public static C2G_EnterGame Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(C2G_EnterGame), isFromPool) as C2G_EnterGame;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(OuterMessage2.G2C_EnterGame)]
    public partial class G2C_EnterGame : MessageObject, ISessionResponse
    {
        public static G2C_EnterGame Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(G2C_EnterGame), isFromPool) as G2C_EnterGame;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public long MyUnitId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.MyUnitId = default;

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
        public const ushort RoleInfoProto = 10058;
        public const ushort C2R_GetRoles = 10059;
        public const ushort R2C_GetRoles = 10060;
        public const ushort C2R_CreateRole = 10061;
        public const ushort R2C_CreateRole = 10062;
        public const ushort C2R_GetRealmKey = 10063;
        public const ushort R2C_GetRealmKey = 10064;
        public const ushort C2G_LoginGameGate = 10065;
        public const ushort G2C_LoginGameGate = 10066;
        public const ushort C2G_EnterGame = 10067;
        public const ushort G2C_EnterGame = 10068;
    }
}