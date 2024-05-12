using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    [MemoryPackable]
    [Message(InnerMessage3.A2U_AddOrUpdateUnitCache)]
    [ResponseType(nameof(U2A_AddOrUpdateUnitCache))]
    public partial class A2U_AddOrUpdateUnitCache : MessageObject, IRequest
    {
        public static A2U_AddOrUpdateUnitCache Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(A2U_AddOrUpdateUnitCache), isFromPool) as A2U_AddOrUpdateUnitCache;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public long UnitId { get; set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        [MemoryPackOrder(2)]
        public List<string> EntityTypes { get; set; } = new();

        /// <summary>
        /// 实体数据
        /// </summary>
        [MemoryPackOrder(3)]
        public List<byte[]> EntityBytes { get; set; } = new();

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.UnitId = default;
            this.EntityTypes.Clear();
            this.EntityBytes.Clear();

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(InnerMessage3.U2A_AddOrUpdateUnitCache)]
    public partial class U2A_AddOrUpdateUnitCache : MessageObject, IResponse
    {
        public static U2A_AddOrUpdateUnitCache Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(U2A_AddOrUpdateUnitCache), isFromPool) as U2A_AddOrUpdateUnitCache;
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
    [Message(InnerMessage3.A2U_GetUnitCache)]
    [ResponseType(nameof(U2A_GetUnitCache))]
    public partial class A2U_GetUnitCache : MessageObject, IRequest
    {
        public static A2U_GetUnitCache Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(A2U_GetUnitCache), isFromPool) as A2U_GetUnitCache;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public long UnitId { get; set; }

        [MemoryPackOrder(2)]
        public List<string> ComponentNameList { get; set; } = new();

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.UnitId = default;
            this.ComponentNameList.Clear();

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(InnerMessage3.U2A_GetUnitCache)]
    public partial class U2A_GetUnitCache : MessageObject, IResponse
    {
        public static U2A_GetUnitCache Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(U2A_GetUnitCache), isFromPool) as U2A_GetUnitCache;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public List<Entity> EntityList { get; set; } = new();

        [MemoryPackOrder(4)]
        public List<string> ComponentNameList { get; set; } = new();

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.EntityList.Clear();
            this.ComponentNameList.Clear();

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(InnerMessage3.A2U_DeleteUnitCache)]
    [ResponseType(nameof(U2A_DeleteUnitCache))]
    public partial class A2U_DeleteUnitCache : MessageObject, IRequest
    {
        public static A2U_DeleteUnitCache Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(A2U_DeleteUnitCache), isFromPool) as A2U_DeleteUnitCache;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public long UnitId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.UnitId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(InnerMessage3.U2A_DeleteUnitCache)]
    public partial class U2A_DeleteUnitCache : MessageObject, IResponse
    {
        public static U2A_DeleteUnitCache Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(U2A_DeleteUnitCache), isFromPool) as U2A_DeleteUnitCache;
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

    public static class InnerMessage3
    {
        public const ushort A2U_AddOrUpdateUnitCache = 20102;
        public const ushort U2A_AddOrUpdateUnitCache = 20103;
        public const ushort A2U_GetUnitCache = 20104;
        public const ushort U2A_GetUnitCache = 20105;
        public const ushort A2U_DeleteUnitCache = 20106;
        public const ushort U2A_DeleteUnitCache = 20107;
    }
}