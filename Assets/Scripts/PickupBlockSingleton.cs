using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBlockSingleton : Singleton<PickupBlockSingleton>
{
    // Cheaty way to get spaceship part to be a singleton
    // No multiple inheritance in c#
    // And more complicated (but better) to use inteface + class combo
    // (SpaceshipPart has PickupBlock script which is already a derived class of FindPlayerTransform*)
}
