using UnityEngine;

public class Pseudocode : MonoBehaviour
{
    /* 
 ON START
 currentSprite = A //Sprites are like costumes for the player object
 currentState = CharacterA //State will make sure you don’t use an ability with the wrong character
 activeAbility = false //This check makes sure you aren’t changing character while still using an ability


 SWITCH CHARACTER
 If activeAbility = true, disable activeAbility
 If activeAbility = false
 On mouse pressed, change sprite by 1
 If currentSprite is A, change to B
     currentState = CharacterB
 If currentSprite is B, change to C
     currentState = CharacterC
 If currentSprite is C, change to A
     currentState = CharacterA


 ANTIGRAV
 On[ability button] pressed
 If currentSprite is A and activeAbility = false
     ‘Fall’ to ceiling(transform.translate?)
 currentState = AntigravActive
 activeAbility = true
 If currentSprite is A and activeAbility = true
     ‘Fall’ to floor
     currentState = CharacterA
     activeAbility = false


 ATTACK
 On[ability button] pressed
 If currentSprite is B and activeAbility = false
     Detect collision
     Destroy all objects within collision field marked breakable
 currentState = AttackActive
 activeAbility = true
 If currentSprite is B and activeAbility = true //find timed way to get out of AttackActive
     currentState = Character B
     activeAbility = false


 SHRINK
 On[ability button] pressed
 If currentSprite is C and activeAbility = false
     Object transform.scale 0.5
     Collision transform.scale 0.5
 currentState = ShrinkActive
 activeAbility = true
 If currentSprite is C and activeAbility = true
     Object transform.scale 1
     Collision transform.scale 1
     currentState = Character C
     activeAbility = false

     STATES
 CharacterA
 CharacterB
 CharacterC
 AntigravActive
 AttackActive
 ShrinkActive
 Jumping

    */
}
