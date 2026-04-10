
using UnityEngine;


[CreateAssetMenu (fileName = "NewPalletsettings", menuName = "ScriptableObject/Pallet", order = 1)]
public class CellPalletSettings : ScriptableObject
{
    [field:SerializeField, Space(20f)]
    [field: Tooltip("Клетка под выбранным юнитом")]
    public Material SelectCell {  get; private set; }


    [field: SerializeField]
    [field: Tooltip("Клетка доступная для передвижения")]
    public Material MoveCell { get; private set; }


    [field: SerializeField]
    [field: Tooltip("Кдетка доступная для атаки")]
    public Material AttackCell { get; private set; }


    [field: SerializeField]
    [field: Tooltip("Клетка доступная для атаки и передвижения")]
    public Material ConfirmCell { get; private set; }


}

