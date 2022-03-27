using System;
using Ez;
using UnityEngine;

namespace UI.Interfaces
{
    public class Prompter : MonoBehaviour
    {
        [SerializeField] private GameObject reactorPrefab;

        private GameObject reactorInstance;

        private void Update()
        {
            //// Trigger event
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (reactorInstance == null) reactorInstance = Instantiate(reactorPrefab);
                // reactorInstance.Send<IReactor>(_=>_.Count(), true);
                var seq = EzMsg.Send<ICounter>(reactorInstance, _ => _.Count())
                    .Wait(1f)
                    .Send<IHealth>(reactorInstance, _ => _.TakeDamage(2));
                
                seq.Run();
            }

            ////Request info
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (reactorInstance == null) reactorInstance = Instantiate(reactorPrefab);

                int? counterNum = reactorInstance.Request<ICounter, int?>(_ => _.Num);
                if (counterNum.HasValue) Debug.Log($"counter is at {counterNum.ToString()}");

                // var reactorHealth = 
                //     EzMsg.Request<IHealth, int?>(reactorInstance, _ => _.CurrentHealth);
            }

            ////Both request and trigger
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (reactorInstance == null) reactorInstance = Instantiate(reactorPrefab);

                var seq = EzMsg.Send<ICounter>(reactorInstance, _ => _.Count())
                    .Wait(1f)
                    .Send<IHealth>(reactorInstance, _ => _.TakeDamage(
                        reactorInstance.Request<ICounter, int?>(_ => _.Num) ?? 0));
                seq.Run();
            }
            
            ////Destroy instance ref
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Destroy(reactorInstance);
            }
        }
    }
}