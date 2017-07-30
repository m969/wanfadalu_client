using UnityEngine;
using System.Collections;

namespace KBEngine
{
	public partial class Account : Entity 
    {

	    public override void __init__()
	    {
	        base.__init__();
            Event.fireOut("onLoginSuccessfully", new object[] { KBEngineApp.app.entity_uuid, id, this });
	    }

        public override void onDestroy()
        {
            base.onDestroy();
            Event.deregisterIn(this);
        }
	
	}
}
