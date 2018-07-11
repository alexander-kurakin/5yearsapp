using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string username;
    public string password;
    public int age;
    public string sex;

    public User()
    {
    }

    public User(string username, string password, int age, string sex)
    {
        this.username = username;
        this.password = password;
        this.age = age;
        this.sex = sex;
    }
}