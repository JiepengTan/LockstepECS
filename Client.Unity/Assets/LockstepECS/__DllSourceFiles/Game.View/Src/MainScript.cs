using System;
using System.IO;
using System.Xml;
using Lockstep.Game;
using Lockstep.Game.UI;
using Lockstep.Network;
using Lockstep.UnsafeECS;
using Lockstep.UnsafeECS.Game;
using UnityEngine;

public class MainScript : BaseMainScript {
    protected override ServiceContainer CreateServiceContainer(){
        return new UnityServiceContainer();
    }

    protected override object CreateWorld(IServiceContainer services, object contextsObj, object logicFeatureObj){
        return new UnsafeWorld(services, contextsObj, logicFeatureObj);
    }

    public string Ip = "127.0.0.1";
    public ushort port = 10083;
    protected override void Awake(){
        NetClient.serverIpPoint =  NetworkUtil.ToIPEndPoint(Ip, port);
        base.Awake();
        gameObject.AddComponent<PingMono>();
        gameObject.AddComponent<InputMono>();
        Screen.SetResolution(1024, 768, false);
    }
}