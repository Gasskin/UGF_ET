using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    [MemoryPackable]
    [Message(InnerMessage2.R2L_LoginAccountRequest)]
    [ResponseType(nameof(L2R_LoginAccountRequest))]
    public partial class R2L_LoginAccountRequest : MessageObject, IRequest
    {
        public static R2L_LoginAccountRequest Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(R2L_LoginAccountRequest), isFromPool) as R2L_LoginAccountRequest;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(InnerMessage2.L2R_LoginAccountRequest)]
    public partial class L2R_LoginAccountRequest : MessageObject, IResponse
    {
        public static L2R_LoginAccountRequest Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(L2R_LoginAccountRequest), isFromPool) as L2R_LoginAccountRequest;
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
    [Message(InnerMessage2.L2G_DisconnectGateUnit)]
    [ResponseType(nameof(G2L_DisconnectGateUnit))]
    public partial class L2G_DisconnectGateUnit : MessageObject, IRequest
    {
        public static L2G_DisconnectGateUnit Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(L2G_DisconnectGateUnit), isFromPool) as L2G_DisconnectGateUnit;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(InnerMessage2.G2L_DisconnectGateUnit)]
    public partial class G2L_DisconnectGateUnit : MessageObject, IResponse
    {
        public static G2L_DisconnectGateUnit Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(G2L_DisconnectGateUnit), isFromPool) as G2L_DisconnectGateUnit;
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

    public static class InnerMessage2
    {
        public const ushort R2L_LoginAccountRequest = 20052;
        public const ushort L2R_LoginAccountRequest = 20053;
        public const ushort L2G_DisconnectGateUnit = 20054;
        public const ushort G2L_DisconnectGateUnit = 20055;
    }
}