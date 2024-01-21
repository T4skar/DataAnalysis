﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class DeathVolume : MonoBehaviour
    {
        public new AudioSource audio;


        void OnTriggerEnter(Collider other)
        {
            var pc = other.GetComponent<Damageable>();
            if (pc != null)
            {
                var msg = new Damageable.DamageMessage()
                {
                    amount = 69,
                    damager = this,
                    direction = Vector3.up,
                    stopCamera = true
                };

                pc.isInvulnerable = false;
                pc.ApplyDamage(msg);
            }
            if (audio != null)
            {
                audio.transform.position = other.transform.position;
                if (!audio.isPlaying)
                    audio.Play();
            }
        }

        void Reset()
        {
            if (LayerMask.LayerToName(gameObject.layer) == "Default")
                gameObject.layer = LayerMask.NameToLayer("Environment");
            var c = GetComponent<Collider>();
            if (c != null)
                c.isTrigger = true;
        }

    }
}
