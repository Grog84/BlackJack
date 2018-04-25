using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    [SerializeField]
    public Sprite[] spriteArray;

    private Dictionary<string, Sprite> _spriteDict = new Dictionary<string, Sprite>();

    public static SpriteManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.LoadCardTextureAtlases();
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

    public Sprite GetSprite(string itemID)
    {
        Sprite sprite = null;
        if (!_spriteDict.TryGetValue(itemID, out sprite))
        {
            Debug.LogWarning("SpriteManager : unable to find sprite '" + itemID + "'");
            _spriteDict.TryGetValue("empty", out sprite);
        }
        return sprite;
    }

    private void LoadCardTextureAtlases()
    {
        _spriteDict.Clear();
        for (int i = 0; i < spriteArray.Length; i++)
        {
            Sprite sprite = spriteArray[i];
            _spriteDict[NameToID(sprite.name)] = sprite;
        }
    }

    public string NameToID(string name)
    {
        string id = "_PARSE_ERROR_";

        int cardIdx = -1;
        if (Int32.TryParse(name.Split('_')[1], out cardIdx))
        {
            if (cardIdx >= 0 && cardIdx <= 12)
            {
                // hearts
                id = cardIdx + "H";
            }
            else if (cardIdx >= 13 && cardIdx <= 25)
            {
                // diamonds
                id = (cardIdx - 13) + "D";
            }
            else if (cardIdx >= 26 && cardIdx <= 38)
            {
                // clubs
                id = (cardIdx - 26) + "C";
            }
            else if (cardIdx >= 39 && cardIdx <= 51)
            {
                // spades
                id = (cardIdx - 39) + "S";
            }
            else
            {
                id = cardIdx.ToString();
            }
        }

        return id;
    }

}
