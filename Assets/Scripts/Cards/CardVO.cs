using System;
using System.Linq;
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
                var stringValue = _id.TakeWhile<char>(c => !Char.IsDigit(c));
                if (!Int32.TryParse(stringValue.ToString(), out _idValue))
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
