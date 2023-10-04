using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : FSM<eGameState>
{
    private void Start()
    {

        AddState(eGameState.Init, In_Init, Modify_Init, null);
        AddState(eGameState.Gaming, In_Gaming, Modify_Gaming, null);

		MoveState(eGameState.Init);
    }

    #region Init
    void In_Init()
    {
		CharInfoManager.ins.ReadAndParse();
		ItemInfoManager.ins.ReadAndParse();

        if (Constant.DEBUG_GM)
        {
            Debug.Log(this + " In_Init");
        }

    }


    void Modify_Init()
    {
        //로딩중검사...
        if (MonsterManager.ins.IsLoad())
        {
            MoveState(eGameState.Gaming);
            return;
        }
    }

    #endregion

    #region Gaming
    void In_Gaming()
    {
        if (Constant.DEBUG_GM)Debug.Log(this + " In_Init");

    }


    void Modify_Gaming()
    {
        //if (로딩완료)
        //{
        //    MoveState(eGameState.Gaming);
        //    return;
        //}
    }
    #endregion

}
