﻿using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using FFXIVClientStructs.FFXIV.Client.UI.Shell;
using ImGuiNET;
using System.Numerics;
using TPie.Helpers;
using Macro = FFXIVClientStructs.FFXIV.Client.UI.Misc.RaptureMacroModule.Macro;

namespace TPie.Models.Elements
{
    public class GameMacroElement : RingElement
    {
        public string Name;
        public int Identifier;
        public bool IsShared;

        public GameMacroElement(string name, int id, bool shared, uint iconId)
        {
            Name = name;
            Identifier = id;
            IsShared = shared;
            IconID = iconId;
        }

        public GameMacroElement() : this("New Macro", 0, false, 66001) { }

        public override string Description()
        {
            string sharedString = IsShared ? "Shared " : "";

            return $"{Name} ({sharedString}Macro #{Identifier})";
        }

        private unsafe Macro* GetGameMacro()
        {
            if (Identifier < 0 || Identifier > 99) return null;

            return IsShared ? RaptureMacroModule.Instance->Shared[Identifier] : RaptureMacroModule.Instance->Individual[Identifier];
        }

        public override unsafe void ExecuteAction()
        {
            // already executing macro?
            if (RaptureShellModule.Instance->MacroLocked || RaptureShellModule.Instance->MacroCurrentLine >= 0) return;

            Macro* macro = GetGameMacro();
            if (macro != null)
            {
                RaptureShellModule.Instance->ExecuteMacro(macro);
            }
        }

        public override string InvalidReason()
        {
            return "Invalid macro";
        }

        public override unsafe bool IsValid()
        {
            return GetGameMacro() != null;
        }

        public override void Draw(Vector2 position, Vector2 size, float scale, bool selected, uint color, float alpha, bool tooltip, ImDrawListPtr drawList)
        {
            base.Draw(position, size, scale, selected, color, alpha, tooltip, drawList);

            // name
            DrawHelper.DrawOutlinedText($"{Name}", position, true, scale, drawList);
        }
    }
}
