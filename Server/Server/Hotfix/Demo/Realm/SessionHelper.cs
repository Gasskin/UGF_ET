namespace ET.Server;

public static class SessionHelper
{
    public static async ETTask Disconnect(this Session session)
    {
        if (session == null || session.IsDisposed) 
        {
            return;
        }
        var instanceId = session.InstanceId;

        var timerComponent = session.Root().GetComponent<TimerComponent>();
        await timerComponent.WaitAsync(1000);

        if (session.InstanceId != instanceId)
            return;
        session.Dispose();
    }
}