using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public const float SPEED_ROTATE_WEAPON_VT_Y = -700f;
    public const float SPEED_ROTATE_WEAPON_VT_Z = -100f;
    public const float TIME_COROUTINE_DESPAWN_CHARATER = 1;
    public const float TIME_DELAY_THROW_BULLET = 0.3f;
    public const float NUM_SCALE_CHARACTER = 0.1f;

    public const int NUM_KILL_CHARACTER_LEVEL_UP = 2;
    public const int NUM_OF_ENEMY_IN_MAP = 9;
    public const int NUM_OF_ENEMY_SPAWN = 19;
    public const int NUM_OF_CHARATER_IN_MAP = NUM_OF_ENEMY_SPAWN + 1;

    public const string ANIM_ATTACK = "charater";

    public const string TAG_CHARACTER = "charater";
    public const string TAG_BUILDING = "building";
    public const string TAG_WARZONE = "warzone";

    public static Vector3 VECTOR_SPAWN_WEAPON = Vector3.up;

    public const string KEY_NAME = "name";
    public const string KEY_COIN = "coin";
    public const string KEY_LEVEL = "level";
}
