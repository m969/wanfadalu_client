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
    
    
    public partial class OnDestroyCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class RequestPullStorePropListCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _StoreNpcID;
        
        public Int32 StoreNpcID {
            get {
                return _StoreNpcID;
            }
            set {
                _StoreNpcID = value;
            }
        }
    }
    
    public partial class RequestMoveCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Vector3 _Point;
        
        public Vector3 Point {
            get {
                return _Point;
            }
            set {
                _Point = value;
            }
        }
    }
    
    public partial class OnStopMoveCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class OnDeadCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class SelectDialogItemCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _DialogID;
        
        public Int32 DialogID {
            get {
                return _DialogID;
            }
            set {
                _DialogID = value;
            }
        }
    }
    
    public partial class OnPullStorePropListReturnCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _NpcID;
        
        private object _StorePropList;
        
        public Int32 NpcID {
            get {
                return _NpcID;
            }
            set {
                _NpcID = value;
            }
        }
        
        public object StorePropList {
            get {
                return _StorePropList;
            }
            set {
                _StorePropList = value;
            }
        }
    }
    
    public partial class onMainAvatarEnterSpaceCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _SpaceId;
        
        private String _SpaceName;
        
        public Int32 SpaceId {
            get {
                return _SpaceId;
            }
            set {
                _SpaceId = value;
            }
        }
        
        public String SpaceName {
            get {
                return _SpaceName;
            }
            set {
                _SpaceName = value;
            }
        }
    }
    
    public partial class OnLeaveWorldCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class OnExitArenaCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Vector3 _OutPosition;
        
        public Vector3 OutPosition {
            get {
                return _OutPosition;
            }
            set {
                _OutPosition = value;
            }
        }
    }
    
    public partial class TeleportCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Vector3 _Position;
        
        public Vector3 Position {
            get {
                return _Position;
            }
            set {
                _Position = value;
            }
        }
    }
    
    public partial class OnSkillEndCastCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _skillID;
        
        private String _argsString;
        
        public Int32 skillID {
            get {
                return _skillID;
            }
            set {
                _skillID = value;
            }
        }
        
        public String argsString {
            get {
                return _argsString;
            }
            set {
                _argsString = value;
            }
        }
    }
    
    public partial class learnGongFaCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private String _gongFaName;
        
        public String gongFaName {
            get {
                return _gongFaName;
            }
            set {
                _gongFaName = value;
            }
        }
    }
    
    public partial class OnRequestForgeResultCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _Result;
        
        public Int32 Result {
            get {
                return _Result;
            }
            set {
                _Result = value;
            }
        }
    }
    
    public partial class RequestExitArenaCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class RequestStopMoveCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class RequestSelfRankingCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class OnJoinSectResultCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _SectID;
        
        private Int32 _Result;
        
        public Int32 SectID {
            get {
                return _SectID;
            }
            set {
                _SectID = value;
            }
        }
        
        public Int32 Result {
            get {
                return _Result;
            }
            set {
                _Result = value;
            }
        }
    }
    
    public partial class OnErrorCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _ErrorCode;
        
        public Int32 ErrorCode {
            get {
                return _ErrorCode;
            }
            set {
                _ErrorCode = value;
            }
        }
    }
    
    public partial class RequestEnterArenaCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _ArenaID;
        
        public Int32 ArenaID {
            get {
                return _ArenaID;
            }
            set {
                _ArenaID = value;
            }
        }
    }
    
    public partial class OnDialogItemsReturnCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private object _DialogItemsObject;
        
        public object DialogItemsObject {
            get {
                return _DialogItemsObject;
            }
            set {
                _DialogItemsObject = value;
            }
        }
    }
    
    public partial class OnRespawnCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Vector3 _RespawnPosition;
        
        public Vector3 RespawnPosition {
            get {
                return _RespawnPosition;
            }
            set {
                _RespawnPosition = value;
            }
        }
    }
    
    public partial class OnMatchEndCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _IsWin;
        
        public Int32 IsWin {
            get {
                return _IsWin;
            }
            set {
                _IsWin = value;
            }
        }
    }
    
    public partial class OnRequestRankingListReturnCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private object _RankingList;
        
        public object RankingList {
            get {
                return _RankingList;
            }
            set {
                _RankingList = value;
            }
        }
    }
    
    public partial class OnEnterArenaCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Vector3 _CenterPosition;
        
        public Vector3 CenterPosition {
            get {
                return _CenterPosition;
            }
            set {
                _CenterPosition = value;
            }
        }
    }
    
    public partial class OnSkillStartSingCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Single _singTime;
        
        public Single singTime {
            get {
                return _singTime;
            }
            set {
                _singTime = value;
            }
        }
    }
    
    public partial class RequestCastSkillCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _gongFaID;
        
        private Int32 _skillIndex;
        
        private String _argsString;
        
        public Int32 gongFaID {
            get {
                return _gongFaID;
            }
            set {
                _gongFaID = value;
            }
        }
        
        public Int32 skillIndex {
            get {
                return _skillIndex;
            }
            set {
                _skillIndex = value;
            }
        }
        
        public String argsString {
            get {
                return _argsString;
            }
            set {
                _argsString = value;
            }
        }
    }
    
    public partial class OnSkillStartCastCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _skillID;
        
        private String _argsString;
        
        private Single _castTime;
        
        public Int32 skillID {
            get {
                return _skillID;
            }
            set {
                _skillID = value;
            }
        }
        
        public String argsString {
            get {
                return _argsString;
            }
            set {
                _argsString = value;
            }
        }
        
        public Single castTime {
            get {
                return _castTime;
            }
            set {
                _castTime = value;
            }
        }
    }
    
    public partial class OnRequestSelfRankingReturnCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private object _RankingInfo;
        
        public object RankingInfo {
            get {
                return _RankingInfo;
            }
            set {
                _RankingInfo = value;
            }
        }
    }
    
    public partial class OnTargetItemListReturnCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private object _TargetItemListObject;
        
        public object TargetItemListObject {
            get {
                return _TargetItemListObject;
            }
            set {
                _TargetItemListObject = value;
            }
        }
    }
    
    public partial class RequestRankingListCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class onMainAvatarLeaveSpaceCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
    }
    
    public partial class RequestDialogCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Int32 _NpcID;
        
        public Int32 NpcID {
            get {
                return _NpcID;
            }
            set {
                _NpcID = value;
            }
        }
    }
    
    public partial class DoMoveCommand : uFrame.MVVM.ViewModels.ViewModelCommand {
        
        private Vector3 _Point;
        
        public Vector3 Point {
            get {
                return _Point;
            }
            set {
                _Point = value;
            }
        }
    }
}
