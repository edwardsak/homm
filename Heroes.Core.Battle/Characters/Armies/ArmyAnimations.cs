using System;
using System.Collections.Generic;
using System.Text;

using Heroes.Core.Battle.Characters.Graphics;

namespace Heroes.Core.Battle.Characters.Armies
{
    public class ArmyAnimations : Animations
    {
        public Animation _firstStandingRight;
        public Animation _firstStandingLeft;
        public Animation _standingRight;
        public Animation _standingRightActive;
        public Animation _standingRightHover;
        public Animation _standingLeft;
        public Animation _standingLeftActive;
        public Animation _standingLeftHover;
        public Animation _startMovingRight;
        public Animation _startMovingLeft;
        public Animation _stopMovingRight;
        public Animation _stopMovingLeft;
        public Animation _movingRight;
        public Animation _movingLeft;
        
        public Animation _attackStraightRightBegin;
        public Animation _attackStraightRightEnd;
        public Animation _attackStraightLeftBegin;
        public Animation _attackStraightLeftEnd;

        public Animation _shootStraightRightBegin;
        public Animation _shootStraightRightEnd;
        public Animation _shootStraightLeftBegin;
        public Animation _shootStraightLeftEnd;

        public Animation _defendRight;
        public Animation _defendLeft;

        public Animation _gettingHitRight;
        public Animation _gettingHitLeft;

        public Animation _deathRight;
        public Animation _deathLeft;

        public ArmyAnimations() 
        { 
        }

        public ArmyAnimations(Animation standingRight, Animation standingLeft, 
            Animation standingRightActive, Animation standingLeftActive,
            Animation standingRightHover, Animation standingLeftHover, 
            Animation startMovingRight, Animation startMovingLeft,
            Animation stopMovingRight, Animation stopMovingLeft,
            Animation movingRight, Animation movingLeft,
            Animation attackStraightRightBegin, Animation attackStraightRightEnd,
            Animation attackStraightLeftBegin, Animation attackStraightLeftEnd,
            Animation shootStraightRightBegin, Animation shootStraightRightEnd,
            Animation shootStraightLeftBegin, Animation shootStraightLeftEnd,
            Animation defendRight, Animation defendLeft,
            Animation gettingHitRight, Animation gettingHitLeft,
            Animation deathRight, Animation deathLeft)
        {
			this._standingRight = standingRight;
            this._standingRightActive = standingRightActive;
            this._standingRightHover = standingRightHover;
		    this._standingLeft = standingLeft;
            this._standingLeftActive = standingLeftActive;
            this._standingLeftHover = standingLeftHover;
            this._startMovingRight = startMovingRight;
            this._startMovingLeft = startMovingLeft;
            this._stopMovingRight = stopMovingRight;
            this._stopMovingLeft = stopMovingLeft;
			this._movingRight = movingRight;
			this._movingLeft = movingLeft;

            this._attackStraightRightBegin = attackStraightRightBegin;
            this._attackStraightRightEnd = attackStraightRightEnd;
            this._attackStraightLeftBegin = attackStraightLeftBegin;
            this._attackStraightLeftEnd = attackStraightLeftEnd;

            this._shootStraightRightBegin = shootStraightRightBegin;
            this._shootStraightRightEnd = shootStraightRightEnd;
            this._shootStraightLeftBegin = shootStraightLeftBegin;
            this._shootStraightLeftEnd = shootStraightLeftEnd;

            this._defendRight = defendRight;
            this._defendLeft = defendLeft;

            this._gettingHitRight = gettingHitRight;
            this._gettingHitLeft = gettingHitLeft;
            
            this._deathRight = deathRight;
            this._deathLeft = deathLeft;
		}

    }
}
