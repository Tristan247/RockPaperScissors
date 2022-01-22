using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{
    public string name;
    public int score;

    public User(string name)
    {
        this.name = name;
    }
}
