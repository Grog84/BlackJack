/*
 * Definition of the card Vlaue Object
 * 
 * */

using System;
using UnityEngine;

namespace Cards
{
    public class CardVO
    {
        string _id;
        int _idValue;

        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
                if (!Int32.TryParse(_id.ToString(), out _idValue))
                {
                    Debug.LogError("Card '" + _id + "' value not assigned");
                }
            
            }
        }

        public int idValue
        {
            get { return _idValue; }
            set { _idValue = value; }
        }

        public CardVO() { _idValue = -1; }
    }

}
