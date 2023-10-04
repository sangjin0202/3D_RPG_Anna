using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step06
{
    public class FSMData<T>
    {
        public T t;
        public System.Action cbIn;
        public System.Action cbLoop;
        public System.Action cbOut;
    }


    public class FSM<T> : MonoBehaviour
    {
        Dictionary<T, FSMData<T>> dicFun = new Dictionary<T, FSMData<T>>();
        protected T preState   = default(T);
        protected T curState   = default(T);
        protected T nextState  = default(T);

        System.Action cbIn;
        System.Action cbLoop;
        System.Action cbOut;

        protected void InitState(T _t)
        {
            curState = _t;
        }

        public void AddState(T _t, System.Action _cbIn, System.Action _cbLoop, System.Action _cbOut)
        {
            if(dicFun.ContainsKey(_t))
            {
                Debug.LogError("Already added state " + _t);
            }
            else
            {
                FSMData<T> _data = new FSMData<T>();
                _data.t         = _t;
                _data.cbIn      = _cbIn;
                _data.cbLoop    = _cbLoop;
                _data.cbOut     = _cbOut;
                dicFun.Add(_t, _data);
            }
        }

        public void MoveState(T _nextState)
        {
            if( !dicFun.ContainsKey(_nextState))
            {
                Debug.LogError("I don't Know " + _nextState);
                return;
            }
            //else if(curState.Equals(_nextState))
            //{
            //    return;
            //}

            //1. curstate 처리...
            if(cbOut != null)
            {
                cbOut();
            }

            //2. state
            preState    = curState;
            curState    = _nextState;
            nextState   = _nextState;

            //2-2. call back
            FSMData<T> _s = dicFun[curState];
            cbIn    = _s.cbIn;
            cbLoop  = _s.cbLoop;
            cbOut   = _s.cbOut;

            //2-3. In
            if(cbIn != null)
            {
                cbIn();
            }

            //2-4. Loop -> Updata..
        }

        private void Update()
        {
            if(cbLoop != null)
            {
                cbLoop();
            }
        }
    }
}