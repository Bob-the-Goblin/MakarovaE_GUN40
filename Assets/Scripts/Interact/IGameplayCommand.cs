using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayCommand 
{
   public IEnumerable<Cell> Variants {  get; }
   public void Interact (Cell cell)
    { 
    }
    public void ClearSet() { }
}
