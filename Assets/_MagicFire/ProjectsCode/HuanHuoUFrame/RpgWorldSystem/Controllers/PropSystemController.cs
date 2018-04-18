namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Newtonsoft.Json.Linq;


    public struct Prop
    {
        public int id;
        public int type;
        public int gongFa;
        public string name;
        public ulong propUUID;
        public int index;
        public JObject propData;
    }
    
    
    public class PropSystemController : PropSystemControllerBase {

        public static Dictionary<int, Prop> PropConfigList = new Dictionary<int, Prop>();

        public static void InitPropConfigTable()
        {
            var propJsonText = Resources.Load<TextAsset>("JsonConfigDatas/prop_config_Table");
            var propConfigJson = JObject.Parse(propJsonText.text);
            foreach (var item in propConfigJson)
            {
                var name = item.Value["name"].ToString();
                var id = int.Parse(item.Value["id"].ToString());
                var type = int.Parse(item.Value["type"].ToString());
                var gongFa = int.Parse(item.Value["gongFa"].ToString());
                var prop = new Prop() {
                    id = id,
                    type = type,
                    gongFa = gongFa,
                    name = name
                };
                PropConfigList.Add(id, prop);
            }
        }
        
        public override void InitializePropSystem(PropSystemViewModel viewModel) {
            base.InitializePropSystem(viewModel);
            // This is called when a PropSystemViewModel is created
        }

        public override void RequestPullStorePropList(PropSystemViewModel viewModel, RequestPullStorePropListCommand arg)
        {
            Debug.Log("PropSystemController:RequestPullStorePropList");
            viewModel.cellCall("requestPullStorePropList", arg.StoreNpcID);
        }
    }
}
