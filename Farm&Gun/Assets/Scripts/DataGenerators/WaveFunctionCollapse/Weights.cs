using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Weights
{
    [Range(0, 10)] public int roadStraightWeight;
    [Range(0, 10)] public int roadBendWeight;
    [Range(0, 10)] public int waterWeight;
    [Range(0, 10)] public int grassFlatWeight;
    [Range(0, 10)] public int grassBouncyWeight;
    public int GetWeight(Attribute a)
    {
        switch (a)
        {
            case Attribute.RoadStraight:
                return roadStraightWeight;
            case Attribute.RoadBend:
                return roadBendWeight;
            case Attribute.GrassFlat:
                return grassFlatWeight;
            case Attribute.GrassBouncy:
                return grassBouncyWeight;
            case Attribute.Water:
                return waterWeight;
            default:
                Debug.Log("Whaaaaat");
                break;
        }

        return 0;
    }
}
public enum Attribute { RoadStraight, RoadBend, GrassFlat, GrassBouncy, Water };