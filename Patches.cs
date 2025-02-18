using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ImprovedRestForAll
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Condition), nameof(Condition.Update))]
        public static class ConditionRecoveryFromImprovedRest
        {
            static void Postfix(Condition __instance)
            {
                if(GameManager.GetExperienceModeManagerComponent().GetConditonRecoveryFromRestScale() == 0)
                { 
                    var playerComponent = GameManager.GetPlayerManagerComponent();

                    if (GameManager.m_IsPaused ||
                        GameManager.s_IsGameplaySuspended ||
                        playerComponent.m_SuspendConditionUpdate ||
                        Utils.IsZero(GameManager.GetTimeOfDayComponent().GetDayLengthSeconds(), 0.0001f))
                    {
                        return;
                    }

                    var restComponent = GameManager.GetRestComponent();

                    if (restComponent == null ||
                        restComponent.IsForcedSleep() ||
                        CinematicManager.s_IsCutsceneActive ||
                        playerComponent.GetControlMode() == PlayerControlMode.Dead)
                    {
                        return;
                    }

                    if (restComponent.IsSleeping() && 
                        playerComponent.GetConditionRestBuffTimeRemainingHours() > 0 &&
                        !GameManager.GetFatigueComponent().IsExhausted() &&
                        !GameManager.GetThirstComponent().IsDehydrated() &&
                        !GameManager.GetHungerComponent().IsStarving())
                    {

                        float bonusRecovery = playerComponent.GetConditionPercentBonus() * 24;
                        MelonLogger.Msg("bonus : " + bonusRecovery);

                        float deltaTime = Time.deltaTime / GameManager.GetTimeOfDayComponent().GetDayLengthSeconds();
                        MelonLogger.Msg("delta time : " + deltaTime);

                        float adjustedHealthRecovery = bonusRecovery * deltaTime;
                        MelonLogger.Msg("adjustedHealth : " + adjustedHealthRecovery);

                        __instance.AddHealth(adjustedHealthRecovery, DamageSource.Sleeping);
                    }
                }
            }
        }
    }
}
