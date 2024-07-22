using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class CombatUi:Update
    {
        public CombatUi(PlayableLevel CurrentLevel) 
        {
            _points = new Text(new Transform(10, 30,0,.3f,.3f), GameManager.Instance.points.ToString() + " of 10");
            _lifes = new Text(new Transform(10, 10, 0, .3f, .3f), GameManager.Instance.lives.ToString() + " of 3");
            CurrentLevel.lossLife += UpdateLife;
            CurrentLevel.getPoint += UpdatePoints;
            LevelsManager.Instance.CurrentLevel.updates.Add(this);
        }
        private Text _points;
        private Text _lifes;
        private float _lifeTimer;
        private float _pointsTimer;

        public void Update()
        {
            if (_points.active)
            {
                _pointsTimer += MyDeltaTimer.deltaTime;
                if (_pointsTimer > 2)
                    _points.active = false;
            }
            if (_lifes.active )
            {
                _lifeTimer += MyDeltaTimer.deltaTime;
                if( _lifeTimer > 2)
                    _lifes.active = false;
            }
        }
        public void UpdateLife()
        {
            _lifes.text = GameManager.Instance.lives.ToString() + " of 3";
            _lifes.active=true;
            _lifeTimer = 0;
        }
        public void UpdatePoints()
        {
            _points.text = GameManager.Instance.points.ToString() + " of 10";
            _points.active=true;
            _pointsTimer = 0;
        }

    }
}
