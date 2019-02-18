using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Weapon")]
public class Weapon : Equipment
{  
  
   public int attackDamageOnTrees;
   public int attackDamageOnCreature;

   public int attackDamageOnRock;
}
