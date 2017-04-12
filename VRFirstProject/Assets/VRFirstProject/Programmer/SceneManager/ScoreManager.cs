using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    public enum Rank { C, B, A, S}

    public static Rank ClearRank
    {
        get
        {
            if (score > 9) return Rank.S;
            if (score > 6) return Rank.A;
            if (score > 3) return Rank.B;
            return Rank.C;
        }
    }
    public static int score { get; private set; }

    public static void AddScore(int value)
    {
        score += value;
    }
}
