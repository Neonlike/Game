using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour {

    // коллекция всех наших спрайтов с ключом = иненем српайта
    private static Dictionary<string, Sprite> sprites_array = new Dictionary<string, Sprite>();

    // получаем спрайт по имени
    public static Sprite GetSprite(string name)
    {
        if (sprites_array.ContainsKey(name))
            return sprites_array[name];
        else
        {
            Debug.LogError("Спрайта с именем " + name + " не найдено");
            return null;
        }
    }

    // не забывать чистить словарь! 
    public static void ClearSpritesArray()
    {
        Debug.Log("Чистим Словарь...");
        sprites_array.Clear();
        Resources.UnloadUnusedAssets();
    }

    // загрузить все спрайты из папки
    // path - путь к папке со спрайтами, находящейся в ресурсах(Assets/Resources/)
    public static void LoadAllSprites(string path)
    {
        Sprite[] temp_spr = Resources.LoadAll<Sprite>(path);
        
        for (int i = 0; i < temp_spr.Length; i++)
            sprites_array.Add(temp_spr[i].name, temp_spr[i]);
    }

    // вернуть имена всех найденных спрайтов
    public static string[] GetAllNames()
    {
        string[] str = new string[sprites_array.Count];
        int i = 0;
        foreach (KeyValuePair<string, Sprite> spr in sprites_array)
        {
            str[i] = spr.Value.name;
            i++;
        }
        if (i == 0)
            Debug.Log("Коллекция пуста!");

        return str;
    }
}