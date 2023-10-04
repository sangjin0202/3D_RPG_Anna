using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Step06
{
    public enum eGameState { None, Menu, Ready, Gaming, Result }
    public class GameManager : MonoBehaviour
    {
        public eGameState gameState;
        public Text textMessage;
        void Start()
        {
            PInMenu();
        }

        #region Menu
        void PInMenu()
        {
            gameState = eGameState.Menu;
            Debug.Log(this + "PInMenu");
            textMessage.text = gameState.ToString();
        }

        void ModifyMenu()
        {
            Debug.Log(this + "ModifyMenu");
            if (Input.GetKeyDown(KeyCode.N))
            {
                POutMenu();
                PInReady();
                return;
            }
        }


        void POutMenu()
        {
            Debug.Log(this + "POutMenu");
        }

        #endregion

        #region Ready
        float readyNextTime;
        public float READY_NEXT_TIME = 3f;
        void PInReady()
        {
            gameState = eGameState.Ready;
            Debug.Log(this + "PInReady");
            textMessage.text = gameState.ToString();
            readyNextTime = Time.time + READY_NEXT_TIME;
        }

        void ModifyReady()
        {
            Debug.Log(this + "ModifyReady");
            textMessage.text = gameState.ToString() + (int)(1f + readyNextTime - Time.time);
            if (Time.time > readyNextTime)
            {
                POutReady();
                PInGaming();
                return;
            }
        }


        void POutReady()
        {
            Debug.Log(this + "POutReady");
        }

        #endregion

        #region Gaming
        void PInGaming()
        {
            gameState = eGameState.Gaming;
            Debug.Log(this + "PInGaming");
            textMessage.text = gameState.ToString();
        }

        void ModifyGaming()
        {
            Debug.Log(this + "ModifyGaming");
            if (Input.GetKeyDown(KeyCode.N))
            {
                POutGaming();
                PInResult();
                return;
            }
        }


        void POutGaming()
        {
            Debug.Log(this + "POutGaming");
        }

        #endregion

        #region Result
        void PInResult()
        {
            gameState = eGameState.Result;
            Debug.Log(this + "PInResult");
            textMessage.text = gameState.ToString();
        }

        void ModifyResult()
        {
            Debug.Log(this + "ModifyResult");
            if (Input.GetKeyDown(KeyCode.N))
            {
                POutResult();
                PInMenu();
                return;
            }
        }


        void POutResult()
        {
            Debug.Log(this + "POutResult");
        }

        #endregion


        void Update()
        {
            switch (gameState)
            {
                case eGameState.Menu:
                    ModifyMenu();
                    break;
                case eGameState.Ready:
                    ModifyReady();
                    break;
                case eGameState.Gaming:
                    ModifyGaming();
                    break;
                case eGameState.Result:
                    ModifyResult();
                    break;
            }
        }
    }
}