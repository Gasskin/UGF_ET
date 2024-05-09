using Luban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using cfg;

namespace ET
{
    [Invoke]
    public class GetAllConfigBytes: AInvokeHandler<ConfigLoader.GetAllConfigBytes, ETTask>
    {
        public override async ETTask Handle(ConfigLoader.GetAllConfigBytes args)
        {
            Dictionary<Type, ByteBuf> output = new Dictionary<Type, ByteBuf>();
            var tables = new Tables();

            await tables.LoadAsync((async cfg =>
            {
                var bytes = await File.ReadAllBytesAsync($"../Config/Generate/{cfg}.bytes");
                return new ByteBuf(bytes);
            }));

            
            
            await ETTask.CompletedTask;
        }
    }
}