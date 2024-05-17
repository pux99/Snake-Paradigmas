using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class Animation:Draw,Update
    {
        public Animation(string path, Transform transform,float timeBetweenFrames, int frameCount)
        {
            _path = path;
            _transform = transform;
            _timeBetweenFrames = timeBetweenFrames;
            _timer = timeBetweenFrames;
            _frameCount = frameCount;
            
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
            LevelsManager.Instance.CurrentLevel.updates.Add(this);
        }

        private int _frameCount;
        private int _currentFrame=0;
        private float _timeBetweenFrames;
        private float _timer;
        private string _path;
        private Transform _transform;
        public bool active { get { return active; } }


        public void Draw()
        {
            Engine.Draw(_path + _currentFrame.ToString()+".png",
                        _transform.positon.x, 
                        _transform.positon.y, 
                        _transform.scale.x, 
                        _transform.scale.y);
        }

        public void Update()
        {
            _timer -= MyDeltaTimer.deltaTime;
            if (_timer <= 0)
            {
                if(_currentFrame!=_frameCount-1)
                    _currentFrame++;
                else
                    _currentFrame = 0;
                _timer = _timeBetweenFrames;
            }
        }
    }
}
