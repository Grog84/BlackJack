using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    [SerializeField]
    public Sprite[] spriteArray;

    public Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();

    public static SpriteManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Init()
    {
        instance.LoadCardTextureAtlases();
    }

    public Sprite GetSprite(string itemID)
    {
        Sprite sprite = null;
        if (!spriteDict.TryGetValue(itemID, out sprite))
        {
            Debug.LogWarning("SpriteManager : unable to find sprite '" + itemID + "'");
            spriteDict.TryGetValue("empty", out sprite);
        }
        return sprite;
    }

    private void LoadCardTextureAtlases()
    {
        spriteDict.Clear();
        for (int i = 0; i < spriteArray.Length; i++)
        {
            Sprite sprite = spriteArray[i];
            spriteDict[NameToID(sprite.name)] = sprite;
        }
    }

    public string NameToID(string name)
    {
        string id = "_PARSE_ERROR_";

        int cardIdx = -1;
        if (Int32.TryParse(name.Split('_')[1], out cardIdx))
        {
            id = cardIdx.ToString();
        }

        return id;
    }

}
