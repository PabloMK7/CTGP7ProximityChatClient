using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int playerID = -1;
    public float previousDistance = 0;
    public float previousCalcTime = 0;
    public bool wasStar = false, isStar = false, isSmall = false, isMega = false;
}
