﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Text : Draw
    {
        private Transform _transform;
        private string _text;
        public bool active { get { return active; } }

        public Text(Transform t_transform, string t_text)
        {
            this._transform = t_transform;
            _text = t_text;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }
        public void Draw()
        {
            for (int i = 0; i < _text.Count(); i++)
            {
                char letter = _text[i];
                Transform tran = new Transform(_transform.positon.x + i * 56 * _transform.scale.x, _transform.positon.y, _transform.rotation, _transform.scale.x, _transform.scale.y);
                if (letter >= 'a' && letter <= 'z')//Transforma las minusculas en mayusculas. No tenemos sprites de minusculas.
                {
                    letter = (char)(letter - 32); 
                }
                if ((letter >= 'A' && letter <= 'Z' || letter >= '0' && letter <= '9'))//Chequea si el caracter es una letra o un numero.
                {
                    Engine.Draw($"Sprites/Caraters/" + letter + ".png", tran.positon.x, tran.positon.y, tran.scale.x, tran.scale.y, tran.rotation);
                }
            }
        }
    }
}
