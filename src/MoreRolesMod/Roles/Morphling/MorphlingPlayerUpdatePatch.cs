using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreRolesMod.Roles.Morphling
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    class MorphlingPlayerUpdatePatch
    {
        public static void SetSkinWithAnim(PlayerPhysics playerPhysics, uint skinId)
        {
            // https://github.com/Eisbison/TheOtherRoles/blob/main/Source%20Code/Helpers.cs
            SkinData nextSkin = DestroyableSingleton<HatManager>.Instance.AllSkins[(int)skinId];
            AnimationClip clip = null;
            var spriteAnim = playerPhysics.Skin.animator;
            var anim = spriteAnim.m_animator;
            var skinLayer = playerPhysics.Skin;

            var currentPhysicsAnim = playerPhysics.Animator.GetCurrentAnimation();
            if (currentPhysicsAnim == playerPhysics.RunAnim) clip = nextSkin.RunAnim;
            else if (currentPhysicsAnim == playerPhysics.SpawnAnim) clip = nextSkin.SpawnAnim;
            else if (currentPhysicsAnim == playerPhysics.EnterVentAnim) clip = nextSkin.EnterVentAnim;
            else if (currentPhysicsAnim == playerPhysics.ExitVentAnim) clip = nextSkin.ExitVentAnim;
            else if (currentPhysicsAnim == playerPhysics.IdleAnim) clip = nextSkin.IdleAnim;
            else clip = nextSkin.IdleAnim;

            float progress = playerPhysics.Animator.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            skinLayer.skin = nextSkin;

            spriteAnim.Play(clip, 1f);
            anim.Play("a", 0, progress % 1);
            anim.Update(0f);
        }

        public static void Postfix(PlayerControl __instance)
        {
            if (!__instance.HasRole(Role.Morphling))
                return;

            if (Morphling.SampledPlayer != null && Morphling.IsMorphed)
            {
                // https://github.com/Eisbison/TheOtherRoles/blob/main/Source%20Code/UpdatePatch.cs
                Morphling.MorphlingPlayer.myRend.material.SetColor("_BackColor", Palette.ShadowColors[Morphling.SampledPlayer.Data.ColorId]);
                Morphling.MorphlingPlayer.myRend.material.SetColor("_BodyColor", Palette.PlayerColors[Morphling.SampledPlayer.Data.ColorId]);
                Morphling.MorphlingPlayer.HatRenderer.SetHat(Morphling.SampledPlayer.Data.HatId, Morphling.SampledPlayer.Data.ColorId);
                Morphling.MorphlingPlayer.nameText.transform.localPosition = new Vector3(0f, (Morphling.SampledPlayer.Data.HatId == 0U) ? 0.7f : 1.05f, -0.5f);

                if (Morphling.MorphlingPlayer.MyPhysics.Skin.skin.ProdId != DestroyableSingleton<HatManager>.Instance.AllSkins[(int)Morphling.SampledPlayer.Data.SkinId].ProdId)
                {
                    SetSkinWithAnim(Morphling.MorphlingPlayer.MyPhysics, Morphling.SampledPlayer.Data.SkinId);
                }
                if (Morphling.MorphlingPlayer.CurrentPet == null || Morphling.MorphlingPlayer.CurrentPet.ProdId != DestroyableSingleton<HatManager>.Instance.AllPets[(int)Morphling.SampledPlayer.Data.PetId].ProdId)
                {
                    if (Morphling.MorphlingPlayer.CurrentPet) UnityEngine.Object.Destroy(Morphling.MorphlingPlayer.CurrentPet.gameObject);
                    Morphling.MorphlingPlayer.CurrentPet = UnityEngine.Object.Instantiate<PetBehaviour>(DestroyableSingleton<HatManager>.Instance.AllPets[(int)Morphling.SampledPlayer.Data.PetId]);
                    Morphling.MorphlingPlayer.CurrentPet.transform.position = Morphling.MorphlingPlayer.transform.position;
                    Morphling.MorphlingPlayer.CurrentPet.Source = Morphling.MorphlingPlayer;
                    Morphling.MorphlingPlayer.CurrentPet.Visible = Morphling.MorphlingPlayer.Visible;
                    PlayerControl.SetPlayerMaterialColors(Morphling.SampledPlayer.Data.ColorId, Morphling.MorphlingPlayer.CurrentPet.rend);
                }
                else if (Morphling.MorphlingPlayer.CurrentPet)
                {
                    PlayerControl.SetPlayerMaterialColors(Morphling.SampledPlayer.Data.ColorId, Morphling.MorphlingPlayer.CurrentPet.rend);
                }
            }
        }
    }
}
