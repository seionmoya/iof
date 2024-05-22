using System;
using System.Reflection;
using EFT.UI.DragAndDrop;
using HarmonyLib;
using Aki.Reflection.Patching;
using static Seion.Iof.Features.Locker;

namespace Seion.Iof.Patches
{
    internal class PreItemViewOnBeginDrag : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(ItemView), "OnBeginDrag");
        }

        [PatchPrefix]
        private static bool PatchPrefix(ref ItemView __instance)
        {
            try
            {
                // Don't execute original event handler if item IsMoveLocked, otherwise execute.
                if (IsMoveLocked(__instance.Item)) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw Plugin.ShowErrorNotif(ex);
            }
        }
    }
}