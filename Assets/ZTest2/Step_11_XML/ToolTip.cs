using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step11
{
    public class ToolTip
    {
        public string count;
        public string tip;
        public string eng;

        public override string ToString()
        {
            return count + " >> " + tip + " >> " + eng;
        }
    }
}