// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MagicFire.HuanHuoUFrame {
    using MagicFire.HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using UnityEngine;
    
    
    public partial class UserLoginCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class ShowAvatarBagPanelCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class Test03LoginCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class ShowRegistePanelCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private MagicFire.HuanHuoUFrame.RegistePanel _RegistePanel;
        
        public MagicFire.HuanHuoUFrame.RegistePanel RegistePanel {
            get {
                return _RegistePanel;
            }
            set {
                _RegistePanel = value;
            }
        }
    }
    
    public partial class Test01LoginCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class ExitGameCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class RegisteUserCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class CloseRegistePanelCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private MagicFire.HuanHuoUFrame.RegistePanel _RegistePanel;
        
        public MagicFire.HuanHuoUFrame.RegistePanel RegistePanel {
            get {
                return _RegistePanel;
            }
            set {
                _RegistePanel = value;
            }
        }
    }
    
    public partial class Test02LoginCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class ShowMessageCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class ShowCharacterInfoPanelCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class CloseMessageCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
}
