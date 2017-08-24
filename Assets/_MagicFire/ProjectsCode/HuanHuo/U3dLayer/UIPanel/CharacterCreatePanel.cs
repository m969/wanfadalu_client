using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MagicFire.Mmorpg.UI;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreatePanel : Panel
{
    [SerializeField]
    private Text _avatarNameFeedbackText;

    public void CreateAvatar(string avatarName)
    {
        
    }

    public void CheckAvatarName(string avatarName)
    {
        var rgx = new Regex("[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*");
        var b = rgx.IsMatch(avatarName);

        if (!b)
        {
            _avatarNameFeedbackText.text = "非法的角色名" + avatarName;
        }
        else
        {
            _avatarNameFeedbackText.text = "合法的角色名" + avatarName;
        }
    }
}
