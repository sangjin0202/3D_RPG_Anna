using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step06
{
    public class CubeMove : MonoBehaviour
    {
        public enum eCubeState { None, Wait, ColorChange, Move, Turn }
        public eCubeState beforeState;
        public eCubeState currentState;
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
            material = GetComponent<Renderer>().material;
            PInWait();
        }

        #region Wait

        void PInWait()
        {
            currentState = eCubeState.Wait;
            nextTime = Time.time + NEXT_TIME;
        }

        void ModifyWait()
        {
            if (Time.time > nextTime)
            {
                PInColorChange();
                return;

            }
        }


        #endregion

        #region ColorChange
        Color startColor, endColor;
        void PInColorChange()
        {
            beforeState  = currentState;
            currentState = eCubeState.ColorChange;

            nextTime = Time.time + NEXT_TIME;

            if(beforeState == eCubeState.Wait)
            {
                startColor = targetColor;
                endColor = targetColor2;
            }
            else
            {
                startColor = targetColor2;
                endColor = targetColor;
            }
            material.color = startColor;
        }

        void ModifyColorChange()
        {
            if (Time.time > nextTime)
            {
                if (beforeState == eCubeState.Wait)
                {
                    PInMove();
                }
                else if (beforeState == eCubeState.Move)
                {
                    PInTurn();
                }
                return;
            }
            material.color = Color.Lerp(material.color, endColor, interpolate * Time.deltaTime);
        }


        #endregion

        #region Move

        void PInMove()
        {
            currentState = eCubeState.Move;
            nextTime = Time.time + NEXT_TIME;
        }

        void ModifyMove()
        {
            if (Time.time > nextTime)
            {
                PInColorChange();
                return;

            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }


        #endregion

        #region Turn

        void PInTurn()
        {
            currentState = eCubeState.Turn;
            nextTime = Time.time + NEXT_TIME;
        }

        void ModifyTurn()
        {
            if (Time.time > nextTime)
            {
                PInWait();
                return;

            }

            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            //transform.rotation *= Quaternion.Euler(Vector3.up * turnSpeed * Time.deltaTime);
        }


        #endregion

        void Update()
        {
            switch (currentState)
            {
                case eCubeState.Wait:
                    ModifyWait();
                    break;
                case eCubeState.ColorChange:
                    ModifyColorChange();
                    break;
                case eCubeState.Move:
                    ModifyMove();
                    break;
                case eCubeState.Turn:
                    ModifyTurn();
                    break;
            }
        }
    }
}