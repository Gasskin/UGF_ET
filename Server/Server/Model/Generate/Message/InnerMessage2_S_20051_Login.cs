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

    public static class InnerMessage2
    {
        public const ushort R2L_LoginAccountRequest = 20052;
        public const ushort L2R_LoginAccountRequest = 20053;
    }
}