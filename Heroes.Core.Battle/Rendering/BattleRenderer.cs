using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Heroes.Core.Battle.Characters;
using Heroes.Core.Battle.Characters.Graphics;

namespace Heroes.Core.Battle.Rendering
{
    public class BattleRenderer
        : Renderer
    {
        BattleEngine _engine;

        public BattleRenderer(BattleEngine engine)
        {
            _engine = engine;
        }

        public override void Render(Controller controller)
        {
            controller.Sprite.Draw(controller.TextureStore._texBg.Texture, new Rectangle(0, 0, 800, 556), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), Color.White);
            controller.Sprite.Draw(controller.TextureStore._texBar.Texture, new Rectangle(0, 0, 800, 44), new Vector3(0f, 0f, 0f), new Vector3(0f, 556f, 0f), Color.White);

            _engine._battleTerrain.Draw();

            RenderHeroes(controller);
            RenderCharacters(controller);
            RenderSpells(controller);
        }

        private void RenderCharacters(Controller controller)
        {
            // sort by cell
            ArrayList stdCharacters = new ArrayList();
            foreach (StandardCharacter cs in _engine.ActiveCharacters)
            {
                stdCharacters.Add(cs);
            }
            BattleEngine.SortByCell(stdCharacters);

            foreach (StandardCharacter cs in stdCharacters)
            {
                AnimationRunner runner = cs.CurrentAnimationRunner;
                if (runner == null) continue;

                controller.Sprite.Draw(runner._currentCue._tex.Texture,
                    new Rectangle(new Point(0, 0), runner._currentCue._size),
                    new Vector3(0.0F, 0.0F, 0.0F),
                    new Vector3(cs._currentAnimationPt.X - runner._currentCue._point.X, cs._currentAnimationPt.Y - runner._currentCue._point.Y, 0.0F), Color.White);

                // draw qty left
                Color fontColor;
                if (cs._armySide == ArmySideEnum.Attacker)
                    fontColor = Color.Red;
                else
                    fontColor = Color.Cyan;

                if (cs._qtyLeft > 0)
                {
                    controller._font.DrawText(controller.Sprite, cs._qtyLeft.ToString(),
                        new Rectangle((int)cs._currentAnimationPt.X + 25, (int)cs._currentAnimationPt.Y - 20, 50, 32),
                        DrawTextFormat.Left | DrawTextFormat.Top | DrawTextFormat.WordBreak, fontColor);
                }
            }

            // draw status message
            controller._font.DrawText(controller.Sprite, _engine._statusMsg,
                _engine._rectStatusMsg,
                //new Rectangle(_engine._rectStatusMsg.X + 10, (_engine._rectStatusMsg.Y + _engine._rectStatusMsg.Height / 2) - 10, _engine._rectStatusMsg.Width, _engine._rectStatusMsg.Height),
                DrawTextFormat.Center | DrawTextFormat.Center | DrawTextFormat.WordBreak, Color.White);
        }

        private void RenderHeroes(Controller controller)
        {
            // attacker
            {
                Heroes.Core.Battle.Characters.Hero cs = _engine._attacker;

                AnimationRunner runner = cs._currentAnimationRunner;
                if (runner != null)
                {
                    controller.Sprite.Draw(runner._currentCue._tex.Texture,
                        new Rectangle(new Point(0, 0), runner._currentCue._size),
                        new Vector3(0.0F, 0.0F, 0.0F),
                        new Vector3(cs._currentAnimationPt.X - runner._currentCue._point.X, cs._currentAnimationPt.Y - runner._currentCue._point.Y, 0.0F), Color.White);
                }
            }

            // defender
            if (_engine._defender != null)
            {
                Heroes.Core.Battle.Characters.Hero cs = _engine._defender;

                AnimationRunner runner = cs._currentAnimationRunner;
                if (runner != null)
                {
                    controller.Sprite.Draw(runner._currentCue._tex.Texture,
                        new Rectangle(new Point(0, 0), runner._currentCue._size),
                        new Vector3(0.0F, 0.0F, 0.0F),
                        new Vector3(cs._currentAnimationPt.X - runner._currentCue._point.X, cs._currentAnimationPt.Y - runner._currentCue._point.Y, 0.0F), Color.White);
                }
            }
        }

        private void RenderSpells(Controller controller)
        {
            foreach (Heroes.Core.Battle.Characters.Spells.Spell cs in _engine._spellActions)
            {
                AnimationRunner runner = cs.CurrentAnimationRunner;
                if (runner == null) continue;

                controller.Sprite.Draw(runner._currentCue._tex.Texture,
                    new Rectangle(new Point(0, 0), runner._currentCue._size),
                    new Vector3(0.0F, 0.0F, 0.0F),
                    new Vector3(cs._currentAnimationPt.X - runner._currentCue._point.X, cs._currentAnimationPt.Y - runner._currentCue._point.Y, 0.0F), Color.White);
            }
        }

    }
}
