using System;
using System.Collections.Generic;

namespace ET.Server;

[FriendOf(typeof(UnitCacheComponent))]
[MessageHandler(SceneType.UnitCache)]
public class A2U_GetUnitCacheHandler: MessageHandler<Scene, A2U_GetUnitCache, U2A_GetUnitCache>
{
    protected override async ETTask Run(Scene scene, A2U_GetUnitCache request, U2A_GetUnitCache response)
    {
        var unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
        var dic = ObjectPool.Instance.Fetch<Dictionary<string, Entity>>();
        try
        {
            if (request.ComponentNameList.Count == 0)
            {
                dic.Add(nameof (Unit), null);
                foreach (var key in unitCacheComponent.UnitCacheKeyList)
                {
                    dic.Add(key, null);
                }
            }
            else
            {
                foreach (string key in request.ComponentNameList)
                {
                    dic.Add(key, null);
                }
            }

            foreach (string key in dic.Keys)
            {
                var entity = await unitCacheComponent.Get(request.UnitId, key);
                dic[key] = entity;
            }
            
            response.ComponentNameList.AddRange(dic.Keys);
            response.EntityList.AddRange(dic.Values);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            dic.Clear();
            ObjectPool.Instance.Recycle(dic);
        }
        
        await ETTask.CompletedTask;
    }
}