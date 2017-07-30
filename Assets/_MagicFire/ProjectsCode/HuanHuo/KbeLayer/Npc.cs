using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KBEngine
{
    public partial class Npc : Model
    {
        public List<object> _storeGoodsIDList = new List<object>();
        public List<object> StoreGoodsIDList
        {
            get
            {
                if (_storeGoodsIDList.Count == 0)
                {
                    object storeGoodsIDListObject = getDefinedProperty("storeGoodsIDList");
                    _storeGoodsIDList = ((Dictionary<string, object>)storeGoodsIDListObject)["values"] as List<object>;
                }
                return _storeGoodsIDList;
            }
        }

        public override void __init__()
        {
            base.__init__();
        }

        public override void onEnterWorld()
        {
            base.onEnterWorld();
        }

        public void set_storeGoodsIDList(object old)
        {
            object storeGoodsIDListObject = getDefinedProperty("storeGoodsIDList");
            _storeGoodsIDList = ((Dictionary<string, object>)storeGoodsIDListObject)["values"] as List<object>;
            Event.fireOut("set_storeGoodsIDList", new object[] { this, _storeGoodsIDList });
        }
    }
}