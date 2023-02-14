using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    // G E T T E R S

    // Returns an int using a given key
    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }


    // Returns a float using a given key
    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    // Returns a string using a given key
    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    // Returns a bool using a given key
    public bool GetBool(string key)
    {
        return bool.Parse(PlayerPrefs.GetString(key));
    }


    // S E T T E R S

    // Store an int value along with a key locally
    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // Store a float value along with a key locally
    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    // Store a string value along with a key locally
    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    // Store a bool value along with a key locally
    public void SetBool(string key, bool value)
    {

        PlayerPrefs.SetString(key, value.ToString());
        PlayerPrefs.Save();
    }

    ///////////////

    // Checks if any value exists with the given key
    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    // Deletes all values stored locally
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
