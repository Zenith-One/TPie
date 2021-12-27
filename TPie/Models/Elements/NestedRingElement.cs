﻿using ImGuiNET;
using System.Linq;
using System.Numerics;
using TPie.Helpers;

namespace TPie.Models.Elements
{
    public class NestedRingElement : RingElement
    {
        public string RingName;
        public float ActivationTime;
        public bool DrawText;

        public NestedRingElement(string ringName, float activationTime, bool drawText, uint iconId)
        {
            RingName = ringName;
            ActivationTime = activationTime;
            DrawText = drawText;
            IconID = iconId;
        }

        public NestedRingElement() : this("Ring Name", 0.5f, true, 66319) { }

        public override string Description()
        {
            return RingName;
        }

        public override void ExecuteAction()
        {
        }

        public override string InvalidReason()
        {
            return $"Couldn't find a ring named \"{RingName}\"";
        }

        public override bool IsValid()
        {
            return Ring != null;
        }

        public Ring? Ring => Plugin.Settings.Rings.FirstOrDefault(ring => ring.Name == RingName);

        public override void Draw(Vector2 position, Vector2 size, float scale, bool selected, uint color, float alpha, bool tooltip, ImDrawListPtr drawList)
        {
            base.Draw(position, size, scale, selected, color, alpha, tooltip, drawList);

            // name
            if (DrawText)
            {
                DrawHelper.DrawOutlinedText($"{RingName}", position, true, scale, drawList);
            }
        }
    }
}
