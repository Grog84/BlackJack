using System;

public class CardVO
{

    public string _id;

    public string id
    {
        get { return _id; }
        set
        {
            _id = SpriteManager.instance.NameToID(value);
        }
    }

    public CardVO() { }
}
