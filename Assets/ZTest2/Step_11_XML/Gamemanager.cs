using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step06;

namespace Step11
{
    public class Gamemanager : Step06.FSM<Step06.eGameState>
    {
        void Start()
        {
            AddState(Step06.eGameState.Menu, Init_Menu, null, null);

            MoveState(Step06.eGameState.Menu);
        }

        void Init_Menu()
        {
            ToolTipManager.ins.ReadAndParse();
            ItemInfoManager.ins.ReadAndParse();
        }
    }
}