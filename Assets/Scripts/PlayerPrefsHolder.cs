using UnityEngine;

public class PlayerPrefsHolder
{
    public void increaseLevel()
    {
        PlayerPrefs.SetInt("level", getLevel() + 1);
    }

    public int getLevel()
    {
        return PlayerPrefs.GetInt("level");
    }

    public void saveCurrency(int currency)
    {
        PlayerPrefs.SetInt("currency", currency);
    }

    public int getCurrency()
    {
        return PlayerPrefs.GetInt("currency");
    }

    public void saveCurrentSkin(int skin)
    {
        PlayerPrefs.SetInt("skin", skin);
    }

    public int getCurrentSkin()
    {
        return PlayerPrefs.GetInt("skin");
    }

    public void saveBoughtItems()
    {

    }
}
