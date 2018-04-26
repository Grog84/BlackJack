using System;
using System.Linq;
using UnityEngine;

namespace Cards
{
    public class CardVO
    {
        string _id;
        int value;

        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
                var stringValue = _id.TakeWhile<char>(c => !Char.IsDigit(c));
                if (!Int32.TryParse(stringValue.ToString(), out this.value))
                {
                    Debug.LogError("Card '" + _id + "' value not assigned");
                }
            
            }
        }

        public CardVO() { value = 0; }
    }

}
