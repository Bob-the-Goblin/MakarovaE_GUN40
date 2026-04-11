using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeam 
{
    Team Current { get; set; }
    public void Next();
    

}
