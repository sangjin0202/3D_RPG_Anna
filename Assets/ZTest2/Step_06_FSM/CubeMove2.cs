using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step06
{
    public class CubeMove2 : FSM<CubeMove2.eCubeState>
    {
        public enum eCubeState { None, Wait, ColorChange, Move, Turn }

        float nextTime;
        public float NEXT_TIME = 2f;
        public float speed = 2f;
        public float turnSpeed = 90f;
        Material material;
        public float interpolate = 2f;
        public Color targetColor = Color.green;
        public Color targetColor2 = Color.red;

        void Start()
        {
            AddState(eCubeState.Wait,           PInWait,         ModifyWait, null);
            AddState(eCubeState.ColorChange,    PInColorChange , ModifyColorChange, null);
            AddState(eCubeState.Move,           PInMove,         ModifyMove, null);
            AddState(eCubeState.Turn,           PInTurn,         ModifyTurn, null);

            MoveState(eCubeState.Wait);
        }


        #region Wait
        void PInWait()
        {
            nextTime = Time.time + NEXT_TIME;
        }

        void ModifyWait()
        {
            if (Time.time > nextTime)
            {
                MoveState(eCubeState.ColorChange);
                return;
            }
        }
        #endregion

        #region ColorChange
        Color startColor, endColor;
        void PInColorChange()
        {
            nextTime = Time.time + NEXT_TIME;
            if (preState == eCubeState.Wait)
            {
                startColor = targetColor;
                endColor = targetColor2;
            }
            else
            {
                startColor = targetColor2;
                endColor = targetColor;
            }

            if(material == null)
            {
                material = GetComponent<Renderer>().material;
            }
            material.color = startColor;
        }

        void ModifyColorChange()
        {
            if (Time.time > nextTime)
            {
                if (preState == eCubeState.Wait)
                {
                    MoveState(eCubeState.Move);
                }
                else if (preState == eCubeState.Move)
                {
                    MoveState(eCubeState.Turn);
                }
                return;
            }
            material.color = Color.Lerp(material.color, endColor, interpolate * Time.deltaTime);
        }


        #endregion

        #region Move
        void PInMove()
        {
            nextTime = Time.time + NEXT_TIME;
        }

        void ModifyMove()
        {
            if (Time.time > nextTime)
            {
                MoveState(eCubeState.ColorChange);
                return;
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        #endregion

        #region Turn

        void PInTurn()
        {
            nextTime = Time.time + NEXT_TIME;
        }

        void ModifyTurn()
        {
            if (Time.time > nextTime)
            {
                MoveState(eCubeState.Wait);
                return;

            }

            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            //transform.rotation *= Quaternion.Euler(Vector3.up * turnSpeed * Time.deltaTime);
        }


        #endregion
    }
}
