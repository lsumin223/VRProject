using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PlayerCharactor", menuName = "Scriptable Object/PlayerCharactorData")]
public class PlayerData : ScriptableObject
{
    [Header("# Main info")]
    public int playerId;
    public string playerName;
    public string itemDesc;
    public GameObject playerPrefab;

    [Header("# IngameData")]
    public float flightspeed;
    public float rollangle;
    public float pitchangle;
    public float maxspeed;
    public float minspeed;
    public float accelerator;
    public float breaktime;

    public float maxboostguage;
    public float boostspeed;
    public float boostRecharge;
    public float boostDecrease;

    public float hitCooltime;

    [Range (0f, 100f)]
    public float DamageDecreasePercent;
}
