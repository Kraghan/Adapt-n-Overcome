using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Factions {

	NEUTRAL,

}

public class FactionHelper
{
    public static string FactionsToString(Factions faction)
    {
        switch(faction)
        {
            case Factions.NEUTRAL:
                return "neutral";
        }

        return "neutral";
    }

    public static Factions StringToFactions(string str)
    {
        switch(str)
        {
            case "neutral":
                return Factions.NEUTRAL;
        }

        return Factions.NEUTRAL;
    }
}